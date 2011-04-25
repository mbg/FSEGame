namespace FSELevelEditor
{
    partial class SynchronisationResultsDialog
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
            this.messageLabel = new ComponentFactory.Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonListBox1 = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.okButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).BeginInit();
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Controls.Add(this.kryptonTextBox1);
            this.backgroundPanel.Controls.Add(this.okButton);
            this.backgroundPanel.Controls.Add(this.kryptonListBox1);
            this.backgroundPanel.Controls.Add(this.messageLabel);
            this.backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(527, 273);
            this.backgroundPanel.TabIndex = 0;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = false;
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.messageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.messageLabel.Location = new System.Drawing.Point(12, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(503, 58);
            this.messageLabel.Text = "Files in the working copy and the build copy have been synchronised. \r\n\r\n{0} erro" +
    "r(s) occurred, see the log below for details.";
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Location = new System.Drawing.Point(12, 70);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(256, 157);
            this.kryptonListBox1.TabIndex = 1;
            this.kryptonListBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonListBox1_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(425, 236);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 25);
            this.okButton.TabIndex = 2;
            this.okButton.Values.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(274, 70);
            this.kryptonTextBox1.Multiline = true;
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.ReadOnly = true;
            this.kryptonTextBox1.Size = new System.Drawing.Size(241, 157);
            this.kryptonTextBox1.TabIndex = 3;
            // 
            // SynchronisationResultsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 273);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SynchronisationResultsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synchronisation Results";
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).EndInit();
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel backgroundPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton okButton;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonWrapLabel messageLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
    }
}