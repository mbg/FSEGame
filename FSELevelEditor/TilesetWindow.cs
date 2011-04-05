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

            this.tilesetExplorer1.TileSelected += new EventHandler<EventArgs>(TileSelected);
        }

        private void TileSelected(object sender, EventArgs e)
        {
            Tile selectedTile = this.tilesetExplorer1.SelectedTile;

            if (selectedTile != null)
            {
                this.nameLabel.Text = selectedTile.Name;
                this.passableLabel.Text = selectedTile.Passable.ToString();
                this.animatedLabel.Text = selectedTile.Animated.ToString();
                this.framesLabel.Text = selectedTile.Frames.Count.ToString();
                this.animationSpeedLabel.Text = String.Format("{0}s", selectedTile.Speed);
            }
            else
            {
                this.nameLabel.Text = "No tile selected";
                this.passableLabel.Text = "-";
                this.animatedLabel.Text = "-";
                this.framesLabel.Text = "-";
                this.animationSpeedLabel.Text = "-";
            }

            this.mainWindow.LevelEditor.SelectedTile = selectedTile;
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
