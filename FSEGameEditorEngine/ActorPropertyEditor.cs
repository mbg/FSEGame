// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorPropertyEditor : UITypeEditor
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ActorPropertyEditor()
        {

        }
        #endregion

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Dictionary<String, String> result = (Dictionary<String, String>)value;

            using (ActorPropertyEditorForm form = new ActorPropertyEditorForm(result))
            {
                if (form.ShowDialog() == DialogResult.Cancel)
                    return value;

                result.Clear();

                foreach (String key in form.Properties.Keys)
                {
                    result.Add(key, form.Properties[key]);
                }

                return result;
            }
        }


    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::