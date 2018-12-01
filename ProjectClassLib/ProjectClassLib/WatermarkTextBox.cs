using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectClassLib
{
    public partial class WatermarkTextBox : UserControl
    {
        TextBox t = new TextBox();
        // Panel zur Darstellung der Umrandung
        Panel p = new Panel();
        // Panel zur wasserzeichen-darstellung
        Panel w = new Panel();

        public WatermarkTextBox()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            w.Click += W_Click;
            w.Paint += W_Paint;
            p.Paint += P_Paint;

            if (t.Text.Length <= 0)
            {
                w = new Panel()
                {
                    Parent = t,
                    Location = new Point(1, 0),
                    Height = t.Height,
                    Width = t.Width - 1
                };
                w.Click += W_Click;
                w.Paint += W_Paint;
            }
            else
            {
                w.Dispose();
            }
            this.Invalidate();


            t.Parent = p;
            t.TextChanged += T_TextChanged;
            t.BorderStyle = BorderStyle.None;
            t.BackColor = this.BackColor;
            t.Font = this.Font;
            t.Location = new Point(1, 1);
            t.Dock = DockStyle.Fill;

            p.Parent = this;
            p.BackColor = this.BackColor;
            p.Font = this.Font;
            p.Location = new Point(1, 1);
            p.Width = this.Width;
            p.Padding = new Padding(3, 1, 2, 1);
            //p.Padding = new Padding(2);
            p.Height = t.Height + p.Padding.Top + p.Padding.Bottom;

            //WireAllControls(this);
            WireAllControlsKeyDown(this);
            TextBoxKeyDown += WatermarkTextBox_TextBoxKeyDown;
        }

        private void WatermarkTextBox_TextBoxKeyDown(object sender, KeyEventArgs e)
        {
        }

        public delegate void KeyDownDown(object sender, KeyEventArgs e);
        public event KeyDownDown TextBoxKeyDown;

        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.MouseClick += ctl_Click;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }

        private void WireAllControlsKeyDown(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.KeyDown += Ctl_KeyDown;
                if (ctl.HasChildren)
                {
                    WireAllControlsKeyDown(ctl);
                }
            }
        }

        private void Ctl_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxKeyDown.Invoke(sender, e);
            //try
            //{
            //    TextBoxKeyDown(sender, e);
            //}
            //catch (Exception)
            //{
            //}
        }

        private void ctl_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
        }
        
        private void W_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(watermark, this.Font, new SolidBrush(watermarkColor), new Point(0, 0));
        }

        void activateTB()
        {
            t.Focus();
            this.ActiveControl = t;
        }

        private void W_Click(object sender, EventArgs e)
        {
            activateTB();
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            if (t.Text.Length <= 0)
            {
                w = new Panel()
                {
                    Parent = t,
                    Location = new Point(1, 0),
                    Height = t.Height,
                    Width = t.Width
                };
                w.Click += W_Click;
                w.Paint += W_Paint;
            }
            else
            {
                w.Dispose();
            }
            this.Invalidate();
        }

        ButtonBorderStyle borderStyle = ButtonBorderStyle.Solid;
        Color watermarkColor = Color.DarkGray;
        FontStyle watermarkStyle = FontStyle.Italic;
        Color borderColor = Color.Black;
        string watermark = "";

        [Description("Text of the TextBox"), Category("Appearance"), DisplayName("Text")]
        public string Text
        {
            get => t.Text;
            set
            {
                t.Text = value;
                this.Invalidate();
            }
        }

        [Description("Watermark of the TextBox"), Category("Appearance")]
        public string Watermark
        {
            get => watermark;
            set
            {
                watermark = value;
                this.Invalidate();
            }
        }

        [Description("Color of the TextBox Borders"), Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Description("Color of the Watermark"), Category("Appearance")]
        public Color WatermarkColor
        {
            get => watermarkColor;
            set
            {
                watermarkColor = value;
                w.Invalidate();
                this.Invalidate();
            }
        }

        [Description("Style of the border"), Category("Appearance")]
        public ButtonBorderStyle CustomBorderStyle
        {
            get => borderStyle;
            set
            {
                borderStyle = value;
                this.Invalidate();
            }
        }


        [Description("FontStyle of the Watermark"), Category("Appearance")]
        public FontStyle WatermarkStyle
        {
            get => watermarkStyle;
            set
            {
                watermarkStyle = value;
                this.Invalidate();
            }
        }

        [Description("PasswordChar of the TextBox"), Category("Behaviour")]
        public char PasswordChar
        {
            get => t.PasswordChar;
            set
            {
                t.PasswordChar = value;
                this.Invalidate();
            }
        }

        void resize()
        {
            p.Location = new Point(1, 1);
            t.Location = new Point(0, 0);
            t.Font = this.Font;
            this.Height = p.Height + 3;
            p.Width = this.Width - 3;
            t.Width = p.Width;

            w.Size = Size;
            w.Location = new Point(1, 0);
        }

        private void WatermarkTextBox_SizeChanged(object sender, EventArgs e)
        {
            resize();
        }

        private void WatermarkTextBox_Paint(object sender, PaintEventArgs e)
        {
            w.Invalidate();
            Rectangle rec = new Rectangle(new Point(p.Bounds.X - 1, p.Bounds.Y - 1), new Size(p.Bounds.Width + 2, p.Bounds.Height + 2));
            Font wFont = new Font(this.Font.FontFamily, this.Font.Size, watermarkStyle, this.Font.Unit);
            if (t.Text.Length <= 0) e.Graphics.DrawString(watermark, wFont, new SolidBrush(watermarkColor), new Point());
            ControlPaint.DrawBorder(e.Graphics, rec, borderColor, CustomBorderStyle);
        }

        private void P_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 1, 1, Width - 2, Height - 2);
        }

        private void WatermarkTextBox_FontChanged(object sender, EventArgs e)
        {
            resize();
        }

        private void WatermarkTextBox_BackColorChanged(object sender, EventArgs e)
        {
            t.BackColor = this.BackColor;
        }
    }
}
