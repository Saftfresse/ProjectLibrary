namespace ProjectClassLib
{
    partial class CustomTrackBar
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_main = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_main)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_main
            // 
            this.pb_main.BackColor = System.Drawing.Color.Transparent;
            this.pb_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_main.Location = new System.Drawing.Point(0, 0);
            this.pb_main.Name = "pb_main";
            this.pb_main.Size = new System.Drawing.Size(62, 74);
            this.pb_main.TabIndex = 0;
            this.pb_main.TabStop = false;
            this.pb_main.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_main_Paint);
            this.pb_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_main_MouseDown);
            this.pb_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_main_MouseUp);
            // 
            // CustomTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_main);
            this.Name = "CustomTrackBar";
            this.Size = new System.Drawing.Size(62, 74);
            ((System.ComponentModel.ISupportInitialize)(this.pb_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_main;
    }
}
