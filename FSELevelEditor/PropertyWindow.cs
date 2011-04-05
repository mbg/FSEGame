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

        public PropertyWindow()
        {
            InitializeComponent();
        }
    }
}
