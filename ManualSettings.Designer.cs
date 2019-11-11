namespace ManualPlugin
{
    partial class ManualSettings
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
            this.ManualDropBox = new System.Windows.Forms.ComboBox();
            this.ManualLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ManualDropBox
            // 
            this.ManualDropBox.FormattingEnabled = true;
            this.ManualDropBox.Location = new System.Drawing.Point(83, 10);
            this.ManualDropBox.Name = "ManualDropBox";
            this.ManualDropBox.Size = new System.Drawing.Size(182, 21);
            this.ManualDropBox.TabIndex = 0;
            this.ManualDropBox.SelectedIndexChanged += new System.EventHandler(this.ManualDropBox_SelectedIndexChanged);
            this.ManualDropBox.Click += new System.EventHandler(this.ManualDropBox_Click);
            // 
            // ManualLabel
            // 
            this.ManualLabel.AutoSize = true;
            this.ManualLabel.Location = new System.Drawing.Point(12, 13);
            this.ManualLabel.Name = "ManualLabel";
            this.ManualLabel.Size = new System.Drawing.Size(64, 13);
            this.ManualLabel.TabIndex = 1;
            this.ManualLabel.Text = "选择指南";
            // 
            // ManualSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 36);
            this.Controls.Add(this.ManualLabel);
            this.Controls.Add(this.ManualDropBox);
            this.Name = "ManualSettings";
            this.Text = "指南设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ManualDropBox;
        private System.Windows.Forms.Label ManualLabel;
    }
}