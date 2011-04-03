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

namespace FSELevelEditor
{
    public partial class MainWindow : Form
    {
        private TilesetWindow tilesetWindow;
        private TilesetManager tilesetManager;

        public TilesetManager TilesetManager
        {
            get
            {
                return this.tilesetManager;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.levelEditor.ContentManager.RootDirectory = "FSEGame";
            this.tilesetManager = new TilesetManager(this.levelEditor.ContentManager);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.tilesetManager.LoadTilesets();

            this.tilesetWindow = new TilesetWindow(this);
            this.tilesetWindow.TilesetChanged += new EventHandler<TilesetChangedEventArgs>(tilesetWindow_TilesetChanged);

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
    }
}
