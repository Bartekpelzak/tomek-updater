
namespace tomek_updater
{
    partial class Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.downloadStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // downloadProgress
            // 
            this.downloadProgress.Location = new System.Drawing.Point(12, 39);
            this.downloadProgress.Name = "downloadProgress";
            this.downloadProgress.Size = new System.Drawing.Size(445, 15);
            this.downloadProgress.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(382, 63);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(9, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(133, 16);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Updating tomek.exe...";
            // 
            // downloadStatusLabel
            // 
            this.downloadStatusLabel.AutoSize = true;
            this.downloadStatusLabel.Location = new System.Drawing.Point(9, 66);
            this.downloadStatusLabel.Name = "downloadStatusLabel";
            this.downloadStatusLabel.Size = new System.Drawing.Size(0, 16);
            this.downloadStatusLabel.TabIndex = 0;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(469, 96);
            this.ControlBox = false;
            this.Controls.Add(this.downloadStatusLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.downloadProgress);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updating tomek.exe";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Updater_FormClosing);
            this.Load += new System.EventHandler(this.Updater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar downloadProgress;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label downloadStatusLabel;
    }
}

