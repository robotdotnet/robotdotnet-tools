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

namespace DeployRobot
{
    public partial class Main : Form
    {
        Dictionary<string, string> cmdArguments;

        const string TEAM_NUMBER_FILE = "team.txt";
        const string USB_IP = "172.22.11.2";

        public Main()
        {
            InitializeComponent();
            ArgParse.AddArgument("ac", "autoconnect", false, true, "Use if asked to AutoConnect to Robot.");
            ArgParse.AddArgument("ad", "autodeploy", false, "Use to autodeploy code to the robot.");
            ArgParse.AddArgument("hal", "skip-hal", false, true, "Use to check skip the check for the newest HAL Version.");
            ArgParse.AddArgument("team", "teamnumber", false, "Add Team Number");

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
    }
}
