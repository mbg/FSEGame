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
using Microsoft.Xna.Framework.Graphics;
using FSEGame.Engine.UI;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public class GameOverScreen : MenuScreen
    {
        #region Instance Members
        private UIButtonList buttonList;
        private UIButton mainMenuButton;
        private UIButton exitButton;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GameOverScreen()
            : base("GameOver")
        {
            this.buttonList = new UIButtonList();
            this.buttonList.ScrollThrough = true;

            this.mainMenuButton = new UIButton("DefaultButton", "SelectedButton");
            this.mainMenuButton.Text = "Main Menu";
            this.mainMenuButton.OnAction += new ButtonActionDelegate(mainMenuButton_OnAction);
            this.mainMenuButton.Position = new Vector2(200.0f, 320.0f);

            this.exitButton = new UIButton("DefaultButton", "SelectedButton");
            this.exitButton.Text = "Exit";
            this.exitButton.OnAction += new ButtonActionDelegate(exitButton_OnAction);
            this.exitButton.Position = new Vector2(200.0f, 390.0f);

            this.buttonList.Buttons.Add(this.mainMenuButton);
            this.buttonList.Buttons.Add(this.exitButton);

            this.Children.Add(this.buttonList);
        }
        #endregion

        private void mainMenuButton_OnAction(UIButton sender)
        {
            FSEGame.Singleton.OpenMainMenu();
        }

        private void exitButton_OnAction(UIButton sender)
        {
            GameBase.Singleton.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.Visible)
                return;

            base.Update(gameTime);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::