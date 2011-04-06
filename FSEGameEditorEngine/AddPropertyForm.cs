using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSEGameEditorEngine
{
    public partial class AddPropertyForm : Form
    {
        public String SelectedName
        {
            get
            {
                return this.kryptonTextBox1.Text;
            }
        }

        public AddPropertyForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.kryptonTextBox1.Text))
            {
                MessageBox.Show("The name cannot be empty.",
                    "Level Editor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
