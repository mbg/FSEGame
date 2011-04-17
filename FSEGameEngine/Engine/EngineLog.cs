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
using System.IO;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EngineLog : IDisposable
    {
        #region Instance Members
        private StreamWriter log;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public EngineLog()
        {
            String directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"FSEGame\Logs\");

            String filename = Path.Combine(
                directory,
                @"Engine.log");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            this.log = new StreamWriter(filename);
            this.log.AutoFlush = true;
        }
        #endregion

        public void WriteLine(String source, String message)
        {
            this.log.WriteLine("[{0}] {1}: {2}",
                DateTime.Now.ToLongTimeString(),
                source,
                message);
            this.log.Flush();
        }

        public void WriteLine(String source, String message, params Object[] args)
        {
            this.WriteLine(source, String.Format(message, args));
        }

        #region Dispose
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.log.Close();
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::