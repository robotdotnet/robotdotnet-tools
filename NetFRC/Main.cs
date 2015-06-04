using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using RobotDotNetBuildTasks;

namespace NetFRC
{
    public partial class Main : Form
    {
        public static RichTextBox statusWindow;

        private ConnectionManager connectionManager;

        //public static Dictionary<string, string> downloadedVersions = new Dictionary<string, string>();
        //public static Dictionary<string, string> preDownloadedVersions = new Dictionary<string, string>();
        //public static Dictionary<string, string> currentVersions = new Dictionary<string, string>();

        //public static readonly string dlFileLocation = "Downloads" + Path.DirectorySeparatorChar + "newestversions.txt";

        //public static readonly string preDLFileLocation = "Downloads" + Path.DirectorySeparatorChar + "preversions.txt";

        public static string DeployRobotLocation;

        public Main()
        {
            /*
            DeployRobotLocation = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                  Path.DirectorySeparatorChar + "wpilib" + Path.DirectorySeparatorChar + "dotnet" +
                                  Path.DirectorySeparatorChar + "tools";
            if (!Directory.Exists(DeployRobotLocation))
            {
                Directory.CreateDirectory(DeployRobotLocation);
            }
            */
            
            InitializeComponent();

            connectionManager = new ConnectionManager();
            connectionManager.TaskComplete += connectionManager_TaskComplete;

            this.SuspendLayout();
            statusWindow = new RichTextBox();
            statusWindow.BackColor = System.Drawing.SystemColors.WindowText;
            statusWindow.ForeColor = System.Drawing.SystemColors.Window;
            statusWindow.Location = new System.Drawing.Point(579, 13);
            statusWindow.Name = "statusWindow";
            statusWindow.Size = new System.Drawing.Size(374, 281);
            statusWindow.TabIndex = 16;
            statusWindow.Text = "";
            statusWindow.HideSelection = false;

            this.Controls.Add(statusWindow);
            this.ResumeLayout(false);
            this.PerformLayout();

            if (!Directory.Exists("Downloads"))
                Directory.CreateDirectory("Downloads");


            //Down
            //connectionManager.Connect("4488", false, true);
            

        }

        private void Main_Load(object sender, EventArgs e)
        {
            DownloadedVersions.Start(DownloadedVersionsCallback);
        }

        public void CheckHAL()
        {
            if (connectionManager.Connected && DownloadedVersions.Versions.ContainsKey("HAL"))
            {
                halInstallButton.Enabled = true;
            }
        }

        public void DownloadedVersionsCallback()
        {
            Action action = () =>
                {
                    halDownloadedVersionLabel.Text = "Downloaded Version: None";
                    //Do other 4 labels
                    if(DownloadedVersions.Versions.ContainsKey("HAL"))
                    {
                        halDownloadedVersionLabel.Text = "Downloaded Version: " + DownloadedVersions.Versions["HAL"];
                        //halInstallButton.Enabled = true;
                        CheckHAL();
                    }
                };
            this.Invoke(action);
            
        }

        public void RecommendedVersionsCallback()
        {
            Action action = () =>
            {
                if (RecommendedVersions.Versions.ContainsKey("HAL"))
                {
                    halRecommendedVersionLabel.Text = "Recommended Version: " + RecommendedVersions.Versions["HAL"];
                    halDownloadButton.Enabled = true;
                }
            };
            this.Invoke(action);
        }
        
        

        public void CheckLocalVersionsComplete()
        {

        }

        public void CheckRIOVersionsComplete()
        {

        }


        void connectionManager_TaskComplete()
        {
            AppendToStatus(connectionManager.GetConnectionStatus());

            Action action = () =>
            {
                string connectionString = "Connection Status: ";
                switch (connectionManager.Connected)
                {
                    case false:
                        connectionString += "Not Connected";
                        break;
                    case true:
                        connectionString += "Connected\n";
                        connectionString += connectionManager.ConnectionIP;
                        break;
                }

                connectionStatus.Text = "";
                connectionStatus.Text = connectionString;




                connectButton.Enabled = true;


                //StartCheckRIOVersions();
                LocalVersions.StartRIO(CheckRIOVersionsComplete, connectionManager);
                CheckHAL();
                //EnableConnections(connectionManager.Connected);
            };

            connectionStatus.Invoke(action);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectionManager.Connect(teamNumber.Text, false, true);
        }

        public static void AppendToStatus(string message)
        {
            Action action = () =>
            {
                statusWindow.AppendText(message + "\n");
            };
            statusWindow.Invoke(action);
        }

        private void halDownloadButton_Click(object sender, EventArgs e)
        {
            
            halDownloadButton.Enabled = false;
            halInstallButton.Enabled = false;
            DownloadManager.DownloadHAL(HALDownloadComplete, RecommendedVersions.Versions["HAL"]);
        }

        public void HALDownloadComplete()
        {
            Action action = () =>
                {
                    AppendToStatus("HAL Download Complete");
                    DownloadedVersions.Start(DownloadedVersionsCallback);
                    halDownloadButton.Enabled = true;
                    CheckHAL();
                };
            Invoke(action);
        }

        private void halInstallButton_Click(object sender, EventArgs e)
        {

        }

        private void getRecommendVersionsButton_Click(object sender, EventArgs e)
        {
            RecommendedVersions.Start(RecommendedVersionsCallback);
        }

        
    }
}
