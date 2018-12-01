namespace ProjectClassLib
{
    partial class ModalBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.time1 = new System.Windows.Forms.MaskedTextBox();
            this.panel_text = new System.Windows.Forms.Panel();
            this.panel_time = new System.Windows.Forms.Panel();
            this.time2 = new System.Windows.Forms.MaskedTextBox();
            this.tb = new ProjectClassLib.WatermarkTextBox();
            this.panel_text.SuspendLayout();
            this.panel_time.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(7, 7);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(392, 42);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "label1\r\nl\r\n";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(124, 84);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(205, 84);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // time1
            // 
            this.time1.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time1.Location = new System.Drawing.Point(114, 0);
            this.time1.Margin = new System.Windows.Forms.Padding(4);
            this.time1.Mask = "90:00";
            this.time1.Name = "time1";
            this.time1.PromptChar = '#';
            this.time1.Size = new System.Drawing.Size(43, 27);
            this.time1.TabIndex = 4;
            this.time1.ValidatingType = typeof(System.DateTime);
            // 
            // panel_text
            // 
            this.panel_text.AutoSize = true;
            this.panel_text.Controls.Add(this.tb);
            this.panel_text.Location = new System.Drawing.Point(10, 52);
            this.panel_text.Name = "panel_text";
            this.panel_text.Size = new System.Drawing.Size(386, 26);
            this.panel_text.TabIndex = 5;
            // 
            // panel_time
            // 
            this.panel_time.Controls.Add(this.time2);
            this.panel_time.Controls.Add(this.time1);
            this.panel_time.Location = new System.Drawing.Point(10, 52);
            this.panel_time.Name = "panel_time";
            this.panel_time.Size = new System.Drawing.Size(386, 29);
            this.panel_time.TabIndex = 6;
            // 
            // time2
            // 
            this.time2.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time2.Location = new System.Drawing.Point(227, 0);
            this.time2.Margin = new System.Windows.Forms.Padding(4);
            this.time2.Mask = "90:00";
            this.time2.Name = "time2";
            this.time2.PromptChar = '#';
            this.time2.Size = new System.Drawing.Size(43, 27);
            this.time2.TabIndex = 5;
            this.time2.ValidatingType = typeof(System.DateTime);
            // 
            // tb
            // 
            this.tb.BackColor = System.Drawing.Color.White;
            this.tb.BorderColor = System.Drawing.Color.Black;
            this.tb.CustomBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb.Location = new System.Drawing.Point(0, 0);
            this.tb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(386, 25);
            this.tb.TabIndex = 0;
            this.tb.Watermark = "Text";
            this.tb.WatermarkColor = System.Drawing.Color.Gray;
            this.tb.WatermarkStyle = System.Drawing.FontStyle.Italic;
            // 
            // ModalBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(406, 112);
            this.Controls.Add(this.panel_time);
            this.Controls.Add(this.panel_text);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ModalBox";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalBox";
            this.Load += new System.EventHandler(this.ModalBox_Load);
            this.panel_text.ResumeLayout(false);
            this.panel_time.ResumeLayout(false);
            this.panel_time.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WatermarkTextBox tb;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.MaskedTextBox time1;
        private System.Windows.Forms.Panel panel_text;
        private System.Windows.Forms.Panel panel_time;
        private System.Windows.Forms.MaskedTextBox time2;
    }
}