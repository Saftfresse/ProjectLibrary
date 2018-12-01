using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ProjectClassLib
{
    public partial class CustomShapeButton : UserControl
    {
        public CustomShapeButton()
        {
            InitializeComponent();
        }

        public enum textPosition
        {
            TopCenter,
            MiddleCenter,
            BottomCenter
        }

        textPosition thisTextPosition;

        int[] points = new int[8];
        Color hoverColor = Color.LightGray;
        Color activeColor = Color.LightSlateGray;
        private Color currentColor = Color.Transparent;

        public string ButtonText { get => this.Text; set { this.Text = value; this.Invalidate(); } }
        public int[] Points { get => points; set { points = value; this.Invalidate(); } }
        public Color HoverColor { get => hoverColor; set { hoverColor = value; this.Invalidate(); } }
        public Color ActiveColor { get => activeColor; set { activeColor = value; this.Invalidate(); } }
        public textPosition ThisTextPosition { get => thisTextPosition; set { thisTextPosition = value; this.Invalidate(); } }

        private void CustomShapeButton_Load(object sender, EventArgs e)
        {
            currentColor = BackColor;
            GraphicsPath gp = new GraphicsPath();
            PointF[] points = new PointF[] {
                new PointF(Points[0], Points[1]),
                new PointF(Points[2], Points[3]),
                new PointF(Points[4], Points[5]),
                new PointF(Points[6], Points[7])};
            gp.AddPolygon(points);
            this.Region = new Region(gp);
        }

        private void CustomShapeButton_Paint(object sender, PaintEventArgs e)
        {
            int n = 0;
            for (int i = 0; i < Points.Length; i++) { n++; }
            PointF[] points = new PointF[8];
            if (n <= 0)
            {
                points = new PointF[] {
                new PointF(0, 0),
                new PointF(Width, 0),
                new PointF(0, Height),
                new PointF(Width, Height)};
            }
            else
            {
                points = new PointF[] {
                new PointF(Points[0], Points[1]),
                new PointF(Points[2], Points[3]),
                new PointF(Points[4], Points[5]),
                new PointF(Points[6], Points[7])};
            }
            
            e.Graphics.FillPolygon(new SolidBrush(currentColor), points);
            SizeF width = e.Graphics.MeasureString(Text, Font);
            switch (ThisTextPosition)
            {
                case textPosition.TopCenter:
                    e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(Width / 2 - width.Width / 2, 2));
                    break;
                case textPosition.MiddleCenter:
                    e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(Width / 2 - width.Width / 2, Height / 2 - width.Height / 2));
                    break;
                case textPosition.BottomCenter:
                    e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(Width / 2 - width.Width / 2, Height - width.Height - 2));
                    break;
            }
        }

        private void CustomShapeButton_MouseEnter(object sender, EventArgs e)
        {
            currentColor = hoverColor;
            Invalidate();
        }

        private void CustomShapeButton_MouseLeave(object sender, EventArgs e)
        {
            currentColor = BackColor;
            Invalidate();
        }

        private void CustomShapeButton_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void CustomShapeButton_MouseDown(object sender, MouseEventArgs e)
        {
            currentColor = activeColor;
            Invalidate();
        }

        private void CustomShapeButton_MouseUp(object sender, MouseEventArgs e)
        {
            currentColor = hoverColor;
            Invalidate();
        }
    }
}
