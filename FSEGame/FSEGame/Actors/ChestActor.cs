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
using Microsoft.Xna.Framework.Graphics;
using FSEGame.Engine.Actors;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// 
    /// </summary>
    public class ChestActor : Actor
    {
        #region Instance Members
        private ActorProperties properties;
        private Tileset tileset;
        private String id;
        private Boolean opened = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets false.
        /// </summary>
        public override Boolean Passable
        {
            get
            {
                return false;
            }
            set
            {
                // ignore
            }
        }
        /// <summary>
        /// Gets a value indicating whether the chest has been opened.
        /// </summary>
        public Boolean Opened
        {
            get
            {
                return this.opened;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ChestActor(ActorProperties properties)
        {
            this.properties = properties;

            this.tileset = new Tileset(16, 1, 2);
            this.tileset.Load(FSEGame.Singleton.Content, @"Tilesets\Chest.xml");
        }
        #endregion

        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            UInt32 tile = 0;

            if (this.opened)
                tile = 1;

            this.tileset.DrawTile(
                batch,
                tile,
                this.AbsolutePosition);
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (FSEGame.Singleton.State != GameState.Exploring)
                return;

            if(this.IsPlayerInInteractionPosition() && 
                (GameBase.Singleton.IsKeyPressed(Keys.Enter) || GameBase.Singleton.IsKeyPressed(Keys.Space)))
            {
                this.PerformAction();
            }
        }

        protected virtual void PerformAction()
        {
            this.opened = true;
        }

        #region IsPlayerInInteractionPosition
        /// <summary>
        /// Checks whether the player character is in a position which allows
        /// it to interact with this actor.
        /// </summary>
        /// <returns>
        /// Returns true if the player is allowed to interact with this NPC 
        /// or false if not.
        /// </returns>
        private Boolean IsPlayerInInteractionPosition()
        {
            Vector2 playerPosition = FSEGame.Singleton.Character.CellPosition;
            float playerOrientation = FSEGame.Singleton.Character.Orientation;

            return ((playerPosition.X == this.CellPosition.X) &&
                (playerPosition.Y == this.CellPosition.Y + 1) &&
                (playerOrientation == 0.0f));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::