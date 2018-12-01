using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControls
{
    public partial class UserProgressBar : UserControl
    {
        // Variablen

        int maxsteps = 10;
        int currentStep = 0;
        int stepsPerStep = 1;

        int buffer = 2;

        int barWidth = 100;
        RectangleF bar = new RectangleF(2, 2, 10, 10);

        Color borderColor = Color.DimGray;
        Color barColor = Color.DarkBlue;

        bool barFilled = false;
        bool barMoving = false;
        bool bounceReset = false;

        int frameSkip = 4;

        bool running = true;

        public enum DisplayMode
        {
            Normal,
            Bounce,
            Marquee
        }

        DisplayMode mode = DisplayMode.Normal;

        // Methoden

        public UserProgressBar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            bar = new RectangleF(2, 2, BarWidth, Height - 4);
        }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Upper bound of steps")]
        public int Maxsteps { get => maxsteps; set { maxsteps = value > 0?value:1; Invalidate(); } }
        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Current position on the bar")]
        public int CurrentStep { get => currentStep; set { currentStep = value; Invalidate(); } }
        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Steps to jump per step")]
        public int StepsPerStep { get => stepsPerStep; set { stepsPerStep = value; Invalidate(); } }
        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Return true if the Bar is complete")]
        public bool BarFilled
        {
            get => barFilled;
        }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Set display mode for the Progress Bar")]
        public DisplayMode Mode
        {
            get => mode; set
            {
                mode = value;
                Invalidate();
                bar.X = 2;
                bounceReset = true;
                doBounce();
            }
        }
        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("Width of the bounce-bar")]
        public int BarWidth { get => barWidth; set { barWidth = value; Invalidate(); } }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("The Cclor of the bar")]
        public Color BarColor { get => barColor; set { barColor = value; Invalidate(); } }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("The color of the border")]
        public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("The buffer between border and bar")]
        public int Buffer { get => buffer; set { buffer = value; Invalidate(); } }

        [PropertyTab("Data")]
        [Browsable(true)]
        [Category("Extended Properties")]
        [Description("The speed of the bar")]
        public int FrameSkip
        {
            get => frameSkip; set
            {
                if (value > 0) frameSkip = value;
            }
        }

        float sc = 0;

        private void UserProgressBar_Paint(object sender, PaintEventArgs e)
        {
            switch (mode)
            {
                case DisplayMode.Normal:
                    float pixelCurr = ((float)Width / (float)maxsteps) * (float)currentStep - ((float)Width / (float)maxsteps) * 1.0f;
                    float pixelCurrFull = ((float)Width / (float)maxsteps) * (float)currentStep;
                    e.Graphics.FillRectangle(new SolidBrush(BarColor), new RectangleF(buffer, buffer, pixelCurrFull - (sc) - buffer, Height - buffer * 2));
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
                    break;
                case DisplayMode.Bounce:
                    e.Graphics.FillRectangle(new SolidBrush(BarColor), bar);
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
                    break;
                case DisplayMode.Marquee:
                    break;
            }
        }

        async void doBounce()
        {
            bar = new RectangleF(2, 2, barWidth, Height - 4);
            bool forward = true;
            while (running)
            {
                if (bounceReset)
                {
                    running = false;
                    bounceReset = false;
                    break;
                }

                if (forward)
                {
                    bar = new RectangleF(bar.X + frameSkip, bar.Y, barWidth, bar.Height);
                }
                else if (!forward)
                {
                    forward = false;
                    bar = new RectangleF(bar.X - frameSkip, bar.Y, barWidth, bar.Height);
                }
                if (bar.X + bar.Width >= Width - 4 && forward) forward = false;
                else if (bar.X <= 2 && !forward) forward = true;
                this.Invalidate();
                await Task.Delay(1);
            }
            running = true;
        }

        public void Reset()
        {
            currentStep = 0;
            barFilled = false;
            this.Invalidate();
        }

        public async void smoothSteps()
        {
            barMoving = true;
            for (float i = ((Width / (float)maxsteps) * 1) / frameSkip; i > 0; i--)
            {
                sc = i * frameSkip;
                Invalidate();
                await Task.Delay(1);
            }
            barMoving = false;
        }

        public void doStep()
        {
            if (!barMoving)
            {
                if (currentStep < maxsteps - 1)
                {
                    smoothSteps();
                    currentStep += stepsPerStep;
                    barFilled = false;
                }
                else if (currentStep == maxsteps - 1)
                {
                    smoothSteps();
                    currentStep += stepsPerStep;
                    barFilled = true;
                }
                else
                {
                    barFilled = true;
                }
            }
            Invalidate();
        }

        private void UserProgressBar_Resize(object sender, EventArgs e)
        {
            bar.Height = this.Height - 4;
            this.Invalidate();
        }

        private void UserProgressBar_SizeChanged(object sender, EventArgs e)
        {
            bar.Height = this.Height - 4;
            this.Invalidate();
        }

        private void UserProgressBar_Load(object sender, EventArgs e)
        {
            if (mode == DisplayMode.Bounce) doBounce();
        }

        private void UserProgressBar_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
