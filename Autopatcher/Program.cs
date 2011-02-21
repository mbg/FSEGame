// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Autopatcher
// :: Copyright 2010 Origin Software
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 7/24/2010 6:50:36 PM
// ::      by: ORI20082009\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.IO;
using System.Windows.Forms;
#endregion

namespace Origin.Autopatcher
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Program()
        {
            // :: Create the patcher directory if required.
            if (!Directory.Exists("Patcher"))
                Directory.CreateDirectory("Patcher");

            
        }
        #endregion

        #region Entry Point
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Int32 Main(String[] arguments)
        {
            Program app;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                app = new Program();
                app.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unhandled exception was caught:\n" +
                    ex.Message,
                    "Autopatcher",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return -1;
            }

            return 0;
        }
        #endregion

        #region Run
        private void Run()
        { 
            Application.Run(new PatcherForm());
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::