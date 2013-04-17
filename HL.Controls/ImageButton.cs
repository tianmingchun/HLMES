using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace HL.Controls
{
    public partial class ImageButton : Control
    {
        private bool bPushed = false;
        private System.Drawing.Image image;
        private Bitmap m_bmpOffscreen;
        public ImageButton()
        {
            InitializeComponent();
            base.Size = new Size(55, 55);
            base.Font = new Font("Tahoma", 10f, FontStyle.Regular);
            this.Image = null;
        }
       
        private Color BackgroundImageColor(System.Drawing.Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            return bitmap.GetPixel(0, 0);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.bPushed = true;
            base.Invalidate();
        }
      
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.bPushed = false;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush;            
            if (((this.m_bmpOffscreen == null) || (this.m_bmpOffscreen.Width != base.Width)) || (this.m_bmpOffscreen.Height != base.Height))
            {
                this.m_bmpOffscreen = new Bitmap(base.Width, base.Height);
            }
            Graphics graphics = Graphics.FromImage(this.m_bmpOffscreen);
            graphics.Clear(this.BackColor);
            if (!this.bPushed)
            {
                brush = new SolidBrush(base.Parent.BackColor);             
            }
            else
            {
                brush = new SolidBrush(Color.LightGray);
            }
            graphics.FillRectangle(brush, base.ClientRectangle);
            if (this.image != null)
            {
                //绘图目标范围
                Rectangle destRect;
                if (this.StretchImage && string.IsNullOrEmpty(this.Text))
                {
                    if (!this.bPushed)
                        destRect = new Rectangle(0, 0, base.Width, base.Height);
                    else
                        destRect = new Rectangle(1, 1, base.Width, base.Height);
                }
                else
                {
                    int x = (base.Width - this.image.Width) / 2;
                    int y;
                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        //Y顶部对齐
                        y = 3;
                    }
                    else
                    {
                        //图标本身带文字，则Y垂直居中。
                        y = (base.Height - this.image.Height) / 2;
                    }

                    if (!this.bPushed)
                    {
                        destRect = new Rectangle(x, y, this.image.Width, this.image.Height);
                    }
                    else
                    {
                        destRect = new Rectangle(x + 1, y + 1, this.image.Width, this.image.Height);
                    }
                }
                //ImageAttributes imageAttr = new ImageAttributes();
                //imageAttr.SetColorKey(this.BackgroundImageColor(this.image), this.BackgroundImageColor(this.image));
                //graphics.DrawImage(this.image, destRect, 0, 0, this.image.Width, this.image.Height, GraphicsUnit.Pixel, imageAttr);//会产生图片边框瑕疵

                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorKey(this.BackColor, this.BackColor);
                graphics.DrawImage(this.image, destRect, 0, 0, this.image.Width, this.image.Height, GraphicsUnit.Pixel, imageAttr);//会消除图片边框问题
                //graphics.DrawImage(this.image, 0, 0);//会消除图片边框问题
            }
            if (!string.IsNullOrEmpty(this.Text))
            {
                //大于4个换行，第一行4个字符，其余第二行显示。
                if (this.Text.Trim().Length > 4)
                {
                    string line1 = this.Text.Substring(0, 4);
                    string line2 = this.Text.Substring(4);
                    SizeF textSize = e.Graphics.MeasureString(line1, this.Font);
                    float textWidth = textSize.Width;
                    float textLeft;
                    if (textWidth < this.Width)
                        textLeft = Convert.ToSingle((this.Width - textWidth) / 2);
                    else
                        textLeft = 0f;
                    float textTop = Convert.ToSingle(this.Image.Height + 2);
                    if (this.bPushed)
                    {
                        textLeft++;
                        textTop++;
                    }
                    graphics.DrawString(line1, this.Font, new SolidBrush(this.ForeColor), textLeft, textTop);
                    int addHeight = Convert.ToInt32(textSize.Height);
                    textTop += addHeight;
                    textSize = e.Graphics.MeasureString(line2, this.Font);
                    textWidth = textSize.Width;
                    if (textWidth < this.Width)
                        textLeft = Convert.ToSingle((this.Width - textWidth) / 2);
                    else
                        textLeft = 0f;
                    graphics.DrawString(line2, this.Font, new SolidBrush(this.ForeColor), textLeft, textTop);
                   
                }
                else
                {
                    float textWidth = e.Graphics.MeasureString(this.Text, this.Font).Width;
                    float textLeft;
                    if (textWidth < this.Width)
                        textLeft = Convert.ToSingle((this.Width - textWidth) / 2);
                    else
                        textLeft = 0f;
                    float textTop = Convert.ToSingle(this.Image.Height + 5);
                    //float textTop = Convert.ToSingle(this.Image.Height + 10);
                    if (this.bPushed)
                    {
                        textLeft++;
                        textTop++;
                    }
                    graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textLeft, textTop);
                }
            }
            if (this.bPushed)
            {
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                graphics.DrawRectangle(new Pen(Color.Black), clientRectangle);
            }
            e.Graphics.DrawImage(this.m_bmpOffscreen, 0, 0);
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Invalidate();
        }

        /// <summary>
        /// 程序集文件
        /// </summary>
        public string Assembly { get; set; }
        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; }

        public System.Drawing.Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                if (this.image != null)
                {
                    base.Size = this.image.Size;
                }
                base.Invalidate();
            }
        }
        private string _text;
        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                this._text = value;
                //if (!string.IsNullOrEmpty(this._text) && this._text.Trim().Length > 5)
                //{
                //    using (Graphics g = this.CreateGraphics())
                //    {
                //        int addHeight = Convert.ToInt32(g.MeasureString(this._text, this.Font).Height);
                //        this.Height += addHeight;
                //    }
                //}
            }
        }

        private bool _stretchImage = true;
        public bool StretchImage
        {
            get
            {
                return _stretchImage;
            }
            set
            {
                _stretchImage = value;
            }
        }
       
    }
}
