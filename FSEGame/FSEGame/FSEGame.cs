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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FSEGame.Engine;
#endregion

namespace FSEGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FSEGame : Game
    {
        #region Instance Members
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Tileset tileset;
        #endregion


        #region Constructor
        /// <summary>
        /// Initialises the game.
        /// </summary>
        public FSEGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreparingDeviceSettings += 
                new EventHandler<PreparingDeviceSettingsEventArgs>(PrepareGraphicsSettings);

            this.Content.RootDirectory = "FSEGame";
        }
        #endregion

        #region PrepareGraphicsSettings
        /// <summary>
        /// Triggered before the graphics device is initialised so 
        /// that we can set up the device settings, etc.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">The device settings.</param>
        private void PrepareGraphicsSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            foreach (DisplayMode displayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (displayMode.Width == 800 && displayMode.Height == 600)
                {
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferFormat = displayMode.Format;
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferHeight = displayMode.Height;
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferWidth = displayMode.Width;
                }
            }

            
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            this.tileset = new Tileset(16, 6, 8);
            this.tileset.Load(this.Content, @"Tilesets\Test");
        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
#if WINDOWS
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
#endif
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.Black);

            // :: [Test]  Draws a tile from the current tileset.
            // :: [mbg]   SamplerState.PointClamp causes no filters to be applied to the scaled tiles
            this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);
            this.tileset.DrawTile(this.spriteBatch, 0, new Vector2(0, 0));
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::