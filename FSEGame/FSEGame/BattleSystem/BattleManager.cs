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
using System.IO;
using FSEGame.Engine;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FSEGame.Actors;
using FSEGame.Engine.UI;
using FSEGame.Engine.Actors;
#endregion

namespace FSEGame.BattleSystem
{
    public delegate void MessageQueueFinishedDelegate();
    public delegate void BattleEndedDelegate(Boolean victory);

    /// <summary>
    /// 
    /// </summary>
    public class BattleManager
    {
        #region Instance Members
        /// <summary>
        /// The current battle data.
        /// </summary>
        private Battle currentBattle;

        private Queue<String> messageQueue;

        private Opponent currentOpponent;

        private IMove playerMove = null;
        private IMove opponentMove = null;

        private UIGridButtonEventDelegate openAttackMenuDelegate;
        private UIGridButtonEventDelegate performMoveDelegate;
        #endregion

        private event BattleEndedDelegate ended = null;

        #region Properties
        #endregion

        public event BattleEndedDelegate Ended
        {
            add
            {
                this.ended += value;
            }
            remove
            {
                this.ended -= value;
            }
        }

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BattleManager()
        {
            this.messageQueue = new Queue<String>();

            this.openAttackMenuDelegate = new UIGridButtonEventDelegate(this.OpenAttackMenu);
            this.performMoveDelegate = new UIGridButtonEventDelegate(this.PerformMove);
        }
        #endregion

        #region Load
        /// <summary>
        /// Loads battle data from the file with the specified path.
        /// </summary>
        /// <param name="filename">
        /// The path of the file from which the battle data should be loaded.
        /// </param>
        public void Load(String filename)
        {
            this.currentBattle = new Battle();

            this.currentBattle.LoadFromFile(Path.Combine(
                GameBase.Singleton.Content.RootDirectory, filename));
        }
        #endregion

        #region Start
        /// <summary>
        /// Starts a battle using previously loaded battle data.
        /// </summary>
        public void Start()
        {
            if (this.currentBattle == null)
                return;

            FSEGame g = FSEGame.Singleton;

            // :: Initialise the attack menu for the player.
            for (Int32 i = 0; i < 4; i++)
            {
                Int32 row = i / 2;
                Int32 column = i % 2;

                String displayName = String.Empty;

                if (i < g.PlayerCharacter.Moves.Count)
                {
                    displayName = g.PlayerCharacter.Moves[i].DisplayName;

                    g.BattleUI.AttackMenu.Buttons[row, column].Tag = g.PlayerCharacter.Moves[i];
                    g.BattleUI.AttackMenu.Buttons[row, column].Triggered += this.performMoveDelegate;
                }

                g.BattleUI.AttackMenu.Buttons[row, column].Text = displayName;
            }

            g.BattleUI.MainMenu.Buttons[0, 0].Triggered += this.openAttackMenuDelegate;

            g.BattleUI.Show();

            GameBase.Singleton.LoadLevel(this.currentBattle.LevelFilename);

            // :: Move the player character to the position specified in the battle data.
            LevelEntryPoint ep = 
                GameBase.Singleton.CurrentLevel.GetEntryPoint(this.currentBattle.PlayerPosition);
            g.Character.CellPosition = new Vector2(ep.X, ep.Y);
            g.Character.Orientation = 90.0f;

            g.Camera.DetachFromPlayer();
            g.Camera.LookAtCell(7, 5); // todo: move to xml

            this.LoadNextOpponent();

            DialogueEventDelegate introEndDelegate = null;
            introEndDelegate = new DialogueEventDelegate(delegate
            {
                FSEGame.Singleton.DialogueManager.OnEnd -= introEndDelegate;

                FSEGame.Singleton.BattleUI.MainMenu.Show();
            });

            g.DialogueManager.OnEnd += introEndDelegate;
            g.DialogueManager.PlayDialogue(this.currentBattle.IntroDialogue);
        }
        #endregion

