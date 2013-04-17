namespace Printlib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class MPK1280 : Printer
    {
        public MPK1280()
        {
        }

        public MPK1280(SerialPort CommPort) : base(CommPort)
        {
        }

        private bool ByteIsNum(byte[] by)
        {
            for (int i = 0; i < by.Length; i++)
            {
                if ((by[i] < 0x30) || (by[i] > 0x39))
                {
                    return false;
                }
            }
            return true;
        }

        public int CheckPrinter()
        {
            byte[] data = new byte[] { 0x1c, 0x4c };
            if (base.SendData(data) >= 0)
            {
                byte[] buffer2;
                int num = base.ReadData(out buffer2, 12, 200);
                if (num < 0)
                {
                    return num;
                }
                try
                {
                    if (((buffer2[0] == 0x4f) && (buffer2[1] == 0x4b)) && (buffer2[2] == 0x21))
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public int Feed(byte row)
        {
            byte[] data = new byte[] { 0x1b, 0x4a, row };
            return base.SendData(data);
        }

        public int FeedToBlack()
        {
            byte[] data = new byte[] { 12 };
            return base.SendData(data);
        }

        public int FontBold(byte op)
        {
            byte[] data = new byte[] { 0x1b, 0x45, op };
            return base.SendData(data);
        }

        public int FontUnderLine(byte op)
        {
            if (op <= 2)
            {
                byte[] data = new byte[] { 0x1b, 0x2d, op };
                return base.SendData(data);
            }
            return -6;
        }

        public int FontZoom(byte height, byte width)
        {
            if ((height > 2) || (width > 2))
            {
                return -6;
            }
            byte[] data = new byte[] { 0x1d, 0x21, (byte) ((((width - 1) * 0x10) + height) - 1) };
            return base.SendData(data);
        }

        public int PrintCode(CodeModle modle, byte high, CodeHRI HRI, string str)
        {
            byte num = 0x49;
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(str);
            switch (modle)
            {
                case CodeModle.UPCA:
                    if (this.ByteIsNum(bytes))
                    {
                        num = 0x41;
                        break;
                    }
                    return -6;

                case CodeModle.UPCE:
                    if (this.ByteIsNum(bytes))
                    {
                        num = 0x42;
                        break;
                    }
                    return -6;

                case CodeModle.EAN13:
                    if (this.ByteIsNum(bytes))
                    {
                        num = 0x43;
                        break;
                    }
                    return -6;

                case CodeModle.EAN8:
                    if (this.ByteIsNum(bytes))
                    {
                        num = 0x44;
                        break;
                    }
                    return -6;

                case CodeModle.CODE39:
                    num = 0x45;
                    break;

                case CodeModle.ITF:
                    if (this.ByteIsNum(bytes))
                    {
                        num = 70;
                        break;
                    }
                    return -6;

                case CodeModle.CODEBAR:
                    num = 0x47;
                    break;

                case CodeModle.CODE93:
                    num = 0x48;
                    break;

                case CodeModle.CODE128:
                    num = 0x49;
                    break;
            }
            byte[] data = new byte[] { 0x1d, 0x68, high };
            base.SendData(data);
            byte[] buffer3 = new byte[] { 0x1d, 0x48, (byte) HRI };
            base.SendData(buffer3);
            byte[] buffer4 = new byte[] { 0x1d, 0x6b, num, (byte) bytes.Length };
            base.SendData(buffer4);
            return base.SendData(bytes);
        }

        public int PrintCR()
        {
            byte[] data = new byte[] { 13, 10 };
            return base.SendData(data);
        }

        public int PrinterBeep()
        {
            byte[] data = new byte[] { 7 };
            return base.SendData(data);
        }

        public int PrinterReset()
        {
            byte[] data = new byte[] { 0x1b, 0x40 };
            return base.SendData(data);
        }

        public int PrinterWake()
        {
            byte[] data = new byte[6];
            base.SendData(data);
            Thread.Sleep(300);
            return 0;
        }

        public int PrintPicInflash(byte Index, Picmode mode)
        {
            if ((Index > 5) || (Index == 0))
            {
                return -6;
            }
            byte[] data = new byte[5];
            data[0] = 0x1c;
            data[1] = 0x70;
            data[3] = Index;
            data[4] = (byte) mode;
            return base.SendData(data);
        }

        public int PrintPicture(Bitmap btm, Picmode mode)
        {
            int num7;
            int num8;
            byte num9;
            int num10;
            int num11;
            int num4 = 0;
            byte[] buffer = new byte[480];
            uint len = 0;
            if (mode == Picmode.Overlapping)
            {
                return -6;
            }
            int height = btm.Height;
            int num2 = (btm.Width > 0x180) ? 0x180 : btm.Width;
            int num3 = (num2 + 7) / 8;
            buffer[0] = 0x1d;
            buffer[1] = 0x76;
            buffer[2] = 0x30;
            buffer[3] = (byte) mode;
            buffer[4] = (byte) (num3 % 0x100);
            buffer[5] = (byte) (num3 / 0x100);
            buffer[6] = (byte) (height % 0x100);
            buffer[7] = (byte) (height / 0x100);
            base.SendData(buffer, 8);
            Rectangle rect = new Rectangle(0, 0, btm.Width, btm.Height);
            BitmapData bitmapdata = btm.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr source = bitmapdata.Scan0;
            int length = (btm.Width * btm.Height) * 3;
            byte[] destination = new byte[length];
            Marshal.Copy(source, destination, 0, length);
            while ((height - num4) > 10)
            {
                len = 0;
                num7 = 0;
                while (num7 < 10)
                {
                    num8 = 0;
                    while (num8 < num3)
                    {
                        num9 = 0;
                        num10 = 0;
                        while (num10 < 8)
                        {
                            if (((num8 * 8) + num10) >= num2)
                            {
                                break;
                            }
                            num11 = (bitmapdata.Stride * (num4 + num7)) + (((num8 * 8) + num10) * 3);
                            try
                            {
                                if (destination[num11] == 0)
                                {
                                    num9 = (byte) (num9 | ((byte) (((int) 0x80) >> num10)));
                                }
                            }
                            catch
                            {
                            }
                            num10++;
                        }
                        buffer[len++] = num9;
                        num8++;
                    }
                    num7++;
                }
                num4 += 10;
                base.SendData(buffer, len);
            }
            len = 0;
            for (num7 = 0; num7 < (height - num4); num7++)
            {
                for (num8 = 0; num8 < num3; num8++)
                {
                    num9 = 0;
                    for (num10 = 0; num10 < 8; num10++)
                    {
                        if (((num8 * 8) + num10) >= num2)
                        {
                            break;
                        }
                        num11 = (bitmapdata.Stride * (num4 + num7)) + (((num8 * 8) + num10) * 3);
                        try
                        {
                            if (destination[num11] == 0)
                            {
                                num9 = (byte) (num9 | ((byte) (((int) 0x80) >> num10)));
                            }
                        }
                        catch
                        {
                        }
                    }
                    buffer[len++] = num9;
                }
            }
            base.SendData(buffer, len);
            btm.UnlockBits(bitmapdata);
            return 0;
        }

        public int PrintQRcode(uint Ver, uint Level, string str)
        {
            if (Ver > 20)
            {
                return -6;
            }
            if (Level > 3)
            {
                return -6;
            }
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(str);
            int length = bytes.Length;
            byte[] data = new byte[] { 0x1d, 0x6c, (byte) Ver, (byte) Level, (byte) (length % 0x100), (byte) (length / 0x100) };
            base.SendData(data);
            return base.SendData(bytes);
        }

        public int PrintStr(string str)
        {
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(str);
            return base.SendData(bytes);
        }

        public int PrintStrLine(string str)
        {
            str = str + "\r\n";
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(str);
            return base.SendData(bytes);
        }

        public int SetAbsPosition(int abspos)
        {
            byte[] data = new byte[] { 0x1b, 0x24, (byte) (abspos % 0x100), (byte) (abspos / 0x100) };
            return base.SendData(data);
        }

        public int SetLeftMargin(byte dots)
        {
            byte[] data = new byte[] { 0x1d, 0x4c, (byte) (dots % 0x100), (byte) (dots / 0x100) };
            return base.SendData(data);
        }

        public int SetRelPosition(int relpos)
        {
            byte[] data = new byte[] { 0x1b, 0x5c, (byte) (relpos % 0x100), (byte) (relpos / 0x100) };
            return base.SendData(data);
        }

        public int SetRowDistance(byte distance)
        {
            byte[] data = new byte[] { 0x1b, 0x33, distance };
            return base.SendData(data);
        }

        public int SetSnapMode(byte snapmode)
        {
            if (snapmode <= 2)
            {
                byte[] data = new byte[] { 0x1b, 0x61, snapmode };
                return base.SendData(data);
            }
            return -6;
        }
    }
}

