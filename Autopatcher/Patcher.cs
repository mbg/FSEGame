// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: AutoPatcher
// :: Copyright 2010 Origin Software
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 7/24/2010 8:00:44 PM
// ::      by: ORI20082009\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
#endregion

namespace Origin.Autopatcher
{
    internal delegate void UpdateStatusDelegate(String status);
    internal delegate void UpdateProgressDelegate(Int32 progress, Int32 max);

    /// <summary>
    /// 
    /// </summary>
    internal class Patcher
    {
        #region Instance Members
        private StreamWriter log;
        private WebClient client;
        private event UpdateStatusDelegate updateStatus;
        private event UpdateProgressDelegate updateProgress;
        private String downloadURL;
        private List<UpdateInformation> updates;
        private Int32 filesUpdated = 0;
        private Boolean success = false;
        #endregion

        #region Properties
        internal event UpdateStatusDelegate UpdateStatus
        {
            add { updateStatus += value; }
            remove { updateStatus -= value; }
        }
        internal event UpdateProgressDelegate UpdateProgress
        {
            add { updateProgress += value; }
            remove { updateProgress -= value; }
        }
        internal Boolean Success
        {
            get
            {
                return this.success;
            }
        }
        internal Int32 FilesUpdated
        {
            get
            {
                return this.filesUpdated;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Patcher()
        {
            this.client = new WebClient();
            this.updates = new List<UpdateInformation>();

            this.log = new StreamWriter("Patcher\\Patcher.log", false);
            this.log.AutoFlush = true;

            this.log.WriteLine("=== Running Autopatcher on {0} ===", DateTime.Now);
            this.log.WriteLine();
        }
        #endregion

        #region Run
        /// <summary>
        /// Runs the patcher.
        /// </summary>
        internal void Run()
        {
            // :: Download the patch information file from the web server.
            this.log.WriteLine("Downloading patch information file from '{0}'...", Autopatcher.PatchInformationFile);

            try
            {
                client.DownloadFile(
                    Autopatcher.PatchInformationFile,
                    "Patcher\\Server.xml");
            }
            catch (WebException ex)
            {
                this.log.WriteLine("Failed to download patch information file. Status: {0}, Messsage: {1}", ex.Status, ex.Message);
                return;
            }

            // :: Look for changes.
            this.Report("Looking for changes...");

            XmlDocument serverDoc = new XmlDocument();
            serverDoc.Load("Patcher\\Server.xml");

            XmlElement serverRootElement = serverDoc.DocumentElement;
            this.downloadURL = serverRootElement.GetAttribute("URL");

            this.log.WriteLine("Patch files are located at '{0}'.", this.downloadURL);
            this.log.WriteLine();

            this.ProcessDirectory(Environment.CurrentDirectory, this.downloadURL, serverRootElement);

            this.log.WriteLine();

            // :: Start updating.
            this.log.WriteLine();
            this.log.WriteLine("{0} files are scheduled for download.", this.updates.Count);
            this.log.WriteLine();

            for (Int32 i = 0; i < this.updates.Count; i++)
            {
                UpdateInformation info = this.updates[i];

                this.ChangeProgress(i);
                this.Report(String.Format("Downloading {0}...", Path.GetFileName(info.Destination)));
                this.log.WriteLine("Downloading file '{0}' to '{1}'...", info.Source, info.Destination);

                this.client.DownloadFile(info.Source, info.Destination);
                this.filesUpdated++;
            }

            this.ChangeProgress(this.updates.Count);

            if (this.filesUpdated > 0)
                this.Report("Update completed.");
            else
                this.Report("Already up-to-date.");

            this.success = true;
        }
        #endregion

        private void ProcessDirectory(String path, String url, XmlElement directoryElement)
        {
            foreach (XmlNode node in directoryElement.ChildNodes)
            {
                if(node.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement element = (XmlElement)node;

                if (element.Name.ToLower() == "file")
                {
                    String actualPath = Path.Combine(path, element.GetAttribute("Name"));
                    Boolean download = true;

                    if (File.Exists(actualPath))
                    {
                        if (this.FileSHA1(actualPath) == element.GetAttribute("Hash"))
                        {
                            download = false;
                            this.log.WriteLine("'{0}' is already up-to-date.", actualPath);
                        }
                        else
                        {
                            this.log.WriteLine("Scheduling to download file '{0}' because it is outdated.", actualPath);
                        }
                    }
                    else
                    {
                        this.log.WriteLine("Scheduling to download file '{0}' because it does not exist.", actualPath);
                    }

                    if (!download)
                        continue;

                    UpdateInformation info = new UpdateInformation();
                    info.Destination = actualPath;
                    info.Source = url + element.GetAttribute("Name");

                    this.updates.Add(info);
                }
                else if (element.Name.ToLower() == "directory")
                {
                    String actualPath = Path.Combine(path, element.GetAttribute("Name"));

                    if (!Directory.Exists(actualPath))
                    {
                        this.log.WriteLine("Creating directory '{0}' because it doesn't exist.", actualPath);
                        Directory.CreateDirectory(actualPath);
                    }

                    this.ProcessDirectory(actualPath, url + element.GetAttribute("Name") + "/", element);
                }
            }
        }

        #region Report
        /// <summary>
        /// Updates the status message.
        /// </summary>
        /// <param name="message"></param>
        private void Report(String message)
        {
            if (this.updateStatus != null)
                this.updateStatus(message);
        }
        #endregion

        private void ChangeProgress(Int32 progress)
        {
            if (this.updateProgress != null)
                this.updateProgress(progress, this.updates.Count);
        }

        #region FileSHA1
        /// <summary>
        /// Calculate the SHA1 hash value of the file with the given name.
        /// </summary>
        /// <param name="filename">The hash value of the file with this name will be calculated.</param>
        /// <returns>Returns the hash value of the given file.</returns>
        private String FileSHA1(String filename)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            if (!File.Exists(filename))
                throw new FileNotFoundException();

            String hash = String.Empty;

            // :: Open the specified file.
            FileStream stream = new FileStream(filename, FileMode.Open);

            SHA1CryptoServiceProvider sp = new SHA1CryptoServiceProvider();
            Byte[] hashb = sp.ComputeHash(stream);

            foreach (Byte b in hashb)
                hash += String.Format("{0:x2}", b);

            stream.Close();

            // :: Return the hash value of the file.
            return hash;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::