        #region LoadNextOpponent
        /// <summary>
        /// Loads the next opponent.
        /// </summary>
        private void LoadNextOpponent()
        {
            if (this.currentBattle.Opponents.Count == 0)
                return;

            GameBase.Singleton.CurrentLevel.Actors.Clear();

            this.currentOpponent = this.currentBattle.Opponents.Dequeue();

            LevelEntryPoint ep = GameBase.Singleton.CurrentLevel.GetEntryPoint(this.currentOpponent.PositionName);

            ActorProperties actorProperties = new ActorProperties("OpponentNPC", ep.X, ep.Y);
            actorProperties.Properties["Tileset"] = this.currentOpponent.TilesetFilename;

            OpponentNPC npc = new OpponentNPC(actorProperties);
            npc.CellPosition = new Vector2(ep.X, ep.Y);

            GameBase.Singleton.CurrentLevel.Actors.Add(npc);

            this.UpdateUI();
        }
        #endregion

        #region UpdateUI
        /// <summary>
        /// Updates the UI.
        /// </summary>
        private void UpdateUI()
        {
            FSEGame.Singleton.BattleUI.OpponentName = this.currentOpponent.Name;
            FSEGame.Singleton.BattleUI.OpponentHealth = this.currentOpponent.BaseAttributes.Health;
            FSEGame.Singleton.BattleUI.CurrentOpponentHealth = this.currentOpponent.CurrentAttributes.Health;

            FSEGame.Singleton.BattleUI.PlayerHealth = FSEGame.Singleton.PlayerCharacter.BaseAttributes.Health;
            FSEGame.Singleton.BattleUI.CurrentPlayerHealth = FSEGame.Singleton.PlayerCharacter.CurrentAttributes.Health;
        }
        #endregion

        #region OpenAttackMenu
        /// <summary>
        /// Hides the main battle menu and opens the attack menu instead.
        /// </summary>
        /// <param name="sender"></param>
        private void OpenAttackMenu(UIGridButton sender)
        {
            FSEGame.Singleton.BattleUI.MainMenu.Hide();
            FSEGame.Singleton.BattleUI.AttackMenu.Show();
        }
        #endregion

        #region RunAI
        /// <summary>
        /// Runs the battle AI for the current opponent to compute which move it
        /// should perform.
        /// </summary>
        private void RunAI()
        {
            // :: Score all moves available to the opponent based on the current
            // :: battle context and sort them.
            ScoredMoveCollection collection = new ScoredMoveCollection();

            foreach (IMove move in this.currentOpponent.Moves)
            {
                ScoredMove scoredMove = new ScoredMove(
                    move, move.Score(this.currentOpponent, FSEGame.Singleton.PlayerCharacter));

                collection.Add(scoredMove);
            }

            // :: Get the top rated move from the sorted collection and verify that
            // :: it can be performed (i.e. that it exists and that it's score is greater
            // :: than zero).
            ScoredMove topMove = collection.GetTopRated();

            if (topMove != null && topMove.Score > 0)
            {
                this.opponentMove = topMove.Move;
            }
        }
        #endregion

        #region ProcessMessageQueue
        /// <summary>
        /// Processes the current message queue.
        /// </summary>
        private void ProcessMessageQueue(MessageQueueFinishedDelegate finished)
        {
            if (this.messageQueue.Count > 0)
            {
                DialogueEventDelegate endDelegate = null;
                endDelegate = new DialogueEventDelegate(delegate
                {
                    FSEGame.Singleton.DialogueManager.OnEnd -= endDelegate;
                    this.ProcessMessageQueue(finished);
                });

                FSEGame.Singleton.DialogueManager.OnEnd += endDelegate;
                FSEGame.Singleton.DialogueManager.ShowSingleMessage(this.messageQueue.Dequeue());
            }
            else
            {
                finished();
            }
        }
        #endregion

