namespace FSEGameEditorEngine
{
    partial class ActorPropertyEditorForm
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
            this.removeButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.addButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.valueTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.keyLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.okButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.propertyListBox = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).BeginInit();
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Controls.Add(this.removeButton);
            this.backgroundPanel.Controls.Add(this.addButton);
            this.backgroundPanel.Controls.Add(this.valueTextBox);
            this.backgroundPanel.Controls.Add(this.kryptonLabel2);
            this.backgroundPanel.Controls.Add(this.kryptonButton1);
            this.backgroundPanel.Controls.Add(this.keyLabel);
            this.backgroundPanel.Controls.Add(this.okButton);
            this.backgroundPanel.Controls.Add(this.propertyListBox);
            this.backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(496, 248);
            this.backgroundPanel.TabIndex = 0;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(92, 211);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(89, 25);
            this.removeButton.TabIndex = 7;
            this.removeButton.Values.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 211);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(74, 25);
            this.addButton.TabIndex = 6;
            this.addButton.Values.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // valueTextBox
            // 
            this.valueTextBox.Enabled = false;
            this.valueTextBox.Location = new System.Drawing.Point(237, 47);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(247, 20);
            this.valueTextBox.TabIndex = 5;
            this.valueTextBox.TextChanged += new System.EventHandler(this.valueTextBox_TextChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(187, 47);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(44, 20);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Value:";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(298, 211);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 3;
            this.kryptonButton1.Values.Text = "Cancel";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // keyLabel
            // 
            this.keyLabel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.keyLabel.Location = new System.Drawing.Point(187, 12);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(196, 29);
            this.keyLabel.TabIndex = 2;
            this.keyLabel.Values.Text = "No property selected";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(394, 211);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 25);
            this.okButton.TabIndex = 1;
            this.okButton.Values.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // propertyListBox
            // 
            this.propertyListBox.Location = new System.Drawing.Point(12, 12);
            this.propertyListBox.Name = "propertyListBox";
            this.propertyListBox.Size = new System.Drawing.Size(169, 193);
            this.propertyListBox.TabIndex = 0;
            this.propertyListBox.SelectedIndexChanged += new System.EventHandler(this.propertyListBox_SelectedIndexChanged);
            // 
            // ActorPropertyEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 248);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActorPropertyEditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actor Properties";
            ((System.ComponentModel.ISupportInitialize)(this.backgroundPanel)).EndInit();
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel backgroundPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox valueTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel keyLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton okButton;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox propertyListBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton removeButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton addButton;
    }
}