namespace HL.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class HeadLabel : Control
    {
        public HeadLabel()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Font != null)
            {
                Graphics graphics = e.Graphics;
                SizeF ef = graphics.MeasureString(this.Text, this.Font);
                Size size = new Size(((int) ef.Width) + 1, ((int) ef.Height) + 1);
                graphics.FillRegion(new SolidBrush(base.Parent.BackColor), graphics.Clip);
                int num = base.Height / 3;
                Point[] pointArray = new Point[num];
                int x = 0;
                int y = num;
                int num4 = 3 - (2 * num);
                int num5 = 0;
                while (x < y)
                {
                    pointArray[num5++] = new Point(x, y);
                    if (num4 < 0)
                    {
                        num4 += (4 * x) + 6;
                    }
                    else
                    {
                        num4 += (4 * (x - y)) + 10;
                        y--;
                    }
                    x++;
                }
                Point[] points = new Point[(2 * num5) + 3];
                for (int i = 0; i < num5; i++)
                {
                    points[i] = new Point((pointArray[i].X + num) + size.Width, num - pointArray[i].Y);
                    points[((2 * num5) - i) - 1] = new Point((pointArray[i].Y + num) + size.Width, num - pointArray[i].X);
                }
                points[2 * num5] = new Point((size.Width + num) + num, base.Height);
                points[(2 * num5) + 1] = new Point(0, base.Height);
                points[(2 * num5) + 2] = new Point(0, 0);
                graphics.FillPolygon(new SolidBrush(this.BackColor), points);
                graphics.FillRegion(new SolidBrush(this.BackColor), new Region(new Rectangle(0, base.Height - 3, base.Width, 3)));
                graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new RectangleF((float) num, 0f, (float) size.Width, (float) size.Height));
                graphics.Dispose();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            base.Invalidate();
        }
    }
}

