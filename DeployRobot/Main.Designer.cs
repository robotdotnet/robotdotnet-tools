namespace DeployRobot
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
            this.teamNumber = new System.Windows.Forms.TextBox();
            this.isUSBConnected = new System.Windows.Forms.CheckBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.halLabel = new System.Windows.Forms.Label();
            this.halNewestVersion = new System.Windows.Forms.Label();
            this.updateHAL = new System.Windows.Forms.CheckBox();
            this.deployButton = new System.Windows.Forms.Button();
            this.codeDirectory = new System.Windows.Forms.TextBox();
            this.codeDirectoryButton = new System.Windows.Forms.Button();
            this.wpilibFound = new System.Windows.Forms.CheckBox();
            this.HALBaseFound = new System.Windows.Forms.CheckBox();
            this.otherFileLabel = new System.Windows.Forms.Label();
            this.robotFileFound = new System.Windows.Forms.CheckBox();
            this.robotFileNameLabel = new System.Windows.Forms.Label();
            this.otherFilesTextBox = new System.Windows.Forms.ListBox();
            this.networkTablesFound = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // teamNumber
            // 
            this.teamNumber.Location = new System.Drawing.Point(35, 30);
            this.teamNumber.Name = "teamNumber";
            this.teamNumber.Size = new System.Drawing.Size(133, 20);
            this.teamNumber.TabIndex = 0;
            // 
            // isUSBConnected
            // 
            this.isUSBConnected.AutoSize = true;
            this.isUSBConnected.Location = new System.Drawing.Point(35, 56);
            this.isUSBConnected.Name = "isUSBConnected";
            this.isUSBConnected.Size = new System.Drawing.Size(141, 17);
            this.isUSBConnected.TabIndex = 1;
            this.isUSBConnected.Text = "Force USB Connection?";
            this.isUSBConnected.UseVisualStyleBackColor = true;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(185, 30);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(105, 43);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect to Robot";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = true;
            this.connectionStatus.Location = new System.Drawing.Point(328, 30);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(100, 13);
            this.connectionStatus.TabIndex = 3;
            this.connectionStatus.Text = "Connection Status: ";
            // 
            // halLabel
            // 
            this.halLabel.AutoSize = true;
            this.halLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.halLabel.Location = new System.Drawing.Point(32, 109);
            this.halLabel.Name = "halLabel";
            this.halLabel.Size = new System.Drawing.Size(72, 13);
            this.halLabel.TabIndex = 4;
            this.halLabel.Text = "HAL Version: ";
            // 
            // halNewestVersion
            // 
            this.halNewestVersion.AutoSize = true;
            this.halNewestVersion.Location = new System.Drawing.Point(32, 132);
            this.halNewestVersion.Name = "halNewestVersion";
            this.halNewestVersion.Size = new System.Drawing.Size(111, 13);
            this.halNewestVersion.TabIndex = 5;
            this.halNewestVersion.Text = "HAL Newest Version: ";
            // 
            // updateHAL
            // 
            this.updateHAL.AutoSize = true;
            this.updateHAL.Checked = true;
            this.updateHAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.updateHAL.Location = new System.Drawing.Point(199, 105);
            this.updateHAL.Name = "updateHAL";
            this.updateHAL.Size = new System.Drawing.Size(91, 17);
            this.updateHAL.TabIndex = 6;
            this.updateHAL.Text = "Update HAL?";
            this.updateHAL.UseVisualStyleBackColor = true;
            // 
            // deployButton
            // 
            this.deployButton.Enabled = false;
            this.deployButton.Location = new System.Drawing.Point(35, 268);
            this.deployButton.Name = "deployButton";
            this.deployButton.Size = new System.Drawing.Size(108, 23);
            this.deployButton.TabIndex = 7;
            this.deployButton.Text = "Deploy Robot Code";
            this.deployButton.UseVisualStyleBackColor = true;
            // 
            // codeDirectory
            // 
            this.codeDirectory.Location = new System.Drawing.Point(35, 161);
            this.codeDirectory.Name = "codeDirectory";
            this.codeDirectory.Size = new System.Drawing.Size(216, 20);
            this.codeDirectory.TabIndex = 8;
            this.codeDirectory.TextChanged += new System.EventHandler(this.codeDirectory_TextChanged);
            // 
            // codeDirectoryButton
            // 
            this.codeDirectoryButton.Location = new System.Drawing.Point(331, 165);
            this.codeDirectoryButton.Name = "codeDirectoryButton";
            this.codeDirectoryButton.Size = new System.Drawing.Size(125, 23);
            this.codeDirectoryButton.TabIndex = 9;
            this.codeDirectoryButton.Text = "Select Code Location";
            this.codeDirectoryButton.UseVisualStyleBackColor = true;
            this.codeDirectoryButton.Click += new System.EventHandler(this.codeDirectoryButton_Click);
            // 
            // wpilibFound
            // 
            this.wpilibFound.AutoSize = true;
            this.wpilibFound.Enabled = false;
            this.wpilibFound.Location = new System.Drawing.Point(35, 187);
            this.wpilibFound.Name = "wpilibFound";
            this.wpilibFound.Size = new System.Drawing.Size(100, 17);
            this.wpilibFound.TabIndex = 10;
            this.wpilibFound.Text = "WPILib Found?";
            this.wpilibFound.UseVisualStyleBackColor = true;
            // 
            // HALBaseFound
            // 
            this.HALBaseFound.AutoSize = true;
            this.HALBaseFound.Enabled = false;
            this.HALBaseFound.Location = new System.Drawing.Point(191, 189);
            this.HALBaseFound.Name = "HALBaseFound";
            this.HALBaseFound.Size = new System.Drawing.Size(116, 17);
            this.HALBaseFound.TabIndex = 11;
            this.HALBaseFound.Text = "HAL_Base Found?";
            this.HALBaseFound.UseVisualStyleBackColor = true;
            // 
            // otherFileLabel
            // 
            this.otherFileLabel.AutoSize = true;
            this.otherFileLabel.Location = new System.Drawing.Point(328, 195);
            this.otherFileLabel.Name = "otherFileLabel";
            this.otherFileLabel.Size = new System.Drawing.Size(90, 13);
            this.otherFileLabel.TabIndex = 13;
            this.otherFileLabel.Text = "Other Files Found";
            // 
            // robotFileFound
            // 
            this.robotFileFound.AutoSize = true;
            this.robotFileFound.Enabled = false;
            this.robotFileFound.Location = new System.Drawing.Point(35, 212);
            this.robotFileFound.Name = "robotFileFound";
            this.robotFileFound.Size = new System.Drawing.Size(150, 17);
            this.robotFileFound.TabIndex = 10;
            this.robotFileFound.Text = "Robot Executable Found?";
            this.robotFileFound.UseVisualStyleBackColor = true;
            // 
            // robotFileNameLabel
            // 
            this.robotFileNameLabel.AutoSize = true;
            this.robotFileNameLabel.Location = new System.Drawing.Point(35, 236);
            this.robotFileNameLabel.Name = "robotFileNameLabel";
            this.robotFileNameLabel.Size = new System.Drawing.Size(80, 13);
            this.robotFileNameLabel.TabIndex = 14;
            this.robotFileNameLabel.Text = "RobotFileName";
            // 
            // otherFilesTextBox
            // 
            this.otherFilesTextBox.FormattingEnabled = true;
            this.otherFilesTextBox.Location = new System.Drawing.Point(331, 212);
            this.otherFilesTextBox.Name = "otherFilesTextBox";
            this.otherFilesTextBox.Size = new System.Drawing.Size(223, 82);
            this.otherFilesTextBox.TabIndex = 15;
            // 
            // networkTablesFound
            // 
            this.networkTablesFound.AutoSize = true;
            this.networkTablesFound.Enabled = false;
            this.networkTablesFound.Location = new System.Drawing.Point(191, 212);
            this.networkTablesFound.Name = "networkTablesFound";
            this.networkTablesFound.Size = new System.Drawing.Size(140, 17);
            this.networkTablesFound.TabIndex = 11;
            this.networkTablesFound.Text = "Network Tables Found?";
            this.networkTablesFound.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 303);
            this.Controls.Add(this.otherFilesTextBox);
            this.Controls.Add(this.robotFileNameLabel);
            this.Controls.Add(this.otherFileLabel);
            this.Controls.Add(this.networkTablesFound);
            this.Controls.Add(this.HALBaseFound);
            this.Controls.Add(this.robotFileFound);
            this.Controls.Add(this.wpilibFound);
            this.Controls.Add(this.codeDirectoryButton);
            this.Controls.Add(this.codeDirectory);
            this.Controls.Add(this.deployButton);
            this.Controls.Add(this.updateHAL);
            this.Controls.Add(this.halNewestVersion);
            this.Controls.Add(this.halLabel);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.isUSBConnected);
            this.Controls.Add(this.teamNumber);
            this.Name = "Main";
            this.Text = "RobotDotNet Deploy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox teamNumber;
        private System.Windows.Forms.CheckBox isUSBConnected;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label connectionStatus;
        private System.Windows.Forms.Label halLabel;
        private System.Windows.Forms.Label halNewestVersion;
        private System.Windows.Forms.CheckBox updateHAL;
        private System.Windows.Forms.Button deployButton;
        private System.Windows.Forms.TextBox codeDirectory;
        private System.Windows.Forms.Button codeDirectoryButton;
        private System.Windows.Forms.CheckBox wpilibFound;
        private System.Windows.Forms.CheckBox HALBaseFound;
        private System.Windows.Forms.Label otherFileLabel;
        private System.Windows.Forms.CheckBox robotFileFound;
        private System.Windows.Forms.Label robotFileNameLabel;
        private System.Windows.Forms.ListBox otherFilesTextBox;
        private System.Windows.Forms.CheckBox networkTablesFound;
    }
}

