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
using FSEGame.Engine.UI;
using FSEGame.BattleSystem;
using FSEGame.BattleSystem.Moves;
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

        #region Instance Members
        private GameState state;
        private MainMenuScreen mainMenu;
        private GameOverScreen gameOverScreen;
        private LoadScreen loadScreen;
        private IngameMenu ingameMenu;
        private SaveScreen saveScreen;
        private BattleUI battleUI;
        private FadeScreen fadeScreen;
        private CharacterController character;
        private PlayerCharacter playerCharacter;
        private Camera camera = null;
        private BattleManager battleManager;
        private StaticText debugText;
        private String savesFolder = null;
        private DialogueEventDelegate dialogueStartDelegate;
        private DialogueEventDelegate dialogueEndDelegate;
        #endregion

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

        #region Properties
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

        public PlayerCharacter PlayerCharacter
        {
            get
            {
                return this.playerCharacter;
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

        public BattleUI BattleUI
        {
            get
            {
                return this.battleUI;
            }
        }

        public BattleManager BattleManager
        {
            get
            {
                return this.battleManager;
            }
        }
        #endregion

        #region Events
        public event GameEventDelegate ContentLoaded;
        #endregion

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
            this.OnUpdate += new GameUpdateDelegate(Update);

            this.dialogueStartDelegate = new DialogueEventDelegate(DialogueStart);
            this.dialogueEndDelegate = new DialogueEventDelegate(DialogueEnd);

            this.RegisterDefaultDialogueHandlers();

            if (this.ContentLoaded != null)
                this.ContentLoaded(this);
        }
        #endregion

        public void RegisterDefaultDialogueHandlers()
        {
            this.DialogueManager.OnStart += this.dialogueStartDelegate;
            this.DialogueManager.OnEnd += this.dialogueEndDelegate;
        }

        public void UnregisterDefaultDialogueHandlers()
        {
            this.DialogueManager.OnStart -= this.dialogueStartDelegate;
            this.DialogueManager.OnEnd -= this.dialogueEndDelegate;
        }

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
            this.gameOverScreen = new GameOverScreen();
            this.loadScreen = new LoadScreen();
            this.ingameMenu = new IngameMenu();
            this.saveScreen = new SaveScreen();
            this.battleUI = new BattleUI();
            this.fadeScreen = new FadeScreen();

            this.gameOverScreen.Visible = false;
            this.loadScreen.Visible = false;
            this.ingameMenu.Visible = false;
            this.saveScreen.Visible = false;
            this.battleUI.Visible = false;
            this.fadeScreen.Visible = false;
            
            this.UIElements.Add(this.mainMenu);
            this.UIElements.Add(this.gameOverScreen);
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
        private new void Update(GameTime gameTime)
        {

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

            if(this.camera != null && this.CurrentTileset != null)
                this.camera.Update(this.GraphicsDevice.Viewport);

            if (this.state == GameState.Exploring)
                this.character.Update(gameTime);

            this.debugText.Text = String.Format("X: {0}\nY: {1}\nLevel: {2}\nTileset: {3}\nFPS: {4}",
                    this.character.CellPosition.X, this.character.CellPosition.Y,
                    this.CurrentLevel.Name, (this.CurrentTileset == null) ? "" : this.CurrentTileset.Name, this.FPSCounter.FPS);

            
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
            this.LoadLevel(name, entryPoint, true);
        }

        public void LoadLevel(String name, String entryPoint, Boolean fade)
        {
            if (fade)
            {
                this.character.Enabled = false;

                FadeScreenEventDelegate cleanupDelegate = null;
                cleanupDelegate = new FadeScreenEventDelegate(delegate
                {
                    this.fadeScreen.Visible = false;
                    this.fadeScreen.Enabled = false;
                    this.fadeScreen.Finished -= cleanupDelegate;

                    this.character.Enabled = true;
                });

                FadeScreenEventDelegate loadDelegate = null;
                loadDelegate = new FadeScreenEventDelegate(delegate
                {
                    base.LoadLevel(name, entryPoint);

                    LevelEntryPoint ep = this.CurrentLevel.GetEntryPoint(entryPoint);
                    this.character.CellPosition = new Vector2(ep.X, ep.Y);
                    this.character.Orientation = ep.Orientation;

                    this.fadeScreen.Finished -= loadDelegate;
                    this.fadeScreen.Finished += cleanupDelegate;
                    this.fadeScreen.FadeOut(1.0d);
                });

                this.fadeScreen.Enabled = true;
                this.fadeScreen.Visible = true;
                this.fadeScreen.Finished += loadDelegate;
                this.fadeScreen.FadeIn(1.0d);
            }
            else
            {
                base.LoadLevel(name, entryPoint);

                LevelEntryPoint ep = this.CurrentLevel.GetEntryPoint(entryPoint);
                this.character.CellPosition = new Vector2(ep.X, ep.Y);
                this.character.Orientation = ep.Orientation;

                this.character.Enabled = true;
            }
        }
        #endregion

        public void OpenMainMenu()
        {
            this.gameOverScreen.Hide();
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

        #region NewGameWithLevel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="entryPoint"></param>
        public void NewGameWithLevel(String level, String entryPoint)
        {
            this.mainMenu.Hide();

            // :: Initialise a new player character.
            CharacterAttributes playerAttributes = new CharacterAttributes();
            playerAttributes.Health = 100;
            playerAttributes.Defence = 2;
            playerAttributes.Strength = 15;
            playerAttributes.Magic = 50;

            this.playerCharacter = new PlayerCharacter(playerAttributes);
            this.playerCharacter.Moves.Add(new MeleeAttack());

            this.LoadLevel(level, entryPoint, false);

            this.character.Enabled = true;
            this.state = GameState.Exploring;
        }
        #endregion

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

            // :: Initialise a new player character.
            CharacterAttributes playerAttributes = new CharacterAttributes();
            playerAttributes.Health = 100;
            playerAttributes.Defence = 2;
            playerAttributes.Strength = 15;
            playerAttributes.Magic = 50;

            this.playerCharacter = new PlayerCharacter(playerAttributes);
            this.playerCharacter.Moves.Add(new MeleeAttack());

            // :: Temporarily change the dialogue event handlers so that
            // :: they don't change the game state to Exploring / Cutscene.
            DialogueEventDelegate introEndDelegate = null;

            introEndDelegate = new DialogueEventDelegate(delegate
            {
                this.DialogueManager.OnEnd -= introEndDelegate;
                this.DialogueManager.OnStart += this.dialogueStartDelegate;
                this.DialogueManager.OnEnd += this.dialogueEndDelegate;

                this.LoadLevel(@"Levels\PlayerHouse.xml", "Default", false);
                this.character.Enabled = false;

                FadeScreenEventDelegate fadeEndEvent = null;
                fadeEndEvent = new FadeScreenEventDelegate(delegate
                {
                    this.fadeScreen.Finished -= fadeEndEvent;
                    this.fadeScreen.Enabled = false;
                    this.fadeScreen.Visible = false;

                    this.character.Enabled = true;
                    this.state = GameState.Exploring;
                });

                this.fadeScreen.Finished += fadeEndEvent;
                this.fadeScreen.Visible = true;
                this.fadeScreen.Enabled = true;
                this.fadeScreen.FadeOut(1.0d);

                this.state = GameState.LevelTransition;
            });

            this.DialogueManager.OnStart -= this.dialogueStartDelegate;
            this.DialogueManager.OnEnd -= this.dialogueEndDelegate;
            this.DialogueManager.OnEnd += introEndDelegate;

            this.DialogueManager.PlayDialogue(@"FSEGame\Dialogues\Intro.xml");
        }
        #endregion

        #region LoadGame
        /// <summary>
        /// Loads the game state from the slot with the specified ID.
        /// </summary>
        /// <param name="slot">The ID of the save slot to load the game state from.</param>
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

                // :: Load the player character's attributes.
                CharacterAttributes baseAttributes = new CharacterAttributes();
                CharacterAttributes currentAttributes = new CharacterAttributes();

                baseAttributes.LoadFromBinary(br);
                currentAttributes.LoadFromBinary(br);

                // :: Initialise the player character using the attributes obtained
                // :: previously.
                this.playerCharacter = new PlayerCharacter(baseAttributes);
                this.playerCharacter.CurrentAttributes = currentAttributes;

                // ::
                Int32 moveCount = br.ReadInt32();

                for (Int32 i = 0; i < moveCount; i++)
                {
                    this.playerCharacter.Moves.Add(
                        MoveHelper.CreateFromName(br.ReadString()));
                }

                // :: Load the persistent storage data. The persistent storage
                // :: contains quest status information, etc.
                this.PersistentStorage.Load(br);

                // :: Hide the menues and enable the character controller.
                this.mainMenu.Hide();
                this.loadScreen.Hide();

                this.state = GameState.Exploring;
                this.character.Enabled = true;
            }
        }
        #endregion

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

                this.playerCharacter.BaseAttributes.SaveToBinary(bw);
                this.playerCharacter.CurrentAttributes.SaveToBinary(bw);

                // :: Save the player's moves.
                bw.Write(this.playerCharacter.Moves.Count);

                foreach (IMove move in this.playerCharacter.Moves)
                {
                    bw.Write(move.Name);
                }

                this.PersistentStorage.Save(bw);
            }
        }
        #endregion

        #region BeginBattle
        /// <summary>
        /// Loads battle data from the specified configuration file and then
        /// initiates a battle using the battle data from the configuration file.
        /// </summary>
        [ScriptFunction("BeginBattle")]
        public void BeginBattle(String configuration)
        {
            DialogueEventDelegate outroDelegate = null;
            outroDelegate = new DialogueEventDelegate(delegate
            {
                this.DialogueManager.OnEnd -= outroDelegate;
                this.RegisterDefaultDialogueHandlers();
                this.state = GameState.Menu;
                this.OpenMainMenu();
            });

            BattleEndedDelegate battleDelegate = null;
            battleDelegate = new BattleEndedDelegate(delegate(Boolean victory)
            {
                this.battleManager.Ended -= battleDelegate;

                if (victory)
                {
                    this.CurrentLevel.Unload();

                    this.character.Orientation = 180.0f;

                    this.UnregisterDefaultDialogueHandlers();
                    this.DialogueManager.OnEnd += outroDelegate;
                    this.DialogueManager.PlayDialogue(@"FSEGame\Dialogues\Outro.xml");
                }
                else
                {
                    this.state = GameState.Menu;
                    this.gameOverScreen.Show();
                }
            });

            this.BeginBattle(configuration, battleDelegate);
        }

        public void BeginBattle(String configuration, BattleEndedDelegate ended)
        {
            this.state = GameState.Battle;

            BattleEndedDelegate battleDelegate = null;
            battleDelegate = new BattleEndedDelegate(delegate(Boolean victory)
            {
                this.battleManager.Ended -= battleDelegate;

                ended(victory);
            });

            FadeScreenEventDelegate fadeEndEvent = null;
            fadeEndEvent = new FadeScreenEventDelegate(delegate
            {
                this.fadeScreen.Finished -= fadeEndEvent;
                this.fadeScreen.Enabled = false;
                this.fadeScreen.Visible = false;

                this.UnregisterDefaultDialogueHandlers();

                this.battleManager.Ended += battleDelegate;
                this.battleManager.Load(configuration);
                this.battleManager.Start();
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