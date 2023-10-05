namespace JToolbox.WinForms.Core.Controls
{
    partial class NullableDateTimeSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chb = new System.Windows.Forms.CheckBox();
            this.dts = new JToolbox.WinForms.Core.Controls.DateTimeSelector();
            this.SuspendLayout();
            // 
            // chb
            // 
            this.chb.AutoSize = true;
            this.chb.Dock = System.Windows.Forms.DockStyle.Left;
            this.chb.Location = new System.Drawing.Point(0, 0);
            this.chb.Name = "chb";
            this.chb.Size = new System.Drawing.Size(15, 20);
            this.chb.TabIndex = 1;
            this.chb.UseVisualStyleBackColor = true;
            // 
            // dts
            // 
            this.dts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dts.Location = new System.Drawing.Point(15, 0);
            this.dts.Name = "dts";
            this.dts.Size = new System.Drawing.Size(135, 20);
            this.dts.TabIndex = 2;
            // 
            // NullableDateTimeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dts);
            this.Controls.Add(this.chb);
            this.Name = "NullableDateTimeSelector";
            this.Size = new System.Drawing.Size(150, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chb;
        private DateTimeSelector dts;
    }
}
