namespace Printlib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class MPK1230 : Printer
    {
        public MPK1230()
        {
        }

        public MPK1230(SerialPort CommPort) : base(CommPort)
        {
        }

        public int Feed(byte row)
        {
            byte[] data = new byte[] { 0x1b, 0x4a, row };
            return base.SendData(data);
        }

        public int FontBold(byte op)
        {
            byte[] data = new byte[] { 0x1b, 0x25, (byte) (op + 0x30) };
            return base.SendData(data);
        }

        public int FontCompress23(byte op)
        {
            if (op > 2)
            {
                return -6;
            }
            byte[] data = new byte[] { 0x1b, 0x75, (byte) (op + 0x31) };
            return base.SendData(data);
        }

        public int FontZoom(byte height, byte width)
        {
            if ((height > 2) || (width > 2))
            {
                return -6;
            }
            byte[] data = new byte[] { 0x1b, 0x65, (byte) (height + 0x30), (byte) (width + 0x30) };
            return base.SendData(data);
        }

        public int PrintCR()
        {
            byte[] data = new byte[] { 13, 10 };
            return base.SendData(data);
        }

        public int PrinterWake()
        {
            byte[] data = new byte[6];
            base.SendData(data);
            Thread.Sleep(300);
            return 0;
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

        public int SetRowDistance(byte distance)
        {
            if ((distance >= 0) && (distance <= 9))
            {
                distance = (byte) (distance + 0x30);
            }
            else if ((distance >= 10) && (distance <= 15))
            {
                distance = (byte) ((distance + 0x61) - 10);
            }
            else
            {
                return -6;
            }
            byte[] data = new byte[] { 0x1b, 0x33, distance };
            return base.SendData(data);
        }
    }
}

