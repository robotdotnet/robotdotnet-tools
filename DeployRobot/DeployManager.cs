using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotDotNetBuildTasks;

namespace DeployRobot
{
    public class DeployManager : Manager
    {
        private ConnectionManager m_manager;

        public DeployManager(ref ConnectionManager manager)
        {
            m_manager = manager;
        }

        public bool Deploy(List<DeployFile> files, bool blocking = false)
        {
            if (!m_manager.Connected) return false;
            Thread t = new Thread(() =>
            {
                Main.AppendToTop("Deploying Files.");
                InstallDeployManager.DeployFiles(files.Select(f => f.FileNamePath).ToArray(), "/home/lvuser/mono", m_manager.Connection);

                UploadCode();


                OnTaskComplete();
            });
            t.Start();
            if (blocking)
            {
                t.Join();
            }

            return true;
        }

        static string deployDir = "/home/lvuser";
        static string monoDeployDir = deployDir + "/mono";

        public void UploadCode()
        {
            if (Main.cmdArguments.ContainsKey("netconsole"))
            {
                StartNetConsole();
            }
            string deployedCmd;
            string deployedCmdFrame;
            string extraCmd;
            if (false)
            {
                deployedCmd = "env LD_PRELOAD=/lib/libstdc++.so.6.0.20 /usr/local/frc/bin/netconsole-host mono --debug " + monoDeployDir + "/" + Main.RobotName;
                deployedCmdFrame = "robotDebugCommand";
                extraCmd = "touch /tmp/frcdebug; chown lvuser:ni /tmp/frcdebug";
            }
            else
            {
                deployedCmd = "env LD_PRELOAD=/lib/libstdc++.so.6.0.20 /usr/local/frc/bin/netconsole-host mono " + monoDeployDir + "/" + Main.RobotName;
                deployedCmdFrame = "robotCommand";
                extraCmd = "";
            }

            List<string> commands = new List<string>();
            commands.Add("echo " + deployedCmd + " > " + deployDir + "/" + deployedCmdFrame);

            Main.AppendToTop("Starting Robot Code.");
            InstallDeployManager.RunCommands(commands.ToArray(), m_manager.Connection);

            commands.Clear();

            commands.Add(
                "/bin/bash -ce '[ ! -f /var/local/natinst/log/FRC_UserProgram.log ] || rm -f /var/local/natinst/log/FRC_UserProgram.log;. /etc/profile.d/natinst-path.sh; chown -R lvuser:ni /home/lvuser/py; /usr/local/frc/bin/frcKillRobot.sh -t -r'");
            InstallDeployManager.RunCommands(commands.ToArray(), m_manager.Connection);
        }

        public void StartNetConsole()
        {
            if (Process.GetProcessesByName("NetConsole.exe").Length == 0)
            {
                Main.AppendToTop("Starting netconsole");
                if (File.Exists(@"C:\Program Files (x86)\NetConsole for cRIO\NetConsole.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files (x86)\NetConsole for cRIO\NetConsole.exe");
                        Main.AppendToTop("netconsole started.");
                        return;
                    }
                    catch
                    {
                        Main.AppendToTop("Could not start netconsole");
                        return;
                    }
                }
                if (File.Exists(@"C:\Program Files\NetConsole for cRIO\NetConsole.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files\NetConsole for cRIO\NetConsole.exe");
                        Main.AppendToTop("netconsole started.");
                        return;
                    }
                    catch
                    {
                        Main.AppendToTop("Could not start netconsole");
                        return;
                    }
                }
                Main.AppendToTop("Could not start netconsole");
            }
        }
    }
}
