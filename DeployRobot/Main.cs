using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DeployRobot
{
    public partial class Main : Form
    {
        Dictionary<string, string> cmdArguments;

        const string TEAM_NUMBER_FILE = "team.txt";
        const string USB_IP = "172.22.11.2";

        List<DeployFile> otherFiles = new List<DeployFile>();
        List<DeployFile> deployFiles = new List<DeployFile>();
        DeployFile robotFile;
        DeployFile WPILibFile;
        DeployFile HALBaseFile;
        DeployFile NetworkTablesFile;


        public Main()
        {
            InitializeComponent();
            ArgParse.AddArgument("ac", "autoconnect", false, true, "Use if asked to AutoConnect to Robot.");
            ArgParse.AddArgument("ad", "autodeploy", false, "Use to autodeploy code to the robot.");
            ArgParse.AddArgument("hal", "skip-hal", false, true, "Use to check skip the check for the newest HAL Version.");
            ArgParse.AddArgument("team", "teamnumber", false, "Add Team Number");
            ArgParse.AddArgument("debug", "debug", false, "Weither to run debug.");

            string[] args = Environment.GetCommandLineArgs();

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
                        teamNumber.Text = fileRead[0].Substring(fileRead[0].IndexOf(':') + 1, fileRead[0].Length - (fileRead[0].IndexOf(':')+ 1)).Trim();
                    }
                }
            }

            wpilibFound.CheckedChanged += checkIfReady;
            networkTablesFound.CheckedChanged += checkIfReady;
            wpilibFound.CheckedChanged += checkIfReady;
            robotFileFound.CheckedChanged += checkIfReady;


            codeDirectory.Text = @"C:\Users\Thad\Documents\Visual Studio 2013\Projects\RobotTest\RobotTest\bin\Debug";//Application.StartupPath;
            
            //Figure out how to get data source to automaticalyl update.
            otherFilesTextBox.DataSource = otherFiles;
            otherFilesTextBox.DisplayMember = "FileName";

            otherFilesTextBox.Refresh();
            


            if (cmdArguments.ContainsKey("autoconnect"))
            {
                //Run Connect
            }
            if (cmdArguments.ContainsKey("autodeploy"))
            {
                //Run Connect
                //Run Deploy
            }
        }

        public void Connect()
        {
            string ipAddr;
            if (isUSBConnected.Checked)
                ipAddr = USB_IP;
            else
            {
                //Check Team number to make sure it is valid
                ipAddr = ("roborio-" + teamNumber.Text + ".local");
            }


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

        Type robotBase = null;

        private void codeDirectory_TextChanged(object sender, EventArgs e)
        {
            deployFiles.Clear();
            otherFiles.Clear();
            HALBaseFound.Checked = false;
            wpilibFound.Checked = false;
            networkTablesFound.Checked = false;
            robotFileFound.Checked = false;
            robotFileNameLabel.Text = "None";
            if (Directory.Exists(codeDirectory.Text))
            {
                //Find Robot Base first (This is so we can figure out the executable.
                if (File.Exists(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll"))
                {
                    var asm = Assembly.LoadFrom(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll");
                    try
                    {
                        var types = asm.GetTypes();
                        var temp = types[0];
                        robotBase = asm.GetType("WPILib.RobotBase", true);
                        //WPILibFile = new DeployFile(codeDirectory.Text + Path.DirectorySeparatorChar + "WPILib.dll", true, false);
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
                    if (f.Contains("pdb") || f.Contains("vshost") || f.Contains(".config") | f.Contains(".manifest"))
                        continue;
                    if (f.Contains(".exe"))
                    {
                        /*
                        string thisName = Assembly.GetExecutingAssembly().GetName().Name;
                        if (f.Contains(thisName))
                        {
                            continue;
                        }
                         * */
                        var asm = Assembly.LoadFrom(f);
                        //Check to see if asm is this assembly
                        //If so skip it.

                        var classes = from t in asm.GetTypes() where robotBase.IsAssignableFrom(t) select t;
                        if (classes.ToList().Count == 0)
                        {
                            DeployFile file = new DeployFile(f, false, false);
                            deployFiles.Add(file);
                            otherFiles.Add(file);
                        }
                        else
                        {
                            robotFile = new DeployFile(f, true, true);
                            robotFileFound.Checked = true;
                            var split = f.Split(Path.DirectorySeparatorChar);
                            robotFileNameLabel.Text = split[split.Length - 1];
                            deployFiles.Add(robotFile);
                        }
                        
                    }
                    else if (f.Contains(".dll"))
                    {
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
                        DeployFile file = new DeployFile(f, false, false);
                        deployFiles.Add(file);
                        otherFiles.Add(file);
                        
                    }
                    else
                    {
                        //Other Files
                        DeployFile file = new DeployFile(f, false ,false);
                        otherFiles.Add(file);
                        deployFiles.Add(file);
                    }
                }


                
            }
        }

        private void codeDirectoryButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                codeDirectory.Text = dialog.SelectedPath;
            }
        }

        private void checkIfReady(object sender, EventArgs e)
        {
            if (wpilibFound.Checked && HALBaseFound.Checked && networkTablesFound.Checked && robotFileFound.Checked)
            {
                deployButton.Enabled = true;
            }
            else
            {
                deployButton.Enabled = false;
            }
        }
    }
}
