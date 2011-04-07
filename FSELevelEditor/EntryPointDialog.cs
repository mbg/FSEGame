using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSEGame.Engine;

namespace FSELevelEditor
{
    public partial class EntryPointDialog : Form
    {
        public String SelectedEntryPoint
        {
            get
            {
                return this.entryPointComboBox.Text;
            }
        }

        public EntryPointDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            foreach (LevelEntryPoint ep in 
                mainWindow.LevelEditor.CurrentLevel.EntryPoints)
            {
                this.entryPointComboBox.Items.Add(ep.Name);
            }

            this.entryPointComboBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
