using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Renci.SshNet;
using RobotDotNetBuildTasks;

namespace DeployRobot
{
    public partial class Main : Form
    {
        public static Dictionary<string, string> cmdArguments;

        const string TEAM_NUMBER_FILE = "team.txt";

        List<DeployFile> otherFiles = new List<DeployFile>();
        List<DeployFile> deployFiles = new List<DeployFile>();
        DeployFile robotFile;
        DeployFile WPILibFile;
        DeployFile HALBaseFile;
        DeployFile HALRoboRIOFile;
        DeployFile NetworkTablesFile;

        private ConnectionManager connectionManager;
        private DeployManager deployManager;

        public static RichTextBox statusWindow;

        public static string RobotName = "Robot.exe";

        


        public Main()
        {
            InitializeComponent();
            connectionManager = new ConnectionManager();
            deployManager = new DeployManager(ref connectionManager);
            ArgParse.AddArgument("nac", "noautoconnect", false, true, "Use to not autoconnect to Robot.");
            ArgParse.AddArgument("ad", "autodeploy", false, true, "Use to autodeploy code to the robot.");
            ArgParse.AddArgument("team", "teamnumber", false, "Add Team Number");
            ArgParse.AddArgument("debug", "debug", false, "Weither to run debug.");
            ArgParse.AddArgument("loc", "filelocation", false, false, "Robot code location");
            ArgParse.AddArgument("nc", "netconsole", false, true,"Start netconsole or not?");

            string[] args = Environment.GetCommandLineArgs();

            //var x = RoboRIOConnection.CheckConnection(4488);

            cmdArguments = ArgParse.ParseArguments(args);

            if (cmdArguments.ContainsKey("teamnumber"))
            {
                teamNumber.Text = cmdArguments["teamnumber"];
            }
            //Load Team Number
            else if (File.Exists(TEAM_NUMBER_FILE))
            {
                var fileRead = File.ReadAllLines(TEAM_NUMBER_FILE);
                if (fileRead.Length > 0)
                {
                    if (fileRead[0].Contains("team"))
                    {
                        teamNumber.Text = fileRead[0].Substring(fileRead[0].IndexOf(':') + 1, fileRead[0].Length - (fileRead[0].IndexOf(':') + 1)).Trim();
                    }
                }
            }

            wpilibFound.CheckedChanged += CheckIfReady;
            networkTablesFound.CheckedChanged += CheckIfReady;
            HALBaseFound.CheckedChanged += CheckIfReady;
            robotFileFound.CheckedChanged += CheckIfReady;
            //connectionStatus.TextChanged += CheckIfReady;

            

            
            


            //this.Hide();

            /*
            if (cmdArguments.ContainsKey("autoconnect"))
            {

                connectionManager.Connect(teamNumber.Text, true);
                halManager.CheckHALVersion(1.0, 326);

            }
            if (cmdArguments.ContainsKey("autodeploy"))
            {
                //Run Connect
                if (!cmdArguments.ContainsKey("autoconnect"))
                {
                    connectionManager.Connect(teamNumber.Text, true);
                }
                //Run Deploy
                halManager.CheckHALVersion(1.0, 326, true);
                deployManager.Deploy(deployFiles, true);
            }
             * */
            connectionManager.TaskComplete += connectionManager_TaskComplete;
            deployManager.TaskComplete += deployManager_TaskComplete;

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
            //this.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (cmdArguments.ContainsKey("filelocation"))
            {
                if (Directory.Exists(cmdArguments["filelocation"]))
                {
                    codeDirectory.Text = cmdArguments["filelocation"];
                }
                else
                {
                    codeDirectory.Text = Application.StartupPath;
                }
            }
            else
            {
                codeDirectory.Text = Application.StartupPath;
            }

            if (!cmdArguments.ContainsKey("noautoconnect"))
            {
                connectButton_Click(null, null);
            }
            AppendToTop(cmdArguments["filelocation"]);
        }


        void deployManager_TaskComplete()
        {
            Action action = () =>
            {
                deployButton.Enabled = true;

                AppendToTop("Successfully deployed robot code");

                if (cmdArguments.ContainsKey("autodeploy"))
                {
                    AppendToTop("Closing");
                    this.Close();
                }
            };
            deployButton.Invoke(action);
        }

        void connectionManager_TaskComplete()
        {
            AppendToTop(connectionManager.GetConnectionStatus());
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

                CheckIfReady(null, null);
            };
            connectionStatus.Invoke(action);
        }

