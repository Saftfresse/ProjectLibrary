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
    public partial class BetterTabControl : UserControl
    {
        public class TabPage : Control
        {
            public string text;
            public Rectangle bounds;
            private Panel p;
            private Button b;
            public Color backColor = Color.White;
            int tabWidth = 100;
            int index;
            bool selected = false;

            public static int maxIndex = 0;

            public int TabWidth
            {
                get => tabWidth;
                set
                {
                    tabWidth = value;
                }
            }

            public int Index { get => index; set => index = value; }
            public bool Selected { get => selected; set => selected = value; }
            public Panel Workspace { get => p; set => p = value; }
            public Button TabButton { get => b; set => b = value; }

            public TabPage(int index)
            {
                bounds.X = index * tabWidth;
                bounds.Y = 0;
                bounds.Height = 30;
                bounds.Width = tabWidth;
                text = "TabPage " + index;
                this.index = index;
                maxIndex++;
            }

            public TabPage()
            {
                bounds.X = index * tabWidth;
                bounds.Y = 0;
                bounds.Height = 30;
                bounds.Width = tabWidth;
                index = maxIndex;
                text = "TabPage " + index;
                //p = new Panel()
                //{
                //    Size = new Size(Parent.Width - 6, Parent.Height - 40),
                //    Location = new Point(5, 40),
                //    Parent = this.Parent,
                //    BorderStyle = BorderStyle.FixedSingle
                //};
                maxIndex++;
            }
        }

        int tabWidth = 100;
        int padding = 5;
        int selectedIndex = 0;
        Color activeColor = Color.Green;
        Color hoverColor = Color.LightGreen;

        public enum ButtonAlignment
        {
            Top,
            Left
        }

        ButtonAlignment currentAlignment = ButtonAlignment.Top;

        public BetterTabControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        List<TabPage> tabPages = new List<TabPage>();

        public int TabWidth
        {
            get => tabWidth;
            set {
                tabWidth = value;
                changeWidth(tabWidth);
            }
        }

        [Category("Data")]
        [Description("asdf")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<TabPage> TabPages
        {
            get => tabPages;
            set
            {
                tabPages = value;
                foreach (var item in tabPages)
                {
                    item.Parent = this;
                    refreshLabels();
                }
                Invalidate();
            }
        }

        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }
        public Color ActiveColor { get => activeColor; set => activeColor = value; }
        public Color HoverColor { get => hoverColor; set => hoverColor = value; }

        void addPage()
        {
            TabPage p = new TabPage();
            TabPages.Add(p);
            this.Invalidate();
        }

        void refreshLabels()
        {
            foreach (var item in tabPages)
            {
                item.Parent = this;
                if (item.Workspace == null)
                {
                    item.Workspace = new Panel()
                    {
                        Size = new Size(Width - padding * 2, Height - 40),
                        Location = new Point(padding, padding + 35),
                        Parent = this,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    item.Workspace.Controls.Add(new Label() { Text = item.text, Parent = item.Workspace, Location = new Point(5,5), Size = new Size(100,100), BorderStyle = BorderStyle.FixedSingle });
                    item.TabButton = new Button()
                    {
                        Bounds = item.bounds,
                        Parent = item.Parent
                    };
                }
            }
        }

        void changeWidth(int newWidth)
        {
            //foreach (var item in TabPages)
            //{
            //    item.bounds.Width = newWidth;
            //    item.bounds.X = item.Index * item.TabWidth;
            //}
            this.Invalidate();
        }

        private void BetterTabControl_Paint(object sender, PaintEventArgs e)
        {
            switch (currentAlignment)
            {
                case ButtonAlignment.Left:
                    foreach (var item in TabPages)
                    {
                        int padding = 3;
                        Rectangle r = item.bounds;
                        r.Offset(padding, padding);
                        e.Graphics.DrawRectangle(new Pen(Color.Red), r);
                    }
                    break;
                case ButtonAlignment.Top:
                    int curX = 0;
                    foreach (var item in TabPages)
                    {
                        item.bounds.X = curX;
                        Rectangle r = item.bounds;
                        r.Offset(padding,padding);
                        item.TabButton.Bounds = r;
                        if (item.Selected)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(activeColor), r);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(item.backColor), r);
                        }
                        e.Graphics.DrawRectangle(new Pen(Color.Red), r);
                        e.Graphics.DrawString(item.text, Font, new SolidBrush(Color.Black), new Point(r.X + padding + 2, padding + (r.Height / 2) - ((int)Font.Size / 2)));
                        Console.WriteLine(item.Index);
                        curX += item.TabWidth;
                    }
                    break;
            }
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void BetterTabControl_Resize(object sender, EventArgs e)
        {

        }

        private void BetterTabControl_SizeChanged(object sender, EventArgs e)
        {

        }
        
        private void BetterTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in TabPages)
            {
                Rectangle r = item.bounds;
                r.Offset(padding, padding);
                if (r.Contains(e.Location))
                {
                    item.backColor = hoverColor;
                }
                else
                {
                    item.backColor = Color.White;
                }
            }
            this.Invalidate();
        }

        private void BetterTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            int selected = -1;
            foreach (var item in TabPages)
            {
                Rectangle r = item.bounds;
                r.Offset(padding, padding);
                if (r.Contains(e.Location))
                {
                    selected = tabPages.IndexOf(item);
                    item.backColor = Color.Blue;
                    SelectedIndex = tabPages.IndexOf(item);
                    item.Selected = true;
                    item.Workspace.Show();
                }
                else
                {
                    item.Workspace.Hide();
                    item.backColor = Color.White;
                }
            }
            if (selected >= 0)
            {
                foreach (var item in tabPages)
                {
                    if (tabPages.IndexOf(item) != selected)
                    {
                        item.Selected = false;
                    }
                }
            }
            this.Invalidate();
        }

        private void BetterTabControl_LocationChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void BetterTabControl_Load(object sender, EventArgs e)
        {
            refreshLabels();
        }
    }
}
