namespace Printlib
{
    using System;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Printer
    {
        private SerialPort commport;
        public int portbaudrate;
        public FlowControl portflowcontrol;
        public int portreadbuffsize;
        public int portreadtimeout;
        public int portwritebuffsize;
        public int portwritetimeout;

        public Printer()
        {
            this.commport = null;
            this.portbaudrate = 0x1c200;
            this.portwritetimeout = 0x1388;
            this.portreadtimeout = 100;
            this.portreadbuffsize = 0x400;
            this.portwritebuffsize = 0x2800;
            this.portflowcontrol = FlowControl.None;
            this.commport = new SerialPort();
        }

        public Printer(SerialPort CommPort)
        {
            this.commport = null;
            this.portbaudrate = 0x1c200;
            this.portwritetimeout = 0x1388;
            this.portreadtimeout = 100;
            this.portreadbuffsize = 0x400;
            this.portwritebuffsize = 0x2800;
            this.portflowcontrol = FlowControl.None;
            this.commport = CommPort;
        }

        public void ClearPort()
        {
            byte[] buffer = null;
            int bytesToRead = this.commport.BytesToRead;
            if (bytesToRead > 0)
            {
                buffer = new byte[bytesToRead];
                try
                {
                    this.commport.Read(buffer, 0, bytesToRead);
                }
                catch
                {
                }
            }
        }

        public bool ClosePort()
        {
            try
            {
                this.commport.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CommIsOpen()
        {
            return this.commport.IsOpen;
        }

        public bool OpenPort(string port)
        {
            int num = 1;
            this.commport.PortName = port;
            this.commport.BaudRate = this.portbaudrate;
            this.commport.ReadBufferSize = this.portreadbuffsize;
            this.commport.WriteBufferSize = this.portwritebuffsize;
            this.commport.ReadTimeout = this.portreadtimeout;
            this.commport.WriteTimeout = this.portwritetimeout;
            this.commport.Handshake = (Handshake) this.portflowcontrol;
            while (num > 0)
            {
                try
                {
                    num--;
                    this.commport.Open();
                }
                catch
                {
                    continue;
                }
                break;
            }
            if (this.commport.IsOpen)
            {
                return true;
            }
            //MessageBox.Show("打开串口失败！");
            return false;
        }

        public int ReadData(out byte[] data)
        {
            if (!this.CommIsOpen())
            {
                data = null;
                return -1;
            }
            int bytesToRead = this.commport.BytesToRead;
            if (bytesToRead > 0)
            {
                data = new byte[bytesToRead];
                try
                {
                    this.commport.Read(data, 0, bytesToRead);
                }
                catch
                {
                    return -3;
                }
                return bytesToRead;
            }
            data = null;
            return bytesToRead;
        }

        public int ReadData(out byte[] data, uint Len, uint TotalTimeOut)
        {
            TimeSpan span;
            int bytesToRead;
            DateTime now = DateTime.Now;
            if (!this.CommIsOpen())
            {
                data = null;
                return -1;
            }
            do
            {
                bytesToRead = this.commport.BytesToRead;
                span = (TimeSpan) (DateTime.Now - now);
            }
            while ((bytesToRead < Len) && (span.TotalMilliseconds < TotalTimeOut));
            if (bytesToRead > Len)
            {
                bytesToRead = (int) Len;
            }
            if (bytesToRead > 0)
            {
                data = new byte[bytesToRead];
                try
                {
                    this.commport.Read(data, 0, bytesToRead);
                }
                catch
                {
                    return -3;
                }
                return bytesToRead;
            }
            data = null;
            return bytesToRead;
        }

        public int SendData(byte[] data)
        {
            if (!this.CommIsOpen())
            {
                return -1;
            }
            try
            {
                this.commport.Write(data, 0, data.Length);
            }
            catch
            {
                return -2;
            }
            return 0;
        }

        public int SendData(byte[] data, uint Len)
        {
            if (!this.CommIsOpen())
            {
                return -1;
            }
            try
            {
                this.commport.Write(data, 0, (int) Len);
            }
            catch
            {
                return -2;
            }
            return 0;
        }
    }
}

