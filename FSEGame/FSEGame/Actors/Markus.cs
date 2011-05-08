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
using FSEGame.BattleSystem.Moves;
using FSEGame.Engine.Actors;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents the NPC named 'Markus' in the Village level.
    /// </summary>
    public class Markus : GenericNPC
    {
        #region Instance Members
        #endregion

        #region Properties
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

        #region CanAcquireStick
        /// <summary>
        /// Gets a value indicating whether the player can acquire a stick from Markus
        /// or not.
        /// </summary>
        /// <returns>Returns true if the stick may be acquired at this time.</returns>
        private Boolean CanAcquireStick()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The stick may be acquired if the BuildTorch quest is in stage 10.
            if (game.PersistentStorage.ContainsKey("Q_BuildTorch"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["Q_BuildTorch"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

        #region HasAcquiredStick
        /// <summary>
        /// Gets a value indicating whether the player has acquired the stick.
        /// </summary>
        /// <returns>Returns true if the stick has been acquired.</returns>
        private Boolean HasAcquiredStick()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The stick was acquired if the I_Stick game state variable is set to 10.
            if (game.PersistentStorage.ContainsKey("I_Stick"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["I_Stick"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

        #region HasAcquiredCoal
        /// <summary>
        /// Gets a value indicating whether the player has acquired the coal.
        /// </summary>
        /// <returns>Returns true if the coal has been acquired.</returns>
        protected Boolean HasAcquiredCoal()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The coal was acquired if the I_Coal game state variable is set to 10.
            if (game.PersistentStorage.ContainsKey("I_Coal"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["I_Coal"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

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

                DialogueEventDelegate stickDelegate = null;
                stickDelegate = new DialogueEventDelegate(delegate
                {
                    FSEGame.Singleton.DialogueManager.OnEnd -= stickDelegate;
                    FSEGame.Singleton.RegisterDefaultDialogueHandlers();

                    FSEGame.Singleton.State = GameState.Exploring;

                    this.GiveStick();
                });

                // :: Declare the event handler for the end of the combat sequence. This
                // :: delegate will be called when the battle has ended and the outcome
                // :: will be supplied as argument.
                BattleEndedDelegate battleEndedDelegate = null;
                battleEndedDelegate = new BattleEndedDelegate(delegate(Boolean victory)
                {
                    FSEGame.Singleton.LoadLevel(@"Levels\House3.xml", "TutorialEnd", false);
                    FSEGame.Singleton.Character.Orientation = 0.0f;
                    FSEGame.Singleton.PlayerCharacter.CurrentAttributes.Health =
                        FSEGame.Singleton.PlayerCharacter.BaseAttributes.Health;

                    // :: If the player is victorious, give him the stick and play
                    // :: a short dialogue - however, if the player has lost, don't
                    // :: give him the stick.
                    if (victory)
                    {
                        FSEGame.Singleton.UnregisterDefaultDialogueHandlers();
                        FSEGame.Singleton.DialogueManager.OnEnd += stickDelegate;

                        FSEGame.Singleton.State = GameState.Cutscene;

                        GameBase.Singleton.DialogueManager.PlayDialogue(
                            @"FSEGame\Dialogues\MarkusPostTutorial.xml");
                    }
                    else
                    {
                        FSEGame.Singleton.RegisterDefaultDialogueHandlers();

                        GameBase.Singleton.DialogueManager.PlayDialogue(
                            @"FSEGame\Dialogues\MarkusPostTutorialLoss.xml");
                    }
                });

                DialogueEventDelegate endDelegate = null;
                endDelegate = new DialogueEventDelegate(delegate
                {
                    FSEGame.Singleton.DialogueManager.OnEnd -= endDelegate;
                    FSEGame.Singleton.RegisterDefaultDialogueHandlers();

                    FSEGame.Singleton.BeginBattle(@"BattleData\Tutorial1.xml", battleEndedDelegate);
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
        /// Sets the I_Stick game state variable to 10 (indicating its availability) and
        /// verifies whether the player owns both the coal and stick.
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

        #region GiveTorch
        /// <summary>
        /// 
        /// </summary>
        protected void GiveTorch()
        {
            FSEGame game = FSEGame.Singleton;

            if (!game.PersistentStorage.ContainsKey("T_Village"))
            {
                game.PersistentStorage.Add("T_Village", new PersistentStorageItem((UInt32)10));
                game.PlayerCharacter.Moves.Add(new BasicAttack());
            }

            GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\TorchObtained.xml");
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::