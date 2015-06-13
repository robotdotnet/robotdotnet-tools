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
            checkedListBox1.Items.Add(new NetworkTablesDotNet());
            checkedListBox1.Items.Add(new WPILib());
            checkedListBox1.Items.Add(new Extension());

            //StartButton(null, null);

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

            //List<IProject> projects = new List<IProject>();
            //NetworkTablesDotNet networkTablesDotNet = new NetworkTablesDotNet();
            //WPILib wpi = new WPILib();

            //projects.Add(networkTablesDotNet);
            //projects.Add(wpi);
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> vers = new Dictionary<string, string>();
            vers.Add("NT", "2015.0.1");
            vers.Add("WPI", "2015.0.1");
            vers.Add("EXT", "2015.0.1");

            List<CheckedProject> projects = new List<CheckedProject>();

            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                projects.Add(new CheckedProject((IProject)checkedItem, true));
            }

            foreach (var item in checkedListBox1.Items)
            {
                if (!checkedListBox1.CheckedItems.Contains(item))
                {
                    projects.Add(new CheckedProject((IProject)item, false));
                }
            }

            //List<Builder.CheckedProject> projects = (from object item in checkedListBox1.CheckedItems select (new Builder.CheckedProject((IProject)item, true))).ToList();

            //projects.AddRange(from object item in checkedListBox1.Items where !checkedListBox1.SelectedItems.Contains(item) select new Builder.CheckedProject((IProject)item, false));
            await Task.Run(() => Builder.Run(projects, vers));
        }
    }
}
