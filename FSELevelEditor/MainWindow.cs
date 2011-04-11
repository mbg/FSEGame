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
using System.Xml;

namespace FSELevelEditor
{
    public partial class MainWindow : Form
    {
        private TilesetWindow tilesetWindow;
        private PropertyWindow propertyWindow;
        private ActorBrowserWindow actorBrowserWindow;
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

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.levelEditor.ContentManager.RootDirectory = "FSEGame";
            this.levelEditor.ObjectSelected += new ObjectSelectedDelegate(LevelObjectSelected);

            this.tilesetManager = new TilesetManager(this.levelEditor.ContentManager);
            this.levelEditor.TilesetManager = this.tilesetManager;
        }
        #endregion

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

            this.propertyWindow = new PropertyWindow(this);
            this.actorBrowserWindow = new ActorBrowserWindow();

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

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.tilesetWindow.Show(this);
            this.propertyWindow.Show(this);
            this.actorBrowserWindow.Show(this);

            this.RestoreWindowLayout();
        }

        #region New Level
        /// <summary>
        /// Creates a new level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // :: Check whether the current level contains unsaved changes and
            // :: prompt the user to decide how to proceed. If this method returns
            // :: false, the user decided to cancel the operation.
            if (!this.HandleUnsavedData())
                return;

            this.levelEditor.New();
        }
        #endregion

        #region Open Level
        /// <summary>
        /// Opens an existing level from a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // :: Check whether the current level contains unsaved changes and
            // :: prompt the user to decide how to proceed. If this method returns
            // :: false, the user decided to cancel the operation.
            if (!this.HandleUnsavedData())
                return;

            using (OpenFileDialog dia = new OpenFileDialog())
            {
                dia.Filter = "Level File (*.xml)|*.xml";

                if (dia.ShowDialog() == DialogResult.Cancel)
                    return;

                try
                {
                    this.levelEditor.CurrentLevel.Load(dia.FileName);
                    this.levelEditor.ResetView();
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
        #endregion

        #region Save
        /// <summary>
        /// Saves the current level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.levelEditor.CurrentLevel.Filename))
            {
                this.saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                this.levelEditor.CurrentLevel.Save(
                    this.levelEditor.CurrentLevel.Filename, true);
                this.statusLabel.Text = String.Format("Saved level to {0}", 
                    this.levelEditor.CurrentLevel.Filename);
            }
        }
        #endregion

        #region Save As
        /// <summary>
        /// Saves the current level after prompting the user for a filename.
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
                    this.levelEditor.CurrentLevel.Save(dia.FileName, true);
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

        #region Level Properties
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LevelPropertiesDialog dia = new LevelPropertiesDialog(this))
            {
                dia.ShowDialog();
            }
        }
        #endregion

        #region Editor Modes
        /// <summary>
        /// Switches to the tile edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tilesEditModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.Mode = EditorMode.Tiles;

            this.tilesEditModeButton.Checked = true;
            this.actorsEditModeButton.Checked = false;
            this.entryPointsEditModeButton.Checked = false;
        }
        /// <summary>
        /// Switches to the actor edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actorsEditModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.Mode = EditorMode.Actors;

            this.tilesEditModeButton.Checked = false;
            this.actorsEditModeButton.Checked = true;
            this.entryPointsEditModeButton.Checked = false;
        }
        /// <summary>
        /// Switches to the entry point edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryPointsEditModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.Mode = EditorMode.EntryPoints;

            this.tilesEditModeButton.Checked = false;
            this.actorsEditModeButton.Checked = false;
            this.entryPointsEditModeButton.Checked = true;
        }
        #endregion

        #region Edit Modes
        /// <summary>
        /// Switches to the edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.RemoveMode = false;
            this.levelEditor.EditMode = true;
            this.editModeButton.Checked = true;
            this.createModeButton.Checked = false;
            this.removeModeButton.Checked = false;
        }
        /// <summary>
        /// Switches to the create mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createModeButton_Click(object sender, EventArgs e)
        {
            this.levelEditor.EditMode = false;
            this.editModeButton.Checked = false;
            this.createModeButton.Checked = true;
            this.removeModeButton.Checked = false;
        }
        /// <summary>
        /// Switches to the remove mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.levelEditor.EditMode = false;
            this.levelEditor.RemoveMode = true;
            this.editModeButton.Checked = false;
            this.createModeButton.Checked = false;
            this.removeModeButton.Checked = true;
        }
        #endregion

        #region Tool Windows
        /// <summary>
        /// Toggles the visibility of the actors browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actorBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.actorBrowserWindow.Visible = !this.actorBrowserWindow.Visible;
        }
        /// <summary>
        /// Toggles the visibility of the tileset explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tilesetExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tilesetWindow.Visible = !this.tilesetWindow.Visible;
        }
        /// <summary>
        /// Toggles the visibility of the properties window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.propertyWindow.Visible = !this.propertyWindow.Visible;
        }
        #endregion

        #region Overlays
        /// <summary>
        /// Toggles the visibility of the actors overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.actorsToolStripMenuItem.Checked = !this.actorsToolStripMenuItem.Checked;
            this.levelEditor.CurrentLevel.ShowActors = this.actorsToolStripMenuItem.Checked;
        }
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
        /// Verifies that the game executable exists.
        /// </summary>
        /// <returns>Returns true if the game executable exists or false if not.</returns>
        private Boolean VerifyGameExecutableExists()
        {
            if (!File.Exists("FSEGame.exe"))
            {
                MessageBox.Show(
                    "Unable to launch the game, because FSEGame.exe could not be found.",
                    "Level Editor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            return true;
        }
        /// <summary>
        /// Runs the level that is currently being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.VerifyGameExecutableExists())
                return;

            if(this.levelEditor.CurrentLevel.EntryPoints.Count == 0)
            {
                MessageBox.Show(
                    "The level needs to contain at least one entry point.",
                    "Level Editor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            this.levelEditor.CurrentLevel.Save(@"FSEGame\Levels\_EditorTemp.xml", false);

            String entryPoint = this.levelEditor.CurrentLevel.EntryPoints[0].Name;

            if (this.levelEditor.CurrentLevel.EntryPoints.Count > 1)
            {
                using (EntryPointDialog dia = new EntryPointDialog(this))
                {
                    dia.ShowDialog();
                    entryPoint = dia.SelectedEntryPoint;
                }
            }

            Process p = new Process();
            p.EnableRaisingEvents = true;
            p.StartInfo.FileName = "FSEGame.exe";
            p.StartInfo.Arguments = String.Format(@"-l Levels\_EditorTemp.xml -e {0}", entryPoint);
            p.Exited += new EventHandler(RemoveTemporaryLevel);

            p.Start();
        }
        /// <summary>
        /// Removes the temporary level file after the game has finished executing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTemporaryLevel(object sender, EventArgs e)
        {
            if(File.Exists(@"FSEGame\Levels\_EditorTemp.xml"))
                File.Delete(@"FSEGame\Levels\_EditorTemp.xml");
        }
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.VerifyGameExecutableExists())
                return;

            Process.Start("FSEGame.exe");
        }
        #endregion

        #region OnClosing
        /// <summary>
        /// Triggered when the form is being closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.HandleUnsavedData())
            {
                e.Cancel = true;
                return;
            }
            
            this.SaveWindowLayout();
        }
        #endregion

        #region RestoreWindowLayout
        /// <summary>
        /// Restores the window layout from a file.
        /// </summary>
        private void RestoreWindowLayout()
        {
            String path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    @"FSEGame\Editor\WindowLayout.xml");

            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement rootElement = doc.DocumentElement;

                foreach (XmlNode node in rootElement.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                        continue;

                    XmlElement childElement = (XmlElement)node;

                    if (childElement.Name.Equals("MainWindow"))
                    {
                        this.RestoreWindowLayout(childElement, this);
                    }
                    else if (childElement.Name.Equals("TilesetWindow"))
                    {
                        this.RestoreWindowLayout(childElement, this.tilesetWindow);
                    }
                    else if (childElement.Name.Equals("PropertyWindow"))
                    {
                        this.RestoreWindowLayout(childElement, this.propertyWindow);
                    }
                    else if (childElement.Name.Equals("ActorBrowser"))
                    {
                        this.RestoreWindowLayout(childElement, this.actorBrowserWindow);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="window"></param>
        private void RestoreWindowLayout(XmlElement element, Form window)
        {
            window.Left = Convert.ToInt32(element.GetAttribute("Left"));
            window.Top = Convert.ToInt32(element.GetAttribute("Top"));
            window.Width = Convert.ToInt32(element.GetAttribute("Width"));
            window.Height = Convert.ToInt32(element.GetAttribute("Height"));
            window.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), element.GetAttribute("State"));

            if (window.WindowState == FormWindowState.Minimized)
                window.WindowState = FormWindowState.Normal;

            window.Visible = Convert.ToBoolean(element.GetAttribute("Visible"));
        }
        #endregion

        #region SaveWindowLayout
        /// <summary>
        /// Saves the current window layout.
        /// </summary>
        private void SaveWindowLayout()
        {
            try
            {
                String path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    @"FSEGame\Editor");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                XmlDocument doc = new XmlDocument();
                XmlElement rootElement = doc.CreateElement("WindowLayout");

                rootElement.AppendChild(this.SaveWindowLayout(doc, this, "MainWindow"));
                rootElement.AppendChild(this.SaveWindowLayout(doc, this.tilesetWindow, "TilesetWindow"));
                rootElement.AppendChild(this.SaveWindowLayout(doc, this.propertyWindow, "PropertyWindow"));
                rootElement.AppendChild(this.SaveWindowLayout(doc, this.actorBrowserWindow, "ActorBrowser"));

                doc.AppendChild(rootElement);
                doc.Save(File.Open(Path.Combine(path, "WindowLayout.xml"), FileMode.Create));
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="window"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private XmlElement SaveWindowLayout(XmlDocument doc, Form window, String name)
        {
            XmlElement windowElement = doc.CreateElement(name);
            XmlAttribute leftAttribute = doc.CreateAttribute("Left");
            XmlAttribute topAttribute = doc.CreateAttribute("Top");
            XmlAttribute widthAttribute = doc.CreateAttribute("Width");
            XmlAttribute heightAttribute = doc.CreateAttribute("Height");
            XmlAttribute stateAttribute = doc.CreateAttribute("State");
            XmlAttribute visibleAttribute = doc.CreateAttribute("Visible");
            
            if (window.WindowState == FormWindowState.Normal)
            {
                leftAttribute.Value = window.Left.ToString();
                topAttribute.Value = window.Top.ToString();
                widthAttribute.Value = window.Width.ToString();
                heightAttribute.Value = window.Height.ToString();
            }
            else
            {
                leftAttribute.Value = window.RestoreBounds.Left.ToString();
                topAttribute.Value = window.RestoreBounds.Top.ToString();
                widthAttribute.Value = window.RestoreBounds.Width.ToString();
                heightAttribute.Value = window.RestoreBounds.Height.ToString();
            }

            stateAttribute.Value = window.WindowState.ToString();
            visibleAttribute.Value = window.Visible.ToString();

            windowElement.Attributes.Append(leftAttribute);
            windowElement.Attributes.Append(topAttribute);
            windowElement.Attributes.Append(widthAttribute);
            windowElement.Attributes.Append(heightAttribute);
            windowElement.Attributes.Append(stateAttribute);
            windowElement.Attributes.Append(visibleAttribute);

            return windowElement;
        }
        #endregion

        #region Lock Tiles
        /// <summary>
        /// Toggles whether the tiles in the current level should be locked or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lockTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.lockTilesToolStripMenuItem.Checked = !this.lockTilesToolStripMenuItem.Checked;
            this.levelEditor.LockTiles = this.lockTilesToolStripMenuItem.Checked;
        }
        #endregion

        private Boolean HandleUnsavedData()
        {
            if (this.levelEditor.CurrentLevel.Changed)
            {
                DialogResult result = MessageBox.Show(
                    "The current level contains unsaved changes. Would you like to save now?",
                    "Level Editor",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.saveToolStripMenuItem_Click(null, EventArgs.Empty);
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
