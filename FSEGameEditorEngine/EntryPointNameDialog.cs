using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSEGame.Engine;

namespace FSEGameEditorEngine
{
    public partial class EntryPointNameDialog : Form
    {
        private EditorLevel level;

        public String SelectedName
        {
            get
            {
                return this.entryPointTextBox.Text.Trim();
            }
        }

        public EntryPointNameDialog(EditorLevel level)
        {
            InitializeComponent();

            this.level = level;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            String selectedName = this.entryPointTextBox.Text.Trim();

            foreach (LevelEntryPoint ep in this.level.EntryPoints)
            {
                if (selectedName.Equals(ep.Name))
                {
                    MessageBox.Show(
                        "An entry point with the specified name does already exist.",
                        "Level Editor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
