using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectClassLib
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
            style = NotificationStyle.Light;
        }

        public Notification(NotificationStyle _style, NotificationLocation _loc, string title, string text, NotificationImage _img)
        {
            InitializeComponent();
            style = _style;
            loc = _loc;
            img = _img;
            lbl_text.Text = text;
            lbl_title.Text = title;
        }

        public Notification(NotificationStyle _style, NotificationLocation _loc, string title, string text, NotificationImage _img, int timeToLive, int steps, double toBeAdded, int delay)
        {
            InitializeComponent();
            style = _style;
            loc = _loc;
            img = _img;
            lbl_text.Text = text;
            lbl_title.Text = title;
            timeOut(timeToLive, steps, toBeAdded, delay);
        }

        int padding = 30;
        NotificationImage img = NotificationImage.Info;
        NotificationLocation loc = NotificationLocation.BottomCenter;
        NotificationStyle style = NotificationStyle.Light;

        public enum NotificationLocation
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleCenter,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        public enum NotificationStyle
        {
            Dark,
            Light,
            Colorful
        }
        public enum NotificationImage
        {
            Info,
            Good,
            Warning
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;           
            switch (style)
            {
                case NotificationStyle.Dark:
                    BackColor = Color.FromArgb(64,64,64);
                    ForeColor = Color.WhiteSmoke;
                    lbl_title.BackColor = Color.FromArgb(40,40,40);
                    btn_close.BackgroundImage = Properties.Resources.closeButton;
                    break;
                case NotificationStyle.Light:
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                    lbl_title.BackColor = Color.FromArgb(245,245,245);
                    btn_close.BackgroundImage = Properties.Resources.closeButton_light;
                    break;
                case NotificationStyle.Colorful:
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                    lbl_title.ForeColor = Color.Black;
                    lbl_title.BackColor = Color.Orange;
                    btn_close.BackgroundImage = Properties.Resources.closeButton_color;
                    break;
            }
            switch (img)
            {
                case NotificationImage.Info:
                    picture.BackgroundImage = Properties.Resources.info;
                    break;
                case NotificationImage.Good:
                    picture.BackgroundImage = Properties.Resources.good_icon;
                    break;
                case NotificationImage.Warning:
                    picture.BackgroundImage = Properties.Resources.warning;
                    break;
            }
            Location = new Point(100,100);
            TopMost = true;
        }

        private void Notification_Paint(object sender, PaintEventArgs e)
        {
            switch (style)
            {
                case NotificationStyle.Dark:
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
                    break;
                case NotificationStyle.Light:
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
                    break;
                case NotificationStyle.Colorful:
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Orange, ButtonBorderStyle.Solid);
                    break;
            }
        }

        private void Notification_Shown(object sender, EventArgs e)
        {
            int scrX = Screen.PrimaryScreen.WorkingArea.X;
            int scrY = Screen.PrimaryScreen.WorkingArea.Y;
            int scrHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int scrWidth = Screen.PrimaryScreen.WorkingArea.Width;
            
            switch (loc)
            {
                case NotificationLocation.TopLeft:
                    Location = new Point(scrX + padding, scrY + padding);
                    break;
                case NotificationLocation.TopCenter:
                    Location = new Point(scrWidth / 2 - Width / 2, scrY + padding);
                    break;
                case NotificationLocation.TopRight:
                    Location = new Point(scrX + scrWidth - Width - padding, scrY + padding);
                    break;
                case NotificationLocation.MiddleCenter:
                    Location = new Point(scrWidth / 2 - Width / 2, (scrY + scrHeight) / 2 - Height);
                    break;
                case NotificationLocation.BottomLeft:
                    Location = new Point(scrX + padding, scrY + scrHeight - Height - padding);
                    break;
                case NotificationLocation.BottomCenter:
                    Location = new Point(scrWidth / 2 - Width / 2, scrY + scrHeight - Height - padding);
                    break;
                case NotificationLocation.BottomRight:
                    Location = new Point(scrX + scrWidth - Width - padding, scrY + scrHeight - Height - padding);
                    break;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        async void fadeOut(int steps, double adder, int delay)
        {
            for (int i = 0; i < steps; i++)
            {
                Opacity -= adder;
                await Task.Delay(delay);
            }
            this.Close();
        }

        private async void timeOut(int timeShown, int steps, double toBeAdded, int delay)
        {
            await Task.Delay(timeShown);

            //for (int i = 0; i < fadeOutTime; i++)
            //{
            //    string dbl = "0,0" + fadeOutTime.ToString();
            //    double d = double.Parse(dbl);
            //    Opacity -= double.Parse(dbl);
            //    await Task.Delay(1);
            //}
            fadeOut(steps, toBeAdded, delay);
        }

        private async void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine(Opacity);
            if (Opacity == 1)
            {
                e.Cancel = true;
                for (int i = 0; i < 20; i++)
                {
                    Opacity -= 0.05;
                    await Task.Delay(1);
                }
                e.Cancel = false;
            }

        }
    }
}
