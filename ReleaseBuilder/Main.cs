using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitPantry.AssemblyPatcher;
using BitPantry.AssemblyPatcher.Configuration;
using ReleaseBuilder.Projects;

namespace ReleaseBuilder
{
    public partial class Main : Form
    {
        public static RichTextBox statusWindow; 

        public Main()
        {
            InitializeComponent();

            SuspendLayout();
            statusWindow = new RichTextBox
            {
                BackColor = SystemColors.WindowText,
                ForeColor = SystemColors.Window,
                Location = new Point(579, 13),
                Name = "statusWindow",
                Size = new Size(374, 281),
                TabIndex = 16,
                Text = "",
                HideSelection = false
            };

            Controls.Add(statusWindow);
            ResumeLayout(false);
            PerformLayout();

            StartButton(null, null);

        }

        public static void WriteStatusWindow(string status)
        {
            Action action = () =>
            {
                statusWindow.AppendText(status + "\n");
            };
            statusWindow.Invoke(action);
        }

        private async void StartButton(object o, EventArgs e)
        {
            List<IProject> projects = new List<IProject>();
            NetworkTablesDotNet networkTablesDotNet = new NetworkTablesDotNet();
            WPILib wpi = new WPILib();

            projects.Add(networkTablesDotNet);
            projects.Add(wpi);
            await Task.Run(() => Builder.Run(projects));
        }
    }
}
