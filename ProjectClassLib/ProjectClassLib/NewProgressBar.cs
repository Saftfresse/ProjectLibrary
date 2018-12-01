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
    public partial class NewProgressBar : UserControl
    {
        // Variablen

        Color backColor;
        Color barColor;
        Color borderColor;
        
        int maxSteps = 10;
        int currStep = 0;
        int step = 1;

        bool barFull = false;

        [Description("Backcolor of the Progress Bar"), Category("Appearance")]
        public Color BackColor { get => backColor; set => backColor = value; }
        [Description("Color of the Progress Bar"), Category("Appearance")]
        public Color BarColor { get => barColor; set => barColor = value; }
        [Description("Bordercolor of the Progress Bar"), Category("Appearance")]
        public Color BorderColor { get => borderColor; set => borderColor = value; }

        public bool BarFull { get => barFull; }
        [Description("Bordercolor of the Progress Bar"), Category("Appearance")]
        public int MaxSteps { get => maxSteps; set => maxSteps = value; }
        [Description("Bordercolor of the Progress Bar"), Category("Appearance")]
        public int CurrStep { get => currStep; set => currStep = value; }
        [Description("Bordercolor of the Progress Bar"), Category("Appearance")]
        public int Step { get => step; set => step = value; }
        [Description("Bordercolor of the Progress Bar"), Category("Appearance")]
        public ProgressType MovementType { get => movementType; set => movementType = value; }
        public int SmoothDelay { get => smoothDelay; set => smoothDelay = value; }

        public enum ProgressType
        {
            Normal,
            Smooth
        }

        ProgressType movementType = ProgressType.Normal;
        int smoothDelay = 1;

        // Methoden

        public NewProgressBar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            max = Width;
            step_single = max / maxSteps;
            curr = currStep * step_single;
        }

        private void NewProgressBar_Resize(object sender, EventArgs e)
        {

        }

        private void NewProgressBar_SizeChanged(object sender, EventArgs e)
        {

        }

        int max = Width;
        int step_single = max / maxSteps;
        int curr = currStep * step_single;

        private async void NewProgressBar_Paint(object sender, PaintEventArgs e)
        {
            switch (movementType)
            {
                case ProgressType.Normal:
                    e.Graphics.FillRectangle(new SolidBrush(backColor), this.ClientRectangle);
                    ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
                    e.Graphics.FillRectangle(new SolidBrush(BarColor), new RectangleF(3, 3, (((this.Width - 6) / MaxSteps) * CurrStep), this.Height - 6));
                    break;
                case ProgressType.Smooth:

                    e.Graphics.FillRectangle(new SolidBrush(backColor), ClientRectangle);
                    while (curr <= curr + step_single)
                    {
                        curr += 1;
                        e.Graphics.FillRectangle(new SolidBrush(BarColor), new Rectangle(3, 3, curr -6, Height - 6));
                        await Task.Delay(smoothDelay);
                    }
                    currStep++;
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, borderColor, ButtonBorderStyle.Solid);
                    break;
            }
        }

        public void doStep()
        {
            if (CurrStep < MaxSteps)
            {
                CurrStep += Step;
            }
            else
            {
                barFull = true;
            }
            this.Invalidate();
        }

        public void doStep(int steps)
        {
            if ((CurrStep + Step) < MaxSteps)
            {
                CurrStep += steps;
            }
            else
            {
                barFull = true;
            }
            this.Invalidate();
        }

        public void reset()
        {
            CurrStep = 0;
            barFull = false;
        }
    }
}
