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
using LuaInterface;
using FSEGame.Engine.UI;
#endregion

namespace FSEGame.Engine
{
    public delegate void GameEventDelegate(Game sender);
    public delegate void GameDrawDelegate(SpriteBatch batch);

    /// <summary>
    /// This is the main type for the FSE coursework game.
    /// </summary>
    public abstract class GameBase : Game
    {
        #region Static Members
        /// <summary>
        /// 
        /// </summary>
        private static GameBase singleton;
        #endregion

        #region Instance Members
        /// <summary>
        /// The graphics device manager for this game.
        /// </summary>
        private GraphicsDeviceManager graphics;

        private Lua luaState;

        private DialogueManager dialogueManager;
        /// <summary>
        /// The main sprite batch used for rendering multiple textures in one pass.
        /// </summary>
        private SpriteBatch spriteBatch;
        /// <summary>
        /// The default game font.
        /// </summary>
        private SpriteFont defaultFont;

        private Level currentLevel = null;
        private Tileset tileset;
        private FadeScreen fadeScreen;
        private List<IUIElement> uiElements;
        private FPSCounter fpsCounter;
        private Vector2 offset;
        #endregion

        #region Events
        private event GameEventDelegate onInitialise = null;
        private event GameDrawDelegate onDraw = null;
        #endregion

        #region Static Properties
        /// <summary>
        /// 
        /// </summary>
        public static GameBase Singleton
        {
            get
            {
                return singleton;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the dialogue manager for this game.
        /// </summary>
        public DialogueManager DialogueManager
        {
            get
            {
                return this.dialogueManager;
            }
        }
        public SpriteFont DefaultFont
        {
            get
            {
                return this.defaultFont;
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
        /// <summary>
        /// Gets the list of UI elements.
        /// </summary>
        public List<IUIElement> UIElements
        {
            get
            {
                return this.uiElements;
            }
        }

        public FPSCounter FPSCounter
        {
            get
            {
                return this.fpsCounter;
            }
        }

        public Lua LuaState
        {
            get
            {
                return this.luaState;
            }
        }

        public Vector2 Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
        #endregion

        #region Event Properties
        /// <summary>
        /// 
        /// </summary>
        public event GameEventDelegate OnInitialise
        {
            add
            {
                this.onInitialise += value;
            }
            remove
            {
                this.onInitialise -= value;
            }
        }

        public event GameDrawDelegate OnDraw
        {
            add
            {
                this.onDraw += value;
            }
            remove
            {
                this.onDraw -= value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises the game.
        /// </summary>
        protected GameBase()
        {
            GameBase.singleton = this;

            this.uiElements = new List<IUIElement>();
            this.fpsCounter = new FPSCounter();

            this.luaState = new Lua();
            this.luaState.RegisterFunction("LoadLevel", this, this.GetType().GetMethod("LoadLevel", new Type[] { typeof(String), typeof(String) }));

            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreparingDeviceSettings += 
                new EventHandler<PreparingDeviceSettingsEventArgs>(PrepareGraphicsSettings);

            this.dialogueManager = new DialogueManager();
            
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
            this.currentLevel = new Level();
            
            if (this.onInitialise != null)
                this.onInitialise(this);

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

            this.fadeScreen = new FadeScreen();
            this.fadeScreen.FadeOut(1.0d);
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
            this.fpsCounter.Update(gameTime);

            this.fadeScreen.Update(gameTime);

            if(this.tileset != null)
                this.tileset.Update(gameTime);

            this.currentLevel.Update(gameTime);
            this.dialogueManager.Update(gameTime);

            base.Update(gameTime);

            foreach (IUIElement uiElement in this.uiElements)
            {
                uiElement.Update(gameTime);
            }
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

            if (this.currentLevel != null)
            {
                this.currentLevel.DrawLevel(this.spriteBatch);
            }

            if (this.onDraw != null)
                this.onDraw(this.spriteBatch);

            //this.fadeScreen.Draw(this.spriteBatch);

            foreach (IUIElement uiElement in this.uiElements)
            {
                uiElement.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region LoadTileset
        /// <summary>
        /// Loads the tileset with the specified name.
        /// </summary>
        /// <param name="name"></param>
        public void LoadTileset(String name)
        {
            this.tileset = new Tileset(16, 6, 8);
            this.tileset.Load(this.Content, name);
        }
        #endregion

        #region LoadLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void LoadLevel(String name)
        {
            this.currentLevel.Load(this.Content, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entryPoint"></param>
        public virtual void LoadLevel(String name, String entryPoint)
        {
            this.LoadLevel(name);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::