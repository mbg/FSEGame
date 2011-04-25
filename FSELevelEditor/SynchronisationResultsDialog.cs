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
    public partial class SynchronisationResultsDialog : Form
    {
        public SynchronisationResultsDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AddMessage(String message)
        {
            this.kryptonListBox1.Items.Add(message);
        }

        public void SetErrorCount(UInt32 errors)
        {
            this.messageLabel.Text = String.Format(this.messageLabel.Text, errors);
        }

        private void kryptonListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.kryptonTextBox1.Text = this.kryptonListBox1.SelectedItem.ToString();
        }
    }
}
