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
using FSEGame.Engine.Dialogues;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Engine
{
    public delegate void DialogueEventDelegate(DialogueManager sender);

    /// <summary>
    /// 
    /// </summary>
    public class DialogueManager
    {
        #region Instance Members
        private Dialogue currentDialogue;
        private DialogueNode currentNode;
        private DialogueScreen screen;
        private float elapsedTime = 0.0f;
        private Boolean isPlaying = false;
        #endregion

        private event DialogueEventDelegate onStart = null;
        private event DialogueEventDelegate onEnd = null;

        #region Properties
        public Boolean IsPlaying
        {
            get
            {
                return this.isPlaying;
            }
        }
        #endregion

        public event DialogueEventDelegate OnStart
        {
            add
            {
                this.onStart += value;
            }
            remove
            {
                this.onStart -= value;
            }
        }

        public event DialogueEventDelegate OnEnd
        {
            add
            {
                this.onEnd += value;
            }
            remove
            {
                this.onEnd -= value;
            }
        }

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueManager()
        {
            this.screen = new DialogueScreen();

            GameBase.Singleton.UIElements.Add(this.screen);
        }
        #endregion

        public void LoadDialogue(String filename)
        {
            XmlDocument doc = new XmlDocument();
            this.currentDialogue = new Dialogue();
            

            doc.Load(filename);

            XmlElement root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)node;

                if (childElement.Name.Equals("Variables"))
                {
                    this.ParseVariables(childElement);
                }
                else if (childElement.Name.Equals("Speech"))
                {
                    this.ParseSpeech(childElement, this.currentDialogue);
                }
            }
        }

        #region ParseVariables
        /// <summary>
        /// Parses variables in a dialogue XML file.
        /// </summary>
        /// <param name="element"></param>
        private void ParseVariables(XmlElement element)
        {
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)node;

                if (childElement.Name.Equals("StringVar"))
                {
                    DialogueStringVariable strVar = new DialogueStringVariable(
                        childElement.GetAttribute("Name"));

                    strVar.Value = childElement.InnerText;

                    this.currentDialogue.Variables.Add(strVar.Name, strVar);
                }
            }
        }
        #endregion

        private void ParseSpeech(XmlElement element, DialogueNode parent)
        {
            DialogueSpeechNode speechNode = new DialogueSpeechNode(
                element.GetAttribute("ID"));

            speechNode.StringVariable = element.GetAttribute("TextVar");
            speechNode.Timeout = Convert.ToSingle(element.GetAttribute("Time"));

            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)node;

                if (childElement.Name.Equals("Speech"))
                {
                    this.ParseSpeech(childElement, speechNode);
                }
            }

            parent.Nodes.Add(speechNode);
        }

        #region PlayDialogue
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void PlayDialogue(String filename)
        {
            if (this.isPlaying)
                return;

            this.LoadDialogue(filename);
            this.PlayDialogue();
        }
        /// <summary>
        /// 
        /// </summary>
        public void PlayDialogue()
        {
            if (this.isPlaying)
                return;

            if (this.onStart != null)
                this.onStart(this);

            this.isPlaying = true;
            this.elapsedTime = 0.0f;
            this.currentNode = this.currentDialogue.GetFirstChild();

            this.screen.Visible = true;

            if (this.currentNode != null)
            {
                this.screen.Text = this.ResolveStringVariable((this.currentNode as DialogueSpeechNode).StringVariable);
            }
        }
        #endregion

        #region ResolveStringVariable
        /// <summary>
        /// Resolves a string variable.
        /// </summary>
        /// <param name="name">The name of the variable to resolve.</param>
        /// <returns>
        /// Returns the value of the variable with the specified name or an 
        /// empty string if no variable with the specified name could be found.
        /// </returns>
        private String ResolveStringVariable(String name)
        {
            if (!this.currentDialogue.Variables.ContainsKey(name))
                return String.Empty;

            DialogueVariable var = this.currentDialogue.Variables[name];

            if (var.Type == DialogueVariableType.String)
                return (var as DialogueStringVariable).Value;

            return String.Empty;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the dialogue manager with the elapsed game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!this.isPlaying)
                return;

            if(this.screen.Finished)
                this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.elapsedTime >= 2.0f)
            {
                DialogueNode nextNode = this.currentNode.GetFirstChild();

                if (nextNode == null)
                {
                    this.screen.Visible = false;
                    this.isPlaying = false;

                    if (this.onEnd != null)
                        this.onEnd(this);
                }
                else
                {
                    this.currentNode = nextNode;
                    this.elapsedTime = 0.0f;

                    this.screen.Text = this.ResolveStringVariable((this.currentNode as DialogueSpeechNode).StringVariable);
                }
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch batch)
        {
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::