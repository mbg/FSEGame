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
using System.Collections.Generic;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Opponent
    {
        #region Instance Members
        /// <summary>
        /// The name of the opponent.
        /// </summary>
        private String name;
        /// <summary>
        /// A value indicating whether this opponent is a boss or not.
        /// </summary>
        private Boolean isBoss = false;
        /// <summary>
        /// The filename of the opponent's tileset.
        /// </summary>
        private String tilesetFilename;
        /// <summary>
        /// The name of the opponent's position in the level.
        /// </summary>
        private String positionName;
        /// <summary>
        /// The current attributes of this opponent.
        /// </summary>
        private CharacterAttributes currentAttributes;
        /// <summary>
        /// The base attributes of this opponent.
        /// </summary>
        private CharacterAttributes baseAttributes;
        /// <summary>
        /// The list of moves which this opponent can use.
        /// </summary>
        private List<IMove> moves;
        #endregion

        #region Properties
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Boolean IsBoss
        {
            get
            {
                return this.isBoss;
            }
            set
            {
                this.isBoss = value;
            }
        }

        public CharacterAttributes CurrentAttributes
        {
            get
            {
                return this.currentAttributes;
            }
            set
            {
                this.currentAttributes = value;
            }
        }
        public CharacterAttributes BaseAttributes
        {
            get
            {
                return this.baseAttributes;
            }
        }

        public String TilesetFilename
        {
            get
            {
                return this.tilesetFilename;
            }
            set
            {
                this.tilesetFilename = value;
            }
        }

        public String PositionName
        {
            get
            {
                return this.positionName;
            }
            set
            {
                this.positionName = value;
            }
        }

        public List<IMove> Moves
        {
            get
            {
                return this.moves;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Opponent(CharacterAttributes attributes)
        {
            // :: Since the base attributes won't change, we can just
            // :: use the obtained reference for them. However, the current
            // :: attributes may change during battle so we have to initialise
            // :: a new variable for it.
            this.baseAttributes = attributes;
            this.currentAttributes = new CharacterAttributes();

            this.currentAttributes.Health = attributes.Health;
            this.currentAttributes.Strength = attributes.Strength;
            this.currentAttributes.Defence = attributes.Defence;
            this.currentAttributes.Magic = attributes.Magic;

            this.moves = new List<IMove>();
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::