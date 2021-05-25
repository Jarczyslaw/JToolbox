
namespace Examples.Desktop.MVP.Forms
{
    partial class MainForm
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
            this.btnPass = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnPassModal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(12, 38);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(156, 22);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Pass value";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(12, 12);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(156, 20);
            this.tbValue.TabIndex = 1;
            // 
            // btnPassModal
            // 
            this.btnPassModal.Location = new System.Drawing.Point(12, 66);
            this.btnPassModal.Name = "btnPassModal";
            this.btnPassModal.Size = new System.Drawing.Size(156, 22);
            this.btnPassModal.TabIndex = 2;
            this.btnPassModal.Text = "Pass value (modal)";
            this.btnPassModal.UseVisualStyleBackColor = true;
            this.btnPassModal.Click += new System.EventHandler(this.btnPassModal_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 115);
            this.Controls.Add(this.btnPassModal);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.btnPass);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnPassModal;
    }
}