
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LC
{
    #region enum
     /// <summary>
    /// ConnectType
    /// </summary>
    public enum ConnType
    {
        Offline = -1,
        Gprs,
        Wlan
    }

    /// <summary>
    /// ConnState
    /// </summary>
    public enum ConnState 
    {
        Disconnectiong = 3,
        Connecting=2,
        ConnectSuccess=1,
        NoConnect=0
    }
    #endregion

    #region ConnectNotifyEvent
    /// <summary>
    /// ConnectNotifyEventArgs
    /// </summary>
    public class ConnectNotifyEventArgs : EventArgs
    {
        private ConnState connectResult;

        private ConnType connectType;

        public ConnState ConnectResult
        {
            get { return connectResult; }
        }

        public ConnType ConnectType
        {
            get { return connectType; }
        }

        public ConnectNotifyEventArgs(ConnState connectResult, ConnType connectType)
        {
            this.connectResult = connectResult;
            this.connectType = connectType;
        }
    }

    /// <summary>
    /// ConnectNotifyEvent delegate
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">ConnectNotifyEventArgs</param>
    public delegate void ConnectNotifyEventHandler(object sender, ConnectNotifyEventArgs e);
    #endregion


    public class NetworkManager
    {

        #region Event
        public event ConnectNotifyEventHandler ConnectNotify;

        protected virtual void OnConnectNotify(ConnectNotifyEventArgs e)
        {
            if (ConnectNotify != null)
            {
                ConnectNotify(this, e);
            }
        }

        #endregion

        #region Variable
        const int eventCount = 3;

        IntPtr[] hEvent = new IntPtr[eventCount];

        const string gprsName = "GPRS";

        uint waitTimeout = Win32.INFINITE;
        #endregion
       
        #region Property

        /// <summary>
        /// ConnectType
        /// </summary>
        public static ConnType ConnectType = ConnType.Offline;

        /// <summary>
        /// IsConnecting
        /// </summary>
        public static bool IsConnecting = false;

        /// <summary>
        /// GprsName
        /// </summary>
        public static string GprsName
        {
            get { return gprsName; }
        }

        #endregion

        #region Structure

        public NetworkManager()
        {
            for (int i = 0; i < eventCount; i++)
            {
                hEvent[i] = Win32.CreateEvent(IntPtr.Zero, false, false, null);
            }
        }

        ~NetworkManager()
        {
            for (int i = 0; i < eventCount; i++)
            {
                Win32.CloseHandle(hEvent[i]);
            }
        }

        #endregion

        #region Method
        /// <summary>
        /// Start NetworkManagement Thread
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            Thread t = new Thread(new ThreadStart(this.NetworkThreadPorc));
            t.Start();
            return true;
        }

        /// <summary>
        /// Stop NetworkManagement Thread
        /// </summary>
        public void Stop()
        {
            waitTimeout = Win32.INFINITE;
            this.ConnectNotify = null;
            Win32.EventModify(hEvent[0], Win32.EVENT_SET);
        }

        /// <summary>
        /// Send Connect Request
        /// </summary>
        /// <param name="type"></param>
        public void Connect(ConnType type)
        {
            waitTimeout = 30 * 1000;
            NetworkManager.ConnectType = type;
            Win32.EventModify(hEvent[1], Win32.EVENT_SET);
        }

        /// <summary>
        /// Send Disconnect Request
        /// </summary>
        /// <param name="netType"></param>
        public void Disconnect()
        {
            waitTimeout = Win32.INFINITE;
            Win32.EventModify(hEvent[2], Win32.EVENT_SET);
        }

        /// <summary>
        /// NetworkThreadPorc
        /// </summary>
        private void NetworkThreadPorc()
        {
            while (true)
            {
                //wait for 3 minute if not connect request.
                //the thread will auto maintenance the network state.
                uint evt = Win32.WaitForMultipleObjects(eventCount, hEvent, false, waitTimeout);
                if (evt == 0)//return the thread
                {
                    return;
                }
                if (evt == 2)
                {
                    //fire the ConnectNotify Event
                    OnConnectNotify(new ConnectNotifyEventArgs(ConnState.Disconnectiong, ConnectType));

                    switch (ConnectType)
                    {
                        case ConnType.Offline:
                            Device.DisConnectGprs(gprsName);
                            Device.DisableGsmModule();
                            Device.DisableWlanModule();
                            break;
                        case ConnType.Gprs:
                            Device.DisConnectGprs(gprsName);
                            Device.DisableGsmModule();
                            break;
                        case ConnType.Wlan:
                            Device.DisableWlanModule();
                            break;
                    }
                    //fire the ConnectNotify Event
                    OnConnectNotify(new ConnectNotifyEventArgs(ConnState.NoConnect, ConnectType));
                }
                else
                {
                    IsConnecting = true;

                    //fire the ConnectNotify Event
                    OnConnectNotify(new ConnectNotifyEventArgs(ConnState.Connecting, ConnectType));

                    //if the connect request then disable the no need Network.
                    if (evt == 1)
                    {
                        switch (ConnectType)
                        {
                            case ConnType.Offline:
                                Device.DisConnectGprs(gprsName);
                                Device.DisableGsmModule();
                                Device.DisableWlanModule();
                                break;
                            case ConnType.Gprs:
                                Device.DisableWlanModule();
                                break;
                            case ConnType.Wlan:
                                Device.DisConnectGprs(gprsName);
                                Device.DisableGsmModule();
                                break;
                        }
                    }

                    ConnState res = ConnState.ConnectSuccess;
                    if (!Device.CheckNetworkStat())
                    {
                        res = ConnState.NoConnect;

                        switch (ConnectType)
                        {
                            case ConnType.Gprs:
                                if (Device.EnableGsmModule())
                                {
                                    if (Device.GetGprsStatus(gprsName))
                                    {
                                        Device.DisConnectGprs(gprsName);
                                    }

                                    uint err;
                                    if (Device.ConnectGprs(gprsName, out err))
                                    {
                                        res = ConnState.ConnectSuccess;
                                        Thread.Sleep(5000);
                                    }
                                }
                                break;
                            case ConnType.Wlan:
                                bool ret;
                                if (Device.GetWlanPowerStatus() == 0)
                                {
                                    ret = Device.EnableWlanModule();
                                }
                                else
                                {
                                    ret = Device.RefreshWlanPreferredList();
                                }

                                Thread.Sleep(2000);
                                for (int i = 0; i < 15; i++)
                                {
                                    if (Device.CheckNetworkStat())
                                    {
                                        res = ConnState.ConnectSuccess;
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    Thread.Sleep(1000);
                                }
                                break;
                        }
                    }
                    OnConnectNotify(new ConnectNotifyEventArgs(res, ConnectType));
                    IsConnecting = false;
                }
            }
        }
        #endregion
    }
}
