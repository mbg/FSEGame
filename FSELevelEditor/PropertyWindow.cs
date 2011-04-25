using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSELevelEditor
{
    public partial class PropertyWindow : Form
    {
        private MainWindow mainWindow;

        #region Properties
        /// <summary>
        /// Gets or sets the object whose properties should be displayed.
        /// </summary>
        public Object SelectedObject
        {
            get
            {
                return this.propertyGrid1.SelectedObject;
            }
            set
            {
                this.propertyGrid1.SelectedObject = value;
            }
        }
        #endregion

        public PropertyWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void PropertyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mainWindow.LevelEditor.CurrentLevel.ForceChange();
        }
    }
}
