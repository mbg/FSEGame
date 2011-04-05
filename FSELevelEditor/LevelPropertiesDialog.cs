using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FSELevelEditor
{
    public partial class LevelPropertiesDialog : Form
    {
        #region Instance Members
        private MainWindow mainWindow;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public LevelPropertiesDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }
        #endregion

        private void LevelPropertiesDialog_Load(object sender, EventArgs e)
        {
            this.nameTextBox.Text = this.mainWindow.LevelEditor.CurrentLevel.Name;
            this.scriptComboBox.Text = this.mainWindow.LevelEditor.CurrentLevel.ScriptFilename;

            // :: Load a list of scripts into the combobox's items.
            DirectoryInfo dir = new DirectoryInfo(@"FSEGame\Scripts\");

            foreach (FileInfo file in dir.GetFiles("*.lua"))
            {
                this.scriptComboBox.Items.Add(file.Name);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.mainWindow.LevelEditor.CurrentLevel.Name = this.nameTextBox.Text;
            this.mainWindow.LevelEditor.CurrentLevel.ScriptFilename = this.scriptComboBox.Text;

            this.Close();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
