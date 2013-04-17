namespace Printlib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class MPK1320 : Printer
    {
        public MPK1320()
        {
        }

        public MPK1320(SerialPort CommPort) : base(CommPort)
        {
        }

        public int CardAPDU(CARDTYPE CardType, byte[] SendBuff, out byte[] RevBuff, int Delayms)
        {
            byte num2 = 0;
            byte[] data = new byte[0x107];
            RevBuff = null;
            if (((byte) CardType) > 1)
            {
                return -6;
            }
            data[0] = 0x1b;
            data[1] = 0x1b;
            data[2] = 4;
            data[3] = 0;
            data[4] = (byte) SendBuff.Length;
            for (int i = 0; i < ((byte) SendBuff.Length); i++)
            {
                data[5 + i] = SendBuff[i];
                num2 = (byte) (num2 ^ SendBuff[i]);
            }
            data[((byte) SendBuff.Length) + 5] = num2;
            if (CardType == CARDTYPE.PsamCard)
            {
                data[3] = 1;
            }
            base.ClearPort();
            int num = base.SendData(data, (byte) (SendBuff.Length + 6));
            if (num >= 0)
            {
                Thread.Sleep(Delayms);
                num = base.ReadData(out RevBuff);
                if (num < 0)
                {
                    return num;
                }
            }
            return num;
        }

        public int CardAPDU(CARDTYPE CardType, byte[] SendBuff, int SendLen, out byte[] RevBuff, int Delayms)
        {
            byte num2 = 0;
            byte[] data = new byte[0x107];
            RevBuff = null;
            if (((byte) CardType) > 1)
            {
                return -6;
            }
            data[0] = 0x1b;
            data[1] = 0x1b;
            data[2] = 4;
            data[3] = 0;
            data[4] = (byte) SendLen;
            for (int i = 0; i < SendLen; i++)
            {
                data[5 + i] = SendBuff[i];
                num2 = (byte) (num2 ^ SendBuff[i]);
            }
            data[SendLen + 5] = num2;
            if (CardType == CARDTYPE.PsamCard)
            {
                data[3] = 1;
            }
            base.ClearPort();
            int num = base.SendData(data, (byte) (SendLen + 6));
            if (num >= 0)
            {
                Thread.Sleep(Delayms);
                num = base.ReadData(out RevBuff);
                if (num < 0)
                {
                    return num;
                }
            }
            return num;
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
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public int CLoseCard(CARDTYPE CardType, out byte[] RevBuff)
        {
            byte[] data = new byte[] { 0x1b, 0x1b, 5, 0, 1 };
            RevBuff = null;
            if (((byte) CardType) > 1)
            {
                return -6;
            }
            if (CardType == CARDTYPE.PsamCard)
            {
                data[3] = 1;
            }
            base.ClearPort();
            int num = base.SendData(data);
            if ((num >= 0) && (num < 0))
            {
                return num;
            }
            return num;
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

        public int JudgeAPDU(int RevLen, byte[] JudgeBuff)
        {
            int num3 = 0;
            if (RevLen >= 7)
            {
                if (((JudgeBuff[0] != 0x1b) || (JudgeBuff[1] != 0x1b)) || (JudgeBuff[2] != 4))
                {
                    return -7;
                }
                int num = (JudgeBuff[3] * 0x100) + JudgeBuff[4];
                if (num == 0x4552)
                {
                    return ((JudgeBuff[5] * 0x100) + JudgeBuff[6]);
                }
                int num4 = JudgeBuff[6] + 8;
                if (num4 > RevLen)
                {
                    return -7;
                }
                if (num == 0x4f4b)
                {
                    num3 = 0;
                    num4 = JudgeBuff[6];
                    for (int i = 7; i < (num4 + 7); i++)
                    {
                        num3 ^= JudgeBuff[i];
                    }
                    if (num3 != JudgeBuff[RevLen - 1])
                    {
                        return -7;
                    }
                    return ((JudgeBuff[RevLen - 3] * 0x100) + JudgeBuff[RevLen - 2]);
                }
            }
            return -7;
        }

        public int JudgeReset(CARDTYPE CardType, int RevLen, byte[] JudgeBuff)
        {
            int num3 = 0;
            if (RevLen >= 7)
            {
                if (((JudgeBuff[0] != 0x1b) || (JudgeBuff[1] != 0x1b)) || (JudgeBuff[2] != 5))
                {
                    return -7;
                }
                int num = (JudgeBuff[3] * 0x100) + JudgeBuff[4];
                if (num == 0x4552)
                {
                    return ((JudgeBuff[5] * 0x100) + JudgeBuff[6]);
                }
                int num4 = JudgeBuff[6] + 8;
                if (num4 > RevLen)
                {
                    return -7;
                }
                if (num == 0x4f4b)
                {
                    num3 = 0;
                    num4 = JudgeBuff[6];
                    for (int i = 7; i < (num4 + 7); i++)
                    {
                        num3 ^= JudgeBuff[i];
                    }
                    if (num3 != JudgeBuff[RevLen - 1])
                    {
                        return -7;
                    }
                    return 0;
                }
            }
            return -7;
        }

        public int OpenCard(CARDTYPE CardType, out byte[] RevBuff)
        {
            byte[] data = new byte[] { 0x1b, 0x1b, 5, 0, 0, 0 };
            RevBuff = null;
            if (((byte) CardType) > 1)
            {
                return -6;
            }
            if (CardType == CARDTYPE.PsamCard)
            {
                data[3] = 1;
            }
            base.ClearPort();
            int num = base.SendData(data);
            if (num >= 0)
            {
                Thread.Sleep(200);
                num = base.ReadData(out RevBuff);
                if (num < 0)
                {
                    return num;
                }
            }
            return num;
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
            int num4 = (width > 0x240) ? 0x240 : width;
            if ((leftoffset + num4) > 0x240L)
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

        public int Read6001(out byte[] RdBuff, int StartAddr, int RdLen)
        {
            byte[] revBuff = new byte[0x107];
            int num4 = 0;
            RdBuff = null;
            int revLen = this.OpenCard(CARDTYPE.UserCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            int num2 = this.JudgeReset(CARDTYPE.UserCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] sendBuff = new byte[] { 0, 0xa4, 4, 0, 9, 160, 0, 0, 0, 1, 0x20, 0x10, 0, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer3 = new byte[] { 0, 0xa4, 0, 0, 2, 0x6f, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer3, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer4 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer4, out revBuff, 250);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer5 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer5, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer7 = new byte[5];
            buffer7[1] = 0xb0;
            buffer7[2] = 0x81;
            buffer7[3] = (byte) StartAddr;
            buffer7[4] = (byte) RdLen;
            byte[] buffer6 = buffer7;
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer6, out revBuff, 400);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            num4 = revBuff[6];
            RdBuff = new byte[num4];
            for (int i = 0; i < num4; i++)
            {
                RdBuff[i] = revBuff[7 + i];
            }
            this.CLoseCard(CARDTYPE.UserCard, out revBuff);
            return num4;
        }

        public int Read6005(byte BinaryNum, out byte[] RdBuff)
        {
            byte[] revBuff = new byte[0x107];
            RdBuff = null;
            int revLen = this.OpenCard(CARDTYPE.UserCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            int num2 = this.JudgeReset(CARDTYPE.UserCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] sendBuff = new byte[] { 0, 0xa4, 4, 0, 9, 160, 0, 0, 0, 1, 0x20, 0x10, 0, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer3 = new byte[] { 0, 0xa4, 0, 0, 2, 0x6f, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer3, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer4 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer4, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer5 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 5 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer5, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer7 = new byte[] { 0, 0xb2, 0, 4, 0x45 };
            buffer7[2] = BinaryNum;
            byte[] buffer6 = buffer7;
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer6, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            int num4 = revBuff[6];
            RdBuff = new byte[num4];
            for (int i = 0; i < num4; i++)
            {
                RdBuff[i] = revBuff[7 + i];
            }
            this.CLoseCard(CARDTYPE.UserCard, out revBuff);
            return num4;
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

        public int Write6001(byte[] WrBuff, int StartAddr, int WrLen)
        {
            int num3;
            byte[] revBuff = new byte[0x107];
            byte[] sendBuff = new byte[200];
            byte[] buffer3 = new byte[200];
            int revLen = this.OpenCard(CARDTYPE.UserCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            int num2 = this.JudgeReset(CARDTYPE.UserCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] buffer4 = new byte[] { 0, 0xa4, 4, 0, 9, 160, 0, 0, 0, 1, 0x20, 0x10, 0, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer4, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer5 = new byte[] { 0, 0xa4, 0, 0, 2, 0x6f, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer5, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer6 = new byte[] { 0, 0xb2, 1, 4, 0x13 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer6, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 0; num3 < 9; num3++)
            {
                sendBuff[num3] = revBuff[8 + num3];
            }
            for (num3 = 0; num3 < 8; num3++)
            {
                sendBuff[num3] = (byte) (((byte) (sendBuff[num3] << 4)) + ((byte) (sendBuff[num3 + 1] >> 4)));
            }
            byte[] buffer7 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer7, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer11 = new byte[5];
            buffer11[1] = 0x84;
            buffer11[4] = 4;
            byte[] buffer8 = buffer11;
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer8, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            num3 = 0;
            while (num3 < 4)
            {
                buffer3[num3] = revBuff[7 + num3];
                num3++;
            }
            while (num3 < 8)
            {
                buffer3[num3] = 0;
                num3++;
            }
            revLen = this.OpenCard(CARDTYPE.PsamCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeReset(CARDTYPE.PsamCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] buffer9 = new byte[] { 0, 0xa4, 0, 0, 2, 0x20, 1 };
            revLen = this.CardAPDU(CARDTYPE.PsamCard, buffer9, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                sendBuff[num3] = sendBuff[num3 - 5];
            }
            sendBuff[0] = 0x80;
            sendBuff[1] = 0x1a;
            sendBuff[2] = 0x2d;
            sendBuff[3] = 1;
            sendBuff[4] = 8;
            revLen = this.CardAPDU(CARDTYPE.PsamCard, sendBuff, 13, out revBuff, 400);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                buffer3[num3] = buffer3[num3 - 5];
            }
            buffer3[0] = 0x80;
            buffer3[1] = 250;
            buffer3[2] = 0;
            buffer3[3] = 0;
            buffer3[4] = 8;
            revLen = this.CardAPDU(CARDTYPE.PsamCard, buffer3, 13, out revBuff, 400);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 0; num3 < 8; num3++)
            {
                sendBuff[num3] = revBuff[7 + num3];
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                sendBuff[num3] = sendBuff[num3 - 5];
            }
            sendBuff[0] = 0;
            sendBuff[1] = 130;
            sendBuff[2] = 0;
            sendBuff[3] = 0;
            sendBuff[4] = 9;
            sendBuff[13] = 0;
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, 14, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer10 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer10, out revBuff, 200);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            sendBuff[0] = 0;
            sendBuff[1] = 0xd6;
            sendBuff[2] = 0x81;
            sendBuff[3] = (byte) StartAddr;
            sendBuff[4] = (byte) WrLen;
            for (num3 = 0; num3 < WrLen; num3++)
            {
                sendBuff[5 + num3] = WrBuff[num3];
            }
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, WrLen + 5, out revBuff, 400);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            this.CLoseCard(CARDTYPE.UserCard, out revBuff);
            this.CLoseCard(CARDTYPE.PsamCard, out revBuff);
            return 0x9000;
        }

        public int Write6005(byte[] WrBuff, int WrLen)
        {
            int num3;
            byte[] revBuff = new byte[0x107];
            byte[] sendBuff = new byte[100];
            byte[] buffer3 = new byte[20];
            int revLen = this.OpenCard(CARDTYPE.UserCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            int num2 = this.JudgeReset(CARDTYPE.UserCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] buffer4 = new byte[] { 0, 0xa4, 4, 0, 9, 160, 0, 0, 0, 1, 0x20, 0x10, 0, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer4, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer5 = new byte[] { 0, 0xa4, 0, 0, 2, 0x6f, 1 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer5, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer6 = new byte[] { 0, 0xb2, 1, 4, 0x13 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer6, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 0; num3 < 9; num3++)
            {
                sendBuff[num3] = revBuff[8 + num3];
            }
            for (num3 = 0; num3 < 8; num3++)
            {
                sendBuff[num3] = (byte) (((byte) (sendBuff[num3] << 4)) + ((byte) (sendBuff[num3 + 1] >> 4)));
            }
            byte[] buffer7 = new byte[] { 0, 0xa4, 0, 0, 2, 0x60, 0 };
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer7, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            byte[] buffer10 = new byte[5];
            buffer10[1] = 0x84;
            buffer10[4] = 4;
            byte[] buffer8 = buffer10;
            revLen = this.CardAPDU(CARDTYPE.UserCard, buffer8, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            num3 = 0;
            while (num3 < 4)
            {
                buffer3[num3] = revBuff[7 + num3];
                num3++;
            }
            while (num3 < 8)
            {
                buffer3[num3] = 0;
                num3++;
            }
            revLen = this.OpenCard(CARDTYPE.PsamCard, out revBuff);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeReset(CARDTYPE.PsamCard, revLen, revBuff);
            if (num2 != 0)
            {
                return num2;
            }
            byte[] buffer9 = new byte[] { 0, 0xa4, 0, 0, 2, 0x20, 1 };
            revLen = this.CardAPDU(CARDTYPE.PsamCard, buffer9, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                sendBuff[num3] = sendBuff[num3 - 5];
            }
            sendBuff[0] = 0x80;
            sendBuff[1] = 0x1a;
            sendBuff[2] = 0x2e;
            sendBuff[3] = 1;
            sendBuff[4] = 8;
            revLen = this.CardAPDU(CARDTYPE.PsamCard, sendBuff, 13, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                buffer3[num3] = buffer3[num3 - 5];
            }
            buffer3[0] = 0x80;
            buffer3[1] = 250;
            buffer3[2] = 0;
            buffer3[3] = 0;
            buffer3[4] = 8;
            revLen = this.CardAPDU(CARDTYPE.PsamCard, buffer3, 13, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            for (num3 = 0; num3 < 8; num3++)
            {
                sendBuff[num3] = revBuff[7 + num3];
            }
            for (num3 = 12; num3 > 4; num3--)
            {
                sendBuff[num3] = sendBuff[num3 - 5];
            }
            sendBuff[0] = 0;
            sendBuff[1] = 130;
            sendBuff[2] = 0;
            sendBuff[3] = 0;
            sendBuff[4] = 9;
            sendBuff[13] = 1;
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, 14, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            sendBuff[0] = 0;
            sendBuff[1] = 0xe2;
            sendBuff[2] = 0;
            sendBuff[3] = 40;
            sendBuff[4] = (byte) WrLen;
            for (num3 = 0; num3 < WrLen; num3++)
            {
                sendBuff[5 + num3] = WrBuff[num3];
            }
            revLen = this.CardAPDU(CARDTYPE.UserCard, sendBuff, WrLen + 5, out revBuff, 300);
            if (revLen <= 0)
            {
                return revLen;
            }
            num2 = this.JudgeAPDU(revLen, revBuff);
            if (num2 != 0x9000)
            {
                return num2;
            }
            this.CLoseCard(CARDTYPE.UserCard, out revBuff);
            this.CLoseCard(CARDTYPE.PsamCard, out revBuff);
            return 0x9000;
        }
    }
}

