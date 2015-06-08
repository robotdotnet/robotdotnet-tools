namespace NetFRC
{
    partial class Main
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
            this.connectionStatus = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.teamNumber = new System.Windows.Forms.TextBox();
            this.halLabel = new System.Windows.Forms.Label();
            this.halDownloadedVersionLabel = new System.Windows.Forms.Label();
            this.halRecommendedVersionLabel = new System.Windows.Forms.Label();
            this.halCurrentLabel = new System.Windows.Forms.Label();
            this.halDownloadButton = new System.Windows.Forms.Button();
            this.halInstallButton = new System.Windows.Forms.Button();
            this.getRecommendVersionsButton = new System.Windows.Forms.Button();
            this.monoLabel = new System.Windows.Forms.Label();
            this.monoDownloadedLabel = new System.Windows.Forms.Label();
            this.monoRecommendedLabel = new System.Windows.Forms.Label();
            this.monoInstalledLabed = new System.Windows.Forms.Label();
            this.monoDLButton = new System.Windows.Forms.Button();
            this.monoInstallButton = new System.Windows.Forms.Button();
            this.halProgress = new System.Windows.Forms.ProgressBar();
            this.monoProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = true;
            this.connectionStatus.Location = new System.Drawing.Point(309, 12);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(100, 13);
            this.connectionStatus.TabIndex = 6;
            this.connectionStatus.Text = "Connection Status: ";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(166, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(105, 43);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect to Robot";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // teamNumber
            // 
            this.teamNumber.Location = new System.Drawing.Point(16, 12);
            this.teamNumber.Name = "teamNumber";
            this.teamNumber.Size = new System.Drawing.Size(133, 20);
            this.teamNumber.TabIndex = 4;
            // 
            // halLabel
            // 
            this.halLabel.AutoSize = true;
            this.halLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.halLabel.Location = new System.Drawing.Point(21, 74);
            this.halLabel.Name = "halLabel";
            this.halLabel.Size = new System.Drawing.Size(41, 20);
            this.halLabel.TabIndex = 7;
            this.halLabel.Text = "HAL";
            // 
            // halDownloadedVersionLabel
            // 
            this.halDownloadedVersionLabel.AutoSize = true;
            this.halDownloadedVersionLabel.Location = new System.Drawing.Point(25, 98);
            this.halDownloadedVersionLabel.Name = "halDownloadedVersionLabel";
            this.halDownloadedVersionLabel.Size = new System.Drawing.Size(111, 13);
            this.halDownloadedVersionLabel.TabIndex = 8;
            this.halDownloadedVersionLabel.Text = "Downloaded Version: ";
            // 
            // halRecommendedVersionLabel
            // 
            this.halRecommendedVersionLabel.AutoSize = true;
            this.halRecommendedVersionLabel.Location = new System.Drawing.Point(25, 111);
            this.halRecommendedVersionLabel.Name = "halRecommendedVersionLabel";
            this.halRecommendedVersionLabel.Size = new System.Drawing.Size(123, 13);
            this.halRecommendedVersionLabel.TabIndex = 8;
            this.halRecommendedVersionLabel.Text = "Recommended Version: ";
            // 
            // halCurrentLabel
            // 
            this.halCurrentLabel.AutoSize = true;
            this.halCurrentLabel.Location = new System.Drawing.Point(25, 124);
            this.halCurrentLabel.Name = "halCurrentLabel";
            this.halCurrentLabel.Size = new System.Drawing.Size(134, 13);
            this.halCurrentLabel.TabIndex = 8;
            this.halCurrentLabel.Text = "Currently Installed Version: ";
            // 
            // halDownloadButton
            // 
            this.halDownloadButton.Enabled = false;
            this.halDownloadButton.Location = new System.Drawing.Point(25, 152);
            this.halDownloadButton.Name = "halDownloadButton";
            this.halDownloadButton.Size = new System.Drawing.Size(134, 40);
            this.halDownloadButton.TabIndex = 9;
            this.halDownloadButton.Text = "Download Recommended Version";
            this.halDownloadButton.UseVisualStyleBackColor = true;
            this.halDownloadButton.Click += new System.EventHandler(this.halDownloadButton_Click);
            // 
            // halInstallButton
            // 
            this.halInstallButton.Enabled = false;
            this.halInstallButton.Location = new System.Drawing.Point(28, 198);
            this.halInstallButton.Name = "halInstallButton";
            this.halInstallButton.Size = new System.Drawing.Size(134, 40);
            this.halInstallButton.TabIndex = 9;
            this.halInstallButton.Text = "Install HAL";
            this.halInstallButton.UseVisualStyleBackColor = true;
            this.halInstallButton.Click += new System.EventHandler(this.halInstallButton_Click);
            // 
            // getRecommendVersionsButton
            // 
            this.getRecommendVersionsButton.Location = new System.Drawing.Point(25, 38);
            this.getRecommendVersionsButton.Name = "getRecommendVersionsButton";
            this.getRecommendVersionsButton.Size = new System.Drawing.Size(75, 23);
            this.getRecommendVersionsButton.TabIndex = 10;
            this.getRecommendVersionsButton.Text = "button1";
            this.getRecommendVersionsButton.UseVisualStyleBackColor = true;
            this.getRecommendVersionsButton.Click += new System.EventHandler(this.getRecommendVersionsButton_Click);
            // 
            // monoLabel
            // 
            this.monoLabel.AutoSize = true;
            this.monoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monoLabel.Location = new System.Drawing.Point(293, 74);
            this.monoLabel.Name = "monoLabel";
            this.monoLabel.Size = new System.Drawing.Size(49, 20);
            this.monoLabel.TabIndex = 7;
            this.monoLabel.Text = "Mono";
            // 
            // monoDownloadedLabel
            // 
            this.monoDownloadedLabel.AutoSize = true;
            this.monoDownloadedLabel.Location = new System.Drawing.Point(297, 98);
            this.monoDownloadedLabel.Name = "monoDownloadedLabel";
            this.monoDownloadedLabel.Size = new System.Drawing.Size(111, 13);
            this.monoDownloadedLabel.TabIndex = 8;
            this.monoDownloadedLabel.Text = "Downloaded Version: ";
            // 
            // monoRecommendedLabel
            // 
            this.monoRecommendedLabel.AutoSize = true;
            this.monoRecommendedLabel.Location = new System.Drawing.Point(297, 111);
            this.monoRecommendedLabel.Name = "monoRecommendedLabel";
            this.monoRecommendedLabel.Size = new System.Drawing.Size(123, 13);
            this.monoRecommendedLabel.TabIndex = 8;
            this.monoRecommendedLabel.Text = "Recommended Version: ";
            // 
            // monoInstalledLabed
            // 
            this.monoInstalledLabed.AutoSize = true;
            this.monoInstalledLabed.Location = new System.Drawing.Point(297, 124);
            this.monoInstalledLabed.Name = "monoInstalledLabed";
            this.monoInstalledLabed.Size = new System.Drawing.Size(134, 13);
            this.monoInstalledLabed.TabIndex = 8;
            this.monoInstalledLabed.Text = "Currently Installed Version: ";
            // 
            // monoDLButton
            // 
            this.monoDLButton.Enabled = false;
            this.monoDLButton.Location = new System.Drawing.Point(297, 152);
            this.monoDLButton.Name = "monoDLButton";
            this.monoDLButton.Size = new System.Drawing.Size(134, 40);
            this.monoDLButton.TabIndex = 9;
            this.monoDLButton.Text = "Download Recommended Version";
            this.monoDLButton.UseVisualStyleBackColor = true;
            this.monoDLButton.Click += new System.EventHandler(this.monoDLButton_Click);
            // 
            // monoInstallButton
            // 
            this.monoInstallButton.Enabled = false;
            this.monoInstallButton.Location = new System.Drawing.Point(300, 198);
            this.monoInstallButton.Name = "monoInstallButton";
            this.monoInstallButton.Size = new System.Drawing.Size(134, 40);
            this.monoInstallButton.TabIndex = 9;
            this.monoInstallButton.Text = "Install Mono";
            this.monoInstallButton.UseVisualStyleBackColor = true;
            this.monoInstallButton.Click += new System.EventHandler(this.monoInstallButton_Click);
            // 
            // halProgress
            // 
            this.halProgress.Location = new System.Drawing.Point(28, 294);
            this.halProgress.Name = "halProgress";
            this.halProgress.Size = new System.Drawing.Size(165, 23);
            this.halProgress.TabIndex = 11;
            // 
            // monoProgress
            // 
            this.monoProgress.Location = new System.Drawing.Point(297, 294);
            this.monoProgress.Name = "monoProgress";
            this.monoProgress.Size = new System.Drawing.Size(165, 23);
            this.monoProgress.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 454);
            this.Controls.Add(this.monoProgress);
            this.Controls.Add(this.halProgress);
            this.Controls.Add(this.getRecommendVersionsButton);
            this.Controls.Add(this.monoInstallButton);
            this.Controls.Add(this.halInstallButton);
            this.Controls.Add(this.monoDLButton);
            this.Controls.Add(this.monoInstalledLabed);
            this.Controls.Add(this.halDownloadButton);
            this.Controls.Add(this.monoRecommendedLabel);
            this.Controls.Add(this.halCurrentLabel);
            this.Controls.Add(this.monoDownloadedLabel);
            this.Controls.Add(this.halRecommendedVersionLabel);
            this.Controls.Add(this.monoLabel);
            this.Controls.Add(this.halDownloadedVersionLabel);
            this.Controls.Add(this.halLabel);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.teamNumber);
            this.Name = "Main";
            this.Text = "NetFRC";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label connectionStatus;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox teamNumber;
        private System.Windows.Forms.Label halLabel;
        private System.Windows.Forms.Label halDownloadedVersionLabel;
        private System.Windows.Forms.Label halRecommendedVersionLabel;
        private System.Windows.Forms.Label halCurrentLabel;
        private System.Windows.Forms.Button halDownloadButton;
        private System.Windows.Forms.Button halInstallButton;
        private System.Windows.Forms.Button getRecommendVersionsButton;
        private System.Windows.Forms.Label monoLabel;
        private System.Windows.Forms.Label monoDownloadedLabel;
        private System.Windows.Forms.Label monoRecommendedLabel;
        private System.Windows.Forms.Label monoInstalledLabed;
        private System.Windows.Forms.Button monoDLButton;
        private System.Windows.Forms.Button monoInstallButton;
        private System.Windows.Forms.ProgressBar halProgress;
        private System.Windows.Forms.ProgressBar monoProgress;
    }
}

