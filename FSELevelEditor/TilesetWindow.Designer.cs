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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tilesetComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tilesetExplorer1 = new FSEGameEditorEngine.TilesetExplorer();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tilesetComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tilesetExplorer1);
            this.kryptonPanel1.Controls.Add(this.tilesetComboBox);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(530, 456);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tilesetComboBox
            // 
            this.tilesetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetComboBox.DropDownWidth = 452;
            this.tilesetComboBox.Location = new System.Drawing.Point(66, 12);
            this.tilesetComboBox.Name = "tilesetComboBox";
            this.tilesetComboBox.Size = new System.Drawing.Size(452, 21);
            this.tilesetComboBox.TabIndex = 1;
            this.tilesetComboBox.SelectedIndexChanged += new System.EventHandler(this.tilsetComboBox_SelectedIndexChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Tileset:";
            // 
            // tilesetExplorer1
            // 
            this.tilesetExplorer1.ActiveTileset = null;
            this.tilesetExplorer1.Location = new System.Drawing.Point(12, 39);
            this.tilesetExplorer1.Name = "tilesetExplorer1";
            this.tilesetExplorer1.Size = new System.Drawing.Size(506, 405);
            this.tilesetExplorer1.TabIndex = 2;
            this.tilesetExplorer1.Text = "tilesetExplorer1";
            // 
            // TilesetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 456);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TilesetWindow";
            this.Text = "Tileset";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TilesetWindow_FormClosing);
            this.Load += new System.EventHandler(this.TilesetWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tilesetComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox tilesetComboBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private FSEGameEditorEngine.TilesetExplorer tilesetExplorer1;
    }
}