using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Origin.Autopatcher
{
    internal delegate void CrossThreadDelegate();

    public partial class PatcherForm : Form
    {
        private Patcher patcher;

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public PatcherForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Cancel
        /// <summary>
        /// Cancels the patcher.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                patcher = new Patcher();
                patcher.UpdateStatus += new UpdateStatusDelegate(patcher_UpdateStatus);
                patcher.UpdateProgress += new UpdateProgressDelegate(patcher_UpdateProgress);

                patcher.Run();

            }
            catch (Exception ex)
            {
                this.Invoke(new CrossThreadDelegate(delegate 
                {
                    MessageBox.Show(ex.ToString());
                }));
            }
        }

        private void patcher_UpdateProgress(int progress, int max)
        {
            this.Invoke(new CrossThreadDelegate(delegate {
                this.progressBar1.Style = ProgressBarStyle.Continuous;

                if (max > 0)
                {
                    this.progressBar1.Maximum = max;
                    this.progressBar1.Value = progress;
                }
            }));
        }

        private void patcher_UpdateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CrossThreadDelegate(delegate
                {
                    this.statusLabel.Text = status;
                    this.Invalidate();
                }));
            }
            else
            {
                this.statusLabel.Text = status;
            }
        }

        #region OnShown
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatcherForm_Shown(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
        }
        #endregion

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Value = this.progressBar1.Maximum;

            if (this.patcher.Success)
            {
                if (this.patcher.FilesUpdated == 0)
                {
                    MessageBox.Show(
                        String.Format("{0} is already up-to-date.", Autopatcher.ApplicationName),
                        "Autopatcher",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        String.Format("{0} has successfully been updated.", Autopatcher.ApplicationName),
                        "Autopatcher",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                this.playButton.Enabled = true;
                this.playButton.Focus();
            }
            else
            {
                MessageBox.Show(
                    String.Format("Unable to update {0}.", Autopatcher.ApplicationName),
                    "Autopatcher",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                this.Close();
            }
        }

        #region Play
        /// <summary>
        /// Executes the main executable of the game.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void playButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Autopatcher.ApplicationExecutable))
            {
                MessageBox.Show(
                    String.Format("Unable to start the game because {0} could not be found.", Autopatcher.ApplicationExecutable),
                    "Autopatcher",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                Process.Start(Autopatcher.ApplicationExecutable);
            }

            this.Close();
        }
        #endregion

        private void PatcherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to stop updating?",
                    "Autopatcher",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.backgroundWorker1.CancelAsync();
                }
            }
        }

        
    }
}
