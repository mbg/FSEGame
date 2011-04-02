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
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        #region Static Members
        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        private static GraphicsDeviceService singleton;
        /// <summary>
        /// The number of references to this class.
        /// </summary>
        private static Int32 references = 0;
        #endregion

        #region Instance Members
        private GraphicsDevice device;
        private PresentationParameters parameters;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get 
            {
                return this.device;
            }
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> DeviceCreated;

        public event EventHandler<EventArgs> DeviceDisposing;

        public event EventHandler<EventArgs> DeviceReset;

        public event EventHandler<EventArgs> DeviceResetting;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        private GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            this.parameters = new PresentationParameters();

            this.parameters.BackBufferWidth = Math.Max(width, 1);
            this.parameters.BackBufferHeight = Math.Max(height, 1);
            this.parameters.BackBufferFormat = SurfaceFormat.Color;
            this.parameters.DepthStencilFormat = DepthFormat.Depth24;
            this.parameters.DeviceWindowHandle = windowHandle;
            this.parameters.PresentationInterval = PresentInterval.Immediate;
            this.parameters.IsFullScreen = false;

            this.device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                GraphicsProfile.HiDef, parameters);
        }
        #endregion

        #region AddReference
        /// <summary>
        /// Adds a reference to the graphics device service.
        /// </summary>
        /// <param name="handle">The window handle to add.</param>
        /// <param name="width">The width of the window client.</param>
        /// <param name="height">The height of the window client.</param>
        /// <returns></returns>
        public static GraphicsDeviceService AddReference(IntPtr handle, int width, int height)
        {
            // :: If this is the first control which attempts to use this service,
            // :: we need to initialise it.
            if (Interlocked.Increment(ref references) == 1)
            {
                singleton = new GraphicsDeviceService(handle, width, height);
            }

            return singleton;
        }
        #endregion

        #region Release
        /// <summary>
        /// Release a handle from the service.
        /// </summary>
        /// <param name="dispose">
        /// Set this to true if you want to dispose
        /// of the graphics device if no controls are using it.
        /// </param>
        public void Release(Boolean disposing)
        {
            if (Interlocked.Decrement(ref references) == 0)
            {
                if (disposing)
                {
                    if (this.DeviceDisposing != null)
                        this.DeviceDisposing(this, EventArgs.Empty);

                    this.device.Dispose();
                }

                this.device = null;
            }
        }
        #endregion

        public void ResetDevice(int width, int height)
        {
            if (this.DeviceResetting != null)
                this.DeviceResetting(this, EventArgs.Empty);

            this.parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width);
            this.parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height);

            this.device.Reset(this.parameters);

            if (this.DeviceReset != null)
                this.DeviceReset(this, EventArgs.Empty);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::