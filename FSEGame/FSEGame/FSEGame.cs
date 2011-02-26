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
using System.IO;
#endregion

namespace FSEGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FSEGame : Game
    {
        private static FSEGame singleton;

        #region Instance Members
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont defaultFont;

        private String levelFileExtension = ".xml";

        private Camera camera = null;
        private CharacterController character = null;
        private Level currentLevel = null;
        private Tileset tileset;
        #endregion

        public static FSEGame Singleton
        {
            get
            {
                return FSEGame.singleton;
            }
        }

        #region Properties
        public SpriteFont DefaultFont
        {
            get
            {
                return this.defaultFont;
            }
        }

        public Camera Camera
        {
            get
            {
                return this.camera;
            }
        }

        public CharacterController Character
        {
            get
            {
                return this.character;
            }
        }

        public Tileset CurrentTileset
        {
            get
            {
                return this.tileset;
            }
        }

        public Level CurrentLevel
        {
            get
            {
                return this.currentLevel;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises the game.
        /// </summary>
        public FSEGame()
        {
            if (FSEGame.Singleton != null)
                throw new InvalidOperationException("Can only initialise one instance of FSEGame!");

            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreparingDeviceSettings += 
                new EventHandler<PreparingDeviceSettingsEventArgs>(PrepareGraphicsSettings);

            this.Content.RootDirectory = "FSEGame";

            FSEGame.singleton = this;
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

            this.defaultFont = this.Content.Load<SpriteFont>("Arial");

            this.LoadLevel(@"Levels\Test.xml");

            this.character = new CharacterController();
            this.character.CellPosition = new Vector2(3, 4);
            this.camera = new Camera();
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
            this.character.Update(gameTime);
            this.camera.Update(GraphicsDevice.Viewport);

            base.GraphicsDevice.Clear(Color.Black);

            // :: [Test]  Draws a tile from the current tileset.
            // :: [mbg]   SamplerState.PointClamp causes no filters to be applied to the scaled tiles
            this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            if (this.currentLevel != null)
            {
                this.currentLevel.DrawLevel(this.spriteBatch);
            }

            this.character.Draw(this.spriteBatch, this.tileset);

            Texture2D speechTexture = this.Content.Load<Texture2D>("SpeechBox");
            this.spriteBatch.Draw(speechTexture, new Vector2(this.GraphicsDevice.Viewport.Width / 2 - 400, this.GraphicsDevice.Viewport.Height - 100), null, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0.0f);
            this.spriteBatch.DrawString(this.defaultFont, "That's a nice binary tree you have there...\nwould be a shame if something happened to it", new Vector2(20, this.GraphicsDevice.Viewport.Height - 95), Color.Black, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        public void LoadTileset(String name)
        {
            this.tileset = new Tileset(16, 6, 8);
            this.tileset.Load(this.Content, name);
        }

        #region LoadLevel
        public void LoadLevel(String name)
        {
            this.currentLevel = new Level(this.Content, name);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::