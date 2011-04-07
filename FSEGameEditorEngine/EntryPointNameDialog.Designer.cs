namespace FSEGameEditorEngine
{
    partial class EntryPointNameDialog
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
            this.okButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonWrapLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonWrapLabel();
            this.entryPointTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cancelButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).BeginInit();
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Controls.Add(this.cancelButton);
            this.backgroundPanel.Controls.Add(this.entryPointTextBox);
            this.backgroundPanel.Controls.Add(this.okButton);
            this.backgroundPanel.Controls.Add(this.kryptonWrapLabel1);
            this.backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(446, 117);
            this.backgroundPanel.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(344, 80);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 25);
            this.okButton.TabIndex = 1;
            this.okButton.Values.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(12, 9);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(422, 35);
            this.kryptonWrapLabel1.Text = "Please enter a name for the new entry point. Note that each entry point must have" +
    " a unique name.";
            // 
            // entryPointTextBox
            // 
            this.entryPointTextBox.Location = new System.Drawing.Point(12, 47);
            this.entryPointTextBox.Name = "entryPointTextBox";
            this.entryPointTextBox.Size = new System.Drawing.Size(422, 20);
            this.entryPointTextBox.TabIndex = 0;
            this.entryPointTextBox.Text = "EntryPoint";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(248, 80);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 25);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Values.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EntryPointNameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 117);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntryPointNameDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Entry Point";
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).EndInit();
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel backgroundPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton cancelButton;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox entryPointTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton okButton;
        private ComponentFactory.Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel1;
    }
}