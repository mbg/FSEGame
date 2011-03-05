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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public class IngameMenu : UIContainer
    {
        #region Instance Members
        private UIButtonList buttonList;
        private UIButton continueGameButton;
        private UIButton saveGameButton;
        private UIButton exitButton;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public IngameMenu()
        {
            this.buttonList = new UIButtonList();
            this.buttonList.ScrollThrough = true;

            this.continueGameButton = new UIButton("DefaultButton", "SelectedButton");
            this.continueGameButton.Text = "Continue";
            this.continueGameButton.OnAction += new ButtonActionDelegate(ContinueGame);
            this.continueGameButton.Position = new Vector2(200.0f, 200.0f);

            this.saveGameButton = new UIButton("DefaultButton", "SelectedButton");
            this.saveGameButton.Text = "Save Game";
            this.saveGameButton.OnAction += new ButtonActionDelegate(SaveGame);
            this.saveGameButton.Position = new Vector2(200.0f, 270.0f);

            this.exitButton = new UIButton("DefaultButton", "SelectedButton");
            this.exitButton.Text = "Exit";
            this.exitButton.OnAction += new ButtonActionDelegate(Exit);
            this.exitButton.Position = new Vector2(200.0f, 340.0f);

            this.buttonList.Buttons.Add(this.continueGameButton);
            this.buttonList.Buttons.Add(this.saveGameButton);
            this.buttonList.Buttons.Add(this.exitButton);

            this.Children.Add(this.buttonList);
        }
        #endregion

        void Exit(UIButton sender)
        {
            FSEGame.Singleton.CloseIngameMenu();
            FSEGame.Singleton.OpenMainMenu();
        }

        void SaveGame(UIButton sender)
        {
            FSEGame.Singleton.OpenSaveScreen();
        }

        void ContinueGame(UIButton sender)
        {
            FSEGame.Singleton.CloseIngameMenu();
        }

        public override void Update(GameTime time)
        {
            if (!this.Visible)
                return;

            base.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            if(this.Visible)
                base.Draw(batch);
        }

        public override void Show()
        {
            base.Show();
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::