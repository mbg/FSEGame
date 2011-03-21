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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents a non-interactive NPC in a battle.
    /// </summary>
    public class OpponentNPC : Actor
    {
        #region Instance Members

        private ActorProperties properties;
        /// <summary>
        /// The tileset used by this actor.
        /// </summary>
        private Tileset tileset;
        #endregion

        #region Properties
        public override Boolean Passable
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public OpponentNPC(ActorProperties properties)
        {
            this.properties = properties;

            this.tileset = new Tileset(16, 1, 1);
            this.tileset.Load(GameBase.Singleton.Content, properties.Properties["Tileset"]);
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the NPC actor.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the NPC.
        /// </summary>
        /// <param name="batch">The current sprite batch.</param>
        public override void Draw(SpriteBatch batch)
        {
            this.tileset.DrawTile(
                batch, 
                0, 
                GridHelper.GridPositionToAbsolute(base.CellPosition));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::