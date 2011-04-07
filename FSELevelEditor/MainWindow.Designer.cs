namespace FSELevelEditor
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.springLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.positionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entryPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.passableRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impassableRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tilesetExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogueEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.editorToolStrip = new System.Windows.Forms.ToolStrip();
            this.tilesEditModeButton = new System.Windows.Forms.ToolStripButton();
            this.actorsEditModeButton = new System.Windows.Forms.ToolStripButton();
            this.entryPointsEditModeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createModeButton = new System.Windows.Forms.ToolStripButton();
            this.editModeButton = new System.Windows.Forms.ToolStripButton();
            this.lockTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.levelEditor = new FSEGameEditorEngine.LevelEditor();
            this.lcokStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.actorBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.editorToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.springLabel,
            this.lcokStatusLabel,
            this.positionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 538);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(784, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 19);
            this.statusLabel.Text = "Ready";
            // 
            // springLabel
            // 
            this.springLabel.Name = "springLabel";
            this.springLabel.Size = new System.Drawing.Size(621, 19);
            this.springLabel.Spring = true;
            // 
            // positionLabel
            // 
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(22, 19);
            this.positionLabel.Text = "0,0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLevelToolStripMenuItem,
            this.openLevelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.openLevelToolStripMenuItem.Text = "Open Level";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(130, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overlaysToolStripMenuItem,
            this.toolStripMenuItem3,
            this.actorBrowserToolStripMenuItem,
            this.tilesetExplorerToolStripMenuItem,
            this.propertiesToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // overlaysToolStripMenuItem
            // 
            this.overlaysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actorsToolStripMenuItem,
            this.entryPointsToolStripMenuItem,
            this.eventsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.passableRegionsToolStripMenuItem,
            this.impassableRegionsToolStripMenuItem});
            this.overlaysToolStripMenuItem.Name = "overlaysToolStripMenuItem";
            this.overlaysToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.overlaysToolStripMenuItem.Text = "Overlays";
            // 
            // actorsToolStripMenuItem
            // 
            this.actorsToolStripMenuItem.Checked = true;
            this.actorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.actorsToolStripMenuItem.Name = "actorsToolStripMenuItem";
            this.actorsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.actorsToolStripMenuItem.Text = "Actors";
            this.actorsToolStripMenuItem.Click += new System.EventHandler(this.actorsToolStripMenuItem_Click);
            // 
            // entryPointsToolStripMenuItem
            // 
            this.entryPointsToolStripMenuItem.Checked = true;
            this.entryPointsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.entryPointsToolStripMenuItem.Name = "entryPointsToolStripMenuItem";
            this.entryPointsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.entryPointsToolStripMenuItem.Text = "Entry Points";
            this.entryPointsToolStripMenuItem.Click += new System.EventHandler(this.entryPointsToolStripMenuItem_Click);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.Checked = true;
            this.eventsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.eventsToolStripMenuItem.Text = "Events";
            this.eventsToolStripMenuItem.Click += new System.EventHandler(this.eventsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(175, 6);
            // 
            // passableRegionsToolStripMenuItem
            // 
            this.passableRegionsToolStripMenuItem.Name = "passableRegionsToolStripMenuItem";
            this.passableRegionsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.passableRegionsToolStripMenuItem.Text = "Passable Regions";
            this.passableRegionsToolStripMenuItem.Click += new System.EventHandler(this.passableRegionsToolStripMenuItem_Click);
            // 
            // impassableRegionsToolStripMenuItem
            // 
            this.impassableRegionsToolStripMenuItem.Name = "impassableRegionsToolStripMenuItem";
            this.impassableRegionsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.impassableRegionsToolStripMenuItem.Text = "Impassable Regions";
            this.impassableRegionsToolStripMenuItem.Click += new System.EventHandler(this.impassableRegionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(150, 6);
            // 
            // tilesetExplorerToolStripMenuItem
            // 
            this.tilesetExplorerToolStripMenuItem.Image = global::FSELevelEditor.Properties.Resources._037_Colorize_16x16_72;
            this.tilesetExplorerToolStripMenuItem.Name = "tilesetExplorerToolStripMenuItem";
            this.tilesetExplorerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.tilesetExplorerToolStripMenuItem.Text = "Tileset Explorer";
            this.tilesetExplorerToolStripMenuItem.Click += new System.EventHandler(this.tilesetExplorerToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem1
            // 
            this.propertiesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("propertiesToolStripMenuItem1.Image")));
            this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
            this.propertiesToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.propertiesToolStripMenuItem1.Text = "Properties";
            this.propertiesToolStripMenuItem1.Click += new System.EventHandler(this.propertiesToolStripMenuItem1_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockTilesToolStripMenuItem,
            this.toolStripMenuItem5,
            this.propertiesToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = global::FSELevelEditor.Properties.Resources.Properties;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runLevelToolStripMenuItem,
            this.runToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Image = global::FSELevelEditor.Properties.Resources.PlayHS;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialogueEditorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // dialogueEditorToolStripMenuItem
            // 
            this.dialogueEditorToolStripMenuItem.Name = "dialogueEditorToolStripMenuItem";
            this.dialogueEditorToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.dialogueEditorToolStripMenuItem.Text = "Dialogue Editor";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.levelEditor);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 489);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(784, 514);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.editorToolStrip);
            // 
            // editorToolStrip
            // 
            this.editorToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.editorToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.editorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilesEditModeButton,
            this.actorsEditModeButton,
            this.entryPointsEditModeButton,
            this.toolStripSeparator1,
            this.createModeButton,
            this.editModeButton});
            this.editorToolStrip.Location = new System.Drawing.Point(3, 0);
            this.editorToolStrip.Name = "editorToolStrip";
            this.editorToolStrip.Size = new System.Drawing.Size(328, 25);
            this.editorToolStrip.TabIndex = 0;
            // 
            // tilesEditModeButton
            // 
            this.tilesEditModeButton.Checked = true;
            this.tilesEditModeButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tilesEditModeButton.Image = global::FSELevelEditor.Properties.Resources._037_Colorize_16x16_72;
            this.tilesEditModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesEditModeButton.Name = "tilesEditModeButton";
            this.tilesEditModeButton.Size = new System.Drawing.Size(51, 22);
            this.tilesEditModeButton.Text = "Tiles";
            this.tilesEditModeButton.Click += new System.EventHandler(this.tilesEditModeButton_Click);
            // 
            // actorsEditModeButton
            // 
            this.actorsEditModeButton.Image = global::FSELevelEditor.Properties.Resources._1683_Lightbulb_16x16;
            this.actorsEditModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actorsEditModeButton.Name = "actorsEditModeButton";
            this.actorsEditModeButton.Size = new System.Drawing.Size(61, 22);
            this.actorsEditModeButton.Text = "Actors";
            this.actorsEditModeButton.Click += new System.EventHandler(this.actorsEditModeButton_Click);
            // 
            // entryPointsEditModeButton
            // 
            this.entryPointsEditModeButton.Image = global::FSELevelEditor.Properties.Resources._1532_Flag_system_Blue;
            this.entryPointsEditModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.entryPointsEditModeButton.Name = "entryPointsEditModeButton";
            this.entryPointsEditModeButton.Size = new System.Drawing.Size(90, 22);
            this.entryPointsEditModeButton.Text = "Entry Points";
            this.entryPointsEditModeButton.Click += new System.EventHandler(this.entryPointsEditModeButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // createModeButton
            // 
            this.createModeButton.Checked = true;
            this.createModeButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createModeButton.Image = global::FSELevelEditor.Properties.Resources.InsertPictureHS;
            this.createModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createModeButton.Name = "createModeButton";
            this.createModeButton.Size = new System.Drawing.Size(61, 22);
            this.createModeButton.Text = "Create";
            this.createModeButton.Click += new System.EventHandler(this.createModeButton_Click);
            // 
            // editModeButton
            // 
            this.editModeButton.Image = ((System.Drawing.Image)(resources.GetObject("editModeButton.Image")));
            this.editModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editModeButton.Name = "editModeButton";
            this.editModeButton.Size = new System.Drawing.Size(47, 22);
            this.editModeButton.Text = "Edit";
            this.editModeButton.Click += new System.EventHandler(this.editModeButton_Click);
            // 
            // lockTilesToolStripMenuItem
            // 
            this.lockTilesToolStripMenuItem.Name = "lockTilesToolStripMenuItem";
            this.lockTilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.lockTilesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.lockTilesToolStripMenuItem.Text = "Lock Tiles";
            this.lockTilesToolStripMenuItem.Click += new System.EventHandler(this.lockTilesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(163, 6);
            // 
            // levelEditor
            // 
            this.levelEditor.AllowDrop = true;
            this.levelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelEditor.EditMode = false;
            this.levelEditor.Location = new System.Drawing.Point(0, 0);
            this.levelEditor.LockTiles = false;
            this.levelEditor.Mode = FSEGameEditorEngine.EditorMode.Tiles;
            this.levelEditor.Name = "levelEditor";
            this.levelEditor.SelectedTile = null;
            this.levelEditor.Size = new System.Drawing.Size(784, 489);
            this.levelEditor.TabIndex = 0;
            this.levelEditor.Text = "levelEditor1";
            this.levelEditor.TilesetManager = null;
            // 
            // lcokStatusLabel
            // 
            this.lcokStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.lcokStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lcokStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lcokStatusLabel.Name = "lcokStatusLabel";
            this.lcokStatusLabel.Size = new System.Drawing.Size(61, 19);
            this.lcokStatusLabel.Text = "Unlocked";
            // 
            // actorBrowserToolStripMenuItem
            // 
            this.actorBrowserToolStripMenuItem.Image = global::FSELevelEditor.Properties.Resources._1683_Lightbulb_16x16;
            this.actorBrowserToolStripMenuItem.Name = "actorBrowserToolStripMenuItem";
            this.actorBrowserToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.actorBrowserToolStripMenuItem.Text = "Actor Browser";
            this.actorBrowserToolStripMenuItem.Click += new System.EventHandler(this.actorBrowserToolStripMenuItem_Click);
            // 
            // runLevelToolStripMenuItem
            // 
            this.runLevelToolStripMenuItem.Image = global::FSELevelEditor.Properties.Resources.PlayHS;
            this.runLevelToolStripMenuItem.Name = "runLevelToolStripMenuItem";
            this.runLevelToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runLevelToolStripMenuItem.Text = "Run level";
            this.runLevelToolStripMenuItem.Click += new System.EventHandler(this.runLevelToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Level Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.editorToolStrip.ResumeLayout(false);
            this.editorToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip editorToolStrip;
        private FSEGameEditorEngine.LevelEditor levelEditor;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel springLabel;
        private System.Windows.Forms.ToolStripStatusLabel positionLabel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton tilesEditModeButton;
        private System.Windows.Forms.ToolStripButton actorsEditModeButton;
        private System.Windows.Forms.ToolStripButton entryPointsEditModeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton createModeButton;
        private System.Windows.Forms.ToolStripButton editModeButton;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overlaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entryPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tilesetExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem passableRegionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impassableRegionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogueEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripStatusLabel lcokStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem actorBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runLevelToolStripMenuItem;
    }
}

