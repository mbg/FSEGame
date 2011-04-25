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
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerCharacter : Opponent
    {
        #region Instance Members
        /// <summary>
        /// The current number of experience points the player has.
        /// </summary>
        private UInt32 experience = 0;
        /// <summary>
        /// The number of experience points required for the next level.
        /// </summary>
        private UInt32 nextLevelExperience = 500;
        /// <summary>
        /// The current level of the player.
        /// </summary>
        private UInt16 level = 1;
        #endregion

        #region Properties
        public UInt32 Experience
        {
            get
            {
                return this.experience;
            }
        }
        public UInt32 NextLevelExperience
        {
            get
            {
                return this.nextLevelExperience;
            }
        }
        public UInt16 Level
        {
            get
            {
                return this.level;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public PlayerCharacter(CharacterAttributes attributes)
            : base(attributes)
        {
            this.Name = "Landor";
        }
        #endregion

        public void GiveExperience(UInt32 amount)
        {
            this.experience += amount;

            if (this.experience > this.nextLevelExperience)
                this.LevelUp();
        }

        private void LevelUp()
        {
            this.level++;
            this.nextLevelExperience *= 2;

            if (this.experience > this.nextLevelExperience)
                this.LevelUp();
        }

        public void SaveToBinary(BinaryWriter writer)
        {
            writer.Write(this.level);
            writer.Write(this.experience);
            writer.Write(this.nextLevelExperience);
        }

        public void LoadFromBinary(BinaryReader reader)
        {
            this.level = reader.ReadUInt16();
            this.experience = reader.ReadUInt32();
            this.nextLevelExperience = reader.ReadUInt32();
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::