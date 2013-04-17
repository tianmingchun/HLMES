using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace LC
{

    #region PowerNotify Event
    /// <summary>
    /// PowerNotifyEventArgs
    /// </summary>
    public class PowerNotifyEventArgs : EventArgs
    {
        private uint acLineStatus;

        private uint batteryLifePercent;

        public uint ACLineStatus
        {
            get { return acLineStatus; }
        }

        public uint BatteryLifePercent
        {
            get { return batteryLifePercent; }
        }

        public PowerNotifyEventArgs(uint acLineStatus, uint batteryLifePercent)
        {
            this.acLineStatus = acLineStatus;
            this.batteryLifePercent = batteryLifePercent;
        }
    }

    /// <summary>
    ///  PowerNotifyEvent Delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PowerNotifyEventHandler(object sender, PowerNotifyEventArgs e);
    #endregion

    public class PowerManager
    {
  
        #region Event
        public event PowerNotifyEventHandler PowerNotify = null;

        protected virtual void OnPowerNotify(PowerNotifyEventArgs e)
        {
            if (PowerNotify != null)
            {
                PowerNotify(this, e);
            }
        }
        #endregion

        #region Variable
        IntPtr hMsgQ;
        IntPtr hNotify;

        IntPtr[] hEvent = new IntPtr[2];
        #endregion


        #region Method
        /// <summary>
        /// Start  PowerManagement Thread
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            hEvent[0] = Win32.CreateEvent(IntPtr.Zero, false, false, null);


            Win32.MSGQUEUEOPTIONS options = new Win32.MSGQUEUEOPTIONS();
            options.dwSize = 20;
            options.dwFlags = 2;
            options.dwMaxMessages = 1;
            options.cbMaxMessage = 64;
            options.bReadAccess = true;

            hMsgQ = Win32.CreateMsgQueue(null, options);
            if (hMsgQ == IntPtr.Zero)
            {
                return false;
            }

            hNotify = Win32.RequestPowerNotifications(hMsgQ, Win32.PBT_POWERINFOCHANGE);
            if (hNotify == IntPtr.Zero)
            {
                return false;
            }

            hEvent[1] = hMsgQ;

            Thread t = new Thread(new ThreadStart(this.PowerThreadPorc));
            t.Start();

            return true;
        }

        /// <summary>
        /// Stop PowerManagement Thread
        /// </summary>
        public void Stop()
        {
            PowerNotify = null;
            Win32.EventModify(hEvent[0], Win32.EVENT_SET);

            Win32.StopPowerNotifications(hNotify);
            Win32.CloseMsgQueue(hMsgQ);

            Win32.CloseHandle(hEvent[0]);

        }

        /// <summary>
        /// PowerThreadPorc
        /// </summary>
        private void PowerThreadPorc()
        {
            while (true)
            {
                uint evt = Win32.WaitForMultipleObjects(2, hEvent, false, Win32.INFINITE);
                switch (evt)
                {
                    case 0:     //return thread
                        return;
                    case 1:
                        uint bytesRead;
                        uint flags;
                        Win32.POWER_BROADCAST pB = new Win32.POWER_BROADCAST();
                        if (Win32.ReadMsgQueue(hMsgQ, out pB, (uint)Marshal.SizeOf(typeof(Win32.POWER_BROADCAST)), out bytesRead, Win32.INFINITE, out flags))
                        {
                            if (pB.Message == Win32.PBT_POWERINFOCHANGE && pB.PI.bACLineStatus != 0x01 && pB.PI.bBatteryLifePercent <= 5)
                            {
                                Win32.SetSystemPowerState(null, Win32.POWER_STATE_SUSPEND, Win32.POWER_FORCE);
                                break;
                            }
                            OnPowerNotify(new PowerNotifyEventArgs(pB.PI.bACLineStatus, pB.PI.bBatteryLifePercent));
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
