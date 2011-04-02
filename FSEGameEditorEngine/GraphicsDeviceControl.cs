// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   This code is based on code from 
// ::          http://create.msdn.com/en-US/education/catalog/sample/winforms_series_1
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;
#endregion

using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = System.Drawing.Color;
using System.ComponentModel.Design;

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GraphicsDeviceControl : Control
    {
        #region Instance Members
        /// <summary>
        /// 
        /// </summary>
        private GraphicsDeviceService service;

        private ServiceContainer services;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the handle of the graphics device used by this control.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.service.GraphicsDevice;
            }
        }

        public ServiceContainer Services
        {
            get
            {
                return this.services;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GraphicsDeviceControl()
        {
            this.services = new ServiceContainer();
        }
        #endregion

        protected override void OnCreateControl()
        {
            if (!this.DesignMode)
            {
                this.service = GraphicsDeviceService.AddReference(
                    this.Handle, this.ClientSize.Width, this.ClientSize.Height);
                this.services.AddService(typeof(IGraphicsDeviceService), this.service);

                this.Initialise();
            }

            base.OnCreateControl();
        }

        protected abstract void Initialise();

        #region OnPaintBackground
        /// <summary>
        /// Overrides the OnPaintBackground method to prevent WinForms from
        /// drawing the background of the control.
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.DesignMode)
            {
                base.OnPaintBackground(pevent);
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                this.BeginDraw();
                this.Draw();
                this.EndDraw();
            }
            catch (Exception ex)
            {
                this.GDIDraw(e.Graphics, ex.Message);
            }
        }

        private void BeginDraw()
        {
            if (this.service == null)
                throw new Exception("Service is null.");

            Viewport viewport = new Viewport();

            viewport.X = 0;
            viewport.Y = 0;

            viewport.Width = ClientSize.Width;
            viewport.Height = ClientSize.Height;

            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            this.GraphicsDevice.Viewport = viewport;
        }

        protected abstract void Draw();

        #region EndDraw
        /// <summary>
        /// Presents the rendered image on the screen.
        /// </summary>
        private void EndDraw()
        {
            try
            {
                Rectangle rect = new Rectangle(
                    0, 0, this.ClientSize.Width, this.ClientSize.Height);

                this.GraphicsDevice.Present(rect, null, this.Handle);
            }
            catch
            {
            }
        }
        #endregion

        private void GDIDraw(Graphics g, String message)
        {
            g.Clear(Color.White);

            using (Brush brush = new SolidBrush(Color.Black))
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString(message, this.Font, brush, this.ClientRectangle, format);
                }
            }
        }

        #region Dispose
        /// <summary>
        /// Disposes of the graphics device control.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)
        {
            if (this.service != null)
            {
                this.service.Release(disposing);
                this.service = null;
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::