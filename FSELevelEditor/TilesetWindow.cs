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
        private MainWindow mainWindow;

        #region Events
        public event EventHandler<TilesetChangedEventArgs> TilesetChanged;
        #endregion

        public TilesetWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
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
            

            foreach (Tileset t in this.mainWindow.TilesetManager.Tilesets)
            {
                this.tilesetComboBox.Items.Add(String.Format(
                    "{0} [{1}]", t.Name, t.Resource));
            }

            if (this.mainWindow.TilesetManager.Tilesets.Count > 0)
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
                this.mainWindow.TilesetManager.Tilesets[this.tilesetComboBox.SelectedIndex];
            this.tilesetExplorer1.Invalidate();

            if (this.TilesetChanged != null)
                this.TilesetChanged(this, new TilesetChangedEventArgs(this.tilesetExplorer1.ActiveTileset));
        }
    }
}
