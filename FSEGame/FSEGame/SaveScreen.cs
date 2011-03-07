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
using FSEGame.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame
{
    /// <summary>
    /// Represents a menu screen which enables the user to save the game state
    /// in one of four slots.
    /// </summary>
    public class SaveScreen : UIContainer
    {
        #region Instance Members
        /// <summary>
        /// The button list container for the four slot buttons.
        /// </summary>
        private UIButtonList buttonList;
        private UIButton slot1;
        private UIButton slot2;
        private UIButton slot3;
        private UIButton slot4;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public SaveScreen()
        {
            this.buttonList = new UIButtonList();
            this.buttonList.ScrollThrough = true;

            this.slot1 = new UIButton("DefaultButton", "SelectedButton");
            this.slot1.Text = "Empty";
            this.slot1.OnAction += new ButtonActionDelegate(Slot1Action);
            this.slot1.Position = new Vector2(200.0f, 200.0f);

            this.slot2 = new UIButton("DefaultButton", "SelectedButton");
            this.slot2.Text = "Empty";
            this.slot2.OnAction += new ButtonActionDelegate(Slot2Action);
            this.slot2.Position = new Vector2(200.0f, 270.0f);

            this.slot3 = new UIButton("DefaultButton", "SelectedButton");
            this.slot3.Text = "Empty";
            this.slot3.OnAction += new ButtonActionDelegate(Slot3Action);
            this.slot3.Position = new Vector2(200.0f, 340.0f);

            this.slot4 = new UIButton("DefaultButton", "SelectedButton");
            this.slot4.Text = "Empty";
            this.slot4.OnAction += new ButtonActionDelegate(Slot4Action);
            this.slot4.Position = new Vector2(200.0f, 410.0f);

            this.buttonList.Buttons.Add(this.slot1);
            this.buttonList.Buttons.Add(this.slot2);
            this.buttonList.Buttons.Add(this.slot3);
            this.buttonList.Buttons.Add(this.slot4);

            this.Children.Add(this.buttonList);
        }
        #endregion

        #region Button Actions
        /// <summary>
        /// Attempts to load the game in slot 1.
        /// </summary>
        /// <param name="sender"></param>
        private void Slot1Action(UIButton sender)
        {
            FSEGame.Singleton.SaveGame(1);
            FSEGame.Singleton.CloseIngameMenu();
        }
        /// <summary>
        /// Attempts to load the game in slot 2.
        /// </summary>
        /// <param name="sender"></param>
        private void Slot2Action(UIButton sender)
        {
            FSEGame.Singleton.SaveGame(2);
            FSEGame.Singleton.CloseIngameMenu();
        }
        /// <summary>
        /// Attempts to load the game in slot 3.
        /// </summary>
        /// <param name="sender"></param>
        private void Slot3Action(UIButton sender)
        {
            FSEGame.Singleton.SaveGame(3);
            FSEGame.Singleton.CloseIngameMenu();
        }
        /// <summary>
        /// Attempts to load the game in slot 1.
        /// </summary>
        /// <param name="sender"></param>
        private void Slot4Action(UIButton sender)
        {
            FSEGame.Singleton.SaveGame(4);
            FSEGame.Singleton.CloseIngameMenu();
        }
        #endregion

        #region PropagateButtons
        /// <summary>
        /// Propagates the buttons.
        /// </summary>
        private void PropagateButtons()
        {
            if (this.DoesSaveFileExist(1))
                this.slot1.Text = "Game 1";
            if (this.DoesSaveFileExist(2))
                this.slot2.Text = "Game 2";
            if (this.DoesSaveFileExist(3))
                this.slot3.Text = "Game 3";
            if (this.DoesSaveFileExist(4))
                this.slot4.Text = "Game 4";
        }
        #endregion

        #region DoesSaveFileExist
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Boolean DoesSaveFileExist(Byte id)
        {
            return File.Exists(Path.Combine(
                FSEGame.Singleton.SavesFolder,
                String.Format("Slot{0}.sav", id)));
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (!this.Visible)
                return;

            if (FSEGame.Singleton.IsKeyPressed(Keys.Escape))
            {
                FSEGame.Singleton.OpenIngameMenu();
                return;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            if(this.Visible)
                base.Draw(batch);
        }

        public override void Show()
        {
            this.PropagateButtons();
            this.buttonList.Select(0);

            base.Show();
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::