        #region PerformMove
        /// <summary>
        /// Triggered when the player selected a move.
        /// </summary>
        /// <param name="sender"></param>
        private void PerformMove(UIGridButton sender)
        {
            FSEGame.Singleton.BattleUI.AttackMenu.Hide();

            this.PerformMove((IMove)sender.Tag);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="move"></param>
        private void PerformMove(IMove move)
        {
            this.playerMove = move;
            this.opponentMove = null;

            this.RunAI();

            this.playerMove.PrePerform(FSEGame.Singleton.PlayerCharacter, this.currentOpponent);
            this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate 
            {
                this.playerMove.Perform(FSEGame.Singleton.PlayerCharacter, this.currentOpponent);

                this.UpdateUI();

                if (this.currentOpponent.CurrentAttributes.Health <= 0)
                {
                    this.messageQueue.Enqueue(String.Format("{0} was defeated!", this.currentOpponent.Name));
                    this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
                    {
                        this.currentOpponent = null;
                        this.LoadNextOpponent();

                        if (this.currentOpponent != null)
                        {
                            this.UpdateUI();

                            this.EndRound();
                        }
                        else
                        {
                            this.EndBattle(true);
                        }
                    }));
                }
                else if (this.opponentMove != null)
                {
                    this.opponentMove.PrePerform(this.currentOpponent, FSEGame.Singleton.PlayerCharacter);
                    this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
                    {
                        this.opponentMove.Perform(this.currentOpponent, FSEGame.Singleton.PlayerCharacter);

                        this.UpdateUI();

                        if (FSEGame.Singleton.PlayerCharacter.CurrentAttributes.Health <= 0)
                        {
                            this.messageQueue.Enqueue("Landor was defeated!");
                            this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
                            {
                                this.EndBattle(false);
                            }));
                        }
                        else
                        {
                            this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
                            {
                                this.EndRound();
                            }));
                        }
                    }));
                }
                else
                {
                    this.messageQueue.Enqueue(String.Format("{0} can't move!", this.currentOpponent.Name));
                    this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
                    {
                        this.EndRound();
                    }));
                }
            }));
        }
        #endregion

        private void EndBattle(Boolean victory)
        {
            FSEGame g = FSEGame.Singleton;

            if (victory)
                this.messageQueue.Enqueue("Landor is victorious!");

            // :: Uninitialise the attack menu for the player.
            for (Int32 i = 0; i < 4; i++)
            {
                Int32 row = i / 2;
                Int32 column = i % 2;

                g.BattleUI.AttackMenu.Buttons[row, column].Triggered -= this.performMoveDelegate;
            }

            g.BattleUI.MainMenu.Buttons[0, 0].Triggered -= this.openAttackMenuDelegate;

            this.ProcessMessageQueue(new MessageQueueFinishedDelegate(delegate
            {
                g.BattleUI.Hide();
                g.Camera.AttachToPlayer();

                if (this.ended != null)
                    this.ended(victory);

            }));
        }

        public void AddToMessageQueue(String message)
        {
            this.messageQueue.Enqueue(message);
        }

        #region EndMove
        /// <summary>
        /// Ends a round.
        /// </summary>
        private void EndRound()
        {
            this.RegenerateStats();

            FSEGame.Singleton.BattleUI.MainMenu.Show();
        }
        #endregion

        #region RegenerateStats
        /// <summary>
        /// Regenerates the player's and opponent's stats at the end of a round.
        /// </summary>
        private void RegenerateStats()
        {
            FSEGame game = FSEGame.Singleton;

            // :: Regenerate the player's mana.
            if (game.PlayerCharacter.CurrentAttributes.Magic <
                game.PlayerCharacter.BaseAttributes.Magic)
            {
                UInt16 newMana = (UInt16)(game.PlayerCharacter.CurrentAttributes.Magic + 5);

                if (newMana > game.PlayerCharacter.BaseAttributes.Magic)
                    newMana = game.PlayerCharacter.BaseAttributes.Magic;

                game.PlayerCharacter.CurrentAttributes.Magic = newMana;
            }

            // :: Regenerate the opponent's mana.
            if (this.currentOpponent.CurrentAttributes.Magic <
                this.currentOpponent.BaseAttributes.Magic)
            {
                UInt16 newMana = (UInt16)(this.currentOpponent.CurrentAttributes.Magic + 5);

                if (newMana > this.currentOpponent.BaseAttributes.Magic)
                    newMana = this.currentOpponent.BaseAttributes.Magic;

                this.currentOpponent.CurrentAttributes.Magic = newMana;
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::