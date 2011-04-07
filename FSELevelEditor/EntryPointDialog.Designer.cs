namespace FSELevelEditor
{
    partial class EntryPointDialog
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
            this.kryptonWrapLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonWrapLabel();
            this.entryPointComboBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.okButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entryPointComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.okButton);
            this.kryptonPanel1.Controls.Add(this.entryPointComboBox);
            this.kryptonPanel1.Controls.Add(this.kryptonWrapLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(446, 117);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(12, 9);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(422, 35);
            this.kryptonWrapLabel1.Text = "There are multiple entry points in this level. Choose one from the list below whi" +
    "ch should be used as initial position for the player pawn.";
            // 
            // entryPointComboBox
            // 
            this.entryPointComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entryPointComboBox.DropDownWidth = 422;
            this.entryPointComboBox.Location = new System.Drawing.Point(12, 47);
            this.entryPointComboBox.Name = "entryPointComboBox";
            this.entryPointComboBox.Size = new System.Drawing.Size(422, 21);
            this.entryPointComboBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(344, 80);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 25);
            this.okButton.TabIndex = 0;
            this.okButton.Values.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // EntryPointDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 117);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntryPointDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Entry Point";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entryPointComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton okButton;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox entryPointComboBox;
        private ComponentFactory.Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel1;
    }
}