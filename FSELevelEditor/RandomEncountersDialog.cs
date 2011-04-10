using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSELevelEditor
{
    public partial class RandomEncountersDialog : Form
    {
        private MainWindow mainWindow;

        public RandomEncountersDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void RandomEncountersDialog_Load(object sender, EventArgs e)
        {
            foreach (String filename in 
                this.mainWindow.LevelEditor.CurrentLevel.RandomEncountersFiles)
            {
                this.kryptonListBox1.Items.Add(filename);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Remove
        /// <summary>
        /// Removes the currently selected item from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (this.kryptonListBox1.SelectedIndex >= 0)
            {
                this.kryptonListBox1.Items.Remove(
                    this.kryptonListBox1.SelectedItem);
                this.mainWindow.LevelEditor.CurrentLevel.RandomEncountersFiles.Remove(
                    (String)this.kryptonListBox1.SelectedItem);
            }
        }
        #endregion
    }
}
