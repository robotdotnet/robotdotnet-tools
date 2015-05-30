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
        Dictionary<string, string> cmdArguments;

        const string TEAM_NUMBER_FILE = "team.txt";

        List<DeployFile> otherFiles = new List<DeployFile>();
        List<DeployFile> deployFiles = new List<DeployFile>();
        DeployFile robotFile;
        DeployFile WPILibFile;
        DeployFile HALBaseFile;
        DeployFile NetworkTablesFile;

        private ConnectionManager connectionManager;
        private DeployManager deployManager;
        private HALManager halManager;

        private bool checkedHAL = false;


        public Main()
        {
            InitializeComponent();
            connectionManager = new ConnectionManager();
            deployManager = new DeployManager(ref connectionManager);
            halManager = new HALManager(ref connectionManager);

            ArgParse.AddArgument("nac", "noautoconnect", false, true, "Use to not autoconnect to Robot.");
            ArgParse.AddArgument("ad", "autodeploy", false, "Use to autodeploy code to the robot.");
            ArgParse.AddArgument("hal", "skip-hal", false, true, "Use to check skip the check for the newest HAL Version.");
            ArgParse.AddArgument("team", "teamnumber", false, "Add Team Number");
            ArgParse.AddArgument("debug", "debug", false, "Weither to run debug.");

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
            wpilibFound.CheckedChanged += CheckIfReady;
            robotFileFound.CheckedChanged += CheckIfReady;
            connectionStatus.TextChanged += CheckIfReady;
            updateHAL.CheckedChanged += CheckIfReady;


            codeDirectory.Text = @"C:\Users\thad\Documents\GitHub\WildFireDotNet\Wildfire\bin\Release";//Application.StartupPath;


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
            halManager.TaskComplete += halManager_TaskComplete;
            if (!cmdArguments.ContainsKey("noautoconnect"))
            {
                connectButton.Enabled = false;
                connectionManager.Connect(teamNumber.Text);
            }
            //this.Show();
        }

        void halManager_TaskComplete(object sender, EventArgs e)
        {
            
            Action action = () =>
            {
                string hal = "HAL Version: ";
                if (halManager.Matches)
                {
                    hal += halManager.HALVersion;
                    halNewestVersion.Text = "HAL Newest Version: True";
                    updateHAL.Checked = false;
                }
                else
                {
                    hal += "Not Found";
                    halNewestVersion.Text = "HAL Newest Version: False";
                    updateHAL.Checked = true;
                }
                halLabel.Text = hal;
                checkedHAL = true;
            };
            halLabel.Invoke(action);

        }

        void deployManager_TaskComplete(object sender, EventArgs e)
        {
            Action action = () =>
            {
                deployButton.Enabled = true;
            };
            deployButton.Invoke(action);
        }

        void connectionManager_TaskComplete(object sender, EventArgs e)
        {
            if (connectionManager.Connected)
                halManager.CheckHALVersion(1.0, 326);
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
                connectionStatus.Text = connectionString;
                connectButton.Enabled = true;
            };
            connectionStatus.Invoke(action);
        }



        static string deployDir = "/home/lvuser";
        static string monoDeployDir = deployDir + "/mono";
        static string robotFilename = "robot.exe";

        public void UploadCode()
        {
            string deployedCmd;
            string deployedCmdFrame;
            string extraCmd;
            if (cmdArguments.ContainsKey("debug"))
            {
                deployedCmd = "env LD_PRELOAD=/lib/libstdc++.so.6.0.20 /usr/local/frc/bin/netconsole-host mono --debug " + monoDeployDir + "/" + robotFilename;
                deployedCmdFrame = "robotDebugCommand";
                extraCmd = "touch /tmp/frcdebug; chown lvuser:ni /tmp/frcdebug";
            }
            else
            {
                deployedCmd = "env LD_PRELOAD=/lib/libstdc++.so.6.0.20 /usr/local/frc/bin/netconsole-host mono " + monoDeployDir + "/" + robotFilename;
                deployedCmdFrame = "robotCommand";
                extraCmd = "";
            }



        }

        private void codeDirectory_TextChanged(object sender, EventArgs e)
        {
            deployFiles.Clear();
            otherFiles.Clear();
            HALBaseFound.Checked = false;
            wpilibFound.Checked = false;
            networkTablesFound.Checked = false;
            robotFileFound.Checked = false;
            robotFileNameLabel.Text = "None";
            otherFilesTextBox.DataSource = null;
            otherFilesTextBox.DataSource = otherFiles;
            otherFilesTextBox.DisplayMember = "FileName";
            if (Directory.Exists(codeDirectory.Text))
            {
                //Find Robot Base first (This is so we can figure out the executable.
                Type m_robotBase;
                if (File.Exists(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll"))
                {
                    var asm = Assembly.LoadFrom(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll");
                    try
                    {
                        m_robotBase = asm.GetType("WPILib.RobotBase", true);
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
                    if (f.Contains("pdb") || f.Contains("vshost") || f.Contains(".config") | f.Contains(".manifest"))
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

                        var classes = from t in asm.GetTypes() where m_robotBase.IsAssignableFrom(t) select t;
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
                            robotFile = new DeployFile(f, true, true);
                            robotFileFound.Checked = true;
                            var split = f.Split(Path.DirectorySeparatorChar);
                            robotFileNameLabel.Text = split[split.Length - 1];
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
                            WPILibFile = new DeployFile(f, true, false);
                            wpilibFound.Checked = true;
                            deployFiles.Add(WPILibFile);
                            continue;
                        }
                        if (f.Contains("HAL-Base.dll"))
                        {
                            HALBaseFile = new DeployFile(f, true, false);
                            HALBaseFound.Checked = true;
                            deployFiles.Add(HALBaseFile);
                            continue;
                        }
                        if (f.Contains("NetworkTablesDotNet.dll"))
                        {
                            NetworkTablesFile = new DeployFile(f, true, false);
                            networkTablesFound.Checked = true;
                            deployFiles.Add(NetworkTablesFile);
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
            if (wpilibFound.Checked && HALBaseFound.Checked && networkTablesFound.Checked && robotFileFound.Checked && connectionManager.Connected && checkedHAL)
            {
                deployButton.Enabled = true;
            }
            else
            {
                deployButton.Enabled = false;
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            connectionManager.Connect(teamNumber.Text);
        }

        private void deployButton_Click(object sender, EventArgs e)
        {
            deployButton.Enabled = false;
            deployManager.Deploy(deployFiles);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
