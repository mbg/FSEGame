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
using System.Xml;
using FSEGame.BattleSystem.Moves;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Battle
    {
        #region Instance Members
        private String levelFilename;
        private String playerPosition;
        private String introDialogue;
        private Queue<Opponent> opponents;
        #endregion

        #region Properties
        public String LevelFilename
        {
            get
            {
                return this.levelFilename;
            }
        }

        public String PlayerPosition
        {
            get
            {
                return this.playerPosition;
            }
        }

        public String IntroDialogue
        {
            get
            {
                return this.introDialogue;
            }
        }

        public Queue<Opponent> Opponents
        {
            get
            {
                return this.opponents;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Battle()
        {
            this.opponents = new Queue<Opponent>();
        }
        #endregion

        #region LoadFromFile
        /// <summary>
        /// Loads the battle data from an XML document.
        /// </summary>
        /// <param name="filename">
        /// The path to the file containing the XML document.
        /// </param>
        public void LoadFromFile(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlElement rootElement = doc.DocumentElement;

            this.levelFilename = rootElement.GetAttribute("Level");
            this.playerPosition = rootElement.GetAttribute("Position");
            this.introDialogue = rootElement.GetAttribute("Intro");

            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("Opponent"))
                {
                    this.LoadOpponent(childElement);
                }
            }
        }
        #endregion

        #region LoadOpponent
        /// <summary>
        /// Loads an opponent from the data contained in the specified XML element.
        /// </summary>
        /// <param name="element"></param>
        private void LoadOpponent(XmlElement element)
        {
            CharacterAttributes attributes = new CharacterAttributes();
            List<IMove> moves = new List<IMove>();

            foreach (XmlNode childNode in element.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("Attributes"))
                {
                    attributes.LoadFromXML(childElement);
                }
                else if (childElement.Name.Equals("Moves"))
                {
                    foreach (XmlNode moveNode in childElement.ChildNodes)
                    {
                        if (moveNode.NodeType != XmlNodeType.Element)
                            continue;

                        XmlElement moveElement = (XmlElement)moveNode;

                        if (moveElement.Name.Equals("Move"))
                        {
                            moves.Add(MoveHelper.CreateFromName(moveElement.GetAttribute("Type")));
                        }
                    }
                }
            }

            Opponent opponent = new Opponent(attributes);
            opponent.Name = element.GetAttribute("Name");
            opponent.IsBoss = Convert.ToBoolean(element.GetAttribute("IsBoss"));
            opponent.TilesetFilename = element.GetAttribute("Tileset");
            opponent.PositionName = element.GetAttribute("Position");

            foreach (IMove move in moves)
                opponent.Moves.Add(move);

            this.opponents.Enqueue(opponent);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::