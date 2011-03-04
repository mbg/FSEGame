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
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Instance Members
        private UIButtonList buttonList;
        private UIButton newGameButton;
        private UIButton loadGameButton;
        private UIButton exitButton;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public MainMenuScreen()
            : base("MainMenu")
        {
            this.buttonList = new UIButtonList();
            this.buttonList.ScrollThrough = true;

            this.newGameButton = new UIButton("DefaultButton", "SelectedButton");
            this.newGameButton.Text = "New Game";
            this.newGameButton.OnAction += new ButtonActionDelegate(newGameButton_OnAction);
            this.newGameButton.Position = new Vector2(200.0f, 250.0f);

            this.loadGameButton = new UIButton("DefaultButton", "SelectedButton");
            this.loadGameButton.Text = "Load Game";
            this.loadGameButton.OnAction += new ButtonActionDelegate(loadGameButton_OnAction);
            this.loadGameButton.Position = new Vector2(200.0f, 320.0f);

            this.exitButton = new UIButton("DefaultButton", "SelectedButton");
            this.exitButton.Text = "Exit";
            this.exitButton.OnAction += new ButtonActionDelegate(exitButton_OnAction);
            this.exitButton.Position = new Vector2(200.0f, 390.0f);

            this.buttonList.Buttons.Add(this.newGameButton);
            this.buttonList.Buttons.Add(this.loadGameButton);
            this.buttonList.Buttons.Add(this.exitButton);

            this.Children.Add(this.buttonList);
        }
        #endregion
        
        private void newGameButton_OnAction(UIButton sender)
        {
            FSEGame.Singleton.LoadLevel(@"Levels\Test.xml", "Default");
            FSEGame.Singleton.State = GameState.Exploring;

            this.Hide();
        }

        private void loadGameButton_OnAction(UIButton sender)
        {
            
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

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::