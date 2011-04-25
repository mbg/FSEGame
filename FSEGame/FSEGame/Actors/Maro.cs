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
using FSEGame.BattleSystem;
using FSEGame.BattleSystem.Moves;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents the NPC named 'Maro' in the Village level.
    /// </summary>
    public class Maro : GenericNPC
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Maro(ActorProperties properties)
            : base(properties)
        {

        }
        #endregion

        #region CanAcquireStick
        /// <summary>
        /// Gets a value indicating whether the player can acquire coal from Maro
        /// or not.
        /// </summary>
        /// <returns>Returns true if the coal may be acquired at this time.</returns>
        private Boolean CanAcquireCoal()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The coal may be acquired if the BuildTorch quest is in stage 10.
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
        /// Performs Maro's action based on the current state of the game.
        /// </summary>
        protected override void PerformAction()
        {
            if (this.HasAcquiredCoal())
            {
                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MaroPostCoal.xml");
            }
            else if (this.CanAcquireCoal())
            {
                FSEGame.Singleton.UnregisterDefaultDialogueHandlers();

                DialogueEventDelegate endDelegate = null;
                endDelegate = new DialogueEventDelegate(delegate
                {
                    FSEGame.Singleton.DialogueManager.OnEnd -= endDelegate;
                    FSEGame.Singleton.RegisterDefaultDialogueHandlers();
                    
                    FSEGame.Singleton.State = GameState.Exploring;

                    this.GiveCoal();
                });

                FSEGame.Singleton.DialogueManager.OnEnd += endDelegate;
                FSEGame.Singleton.State = GameState.Cutscene;

                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MaroCoal.xml");
            }
            else
            {
                GameBase.Singleton.DialogueManager.PlayDialogue(
                    @"FSEGame\Dialogues\MaroDefault.xml");
            }
        }
        #endregion

        #region GiveCoal
        /// <summary>
        /// Sets the I_Coal game state variable to 10 (indicating its availability) and
        /// verifies whether the player owns both the coal and stick.
        /// </summary>
        private void GiveCoal()
        {
            FSEGame game = FSEGame.Singleton;

            if (!game.PersistentStorage.ContainsKey("I_Coal"))
            {
                game.PersistentStorage.Add("I_Coal", new PersistentStorageItem((UInt32)10));
            }

            // :: If the player has both the stick and the piece of coal,
            // :: give them the finished torch.
            if (this.HasAcquiredStick())
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