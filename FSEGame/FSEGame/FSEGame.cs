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
using FSEGame.Engine;
using Microsoft.Xna.Framework;
using LuaInterface;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FSEGame : GameBase
    {
        #region Static Members
        private static FSEGame singleton;
        #endregion

        private GameState state;
        private MainMenuScreen mainMenu;
        private LoadScreen loadScreen;
        private CharacterController character;
        private Camera camera = null;
        private LuaFunction luaChangeLevelFunction;
        private StaticText debugText;
        private float timeSinceLastKey = 0.0f;

        #region Static Properties
        public static new FSEGame Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new FSEGame();

                return singleton;
            }
        }
        #endregion

        public GameState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public CharacterController Character
        {
            get
            {
                return this.character;
            }
        }

        public Camera Camera
        {
            get
            {
                return this.camera;
            }
        }

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        private FSEGame()
        {
            this.luaChangeLevelFunction = this.LuaState.LoadFile(@"FSEGame\Scripts\ChangeLevel.lua");
        }
        #endregion

        protected override void Initialize()
        {
            base.Initialize();

            this.state = GameState.Menu;

            this.OnDraw += new GameDrawDelegate(Draw);

            this.DialogueManager.OnStart += new DialogueEventDelegate(DialogueStart);
            this.DialogueManager.OnEnd += new DialogueEventDelegate(DialogueEnd);
        }

        void Draw(SpriteBatch batch)
        {
            this.character.Draw(batch);
        }

        void DialogueStart(DialogueManager sender)
        {
            this.state = GameState.Cutscene;
        }
        
        void DialogueEnd(DialogueManager sender)
        {
            this.state = GameState.Exploring;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.mainMenu = new MainMenuScreen();
            this.loadScreen = new LoadScreen();

            this.loadScreen.Visible = false;

            this.UIElements.Add(this.mainMenu);
            this.UIElements.Add(this.loadScreen);

            this.character = new CharacterController();
            this.character.OnChangeLevel += new OnChangeLevelDelegate(ChangeLevel);

            this.camera = new Camera();

            this.debugText = new StaticText(this.DefaultFont);
            this.debugText.Position = new Vector2(20, 20);
            this.debugText.Visible = false;

            this.UIElements.Add(debugText);
        }

        void ChangeLevel(string id)
        {
            this.LuaState["id"] = id;
            this.luaChangeLevelFunction.Call(new Object[] { });
        }

        protected override void Update(GameTime gameTime)
        {
            this.timeSinceLastKey += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState ks = Keyboard.GetState();

#if DEBUG
            if (ks.IsKeyDown(Keys.F2) && this.timeSinceLastKey >= 0.3f)
            {
                this.debugText.Visible = !this.debugText.Visible;
                this.timeSinceLastKey = 0.0f;
            }
#endif

            base.Update(gameTime);

            if(this.camera != null && this.CurrentTileset != null)
                this.camera.Update(this.GraphicsDevice.Viewport);

            if (this.state == GameState.Exploring)
                this.character.Update(gameTime);

            this.debugText.Text = String.Format("X: {0}\nY: {1}\nLevel: {2}\nTileset: {3}\nFPS: {4}",
                    this.character.CellPosition.X, this.character.CellPosition.Y,
                    this.CurrentLevel.Name, (this.CurrentTileset == null) ? "" : this.CurrentTileset.Name, this.FPSCounter.FPS);
        }

        public override void LoadLevel(string name, string entryPoint)
        {
            this.character.Enabled = false;

            base.LoadLevel(name, entryPoint);

            LevelEntryPoint ep = this.CurrentLevel.GetEntryPoint(entryPoint);
            this.character.CellPosition = new Vector2(ep.X, ep.Y);

            this.character.Enabled = true;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::