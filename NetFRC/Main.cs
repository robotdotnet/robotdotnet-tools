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

        public static Dictionary<string, string> downloadedVersions = new Dictionary<string, string>();
        public static Dictionary<string, string> preDownloadedVersions = new Dictionary<string, string>();
        public static Dictionary<string, string> currentVersions = new Dictionary<string, string>();

        public static readonly string dlFileLocation = "Downloads" + Path.DirectorySeparatorChar + "newestversions.txt";

        public static readonly string preDLFileLocation = "Downloads" + Path.DirectorySeparatorChar + "preversions.txt";

        public static string DeployRobotLocation;

        public Main()
        {
            DeployRobotLocation = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                  Path.DirectorySeparatorChar + "wpilib" + Path.DirectorySeparatorChar + "dotnet" +
                                  Path.DirectorySeparatorChar + "tools";
            if (!Directory.Exists(DeployRobotLocation))
            {
                Directory.CreateDirectory(DeployRobotLocation);
            }

            InitializeComponent();


            //Load team file if it exists.

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

            this.Controls.Add(statusWindow);
            this.ResumeLayout(false);
            this.PerformLayout();

            if (!Directory.Exists("Downloads"))
                Directory.CreateDirectory("Downloads");

            connectionManager.Connect("4488", false, true);
            StartGetVersions();
        }

        public void StartDownloadMono()
        {
            new Thread(() =>
            {
                //Start Here



                MonoDownloadComplete();
            }).Start();
        }

        public void MonoDownloadComplete()
        {
            Action action = () =>
            {

            };
            //Invoke an object here.
        }

        public void StartHalDownload()
        {
            new Thread(() =>
            {
                //Start Here



                HalDownloadComplete();
            }).Start();
        }

        public void HalDownloadComplete()
        {
            Action action = () =>
            {

            };
            //Invoke an object here.
        }

        public void StartGetVersions()
        {
            new Thread(() =>
            {
                //Start Here
                using (var client = new WebClient())
                {
                    client.DownloadFile(
                        "https://raw.githubusercontent.com/robotdotnet/robotdotnet-wpilib/master/version.txt",
                        dlFileLocation);
                }
                GetVersionsComplete();
            }).Start();
        }

        public void GetVersionsComplete()
        {
            Action action = () =>
            {
                if (File.Exists(dlFileLocation))
                {
                    foreach (var split in File.ReadAllLines(dlFileLocation).Select(l => l.Split(':')).Where(split => split.Length >= 2))
                    {
                        downloadedVersions.Add(split[0], split[1]);
                    }
                }
            };
            //Gonna need strings
        }

        public void StartCheckPreDownloadedVersions()
        {
            new Thread(() =>
            {
                if (File.Exists(preDLFileLocation))
                {
                    foreach (var split in File.ReadAllLines(preDLFileLocation).Select(l => l.Split(':')).Where(split => split.Length >= 2))
                    {
                        preDownloadedVersions.Add(split[0], split[1]);
                    }
                }
            }).Start();
        }

        public void CheckPreDownloadedVersions()
        {

        }

        public void StartCheckLocalVersion()
        {
            new Thread(() =>
            {
                //Get Deploy Robot Version
                string deployFileName = DeployRobotLocation + Path.DirectorySeparatorChar + "deployrobot.exe";
                if (File.Exists(deployFileName))
                {
                    var deploytool = Assembly.LoadFrom(deployFileName).GetName();
                    currentVersions["Deploy"] = deploytool.Version.ToString();
                }

                //Get Template Version
                string templateFileName = DeployRobotLocation + Path.DirectorySeparatorChar + "templatever.txt";
                if (File.Exists(templateFileName))
                {
                    var file = File.ReadAllLines(templateFileName);
                    if (file.Length >= 1)
                    {
                        currentVersions["Template"] = file[0];
                    }
                }

                CheckLocalVersionsComplete();
            }).Start();
        }

        public void CheckLocalVersionsComplete()
        {

        }

        public void StartCheckRIOVersions()
        {
            new Thread(() =>
            {
                if (connectionManager.Connected)
                {
                    string[] sent = { "cat /home/lvuser/mono/version", "mono --version" };
                    var rioVersionDict = InstallDeployManager.RunCommands(sent, connectionManager.Connection);
                    var split = rioVersionDict[sent[0]].Split(':');
                    if (split[0] == "HAL")
                        currentVersions["HAL"] = rioVersionDict[sent[0]].Trim();

                    split = rioVersionDict[sent[1]].Split(' ');
                    if (split[0] == "Mono")
                    {
                        currentVersions["Mono"] = split[4];
                    }
                }


                CheckRIOVersionsComplete();
            }).Start();
        }

        public void CheckRIOVersionsComplete()
        {
            Action action = () =>
            {

            };
            //Invoke an object here.
        }


        bool preDownloadedChecked = false;
        bool downloadedChecked = false;
        bool RIOChecked = false;
        public void CheckAllVersion()
        {
            if (preDownloadedChecked && downloadedChecked && RIOChecked)
            {

            }
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


                StartCheckRIOVersions();

                //EnableConnections(connectionManager.Connected);
            };

            connectionStatus.Invoke(action);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {

        }

        public static void AppendToStatus(string message)
        {
            Action action = () =>
            {
                statusWindow.AppendText(message + "\n");
            };
            statusWindow.Invoke(action);
        }
    }
}
