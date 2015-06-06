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
            if (connectionManager.Connected && (DownloadedVersions.GetHAL() != null))
            {
                halInstallButton.Enabled = true;
            }
        }

        public void CheckMono()
        {
            if (connectionManager.Connected && (DownloadedVersions.GetMono() != null))
            {
                monoInstallButton.Enabled = true;
            }
        }

        public void CheckTemplate()
        {
            if (connectionManager.Connected && (DownloadedVersions.GetTemplate() != null))
            {
                templateInstallButton.Enabled = true;
            }
        }

        public void CheckDeploy()
        {
            if (connectionManager.Connected && (DownloadedVersions.GetDeploy() != null))
            {
                deployInstallButton.Enabled = true;
            }
        }



        public void DownloadedVersionsCallback()
        {
            Action action = () =>
                {
                    halDownloadedVersionLabel.Text = "Downloaded Version: None";
                    monoDownloadedLabel.Text = "Downloaded Version: None";
                    tempDLLabel.Text = "Downloaded Version: None";
                    deployDLLabel.Text = "Downloaded Version: None";
                    //Do other 4 labels

                    var dl = DownloadedVersions.GetHAL();
                    if(dl != null)
                    {
                        halDownloadedVersionLabel.Text = "Downloaded Version: " + dl;
                        //halInstallButton.Enabled = true;
                        CheckHAL();
                    }

                    dl = DownloadedVersions.GetMono();
                    if (dl != null)
                    {
                        monoDownloadedLabel.Text = "Downloaded Version: " + dl;
                        //halInstallButton.Enabled = true;
                        CheckMono();
                    }

                    dl = DownloadedVersions.GetTemplate();
                    if (dl != null)
                    {
                        tempDLLabel.Text = "Downloaded Version: " + dl;
                        //halInstallButton.Enabled = true;
                        CheckTemplate();
                    }

                    dl = DownloadedVersions.GetDeploy();
                    if (dl != null)
                    {
                        deployDLLabel.Text = "Downloaded Version: " + dl;
                        //halInstallButton.Enabled = true;
                        CheckDeploy();
                    }

                };
            this.Invoke(action);
            
        }

        public void RecommendedVersionsCallback()
        {
            Action action = () =>
            {
                halRecommendedVersionLabel.Text = "Recommended Version: None";
                monoRecommendedLabel.Text = "Recommended Version: None";
                tempRecLabel.Text = "Recommended Version: None";
                deployRecLabel.Text = "Recommended Version: None";

                var rec = RecommendedVersions.GetHAL();
                if (rec != null)
                {
                    halRecommendedVersionLabel.Text = "Recommended Version: " + rec;
                    halDownloadButton.Enabled = true;
                }

                rec = RecommendedVersions.GetMono();
                if (rec != null)
                {
                    monoRecommendedLabel.Text = "Recommended Version: " + rec;
                    monoDLButton.Enabled = true;
                }

                rec = RecommendedVersions.GetTemplate();
                if (rec != null)
                {
                    tempRecLabel.Text = "Recommended Version: " + rec;
                    templateDLButton.Enabled = true;
                }

                rec = RecommendedVersions.GetDeploy();
                if (rec != null)
                {
                    deployRecLabel.Text = "Recommended Version: " + rec;
                    deployDLButton.Enabled = true;
                }
            };
            this.Invoke(action);
        }
        
        

        public void CheckLocalVersionsComplete()
        {

        }

        public void CheckRIOVersionsComplete()
        {
            Action action = () =>
            {
                halCurrentLabel.Text = "Currently Installed Version: None";
                var locHAL = LocalVersions.GetHAL();
                if (locHAL != null)
                {
                    halCurrentLabel.Text = "Currently Installed Version: " + locHAL;
                }

                monoInstalledLabed.Text = "Currently Installed Version: None";
                var locMono = LocalVersions.GetMono();
                if (locMono != null)
                {
                    monoInstalledLabed.Text = "Currently Installed Version: " + locMono;
                }
            };
            Invoke(action);
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
            AppendToStatus("Connecting to RoboRIO");
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
            DownloadManager.DownloadHAL(HALDownloadComplete, HALProgressChanged, RecommendedVersions.GetHAL());
        }

        public void HALProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
        }

        public void HALDownloadComplete()
        {
            Action action = () =>
                {
                    DownloadedVersions.Versions["HAL"] = RecommendedVersions.GetHAL();
                    DownloadedVersions.WriteTxt();

                    AppendToStatus("HAL Download Complete");
                    DownloadedVersions.Start(DownloadedVersionsCallback);
                    halDownloadButton.Enabled = true;
                    CheckHAL();
                };
            Invoke(action);
        }

        private void halInstallButton_Click(object sender, EventArgs e)
        {
            AppendToStatus("Installing HAL");
            halInstallButton.Enabled = false;
            new Thread(() =>
            {
                List<string> deploy = new List<string>();
                deploy.Add("Downloads" + Path.DirectorySeparatorChar + "HAL" + Path.DirectorySeparatorChar +
                           "libHALAthena_shared.so");
                InstallDeployManager.DeployFiles(deploy.ToArray(), "/home/lvuser/mono", connectionManager.Connection);
                deploy.Clear();
                deploy.Add("rm /home/lvuser/mono/version");
                deploy.Add("echo HAL:" + DownloadedVersions.GetHAL() + "> /home/lvuser/mono/version");
                InstallDeployManager.RunCommands(deploy.ToArray(), connectionManager.Connection);
                LocalVersions.StartRIO(CheckRIOVersionsComplete, connectionManager);
                HALInstallFinished();
            }).Start();
        }

        public void HALInstallFinished()
        {
            Action action = () =>
            {
                AppendToStatus("Successfully Deployed HAL");
                halInstallButton.Enabled = true;
            };
            this.Invoke(action);
        }

        private void getRecommendVersionsButton_Click(object sender, EventArgs e)
        {
            RecommendedVersions.Start(RecommendedVersionsCallback);
        }

        private void monoDLButton_Click(object sender, EventArgs e)
        {
            monoDLButton.Enabled = false;
            monoInstallButton.Enabled = false;
            DownloadManager.DownloadMono(MonoDownloadComplete, null);
        }

        private void MonoDownloadComplete()
        {
            Action action = () =>
            {
                AppendToStatus("Mono Download Complete");

            };
            action();
        }

        private void monoInstallButton_Click(object sender, EventArgs e)
        {

        }

        private void templateDLButton_Click(object sender, EventArgs e)
        {

        }

        private void templateInstallButton_Click(object sender, EventArgs e)
        {

        }

        private void deployDLButton_Click(object sender, EventArgs e)
        {

        }

        private void deployInstallButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
