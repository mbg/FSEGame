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
#endregion

namespace FSEGame
{
    /// <summary>
    /// This is the main type for the FSE coursework game.
    /// </summary>
    public class FSEGame : Game
    {
        #region Static Members
        /// <summary>
        /// 
        /// </summary>
        private static FSEGame singleton;
        #endregion

        #region Instance Members
        /// <summary>
        /// Stores the current state of the game.
        /// </summary>
        private GameState gameState = GameState.Exploring;
        /// <summary>
        /// The graphics device manager for this game.
        /// </summary>
        private GraphicsDeviceManager graphics;

        private Lua luaState;
        private LuaFunction luaChangeLevelFunction;
        /// <summary>
        /// The main sprite batch used for rendering multiple textures in one pass.
        /// </summary>
        private SpriteBatch spriteBatch;
        /// <summary>
        /// The default game font.
        /// </summary>
        private SpriteFont defaultFont;

        private String levelFileExtension = ".xml";

        private Camera camera = null;
        private CharacterController character = null;
        private Level currentLevel = null;
        private Tileset tileset;
        private FadeScreen fadeScreen;
        private MainMenuScreen mainMenu;
        private List<IUIElement> uiElements;
        private StaticText debugText;
        private FPSCounter fpsCounter;
        private float timeSinceLastKey = 0.0f;
        #endregion

        #region Static Properties
        /// <summary>
        /// 
        /// </summary>
        public static FSEGame Singleton
        {
            get
            {
                return FSEGame.singleton;
            }
        }
        #endregion

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
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises the game.
        /// </summary>
        public FSEGame()
        {
            if (FSEGame.Singleton != null)
                throw new InvalidOperationException("Can only initialise one instance of FSEGame!");

            this.uiElements = new List<IUIElement>();
            this.fpsCounter = new FPSCounter();

            this.luaState = new Lua();
            this.luaState.RegisterFunction("LoadLevel", this, this.GetType().GetMethod("LoadLevel", new Type[] { typeof(String), typeof(String) }));
            this.luaChangeLevelFunction = this.luaState.LoadFile(@"FSEGame\Scripts\ChangeLevel.lua");

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

            this.fadeScreen = new FadeScreen();
            this.fadeScreen.FadeOut(1.0d);

            this.character = new CharacterController();
            this.character.OnChangeLevel += new OnChangeLevelDelegate(character_OnChangeLevel);

            this.LoadLevel(@"Levels\Test.xml", "Default");
            this.camera = new Camera();

            this.mainMenu = new MainMenuScreen();
            this.mainMenu.Hide();

            this.debugText = new StaticText(this.defaultFont);
            this.debugText.Position = new Vector2(20, 20);
            this.debugText.Visible = false;

            this.uiElements.Add(debugText);
        }
        #endregion

        #region OnChangeLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void character_OnChangeLevel(string id)
        {
            this.luaState["id"] = id;
            this.luaChangeLevelFunction.Call(new Object[] {});
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
            this.timeSinceLastKey += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState ks = Keyboard.GetState();
            GamePadState gs = GamePad.GetState(PlayerIndex.One);

            // :: [DEBUG]: Use ESC to quit.
#if WINDOWS
            if (ks.IsKeyDown(Keys.Escape))
                this.Exit();
#endif
            if (gs.Buttons.Back == ButtonState.Pressed)
                this.Exit();

#if DEBUG
            if (ks.IsKeyDown(Keys.F2) && this.timeSinceLastKey >= 0.3f)
            {
                this.debugText.Visible = !this.debugText.Visible;
                this.timeSinceLastKey = 0.0f;
            }
#endif

            this.fpsCounter.Update(gameTime);

            if (this.gameState == GameState.Exploring)
                this.character.Update(gameTime);

            this.fadeScreen.Update(gameTime);

            this.camera.Update(GraphicsDevice.Viewport);

            base.Update(gameTime);

            this.debugText.Text = String.Format("X: {0}\nY: {1}\nLevel: {2}\nTileset: {3}\nFPS: {4}",
                    this.character.CellPosition.X, this.character.CellPosition.Y,
                    this.currentLevel.Name, this.tileset.Name, this.fpsCounter.FPS);
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

            this.character.Draw(this.spriteBatch);

            //Texture2D speechTexture = this.Content.Load<Texture2D>("SpeechBox");
            //this.spriteBatch.Draw(speechTexture, new Vector2(this.GraphicsDevice.Viewport.Width / 2 - 400, this.GraphicsDevice.Viewport.Height - 100), null, Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0.0f);
            //this.spriteBatch.DrawString(this.defaultFont, "That's a nice binary tree you have there...\nwould be a shame if something happened to it", new Vector2(20, this.GraphicsDevice.Viewport.Height - 95), Color.Black, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);

            //this.fadeScreen.Draw(this.spriteBatch);

            this.mainMenu.Draw(this.spriteBatch);

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
            this.currentLevel = new Level(this.Content, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entryPoint"></param>
        public void LoadLevel(String name, String entryPoint)
        {
            this.character.Enabled = false;

            this.LoadLevel(name);

            LevelEntryPoint ep = this.currentLevel.GetEntryPoint(entryPoint);
            this.character.CellPosition = new Vector2(ep.X, ep.Y);

            this.character.Enabled = true;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::