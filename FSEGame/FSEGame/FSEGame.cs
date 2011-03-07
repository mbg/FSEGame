﻿// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
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
using FSEGame.Engine.UI;
using FSEGame.BattleSystem;
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
        private BattleUI battleUI;
        private FadeScreen fadeScreen;
        private CharacterController character;
        private Camera camera = null;
        private BattleManager battleManager;
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

            this.battleManager = new BattleManager();

            this.RegisterClass(this);
        }
        #endregion

        #region Initialise
        /// <summary>
        /// Initialises the game.
        /// </summary>
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
        #endregion

        #region Draw
        /// <summary>
        /// Draws additional sprites after the level tiles and entities have been
        /// drawn to the frame buffer, but before the UI elements are drawn.
        /// </summary>
        /// <param name="batch"></param>
        private void Draw(SpriteBatch batch)
        {
            if(this.state != GameState.Intro)
                this.character.Draw(batch);
        }
        #endregion

        private void DialogueStart(DialogueManager sender)
        {
            this.state = GameState.Cutscene;
        }
        
        private void DialogueEnd(DialogueManager sender)
        {
            this.state = GameState.Exploring;
        }

        #region LoadContent
        /// <summary>
        /// Loads content for the game.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            this.mainMenu = new MainMenuScreen();
            this.loadScreen = new LoadScreen();
            this.ingameMenu = new IngameMenu();
            this.saveScreen = new SaveScreen();
            this.battleUI = new BattleUI();
            this.fadeScreen = new FadeScreen();

            this.loadScreen.Visible = false;
            this.ingameMenu.Visible = false;
            this.saveScreen.Visible = false;
            this.battleUI.Visible = false;
            this.fadeScreen.Visible = false;

            this.UIElements.Add(this.mainMenu);
            this.UIElements.Add(this.loadScreen);
            this.UIElements.Add(this.ingameMenu);
            this.UIElements.Add(this.saveScreen);
            this.UIElements.Add(this.battleUI);
            this.UIElements.Add(this.fadeScreen);

            this.character = new CharacterController();

            this.camera = new Camera();

            this.debugText = new StaticText(this.DefaultFont);
            this.debugText.Position = new Vector2(20, 20);
            this.debugText.Visible = false;

            this.UIElements.Add(debugText);
        }
        #endregion

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

        #region LoadLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entryPoint"></param>
        public override void LoadLevel(string name, string entryPoint)
        {
            this.character.Enabled = false;
            this.character.Visible = false;

            base.LoadLevel(name, entryPoint);

            LevelEntryPoint ep = this.CurrentLevel.GetEntryPoint(entryPoint);
            this.character.CellPosition = new Vector2(ep.X, ep.Y);

            this.character.Enabled = true;
            this.character.Visible = true;
        }
        #endregion

        public void OpenMainMenu()
        {
            this.loadScreen.Hide();
            this.mainMenu.Show();

            this.CurrentLevel.Unload();
            this.character.Enabled = false;
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
        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void NewGame()
        {
            // :: Change the game state and make sure that no level
            // :: is loaded so that we have a black background.
            this.state = GameState.Intro;

            this.CurrentLevel.Unload();
            this.mainMenu.Hide();

            // :: Temporarily change the dialogue event handlers so that
            // :: they don't change the game state to Exploring / Cutscene.
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

            //this.DialogueManager.PlayDialogue(@"FSEGame\Dialogues\TestDialogue.xml");
            this.DialogueManager.PlayDialogue(@"FSEGame\Dialogues\Intro.xml");
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

        #region BeginBattle
        /// <summary>
        /// 
        /// </summary>
        [ScriptFunction("BeginBattle")]
        public void BeginBattle(String configuration)
        {
            this.state = GameState.Battle;

            FadeScreenEventDelegate fadeEndEvent = null;
            fadeEndEvent = new FadeScreenEventDelegate(delegate
            {
                this.fadeScreen.Finished -= fadeEndEvent;
                this.fadeScreen.Enabled = false;
                this.fadeScreen.Visible = false;

                this.battleManager.Load(configuration);
                this.battleUI.Show();
            });

            this.fadeScreen.Visible = true;
            this.fadeScreen.Enabled = true;
            this.fadeScreen.Finished += fadeEndEvent;
            this.fadeScreen.FadeIn(1.0d);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::