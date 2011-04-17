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
using FSEGame.Engine.Effects;
using System.Reflection;
#endregion

namespace FSEGame.Engine
{
    public delegate void GameEventDelegate(Game sender);
    public delegate void GameDrawDelegate(SpriteBatch batch);
    public delegate void GameUpdateDelegate(GameTime time);

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

        private EngineLog log;

        private ScriptManager scriptManager;

        private DialogueManager dialogueManager;

        private PersistentStorage persistentStorage;
        /// <summary>
        /// The main sprite batch used for rendering multiple textures in one pass.
        /// </summary>
        private SpriteBatch spriteBatch;
        /// <summary>
        /// The default game font.
        /// </summary>
        private SpriteFont defaultFont;
        private SpriteFont boldFont;

        private Level currentLevel = null;
        private Tileset tileset;
        private UIElementCollection uiElements;
        private FPSCounter fpsCounter;
        private Vector2 offset;

        private Blur blurEffect;
        private RenderTarget2D renderTarget;
        private Texture2D frameBuffer;
        private Boolean enableBlur = false;

        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        #endregion

        #region Events
        private event GameEventDelegate onInitialise = null;
        private event GameDrawDelegate onDraw = null;
        private event GameUpdateDelegate onUpdate = null;
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
        /// Gets the engine log's handle.
        /// </summary>
        public EngineLog Log
        {
            get
            {
                return this.log;
            }
        }
        /// <summary>
        /// Gets the script manager for this game.
        /// </summary>
        public ScriptManager ScriptManager
        {
            get
            {
                return this.scriptManager;
            }
        }
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
        /// <summary>
        /// Gets the persistent storage manager.
        /// </summary>
        public PersistentStorage PersistentStorage
        {
            get
            {
                return this.persistentStorage;
            }
        }
        /// <summary>
        /// Gets the default sprite font.
        /// </summary>
        public SpriteFont DefaultFont
        {
            get
            {
                return this.defaultFont;
            }
        }

        public SpriteFont BoldFont
        {
            get
            {
                return this.boldFont;
            }
        }
        /// <summary>
        /// Gets the tileset which is currently being used to
        /// render the current level.
        /// </summary>
        public Tileset CurrentTileset
        {
            get
            {
                return this.tileset;
            }
        }
        /// <summary>
        /// Gets the current level.
        /// </summary>
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
        public UIElementCollection UIElements
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

        public Boolean EnableBlur
        {
            get
            {
                return this.enableBlur;
            }
            set
            {
                this.enableBlur = value;
            }
        }

        public KeyboardState CurrentKeyboardState
        {
            get
            {
                return this.currentKeyboardState;
            }
        }

        public KeyboardState LastKeyboardState
        {
            get
            {
                return this.lastKeyboardState;
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

        public event GameUpdateDelegate OnUpdate
        {
            add
            {
                this.onUpdate += value;
            }
            remove
            {
                this.onUpdate -= value;
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

            this.log = new EngineLog();

            this.uiElements = new UIElementCollection();
            this.fpsCounter = new FPSCounter();

            this.persistentStorage = new PersistentStorage();

            // :: Initialise Lua and register the Engine's functions.
            this.scriptManager = new ScriptManager(@"FSEGame\Scripts\");
            this.scriptManager.RegisterTypeInstance(this);

            // :: Initialise the graphics device manager and register some events.
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
            /*foreach (DisplayMode displayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
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
            }*/

            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 800;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 600;
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

            this.blurEffect = new Blur(this);
            this.blurEffect.ComputeKernel(7, 2.0f);

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
            this.boldFont = this.Content.Load<SpriteFont>("ArialBold");

            PresentationParameters pp = this.GraphicsDevice.PresentationParameters;
            this.renderTarget = new RenderTarget2D(this.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

            this.rt1 = new RenderTarget2D(this.GraphicsDevice, 200, 150, false, pp.BackBufferFormat, DepthFormat.None); // 400, 300
            this.rt2 = new RenderTarget2D(this.GraphicsDevice, 200, 150, false, pp.BackBufferFormat, DepthFormat.None); // 400, 600

            this.blurEffect.ComputeOffsets(400, 300);
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
            this.currentKeyboardState = Keyboard.GetState();

            this.fpsCounter.Update(gameTime);

            if(this.tileset != null)
                this.tileset.Update(gameTime);

            this.currentLevel.Update(gameTime);
            this.dialogueManager.Update(gameTime);

            base.Update(gameTime);

            if (this.onUpdate != null)
                this.onUpdate(gameTime);

            foreach (IUIElement uiElement in this.uiElements)
            {
                uiElement.Update(gameTime);
            }

            this.lastKeyboardState = this.currentKeyboardState;
        }
        #endregion

        private RenderTarget2D rt1;
        private RenderTarget2D rt2;

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if(this.enableBlur)
                this.GraphicsDevice.SetRenderTarget(this.renderTarget);

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

            if (this.enableBlur)
            {
                this.spriteBatch.End();

                this.GraphicsDevice.SetRenderTarget(null);
                this.frameBuffer = (Texture2D)this.renderTarget;

                GraphicsDevice.Clear(Color.White);

                Texture2D result = this.blurEffect.PerformGaussianBlur(this.frameBuffer, this.rt1, this.rt2, this.spriteBatch);

                this.spriteBatch.Begin();
                this.spriteBatch.Draw(result, new Rectangle(0, 0, 800, 600), Color.White);
                this.spriteBatch.End();

                this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);
            }

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
        [ScriptFunction("LoadLevel")]
        public virtual void LoadLevel(String name, String entryPoint)
        {
            this.LoadLevel(name);
        }
        #endregion

        #region IsKeyPressed
        /// <summary>
        /// Returns a value indicating whether the specified key has been
        /// pressed and whether it wasn't pressed during the previous frame.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <returns>
        /// Returns true if the specified key was pressed during the current, 
        /// but not the previous frame.
        /// </returns>
        public Boolean IsKeyPressed(Keys key)
        {
            // :: lastKeyboardState contains the state of the keyboard from
            // :: the previous frame.
            Boolean pressed = this.currentKeyboardState.IsKeyDown(key) &&
                !this.lastKeyboardState.IsKeyDown(key);

            return pressed;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::