using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectClassLib
{
    public partial class ModalBox : Form
    {
        public ModalBox(string caption, string text, ModalType type)
        {
            InitializeComponent();
            lbl.Text = text;
            this.Text = caption;
            this.CancelButton = btn_cancel;
            this.AcceptButton = btn_ok;
            mType = type;
            switch (type)
            {
                case ModalType.TimeRange:
                    panel_time.Show();
                    panel_text.Hide();
                    break;
                case ModalType.Text:
                    panel_text.Show();
                    panel_time.Hide();
                    break;
            }
        }

        public enum ModalType
        {
            TimeRange,
            Text
        };

        private ModalType mType;
        public string Result;
        public DateTime Time_1;
        public DateTime Time_2;

        private void ModalBox_Load(object sender, EventArgs e)
        {

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            switch (mType)
            {
                case ModalType.TimeRange:
                    Regex r = new Regex("[0-9]{2}:[0-9]{2}");
                    if (r.IsMatch(time1.Text) && r.IsMatch(time2.Text))
                    {
                        string[] t1 = time1.Text.Split(':');
                        Time_1 = Time_1.AddHours(int.Parse(t1[0]));
                        Time_1 = Time_1.AddMinutes(int.Parse(t1[1]));

                        string[] t2 = time2.Text.Split(':');
                        Time_2 = Time_2.AddHours(int.Parse(t2[0]));
                        Time_2 = Time_2.AddMinutes(int.Parse(t2[1]));

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Geben sie die korrekten Zeiten ein!");
                    }
                    break;
                case ModalType.Text:
                    if (tb.Text.Length > 0) { Result = tb.Text; DialogResult = DialogResult.OK; }
                    else MessageBox.Show("Geben sie Text ein!");
                    break;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
