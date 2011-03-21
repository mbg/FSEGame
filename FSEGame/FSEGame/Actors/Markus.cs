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
using Microsoft.Xna.Framework.Input;
using FSEGame.BattleSystem;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// 
    /// </summary>
    public class Markus : GenericNPC
    {
        #region Instance Members
        #endregion

        #region Properties
        /// <summary>
        /// Returns false.
        /// </summary>
        public override Boolean Passable
        {
            get 
            {
                return false;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Markus(ActorProperties properties)
            : base(properties)
        {

        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region CanAcquireStick
        /// <summary>
        /// Gets a value indicating whether the player can acquire a stick from Markus
        /// or not.
        /// </summary>
        /// <returns></returns>
        private Boolean CanAcquireStick()
        {
            FSEGame game = FSEGame.Singleton;

            if (game.PersistentStorage.ContainsKey("Q_BuildTorch"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["Q_BuildTorch"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

        private Boolean HasAcquiredStick()
        {
            FSEGame game = FSEGame.Singleton;

            if (game.PersistentStorage.ContainsKey("I_Stick"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["I_Stick"].Item;

                return questProgress == 10;
            }

            return false;
        }

        protected Boolean HasAcquiredCoal()
        {
            FSEGame game = FSEGame.Singleton;

            if (game.PersistentStorage.ContainsKey("I_Coal"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["I_Coal"].Item;

                return questProgress == 10;
            }

            return false;
        }

        #region PerformAction
        /// <summary>
        /// Performs Markus' action based on the current state of the game.
        /// </summary>
        protected override void PerformAction()
        {
            if (this.HasAcquiredStick())
            {
                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MarkusPostStick.xml");
            }
            else if (this.CanAcquireStick())
            {
                FSEGame.Singleton.UnregisterDefaultDialogueHandlers();

                // :: Declare the event handler for the end of the combat sequence. This
                // :: delegate will be called when the battle has ended and the outcome
                // :: will be supplied as argument.
                BattleEndedDelegate battleEndedDelegate = null;
                battleEndedDelegate = new BattleEndedDelegate(delegate(Boolean victory)
                {
                    FSEGame.Singleton.BattleManager.Ended -= battleEndedDelegate;
                    FSEGame.Singleton.RegisterDefaultDialogueHandlers();

                    FSEGame.Singleton.LoadLevel(@"Levels\House3.xml", "TutorialEnd", false);
                    FSEGame.Singleton.Character.Orientation = 0.0f;
                    FSEGame.Singleton.PlayerCharacter.CurrentAttributes.Health =
                        FSEGame.Singleton.PlayerCharacter.BaseAttributes.Health;

                    // :: If the player is victorious, give him the stick and play
                    // :: a short dialogue - however, if the player has lost, don't
                    // :: give him the stick.
                    if (victory)
                    {
                        this.GiveStick();

                        GameBase.Singleton.DialogueManager.PlayDialogue(
                            @"FSEGame\Dialogues\MarkusPostTutorial.xml");
                    }
                    else
                    {
                        GameBase.Singleton.DialogueManager.PlayDialogue(
                            @"FSEGame\Dialogues\MarkusPostTutorialLoss.xml");
                    }
                });

                DialogueEventDelegate endDelegate = null;
                endDelegate = new DialogueEventDelegate(delegate
                {
                    FSEGame.Singleton.DialogueManager.OnEnd -= endDelegate;
                    FSEGame.Singleton.RegisterDefaultDialogueHandlers();

                    FSEGame.Singleton.BattleManager.Ended += battleEndedDelegate;
                    FSEGame.Singleton.BeginBattle(@"BattleData\Tutorial1.xml");
                });

                FSEGame.Singleton.DialogueManager.OnEnd += endDelegate;
                FSEGame.Singleton.State = GameState.Cutscene;

                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MarkusStick.xml");
            }
            else
            {
                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MarkusDefault.xml");
            }
        }
        #endregion

        #region GiveStick
        /// <summary>
        /// 
        /// </summary>
        private void GiveStick()
        {
            FSEGame game = FSEGame.Singleton;

            if (!game.PersistentStorage.ContainsKey("I_Stick"))
            {
                game.PersistentStorage.Add("I_Stick", new PersistentStorageItem((UInt32)10));
            }

            // :: If the player has both the stick and the piece of coal,
            // :: give them the finished torch.
            if (this.HasAcquiredCoal())
            {
                this.GiveTorch();
            }
        }
        #endregion

        protected void GiveTorch()
        {
            FSEGame game = FSEGame.Singleton;

            if (!game.PersistentStorage.ContainsKey("T_Village"))
            {
                game.PersistentStorage.Add("T_Village", new PersistentStorageItem((UInt32)10));
            }

            GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\TorchObtained.xml");
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::