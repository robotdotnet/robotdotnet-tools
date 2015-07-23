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
            this.getRecommendVersionsButton = new System.Windows.Forms.Button();
            this.monoLabel = new System.Windows.Forms.Label();
            this.monoDownloadedLabel = new System.Windows.Forms.Label();
            this.monoRecommendedLabel = new System.Windows.Forms.Label();
            this.monoInstalledLabed = new System.Windows.Forms.Label();
            this.monoDLButton = new System.Windows.Forms.Button();
            this.monoInstallButton = new System.Windows.Forms.Button();
            this.monoProgress = new System.Windows.Forms.ProgressBar();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
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
            // getRecommendVersionsButton
            // 
            this.getRecommendVersionsButton.Location = new System.Drawing.Point(28, 72);
            this.getRecommendVersionsButton.Name = "getRecommendVersionsButton";
            this.getRecommendVersionsButton.Size = new System.Drawing.Size(134, 41);
            this.getRecommendVersionsButton.TabIndex = 10;
            this.getRecommendVersionsButton.Text = "Check for Newest Version";
            this.getRecommendVersionsButton.UseVisualStyleBackColor = true;
            this.getRecommendVersionsButton.Click += new System.EventHandler(this.getRecommendVersionsButton_Click);
            // 
            // monoLabel
            // 
            this.monoLabel.AutoSize = true;
            this.monoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monoLabel.Location = new System.Drawing.Point(21, 116);
            this.monoLabel.Name = "monoLabel";
            this.monoLabel.Size = new System.Drawing.Size(49, 20);
            this.monoLabel.TabIndex = 7;
            this.monoLabel.Text = "Mono";
            // 
            // monoDownloadedLabel
            // 
            this.monoDownloadedLabel.AutoSize = true;
            this.monoDownloadedLabel.Location = new System.Drawing.Point(25, 140);
            this.monoDownloadedLabel.Name = "monoDownloadedLabel";
            this.monoDownloadedLabel.Size = new System.Drawing.Size(111, 13);
            this.monoDownloadedLabel.TabIndex = 8;
            this.monoDownloadedLabel.Text = "Downloaded Version: ";
            // 
            // monoRecommendedLabel
            // 
            this.monoRecommendedLabel.AutoSize = true;
            this.monoRecommendedLabel.Location = new System.Drawing.Point(25, 153);
            this.monoRecommendedLabel.Name = "monoRecommendedLabel";
            this.monoRecommendedLabel.Size = new System.Drawing.Size(123, 13);
            this.monoRecommendedLabel.TabIndex = 8;
            this.monoRecommendedLabel.Text = "Recommended Version: ";
            // 
            // monoInstalledLabed
            // 
            this.monoInstalledLabed.AutoSize = true;
            this.monoInstalledLabed.Location = new System.Drawing.Point(25, 166);
            this.monoInstalledLabed.Name = "monoInstalledLabed";
            this.monoInstalledLabed.Size = new System.Drawing.Size(134, 13);
            this.monoInstalledLabed.TabIndex = 8;
            this.monoInstalledLabed.Text = "Currently Installed Version: ";
            // 
            // monoDLButton
            // 
            this.monoDLButton.Enabled = false;
            this.monoDLButton.Location = new System.Drawing.Point(25, 194);
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
            this.monoInstallButton.Location = new System.Drawing.Point(25, 240);
            this.monoInstallButton.Name = "monoInstallButton";
            this.monoInstallButton.Size = new System.Drawing.Size(134, 40);
            this.monoInstallButton.TabIndex = 9;
            this.monoInstallButton.Text = "Install Mono";
            this.monoInstallButton.UseVisualStyleBackColor = true;
            this.monoInstallButton.Click += new System.EventHandler(this.monoInstallButton_Click);
            // 
            // monoProgress
            // 
            this.monoProgress.Location = new System.Drawing.Point(25, 336);
            this.monoProgress.Name = "monoProgress";
            this.monoProgress.Size = new System.Drawing.Size(165, 23);
            this.monoProgress.TabIndex = 11;
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(25, 391);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(83, 40);
            this.exportButton.TabIndex = 12;
            this.exportButton.Text = "Export Mono";
            this.exportButton.UseVisualStyleBackColor = true;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(156, 391);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(83, 40);
            this.importButton.TabIndex = 12;
            this.importButton.Text = "Import Mono";
            this.importButton.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 454);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.monoProgress);
            this.Controls.Add(this.getRecommendVersionsButton);
            this.Controls.Add(this.monoInstallButton);
            this.Controls.Add(this.monoDLButton);
            this.Controls.Add(this.monoInstalledLabed);
            this.Controls.Add(this.monoRecommendedLabel);
            this.Controls.Add(this.monoDownloadedLabel);
            this.Controls.Add(this.monoLabel);
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
        private System.Windows.Forms.Button getRecommendVersionsButton;
        private System.Windows.Forms.Label monoLabel;
        private System.Windows.Forms.Label monoDownloadedLabel;
        private System.Windows.Forms.Label monoRecommendedLabel;
        private System.Windows.Forms.Label monoInstalledLabed;
        private System.Windows.Forms.Button monoDLButton;
        private System.Windows.Forms.Button monoInstallButton;
        private System.Windows.Forms.ProgressBar monoProgress;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
    }
}

