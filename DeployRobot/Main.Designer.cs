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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 303);
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
    }
}

