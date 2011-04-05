using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSEGame;
using FSEGameEditorEngine;
using System.Diagnostics;
using System.IO;

namespace FSELevelEditor
{
    public partial class MainWindow : Form
    {
        private TilesetWindow tilesetWindow;
        private PropertyWindow propertyWindow;
        private TilesetManager tilesetManager;

        #region Properties
        /// <summary>
        /// Gets the tileset manager instance.
        /// </summary>
        public TilesetManager TilesetManager
        {
            get
            {
                return this.tilesetManager;
            }
        }
        /// <summary>
        /// Gets the level editor component.
        /// </summary>
        public LevelEditor LevelEditor
        {
            get
            {
                return this.levelEditor;
            }
        }

        public PropertyWindow PropertyWindow
        {
            get
            {
                return this.propertyWindow;
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.levelEditor.ContentManager.RootDirectory = "FSEGame";
            this.levelEditor.ObjectSelected += new ObjectSelectedDelegate(LevelObjectSelected);

            this.tilesetManager = new TilesetManager(this.levelEditor.ContentManager);
            this.levelEditor.TilesetManager = this.tilesetManager;
        }

        void LevelObjectSelected(object value)
        {
            this.propertyWindow.SelectedObject = value;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.tilesetManager.LoadTilesets();

            this.tilesetWindow = new TilesetWindow(this);
            this.tilesetWindow.TilesetChanged += new EventHandler<TilesetChangedEventArgs>(tilesetWindow_TilesetChanged);

            this.propertyWindow = new PropertyWindow();

            Application.Idle += delegate
            {
                if (this.Visible)
                {
                    this.levelEditor.Invalidate();
                }
            };
        }

        private void tilesetWindow_TilesetChanged(object sender, TilesetChangedEventArgs e)
        {
            this.levelEditor.CurrentLevel.Tileset = e.Tileset;
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.tilesetWindow.Show(this);
            this.propertyWindow.Show(this);
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dia = new OpenFileDialog())
            {
                dia.Filter = "Level File (*.xml)|*.xml";

                if (dia.ShowDialog() == DialogResult.Cancel)
                    return;

                try
                {
                    this.levelEditor.CurrentLevel.Load(dia.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load level:\n" + ex.Message,
                        "Level Editor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        #region Save As
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dia = new SaveFileDialog())
            {
                dia.Filter = "Level File (*.xml)|*.xml";

                if (dia.ShowDialog() == DialogResult.Cancel)
                    return;

                try
                {
                    this.levelEditor.CurrentLevel.Save(dia.FileName);
                    this.statusLabel.Text = String.Format("Saved level to {0}", dia.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save level:\n" + ex.Message,
                        "Level Editor",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LevelPropertiesDialog dia = new LevelPropertiesDialog(this))
            {
                dia.ShowDialog();
            }
        }

        private void editModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.EditMode = true;
            this.editModeButton.Checked = true;
            this.createModeButton.Checked = false;
        }

        private void createModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.EditMode = false;
            this.editModeButton.Checked = false;
            this.createModeButton.Checked = true;
        }

        private void tilesetExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tilesetWindow.Visible = !this.tilesetWindow.Visible;
        }

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.propertyWindow.Visible = !this.propertyWindow.Visible;
        }

        #region Overlays
        /// <summary>
        /// Toggles the visibility of the entry points overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.entryPointsToolStripMenuItem.Checked = !this.entryPointsToolStripMenuItem.Checked;
            this.levelEditor.CurrentLevel.ShowEntryPoints = this.entryPointsToolStripMenuItem.Checked;
        }
        /// <summary>
        /// Toggles the visibility of the events overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.eventsToolStripMenuItem.Checked = !this.eventsToolStripMenuItem.Checked;
            this.levelEditor.CurrentLevel.ShowEvents = this.eventsToolStripMenuItem.Checked;
        }
        /// <summary>
        /// Toggles the visibility of the passable regions overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passableRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.passableRegionsToolStripMenuItem.Checked = !this.passableRegionsToolStripMenuItem.Checked;
            this.levelEditor.CurrentLevel.ShowPassableRegions = this.passableRegionsToolStripMenuItem.Checked;
        }
        /// <summary>
        /// Toggles the visibility of the impassable regions overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void impassableRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.impassableRegionsToolStripMenuItem.Checked = !this.impassableRegionsToolStripMenuItem.Checked;
            this.levelEditor.CurrentLevel.ShowImpassableRegions = this.impassableRegionsToolStripMenuItem.Checked;
        }
        #endregion

        #region Run
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("FSEGame.exe"))
            {
                MessageBox.Show(
                    "Unable to launch the game, because FSEGame.exe could not be found.",
                    "Level Editor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            Process.Start("FSEGame.exe");
        }
        #endregion
    }
}
