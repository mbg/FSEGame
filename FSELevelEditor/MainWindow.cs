using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSEGame;

namespace FSELevelEditor
{
    public partial class MainWindow : Form
    {
        private TilesetWindow tilesetWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.tilesetWindow = new TilesetWindow();
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.tilesetWindow.Show(this);
        }
    }
}
