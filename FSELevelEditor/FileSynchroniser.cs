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

namespace FSELevelEditor
{
    /// <summary>
    /// Synchronises two files based on the time they were last modified.
    /// </summary>
    public sealed class FileSynchroniser
    {
        #region Instance Members
        /// <summary>
        /// The filename of the first file.
        /// </summary>
        private String a;
        /// <summary>
        /// The filename of the second file.
        /// </summary>
        private String b;
        /// <summary>
        /// A value indicating whether an error has occurred.
        /// </summary>
        private Boolean error = true;
        /// <summary>
        /// A status message describing the action that was performed.
        /// </summary>
        private String message;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether an error has occurred.
        /// </summary>
        public Boolean Error
        {
            get
            {
                return this.error;
            }
        }
        /// <summary>
        /// Gets a message describing the action that was performed.
        /// </summary>
        public String StatusMessage
        {
            get
            {
                return this.message;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public FileSynchroniser(String a, String b)
        {
            this.a = a;
            this.b = b;
        }
        #endregion

        #region Synchronise
        /// <summary>
        /// Synchronises the two files.
        /// </summary>
        public void Synchronise()
        {
            // :: Try to synchronise the two files,
            // :: catch all exceptions and store their messages.
            try
            {
                if (File.Exists(this.a))
                {
                    if (File.Exists(this.b))
                    {
                        // :: Both files exist, decide what to do depending on the time and date
                        // :: when each file was last modified.
                        FileInfo ai = new FileInfo(this.a);
                        FileInfo bi = new FileInfo(this.b);

                        if (ai.LastWriteTimeUtc >
                            bi.LastWriteTimeUtc)
                        {
                            // :: The first file is newer, overwrite the second file.
                            File.Copy(this.a, this.b, true);

                            this.message = String.Format(
                                "Both '{0}' and '{1}' exist, but '{0}' is newer'",
                                this.a, this.b);
                        }
                        else if (bi.LastWriteTimeUtc >
                            ai.LastWriteTimeUtc)
                        {
                            // :: The second file is newer, overwrite the first file.
                            File.Copy(this.b, this.a, true);

                            this.message = String.Format(
                                "Both '{0}' and '{1}' exist, but '{1}' is newer'",
                                this.a, this.b);
                        }
                        else
                        {
                            // :: Both files are synchronised.
                            this.message = String.Format(
                                "Both '{0}' and '{1}' exist and both files are already synchronised.",
                                this.a, this.b);
                        }
                    }
                    else
                    {
                        // :: The second file doesn't exist, but the first does. Copy the
                        // :: first file to the location of the second.
                        File.Copy(this.a, this.b);

                        this.message = String.Format(
                                "'{0}' exists, but '{1}' doesn't.",
                                this.a, this.b);
                    }
                }
                else
                {
                    if (File.Exists(this.b))
                    {
                        // :: The first file doesn't exist, but the second does. Copy the
                        // :: second file to the location of the first.
                        File.Copy(this.b, this.a);

                        this.message = String.Format(
                                "'{1}' exists, but '{0}' doesn't.",
                                this.a, this.b);
                    }
                    else
                    {
                        // :: Neither file exists.
                        throw new Exception(String.Format(
                            "Neither '{0}' nor '{1}' exists.", this.a, this.b));
                    }
                }

                this.error = false;
            }
            catch (Exception ex)
            {
                this.error = true;
                this.message = ex.Message;
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::