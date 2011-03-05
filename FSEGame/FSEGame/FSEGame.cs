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
using System.IO;
using FSEGame.Engine.Effects;
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
        private IngameMenu ingameMenu;
        private SaveScreen saveScreen;
        private CharacterController character;
        private Camera camera = null;
        private LuaFunction luaChangeLevelFunction;
        private StaticText debugText;
        private String savesFolder = null;
        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private DialogueEventDelegate dialogueStartDelegate;
        private DialogueEventDelegate dialogueEndDelegate;

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

        public String SavesFolder
        {
            get
            {
                return this.savesFolder;
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

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        private FSEGame()
        {
            // :: Generate the path to the user's save games folder and create it
            // :: if it doesn't exist yet.
            this.savesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                @"FSEGame\Saves\");

            if (!Directory.Exists(this.savesFolder))
                Directory.CreateDirectory(this.savesFolder);

            // :: Pre-load the level change script.
            this.luaChangeLevelFunction = this.LuaState.LoadFile(@"FSEGame\Scripts\ChangeLevel.lua");
        }
        #endregion

        protected override void Initialize()
        {
            base.Initialize();

            this.state = GameState.Menu;

            this.OnDraw += new GameDrawDelegate(Draw);

            this.dialogueStartDelegate = new DialogueEventDelegate(DialogueStart);
            this.dialogueEndDelegate = new DialogueEventDelegate(DialogueEnd);

            this.DialogueManager.OnStart += this.dialogueStartDelegate;
            this.DialogueManager.OnEnd += this.dialogueEndDelegate;
        }

        void Draw(SpriteBatch batch)
        {
            if(this.state != GameState.Intro)
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
            this.ingameMenu = new IngameMenu();
            this.saveScreen = new SaveScreen();

            this.loadScreen.Visible = false;
            this.ingameMenu.Visible = false;
            this.saveScreen.Visible = false;

            this.UIElements.Add(this.mainMenu);
            this.UIElements.Add(this.loadScreen);
            this.UIElements.Add(this.ingameMenu);
            this.UIElements.Add(this.saveScreen);

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

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            this.currentKeyboardState = Keyboard.GetState();

            if (this.IsKeyPressed(Keys.Escape) && this.state == GameState.Exploring &&
                !this.character.Moving)
            {
                this.EnableBlur = true;
                this.state = GameState.Menu;

                this.ingameMenu.Show();
            }

#if DEBUG
            if (this.IsKeyPressed(Keys.F2))
            {
                this.debugText.Visible = !this.debugText.Visible;
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

            this.lastKeyboardState = this.currentKeyboardState;
        }
        #endregion

        public override void LoadLevel(string name, string entryPoint)
        {
            this.character.Enabled = false;

            base.LoadLevel(name, entryPoint);

            LevelEntryPoint ep = this.CurrentLevel.GetEntryPoint(entryPoint);
            this.character.CellPosition = new Vector2(ep.X, ep.Y);

            this.character.Enabled = true;
        }

        public void OpenMainMenu()
        {
            this.loadScreen.Hide();
            this.mainMenu.Show();

            this.CurrentLevel.Unload();
        }

        #region OpenLoadScreen
        /// <summary>
        /// Opens the load game menu screen.
        /// </summary>
        public void OpenLoadScreen()
        {
            this.mainMenu.Hide();
            this.loadScreen.Show();
        }
        #endregion

        public void OpenIngameMenu()
        {
            this.state = GameState.Menu;
            this.EnableBlur = true;

            this.ingameMenu.Show();
            this.saveScreen.Hide();
        }

        public void OpenSaveScreen()
        {
            this.saveScreen.Show();
            this.ingameMenu.Hide();
        }

        public void CloseIngameMenu()
        {
            this.ingameMenu.Hide();
            this.saveScreen.Hide();

            this.EnableBlur = false;
            this.state = GameState.Exploring;
        }

        #region NewGame
        public void NewGame()
        {
            this.state = GameState.Intro;

            this.mainMenu.Hide();

            DialogueEventDelegate introEndDelegate = null;

            introEndDelegate = new DialogueEventDelegate(delegate
             {
                 this.DialogueManager.OnEnd -= introEndDelegate;
                 this.DialogueManager.OnStart += this.dialogueStartDelegate;
                 this.DialogueManager.OnEnd += this.dialogueEndDelegate;

                 this.LoadLevel(@"Levels\Test.xml", "Default");
                 this.state = GameState.Exploring;
             });

            this.DialogueManager.OnStart -= this.dialogueStartDelegate;
            this.DialogueManager.OnEnd -= this.dialogueEndDelegate;
            this.DialogueManager.OnEnd += introEndDelegate;
            this.DialogueManager.PlayDialogue(@"FSEGame\Dialogues\TestDialogue.xml");
        }
        #endregion

        public void LoadGame(Byte slot)
        {
            String path = Path.Combine(
                this.savesFolder, String.Format("Slot{0}.sav", slot));

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                Byte version = br.ReadByte();

                if (version != 0x01)
                    throw new Exception("!!! Invalid save file");

                String levelFilename = br.ReadString();

                this.CurrentLevel.Load(this.Content, levelFilename);

                this.character.CellPosition = new Vector2(
                    br.ReadSingle(), br.ReadSingle());
                this.character.Orientation = br.ReadSingle();

                this.PersistentStorage.Load(br);

                this.mainMenu.Hide();
                this.loadScreen.Hide();

                this.state = GameState.Exploring;
                this.character.Enabled = true;
            }
        }

        #region SaveGame
        /// <summary>
        /// Saves the current state of the game to the slot with the 
        /// specified ID.
        /// </summary>
        /// <param name="slot">
        /// The ID of the slot to which the state of the game should be saved to.
        /// </param>
        public void SaveGame(Byte slot)
        {
            String path = Path.Combine(
                this.savesFolder, String.Format("Slot{0}.sav", slot));

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write((Byte)0x01);
                bw.Write(this.CurrentLevel.LevelFilename);
                bw.Write(this.character.CellPosition.X);
                bw.Write(this.character.CellPosition.Y);
                bw.Write(this.character.Orientation);

                this.PersistentStorage.Save(bw);
            }
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
            return this.currentKeyboardState.IsKeyDown(key) &&
                !this.lastKeyboardState.IsKeyDown(key);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::