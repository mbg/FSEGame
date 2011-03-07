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
using System.Xml;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterAttributes
    {
        #region Instance Members
        /// <summary>
        /// The health attribute of the character.
        /// </summary>
        private UInt16 health;
        /// <summary>
        /// The strength attribute of the character.
        /// </summary>
        private UInt16 strength;
        /// <summary>
        /// The defence attribute of the character.
        /// </summary>
        private UInt16 defence;
        /// <summary>
        /// The magic attribute of the character.
        /// </summary>
        private UInt16 magic;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the health attribute of the character.
        /// </summary>
        public UInt16 Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
            }
        }
        /// <summary>
        /// Gets or sets the strength attribute of the character.
        /// </summary>
        public UInt16 Strength
        {
            get
            {
                return this.strength;
            }
            set
            {
                this.strength = value;
            }
        }
        /// <summary>
        /// Gets or sets the defence attribute of the character.
        /// </summary>
        public UInt16 Defence
        {
            get
            {
                return this.defence;
            }
            set
            {
                this.defence = value;
            }
        }
        /// <summary>
        /// Gets or sets the magic attribute of the character.
        /// </summary>
        public UInt16 Magic
        {
            get
            {
                return this.magic;
            }
            set
            {
                this.magic = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CharacterAttributes()
        {

        }
        #endregion

        #region SaveToBinary
        /// <summary>
        /// Saves the attributes using the specified BinaryWriter.
        /// </summary>
        /// <param name="bw"></param>
        public void SaveToBinary(BinaryWriter bw)
        {
            bw.Write(this.health);
            bw.Write(this.strength);
            bw.Write(this.defence);
            bw.Write(this.magic);
        }
        #endregion

        #region LoadFromBinary
        /// <summary>
        /// Loads the attributes from the specified BinaryReader.
        /// </summary>
        /// <param name="br"></param>
        public void LoadFromBinary(BinaryReader br)
        {
            this.health = br.ReadUInt16();
            this.strength = br.ReadUInt16();
            this.defence = br.ReadUInt16();
            this.magic = br.ReadUInt16();
        }
        #endregion

        #region LoadFromXML
        /// <summary>
        /// Loads the attributes from the specified XML element.
        /// </summary>
        /// <param name="attributesElement"></param>
        public void LoadFromXML(XmlElement attributesElement)
        {
            foreach (XmlNode childNode in attributesElement.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement element = (XmlElement)childNode;

                if (element.Name.Equals("Attribute"))
                {
                    switch (element.GetAttribute("Type"))
                    {
                        case "Health":
                            this.health = Convert.ToUInt16(element.InnerText);
                            break;
                        case "Strength":
                            this.strength = Convert.ToUInt16(element.InnerText);
                            break;
                        case "Defence":
                            this.defence = Convert.ToUInt16(element.InnerText);
                            break;
                        case "Magic":
                            this.magic = Convert.ToUInt16(element.InnerText);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::