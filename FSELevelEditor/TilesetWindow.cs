using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSEGameEditorEngine;
using FSEGame.Engine;

namespace FSELevelEditor
{
    public partial class TilesetWindow : Form
    {
        private TilesetManager tilesetManager;

        public TilesetWindow()
        {
            InitializeComponent();

            this.tilesetExplorer1.ContentManager.RootDirectory = "FSEGame";
            this.tilesetManager = new TilesetManager(this.tilesetExplorer1.ContentManager);
        }

        private void TilesetWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void TilesetWindow_Load(object sender, EventArgs e)
        {
            this.tilesetManager.LoadTilesets();

            foreach (Tileset t in this.tilesetManager.Tilesets)
            {
                this.tilesetComboBox.Items.Add(String.Format(
                    "{0} [{1}]", t.Name, t.Resource));
            }

            if (this.tilesetManager.Tilesets.Count > 0)
            {
                this.tilesetComboBox.SelectedIndex = 0;
            }

            Application.Idle += delegate 
            {
                if (this.Visible)
                {
                    this.tilesetExplorer1.Invalidate();
                }
            };
        }

        private void tilsetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tilesetExplorer1.ActiveTileset =
                this.tilesetManager.Tilesets[this.tilesetComboBox.SelectedIndex];
            this.tilesetExplorer1.Invalidate();
        }
    }
}
