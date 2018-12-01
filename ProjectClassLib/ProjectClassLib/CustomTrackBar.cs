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
    public class ValueChangedEventArgs : EventArgs
    {
        public int Value;

        public ValueChangedEventArgs(int value)
        {
            Value = value;
        }
    }

    public partial class CustomTrackBar : UserControl
    {
        public CustomTrackBar()
        {
            InitializeComponent();
            pb_main.MouseWheel += Pb_main_MouseWheel;
        }

        public delegate void TrackBarValueChanged(object sender, ValueChangedEventArgs args);
        public event TrackBarValueChanged ValueChanged;

        protected void SendValueChangedEvent(int value)
        {
            ValueChangedEventArgs args = new ValueChangedEventArgs(value);
            if (ValueChanged != null)
            {
                ValueChanged(this, args);
            }
        }

        Color backColor;
        public Color BackColor { get => backColor; set
            {
                backColor = value;
                pb_main.BackColor = value;
            }
        }


        Color trackColor = Color.DimGray;
        public Color TrackColor { get => trackColor; set { trackColor = value;
                this.Invalidate();
            }
        }
        Color barColor = Color.Orange;
        public Color BarColor { get => barColor; set { barColor = value;
                this.Invalidate();
            }
        }

        int barHeight = 10;
        int scrollSpeed = 1;
        int minValue = 0;
        int maxValue = 100;
        int currentStep = 25;
        public int CurrentStep { get => currentStep; set {
                currentStep = value;
                SendValueChangedEvent(CurrentStep);
                this.Invalidate();
            }
        }


        Timer t = new Timer() { Interval = 10 };


        private void pb_main_Paint(object sender, PaintEventArgs e)
        {
            int offset = 12;
            float heigth = ((float)pb_main.Height - offset) * ((float)CurrentStep / maxValue);
            e.Graphics.DrawLine(new Pen(TrackColor, 5), pb_main.Width / 2, 5, pb_main.Width / 2, pb_main.Height - 5);
            e.Graphics.FillRectangle(new SolidBrush(BarColor), 0, ((float)pb_main.Height - offset) - heigth, pb_main.Width, barHeight);
        }

        private void Pb_main_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (CurrentStep + scrollSpeed <= maxValue) CurrentStep += scrollSpeed;
                SendValueChangedEvent(CurrentStep);
                pb_main.Invalidate();
            }
            else
            {
                if (CurrentStep - scrollSpeed >= minValue) CurrentStep -= scrollSpeed;
                SendValueChangedEvent(CurrentStep);
                pb_main.Invalidate();
            }
        }

        private void pb_main_MouseDown(object sender, MouseEventArgs e)
        {
            t.Tick += T_Tick;
            t.Start();
        }

        private void pb_main_MouseUp(object sender, MouseEventArgs e)
        {
            t.Stop();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Rectangle bounds = pb_main.Bounds;
            bounds.Height = bounds.Height - 10;
            bounds.Y = bounds.Y + 5;

            if (bounds.Contains(PointToClient(MousePosition)))
            {
                float y = PointToClient(MousePosition).Y + barHeight / 3;

                float perc = ((y / bounds.Height) * maxValue) - 10;

                if (perc < minValue)
                {
                    perc = minValue;
                }
                else if (perc > maxValue)
                {
                    perc = maxValue;
                }
                perc = maxValue - perc;
                CurrentStep = (int)perc;
                SendValueChangedEvent(CurrentStep);
                pb_main.Invalidate();
            }
        }
    }
}
