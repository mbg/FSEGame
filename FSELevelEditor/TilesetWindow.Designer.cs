namespace FSELevelEditor
{
    partial class TilesetWindow
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
            this.backgroundPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tilesetComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.headerPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.sidebarPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.explorerPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.nameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tilesetExplorer1 = new FSEGameEditorEngine.TilesetExplorer();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.passableLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.animatedLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.framesLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.animationSpeedLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).BeginInit();
            this.backgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tilesetComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).BeginInit();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sidebarPanel)).BeginInit();
            this.sidebarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerPanel)).BeginInit();
            this.explorerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Controls.Add(this.explorerPanel);
            this.backgroundPanel.Controls.Add(this.sidebarPanel);
            this.backgroundPanel.Controls.Add(this.headerPanel);
            this.backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(695, 461);
            this.backgroundPanel.TabIndex = 0;
            // 
            // tilesetComboBox
            // 
            this.tilesetComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetComboBox.DropDownWidth = 452;
            this.tilesetComboBox.Location = new System.Drawing.Point(5, 5);
            this.tilesetComboBox.Name = "tilesetComboBox";
            this.tilesetComboBox.Size = new System.Drawing.Size(685, 21);
            this.tilesetComboBox.TabIndex = 1;
            this.tilesetComboBox.SelectedIndexChanged += new System.EventHandler(this.tilsetComboBox_SelectedIndexChanged);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.tilesetComboBox);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(5);
            this.headerPanel.Size = new System.Drawing.Size(695, 34);
            this.headerPanel.TabIndex = 3;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.Controls.Add(this.animationSpeedLabel);
            this.sidebarPanel.Controls.Add(this.framesLabel);
            this.sidebarPanel.Controls.Add(this.animatedLabel);
            this.sidebarPanel.Controls.Add(this.passableLabel);
            this.sidebarPanel.Controls.Add(this.kryptonLabel4);
            this.sidebarPanel.Controls.Add(this.kryptonLabel3);
            this.sidebarPanel.Controls.Add(this.kryptonLabel2);
            this.sidebarPanel.Controls.Add(this.kryptonLabel1);
            this.sidebarPanel.Controls.Add(this.nameLabel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidebarPanel.Location = new System.Drawing.Point(490, 34);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(205, 427);
            this.sidebarPanel.TabIndex = 4;
            // 
            // explorerPanel
            // 
            this.explorerPanel.Controls.Add(this.tilesetExplorer1);
            this.explorerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerPanel.Location = new System.Drawing.Point(0, 34);
            this.explorerPanel.Name = "explorerPanel";
            this.explorerPanel.Size = new System.Drawing.Size(490, 427);
            this.explorerPanel.TabIndex = 5;
            // 
            // nameLabel
            // 
            this.nameLabel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.nameLabel.Location = new System.Drawing.Point(6, 6);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(103, 26);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Values.Text = "No tile selected";
            // 
            // tilesetExplorer1
            // 
            this.tilesetExplorer1.ActiveTileset = null;
            this.tilesetExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetExplorer1.Location = new System.Drawing.Point(0, 0);
            this.tilesetExplorer1.Name = "tilesetExplorer1";
            this.tilesetExplorer1.Size = new System.Drawing.Size(490, 427);
            this.tilesetExplorer1.TabIndex = 2;
            this.tilesetExplorer1.Text = "tilesetExplorer1";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(6, 64);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Animated: ";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(6, 90);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(52, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Frames: ";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(6, 38);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(60, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "Passable: ";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(6, 116);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "Anim. Speed: ";
            // 
            // passableLabel
            // 
            this.passableLabel.AutoSize = false;
            this.passableLabel.Location = new System.Drawing.Point(105, 38);
            this.passableLabel.Name = "passableLabel";
            this.passableLabel.Size = new System.Drawing.Size(90, 20);
            this.passableLabel.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.passableLabel.TabIndex = 5;
            this.passableLabel.Values.Text = "-";
            // 
            // animatedLabel
            // 
            this.animatedLabel.AutoSize = false;
            this.animatedLabel.Location = new System.Drawing.Point(105, 64);
            this.animatedLabel.Name = "animatedLabel";
            this.animatedLabel.Size = new System.Drawing.Size(90, 20);
            this.animatedLabel.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.animatedLabel.TabIndex = 6;
            this.animatedLabel.Values.Text = "-";
            // 
            // framesLabel
            // 
            this.framesLabel.AutoSize = false;
            this.framesLabel.Location = new System.Drawing.Point(105, 90);
            this.framesLabel.Name = "framesLabel";
            this.framesLabel.Size = new System.Drawing.Size(90, 20);
            this.framesLabel.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.framesLabel.TabIndex = 7;
            this.framesLabel.Values.Text = "-";
            // 
            // animationSpeedLabel
            // 
            this.animationSpeedLabel.AutoSize = false;
            this.animationSpeedLabel.Location = new System.Drawing.Point(105, 116);
            this.animationSpeedLabel.Name = "animationSpeedLabel";
            this.animationSpeedLabel.Size = new System.Drawing.Size(90, 20);
            this.animationSpeedLabel.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.animationSpeedLabel.TabIndex = 8;
            this.animationSpeedLabel.Values.Text = "-";
            // 
            // TilesetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 461);
            this.Controls.Add(this.backgroundPanel);
            this.Name = "TilesetWindow";
            this.Text = "Tileset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TilesetWindow_FormClosing);
            this.Load += new System.EventHandler(this.TilesetWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).EndInit();
            this.backgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tilesetComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).EndInit();
            this.headerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sidebarPanel)).EndInit();
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerPanel)).EndInit();
            this.explorerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel backgroundPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox tilesetComboBox;
        private FSEGameEditorEngine.TilesetExplorer tilesetExplorer1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel headerPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel explorerPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel sidebarPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel nameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel animationSpeedLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel framesLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel animatedLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel passableLabel;
    }
}