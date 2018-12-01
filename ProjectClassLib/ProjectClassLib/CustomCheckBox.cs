using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ausbildungsnachweis_Generator
{
    public partial class CustomCheckBox : UserControl
    {
        public CustomCheckBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public enum markStyle
        {
            Filled,
            Cross,
            Check
        }

        public delegate void CheckedChangeDelegate(object sender, EventArgs e);
        public event CheckedChangeDelegate CheckedChange;

        bool checkedState = false;
        int borderMargin = 8;
        int buffer = 11;
        int markLineWidth = 2;
        Color borderColor = Color.Black;
        Color checkColor = Color.Black;
        bool freeSize = false;
        markStyle checkedStyle = markStyle.Filled;

        [Description("State of the CkecbBox"), Category("Appearance")]
        public bool CheckedState
        {
            get => checkedState;
            set
            {
                checkedState = value;
                this.Invalidate();
            }
        }
        [Description("Margin of the Border"), Category("Appearance")]
        public int BorderMargin
        {
            get => borderMargin;
            set
            {
                borderMargin = value;
                this.Invalidate();
            }
        }
        [Description("Margin of the Checkmark"), Category("Appearance")]
        public int Buffer
        {
            get => buffer;
            set
            {
                buffer = value;
                this.Invalidate();
            }
        }
        [Description("Color of the Border"), Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Description("Color of the Checkmark"), Category("Appearance")]
        public Color CheckColor
        {
            get => checkColor;
            set
            {
                checkColor = value;
                this.Invalidate();
            }
        }

        [Description("Unlock the Control to be freely resized"), Category("Appearance")]
        public bool FreeSize
        {
            get => freeSize;
            set
            {
                freeSize = value;
                this.Invalidate();
            }
        }

        [Description("Unlock the Control to be freely resized"), Category("Appearance"), DisplayName("Checkmark Style")]
        public markStyle CheckedStyle
        {
            get => checkedStyle;
            set {
                checkedStyle = value;
                this.Invalidate();
            }
        }


        [Description("Line Width of the Check Mark (Only visible when CheckMark is not filled!)"), Category("Appearance")]
        public int MarkLineWidth
        {
            get => markLineWidth;
            set
            {
                markLineWidth = value;
                this.Invalidate();
            }
        }

        private void CustomCheckBox_Paint(object sender, PaintEventArgs e)
        {
            int wid = 0;
            if (Width > Height) wid = Height;
            else wid = Width;

            e.Graphics.DrawRectangle(new Pen(borderColor), new Rectangle(new Point(borderMargin, borderMargin), new Size(wid - borderMargin * 2, wid - borderMargin * 2)));

            if (checkedState)
            {
                switch (checkedStyle)
                {
                    case markStyle.Filled:

                        e.Graphics.FillRectangle(new SolidBrush(checkColor), new Rectangle(new Point(buffer, buffer), new Size(wid + 1 - buffer * 2, wid + 1 - buffer * 2)));
                        break;
                    case markStyle.Cross:

                        e.Graphics.DrawLine(new Pen(checkColor, markLineWidth), new PointF(buffer - 0.5f, buffer - 0.5f), new PointF(wid - buffer - 0.5f, wid - buffer - 0.5f));
                        e.Graphics.DrawLine(new Pen(checkColor, markLineWidth), new PointF(wid - buffer - 0.5f, buffer - 0.5f), new PointF(buffer - 0.5f, wid - buffer - 0.5f));
                        break;
                    case markStyle.Check:

                        e.Graphics.DrawLine(new Pen(checkColor, markLineWidth), new PointF(buffer - 0.5f, ((wid - buffer) / 3) * 2 + buffer - 0.5f), new PointF(((wid - buffer) / 3) + markLineWidth / 2, wid - buffer - 0.5f));
                        e.Graphics.DrawLine(new Pen(checkColor, markLineWidth), new PointF(wid - buffer - 0.5f, buffer - 0.5f), new PointF(((wid - buffer) / 3), wid - buffer - 0.5f));
                        break;
                    default:
                        break;
                }
            }
        }

        private void CustomCheckBox_Click(object sender, EventArgs e)
        {
            if (checkedState) checkedState = false;
            else checkedState = true;
            this.Invalidate();
            if (CheckedChange != null)
            {
                CheckedChange(sender, e);
            }
        }

        private void CustomCheckBox_Resize(object sender, EventArgs e)
        {

        }

        private void CustomCheckBox_SizeChanged(object sender, EventArgs e)
        {
            if (!freeSize)
            {
                if (Width > Height) Height = Width;
                else Width = Height;
            }
        }
    }
}
