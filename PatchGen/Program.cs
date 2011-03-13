// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: PatchGenerator
// :: Copyright 2010 Origin Software
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 7/24/2010 7:01:04 PM
// ::      by: ORI20082009\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
#endregion

namespace Origin.PatchGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public static class Program
    {
	    #region Data Members
        private static String updateURL = "http://people.syngate.co.uk/~mbg/FSEGame/GameFiles/";
	    #endregion

        #region Static Members
        private static List<String> excludedFolders = new List<String>();
        private static List<String> excludedFiles = new List<String>();
        private static List<String> excludedExtensions = new List<String>();
        private static StreamWriter log;
        private static XmlDocument doc;
        private static XmlElement root;

        private static List<PatchFileInfo> previousFiles;
        private static List<String> updatedFiles;
        #endregion

        #region Entry Point
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Int32 Main(String[] arguments)
        {
            try
            {
                // :: Create the patcher directory if required.
                if (!Directory.Exists("Patcher"))
                    Directory.CreateDirectory("Patcher");

                // :: Start the log file.
                log = new StreamWriter("Patcher\\PatchGen.log", true);
                log.AutoFlush = true;

                log.WriteLine("=== Running Origin PatchGen on {0} ===", DateTime.Now);
                log.WriteLine();

                // :: Add folders and files to exclude.
                excludedFolders.Add("Patcher");
                excludedFiles.Add("PatchGen.exe");
                excludedFiles.Add("Default.cfg");
                excludedFiles.Add("Autopatcher.exe");
                excludedFiles.Add("Autopatcher.vshost.exe");
                excludedFiles.Add("Autopatcher.vshost.exe.manifest");
                excludedExtensions.Add(".pdb");
                excludedExtensions.Add(".log");
                excludedExtensions.Add(".vshost.exe");
                excludedExtensions.Add(".vshost.exe.manifest");

                foreach (String str in excludedFolders)
                    log.WriteLine("Excluded folder: {0}", str);
                foreach (String str in excludedFiles)
                    log.WriteLine("Excluded file: {0}", str);
                foreach (String str in excludedExtensions)
                    log.WriteLine("Excluded extension: {0}", str);

                log.WriteLine();

                // :: Look for an existing patchfile.
                previousFiles = new List<PatchFileInfo>();
                updatedFiles = new List<String>();

                if (File.Exists("Patcher\\Patch.xml"))
                {
                    log.WriteLine("Analysing existing patchfile...");

                    AnalysePatchfile();

                    log.WriteLine();
                }

                // :: Create the XML document and root node.
                doc = new XmlDocument();
                root = doc.CreateElement("Patch");

                XmlAttribute urlAttr = doc.CreateAttribute("URL");
                urlAttr.Value = Program.updateURL;
                root.Attributes.Append(urlAttr);

                // ::
                DirectoryInfo rootDirectory = new DirectoryInfo(Environment.CurrentDirectory);

                ProcessFolder(rootDirectory.FullName, ref root);

                doc.AppendChild(root);
                doc.Save("Patcher\\Patch.xml");

                log.WriteLine();
                log.WriteLine("The following files have changed since the last patch:");

                foreach (String updatedFile in updatedFiles)
                    log.WriteLine(updatedFile);

                log.WriteLine();
                log.WriteLine("Success.");
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.WriteLine();
                    log.WriteLine("Failure: {0}", ex.Message);
                }

                Console.WriteLine(ex.ToString());

                return -1;
            }
            finally
            {
                if (log != null)
                {
                    log.WriteLine();
                    log.Close();
                }
            }

            return 0;
        }
        #endregion

        #region ProcessFolder
        private static void ProcessFolder(String path, ref XmlElement parent)
        {
            log.WriteLine("Adding directory '{0}'.", path);

            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo info in dir.GetDirectories())
            {
                if (excludedFolders.Contains(info.Name))
                {
                    log.WriteLine("Ignoring directory '{0}'.", info.Name);
                    continue;
                }

                XmlElement dirElement = doc.CreateElement("Directory");
                XmlAttribute nameAttr = doc.CreateAttribute("Name");
                nameAttr.Value = info.Name;

                dirElement.Attributes.Append(nameAttr);

                ProcessFolder(info.FullName, ref dirElement);

                parent.AppendChild(dirElement);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                if (excludedFiles.Contains(file.Name))
                {
                    log.WriteLine("Ignoring file '{0}'.", file.Name);
                    continue;
                }

                bool hasExcludedExtension = false;

                foreach (String ext in excludedExtensions)
                {
                    if (file.Name.ToLower().EndsWith(ext))
                    {
                        log.WriteLine("Ignoring file '{0}' because it has extension '{1}'.",
                            file.Name, ext);

                        hasExcludedExtension = true;
                        break;
                    }
                }

                if (hasExcludedExtension)
                    continue;

                log.WriteLine("Adding file '{0}'.", file.Name);

                XmlElement fileElement = doc.CreateElement("File");

                XmlAttribute nameAttr = doc.CreateAttribute("Name");
                nameAttr.Value = file.Name;

                XmlAttribute hashAttr = doc.CreateAttribute("Hash");
                hashAttr.Value = FileSHA1(file.FullName);

                XmlAttribute sizeAttr = doc.CreateAttribute("Size");
                sizeAttr.Value = file.Length.ToString();

                fileElement.Attributes.Append(nameAttr);
                fileElement.Attributes.Append(hashAttr);
                fileElement.Attributes.Append(sizeAttr);
                parent.AppendChild(fileElement);

                String actualPath = path + "\\" + file.Name;
                PatchFileInfo pfi = GetPreviousFileInfo(actualPath);

                // if (pfi == null)
                    updatedFiles.Add(actualPath);
                // else
                // {
                //    if (pfi.Hash != hashAttr.Value)
                //        updatedFiles.Add(actualPath);
                // }
            }
        }
        #endregion

        private static void AnalysePatchfile()
        {
            XmlDocument patchfile = new XmlDocument();
            patchfile.Load("Patcher\\Patch.xml");

            XmlElement rootElement = patchfile.DocumentElement;
            AnalyseElement(rootElement, ".\\");
        }

        private static void AnalyseElement(XmlElement element, String prefix)
        {
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement e = (XmlElement)node;

                if (e.Name.ToLower() == "file")
                {
                    PatchFileInfo info = new PatchFileInfo();
                    info.Filename = Path.GetFullPath(Path.Combine(prefix, e.GetAttribute("Name")));
                    info.Hash = e.GetAttribute("Hash");

                    previousFiles.Add(info);
                }
                else if (e.Name.ToLower() == "directory")
                {
                    AnalyseElement(e, prefix + e.GetAttribute("Name") + "\\");
                }
            }
        }

        private static PatchFileInfo GetPreviousFileInfo(String path)
        {
            foreach (PatchFileInfo info in previousFiles)
            {
                if (Path.Equals(path, info.Filename))
                    return info;
            }

            return null;
        }

        #region FileSHA1
        /// <summary>
        /// Calculate the SHA1 hash value of the file with the given name.
        /// </summary>
        /// <param name="filename">The hash value of the file with this name will be calculated.</param>
        /// <returns>Returns the hash value of the given file.</returns>
        private static String FileSHA1(String filename)
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

            // :: Return the hash value of the file.
            return hash;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
