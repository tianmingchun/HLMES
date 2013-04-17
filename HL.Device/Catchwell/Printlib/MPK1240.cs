namespace Printlib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class MPK1240 : Printer
    {
        public MPK1240()
        {
        }

        public MPK1240(SerialPort CommPort) : base(CommPort)
        {
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
                    if (((buffer2[0] == 0x6f) && (buffer2[1] == 0x6b)) && (buffer2[2] == 0x21))
                    {
                        return 1;
                    }
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

        public int PrintPicInflash(byte Index, uint leftoffset, byte BGop)
        {
            if ((Index > 5) || (Index == 0))
            {
                return -6;
            }
            byte[] data = new byte[] { 0x1b, 0x2a, (byte) (0x20 - BGop), Index, (byte) (leftoffset % 0x100), (byte) (leftoffset / 0x100) };
            return base.SendData(data);
        }

        public int PrintPicture(Bitmap btm, uint leftoffset)
        {
            int height = btm.Height;
            int width = btm.Width;
            int num3 = (height + 0x17) / 0x18;
            int num4 = (width > 0x180) ? 0x180 : width;
            if ((leftoffset + num4) > 0x180L)
            {
                return -6;
            }
            int num5 = 3 * num4;
            int num6 = (9 + num5) + 2;
            byte[] buffer = new byte[num6];
            buffer[0] = 0x1b;
            buffer[1] = 0x24;
            buffer[2] = (byte) (leftoffset % 0x100);
            buffer[3] = (byte) (leftoffset / 0x100);
            buffer[4] = 0x1b;
            buffer[5] = 0x2a;
            buffer[6] = 0x21;
            buffer[7] = (byte) (num4 % 0x100);
            buffer[8] = (byte) (num4 / 0x100);
            Rectangle rect = new Rectangle(0, 0, btm.Width, btm.Height);
            BitmapData bitmapdata = btm.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr source = bitmapdata.Scan0;
            int length = (btm.Width * btm.Height) * 3;
            byte[] destination = new byte[length];
            Marshal.Copy(source, destination, 0, length);
            for (int i = 0; i < num3; i++)
            {
                int num9 = 9;
                for (int j = 0; j < num4; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        byte num12 = 0;
                        for (int m = 0; m < 8; m++)
                        {
                            int num14 = ((i * 0x18) + (k * 8)) + m;
                            if (num14 <= height)
                            {
                                int index = (bitmapdata.Stride * num14) + (j * 3);
                                try
                                {
                                    if (destination[index] == 0)
                                    {
                                        num12 = (byte) (num12 | ((byte) (((int) 0x80) >> m)));
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        buffer[num9++] = num12;
                    }
                }
                buffer[num9++] = 13;
                buffer[num9++] = 10;
                base.SendData(buffer);
            }
            btm.UnlockBits(bitmapdata);
            return 0;
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

