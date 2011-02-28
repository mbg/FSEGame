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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents a generic NPC with a basic animation with which the player
    /// may interact.
    /// </summary>
    public class GenericNPC : Actor
    {
        #region Instance Members
        /// <summary>
        /// The tileset used by this actor.
        /// </summary>
        private Tileset tileset;
        /// <summary>
        /// The tile that is currently being used to render the NPC.
        /// </summary>
        private UInt32 currentTile = LOOK_RIGHT;
        /// <summary>
        /// The time which has elapsed since the NPC last turned.
        /// </summary>
        private float timeSinceLastTurn = 0.0f;
        #endregion

        #region Constants
        private const UInt32 LOOK_RIGHT = 0;
        private const UInt32 LOOK_LEFT = 1;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GenericNPC()
        {
            this.tileset = new Tileset(16, 1, 2);
            this.tileset.Load(FSEGame.Singleton.Content, @"Tilesets\GenericNPC.xml");
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the NPC actor.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // :: This actor has a basic animation consisting of two states. Every
            // :: 1.5 seconds, the NPC will turn either left or right.
            this.timeSinceLastTurn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timeSinceLastTurn >= 1.5f)
            {
                this.timeSinceLastTurn -= 1.5f;

                if (this.currentTile == LOOK_RIGHT)
                    this.currentTile = LOOK_LEFT;
                else
                    this.currentTile = LOOK_RIGHT;
            }

            // :: The player may interact with this actor. If enter has been
            // :: pressed, verify that the player is in the right position
            // :: to interact with this actor and that it has the right 
            // :: orientation.
            if (ks.IsKeyDown(Keys.Enter))
            {
                if (this.IsPlayerInInteractionPosition())
                {
                    FSEGame.Singleton.DialogueManager.PlayDialogue(
                        @"FSEGame\Dialogues\TestDialogue.xml");
                }
            }
        }
        #endregion

        private Boolean IsPlayerInInteractionPosition()
        {
            Vector2 playerPosition = FSEGame.Singleton.Character.CellPosition;
            float playerOrientation = FSEGame.Singleton.Character.Orientation;

            return ((playerPosition.X == this.CellPosition.X) &&
                (playerPosition.Y == this.CellPosition.Y + 1) &&
                (playerOrientation == 0.0f)) ||
                ((playerPosition.X == this.CellPosition.X) &&
                (playerPosition.Y == this.CellPosition.Y - 1) &&
                (playerOrientation == 180.0f)) ||
                ((playerPosition.X == this.CellPosition.X - 1) &&
                (playerPosition.Y == this.CellPosition.Y) &&
                (playerOrientation == 90.0f)) ||
                ((playerPosition.X == this.CellPosition.X + 1) &&
                (playerPosition.Y == this.CellPosition.Y) &&
                (playerOrientation == 270.0f));
        }

        #region Draw
        /// <summary>
        /// Draws the NPC.
        /// </summary>
        /// <param name="batch">The current sprite batch.</param>
        public override void Draw(SpriteBatch batch)
        {
            this.tileset.DrawTile(
                batch, 
                this.currentTile, 
                GridHelper.GridPositionToAbsolute(base.CellPosition));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::