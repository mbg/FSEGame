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
    public partial class ActorPropertyEditorForm : Form
    {
        private Dictionary<String, String> properties;
        private Dictionary<String, String> tempProperties;

        public Dictionary<String, String> Properties
        {
            get
            {
                return this.properties;
            }
        }

        public ActorPropertyEditorForm(Dictionary<String, String> properties)
        {
            InitializeComponent();

            this.properties = properties;
            this.tempProperties = new Dictionary<string, string>();

            this.CloneProperties();

            foreach (String key in this.tempProperties.Keys)
            {
                this.propertyListBox.Items.Add(key);
            }

            if (this.propertyListBox.Items.Count > 0)
                this.propertyListBox.SelectedIndex = 0;
        }

        private void CloneProperties()
        {
            foreach(KeyValuePair<String, String> kvp in this.properties)
            {
                this.tempProperties.Add(kvp.Key, kvp.Value);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.properties = this.tempProperties;
            this.Close();
        }

        private void propertyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.propertyListBox.SelectedIndex == -1)
            {
                this.keyLabel.Text = "No property selected";
                this.valueTextBox.Text = String.Empty;
                this.valueTextBox.Enabled = false;

                return;
            }

            String key = (String)this.propertyListBox.Items[this.propertyListBox.SelectedIndex];
            String value = this.tempProperties[key];

            this.keyLabel.Text = key;
            this.valueTextBox.Text = value;
            this.valueTextBox.Enabled = true;
        }

        private void valueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.propertyListBox.SelectedIndex == -1)
                return;

            String key = (String)this.propertyListBox.Items[this.propertyListBox.SelectedIndex];
            this.tempProperties[key] = this.valueTextBox.Text;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (this.propertyListBox.SelectedIndex == -1)
                return;

            String key = (String)this.propertyListBox.Items[this.propertyListBox.SelectedIndex];
            this.tempProperties.Remove(key);
            this.propertyListBox.Items.Remove(this.propertyListBox.SelectedItem);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (AddPropertyForm form = new AddPropertyForm())
            {
                if (form.ShowDialog() == DialogResult.Cancel)
                    return;

                this.tempProperties.Add(form.SelectedName, "");
                this.propertyListBox.Items.Add(form.SelectedName);
            }
        }
    }
}
