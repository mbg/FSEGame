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
        public LevelPropertiesDialog()
        {
            InitializeComponent();
        }

        private void LevelPropertiesDialog_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"FSEGame\Scripts\");

            foreach (FileInfo file in dir.GetFiles("*.lua"))
            {
                this.kryptonComboBox1.Items.Add(file.Name);
            }
        }
    }
}
