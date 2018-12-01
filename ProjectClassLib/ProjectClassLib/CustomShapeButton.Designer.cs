namespace ProjectClassLib
{
    partial class CustomShapeButton
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
            this.SuspendLayout();
            // 
            // CustomShapeButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CustomShapeButton";
            this.Size = new System.Drawing.Size(111, 28);
            this.Load += new System.EventHandler(this.CustomShapeButton_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CustomShapeButton_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CustomShapeButton_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomShapeButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.CustomShapeButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CustomShapeButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomShapeButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