        private void codeDirectory_TextChanged(object sender, EventArgs e)
        {
            deployFiles.Clear();
            otherFiles.Clear();
            HALBaseFound.Checked = false;
            wpilibFound.Checked = false;
            networkTablesFound.Checked = false;
            robotFileFound.Checked = false;
            halRoboRIOFound.Checked = false;
            robotFileNameLabel.Text = "None";
            otherFilesTextBox.DataSource = null;
            otherFilesTextBox.DataSource = otherFiles;
            otherFilesTextBox.DisplayMember = "FileName";

            if (Directory.Exists(codeDirectory.Text))
            {
                //Find Robot Base first (This is so we can figure out the executable.
                Type robotBase;
                if (File.Exists(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll"))
                {
                    var wpilib = Assembly.LoadFrom(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll");
                    try
                    {
                        robotBase = wpilib.GetType("WPILib.RobotBase", true);
                    }
                    catch
                    {
                        return;
                    }
                    
                }
                else
                {
                    return;
                }
                foreach (string f in Directory.GetFiles(codeDirectory.Text))
                {
                    //Ignore non pure DLL or EXE files
                    if (f.Contains("pdb") || f.Contains("vshost") || f.Contains(".config") || f.Contains(".manifest") || f.Contains("deploy.bat"))
                        continue;
                    //If its an EXE
                    if (f.Contains(".exe"))
                    {
                        //See if it is the deploy executable. If so, do not include it
                        if (f.Contains(Process.GetCurrentProcess().ProcessName.Split('.')[0]))
                        {
                            continue;
                        }

                        //Load Assembly, in order to find the main robot file.
                        var asm = Assembly.LoadFrom(f);

                        var classes = from t in asm.GetTypes() where robotBase.IsAssignableFrom(t) select t;
                        //if no class extending from RobotBase was found, include it as a normal file
                        if (classes.ToList().Count == 0)
                        {
                            DeployFile file = new DeployFile(f, false, false);
                            deployFiles.Add(file);
                            otherFiles.Add(file);
                        }
                        //Otherwise its the robot file, and we want to keep it specialized.
                        else
                        {
                            AppendToTop("Found Robot Executable");
                            robotFile = new DeployFile(f, true, true);
                            robotFileFound.Checked = true;
                            var split = f.Split(Path.DirectorySeparatorChar);
                            robotFileNameLabel.Text = split[split.Length - 1];
                            RobotName = robotFileNameLabel.Text;
                            deployFiles.Add(robotFile);
                        }

                    }
                    //If its a DLL
                    else if (f.Contains(".dll"))
                    {
                        // Special cases for HAL-Base, WPILib and NetworkTables. Also
                        // ignoring any other HAL files.
                        if (f.Contains("WPILib.dll"))
                        {
                            AppendToTop("Found WPILib");
                            WPILibFile = new DeployFile(f, true, false);
                            wpilibFound.Checked = true;
                            deployFiles.Add(WPILibFile);
                            continue;
                        }
                        if (f.Contains("HAL-Base.dll"))
                        {
                            AppendToTop("Found HAL Base");
                            HALBaseFile = new DeployFile(f, true, false);
                            HALBaseFound.Checked = true;
                            deployFiles.Add(HALBaseFile);
                            continue;
                        }
                        if (f.Contains("NetworkTablesDotNet.dll"))
                        {
                            AppendToTop("Found Network Tables");
                            NetworkTablesFile = new DeployFile(f, true, false);
                            networkTablesFound.Checked = true;
                            deployFiles.Add(NetworkTablesFile);
                            continue;
                        }
                        if (f.Contains("HAL-RoboRIO.dll"))
                        {
                            AppendToTop("Found HAL RoboRIO");
                            HALRoboRIOFile = new DeployFile(f, true, false);
                            halRoboRIOFound.Checked = true;
                            deployFiles.Add(HALRoboRIOFile);
                            continue;
                        }
                        if (f.Contains("HAL"))
                        {
                            continue;
                        }
                        DeployFile file = new DeployFile(f, false, false);
                        deployFiles.Add(file);
                        otherFiles.Add(file);

                    }
                    else
                    {
                        //Adding any other files
                        DeployFile file = new DeployFile(f, false, false);
                        otherFiles.Add(file);
                        deployFiles.Add(file);
                    }
                }

                //Reset the data sources.
                otherFilesTextBox.DataSource = null;
                otherFilesTextBox.DataSource = otherFiles;
                otherFilesTextBox.DisplayMember = "FileName";

            }
        }

        //Browse for the folder. 
        //I want to make this look like the better folder dialog, but that is going to require
        //Some custom code.
        private void codeDirectoryButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog { ShowNewFolderButton = false };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                codeDirectory.Text = dialog.SelectedPath;
            }
        }

        private void CheckIfReady(object sender, EventArgs e)
        {
            if (wpilibFound.Checked && HALBaseFound.Checked && networkTablesFound.Checked && robotFileFound.Checked && connectionManager.Connected && halRoboRIOFound.Checked)
            {
                deployButton.Enabled = true;
                if (cmdArguments.ContainsKey("autodeploy"))
                {
                    deployButton_Click(null, null);
                }
            }
            else
            {
                deployButton.Enabled = false;
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            AppendToTop("Connecting to RoboRIO...");
            connectButton.Enabled = false;
            connectionManager.Connect(teamNumber.Text);
        }

        private void deployButton_Click(object sender, EventArgs e)
        {
            AppendToTop("Deploying robot code...");
            deployButton.Enabled = false;
            deployManager.Deploy(deployFiles);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        public static void AppendToTop(string message)
        {
            Action action = () =>
            {
                statusWindow.AppendText(message + "\n");
            };
            statusWindow.Invoke(action);
        }

        

    }
}
