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
            this.templateLabel = new System.Windows.Forms.Label();
            this.tempDLLabel = new System.Windows.Forms.Label();
            this.deployLabel = new System.Windows.Forms.Label();
            this.tempRecLabel = new System.Windows.Forms.Label();
            this.deployDLLabel = new System.Windows.Forms.Label();
            this.tempInstallLabel = new System.Windows.Forms.Label();
            this.deployRecLabel = new System.Windows.Forms.Label();
            this.templateDLButton = new System.Windows.Forms.Button();
            this.deployInstalledLabel = new System.Windows.Forms.Label();
            this.deployDLButton = new System.Windows.Forms.Button();
            this.templateInstallButton = new System.Windows.Forms.Button();
            this.deployInstallButton = new System.Windows.Forms.Button();
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
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templateLabel.Location = new System.Drawing.Point(24, 267);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(109, 20);
            this.templateLabel.TabIndex = 7;
            this.templateLabel.Text = "VS Templates";
            // 
            // tempDLLabel
            // 
            this.tempDLLabel.AutoSize = true;
            this.tempDLLabel.Location = new System.Drawing.Point(28, 291);
            this.tempDLLabel.Name = "tempDLLabel";
            this.tempDLLabel.Size = new System.Drawing.Size(111, 13);
            this.tempDLLabel.TabIndex = 8;
            this.tempDLLabel.Text = "Downloaded Version: ";
            // 
            // deployLabel
            // 
            this.deployLabel.AutoSize = true;
            this.deployLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deployLabel.Location = new System.Drawing.Point(296, 267);
            this.deployLabel.Name = "deployLabel";
            this.deployLabel.Size = new System.Drawing.Size(158, 20);
            this.deployLabel.TabIndex = 7;
            this.deployLabel.Text = "VS Deploy Extension";
            // 
            // tempRecLabel
            // 
            this.tempRecLabel.AutoSize = true;
            this.tempRecLabel.Location = new System.Drawing.Point(28, 304);
            this.tempRecLabel.Name = "tempRecLabel";
            this.tempRecLabel.Size = new System.Drawing.Size(123, 13);
            this.tempRecLabel.TabIndex = 8;
            this.tempRecLabel.Text = "Recommended Version: ";
            // 
            // deployDLLabel
            // 
            this.deployDLLabel.AutoSize = true;
            this.deployDLLabel.Location = new System.Drawing.Point(300, 291);
            this.deployDLLabel.Name = "deployDLLabel";
            this.deployDLLabel.Size = new System.Drawing.Size(111, 13);
            this.deployDLLabel.TabIndex = 8;
            this.deployDLLabel.Text = "Downloaded Version: ";
            // 
            // tempInstallLabel
            // 
            this.tempInstallLabel.AutoSize = true;
            this.tempInstallLabel.Location = new System.Drawing.Point(28, 317);
            this.tempInstallLabel.Name = "tempInstallLabel";
            this.tempInstallLabel.Size = new System.Drawing.Size(134, 13);
            this.tempInstallLabel.TabIndex = 8;
            this.tempInstallLabel.Text = "Currently Installed Version: ";
            // 
            // deployRecLabel
            // 
            this.deployRecLabel.AutoSize = true;
            this.deployRecLabel.Location = new System.Drawing.Point(300, 304);
            this.deployRecLabel.Name = "deployRecLabel";
            this.deployRecLabel.Size = new System.Drawing.Size(123, 13);
            this.deployRecLabel.TabIndex = 8;
            this.deployRecLabel.Text = "Recommended Version: ";
            // 
            // templateDLButton
            // 
            this.templateDLButton.Enabled = false;
            this.templateDLButton.Location = new System.Drawing.Point(28, 345);
            this.templateDLButton.Name = "templateDLButton";
            this.templateDLButton.Size = new System.Drawing.Size(134, 40);
            this.templateDLButton.TabIndex = 9;
            this.templateDLButton.Text = "Download Recommended Version";
            this.templateDLButton.UseVisualStyleBackColor = true;
            this.templateDLButton.Click += new System.EventHandler(this.templateDLButton_Click);
            // 
            // deployInstalledLabel
            // 
            this.deployInstalledLabel.AutoSize = true;
            this.deployInstalledLabel.Location = new System.Drawing.Point(300, 317);
            this.deployInstalledLabel.Name = "deployInstalledLabel";
            this.deployInstalledLabel.Size = new System.Drawing.Size(134, 13);
            this.deployInstalledLabel.TabIndex = 8;
            this.deployInstalledLabel.Text = "Currently Installed Version: ";
            // 
            // deployDLButton
            // 
            this.deployDLButton.Enabled = false;
            this.deployDLButton.Location = new System.Drawing.Point(300, 345);
            this.deployDLButton.Name = "deployDLButton";
            this.deployDLButton.Size = new System.Drawing.Size(134, 40);
            this.deployDLButton.TabIndex = 9;
            this.deployDLButton.Text = "Download Recommended Version";
            this.deployDLButton.UseVisualStyleBackColor = true;
            this.deployDLButton.Click += new System.EventHandler(this.deployDLButton_Click);
            // 
            // templateInstallButton
            // 
            this.templateInstallButton.Enabled = false;
            this.templateInstallButton.Location = new System.Drawing.Point(31, 391);
            this.templateInstallButton.Name = "templateInstallButton";
            this.templateInstallButton.Size = new System.Drawing.Size(134, 40);
            this.templateInstallButton.TabIndex = 9;
            this.templateInstallButton.Text = "Install Visual Studio Templates";
            this.templateInstallButton.UseVisualStyleBackColor = true;
            this.templateInstallButton.Click += new System.EventHandler(this.templateInstallButton_Click);
            // 
            // deployInstallButton
            // 
            this.deployInstallButton.Enabled = false;
            this.deployInstallButton.Location = new System.Drawing.Point(303, 391);
            this.deployInstallButton.Name = "deployInstallButton";
            this.deployInstallButton.Size = new System.Drawing.Size(134, 40);
            this.deployInstallButton.TabIndex = 9;
            this.deployInstallButton.Text = "Install Visual Studio Deploy Extension";
            this.deployInstallButton.UseVisualStyleBackColor = true;
            this.deployInstallButton.Click += new System.EventHandler(this.deployInstallButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 454);
            this.Controls.Add(this.getRecommendVersionsButton);
            this.Controls.Add(this.deployInstallButton);
            this.Controls.Add(this.monoInstallButton);
            this.Controls.Add(this.templateInstallButton);
            this.Controls.Add(this.halInstallButton);
            this.Controls.Add(this.deployDLButton);
            this.Controls.Add(this.deployInstalledLabel);
            this.Controls.Add(this.monoDLButton);
            this.Controls.Add(this.templateDLButton);
            this.Controls.Add(this.monoInstalledLabed);
            this.Controls.Add(this.deployRecLabel);
            this.Controls.Add(this.halDownloadButton);
            this.Controls.Add(this.tempInstallLabel);
            this.Controls.Add(this.monoRecommendedLabel);
            this.Controls.Add(this.deployDLLabel);
            this.Controls.Add(this.halCurrentLabel);
            this.Controls.Add(this.tempRecLabel);
            this.Controls.Add(this.monoDownloadedLabel);
            this.Controls.Add(this.deployLabel);
            this.Controls.Add(this.halRecommendedVersionLabel);
            this.Controls.Add(this.tempDLLabel);
            this.Controls.Add(this.monoLabel);
            this.Controls.Add(this.templateLabel);
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
        private System.Windows.Forms.Label templateLabel;
        private System.Windows.Forms.Label tempDLLabel;
        private System.Windows.Forms.Label deployLabel;
        private System.Windows.Forms.Label tempRecLabel;
        private System.Windows.Forms.Label deployDLLabel;
        private System.Windows.Forms.Label tempInstallLabel;
        private System.Windows.Forms.Label deployRecLabel;
        private System.Windows.Forms.Button templateDLButton;
        private System.Windows.Forms.Label deployInstalledLabel;
        private System.Windows.Forms.Button deployDLButton;
        private System.Windows.Forms.Button templateInstallButton;
        private System.Windows.Forms.Button deployInstallButton;
    }
}

