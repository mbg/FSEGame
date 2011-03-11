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
using FSEGame.Engine;
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BattleUI : MenuScreen
    {
        #region Instance Members
        private StaticText opponentName;
        private StaticImage bossIcon;
        private UIProgressBar opponentHealthBar;
        private StaticText playerName;
        private UIProgressBar playerHealthBar;
        private UIButtonGrid mainBattleMenu;
        private UIButtonGrid attackMenu;
        private UIButtonGrid magicMenu;
        #endregion

        #region Properties
        public String OpponentName
        {
            get
            {
                return this.opponentName.Text;
            }
            set
            {
                this.opponentName.Text = value;
            }
        }

        public Boolean IsBossBattle
        {
            get
            {
                return this.bossIcon.Visible;
            }
            set
            {
                this.bossIcon.Visible = value;
            }
        }

        public UInt32 OpponentHealth
        {
            get
            {
                return this.opponentHealthBar.Maximum;
            }
            set
            {
                this.opponentHealthBar.Maximum = value;
            }
        }

        public UInt32 CurrentOpponentHealth
        {
            get
            {
                return this.opponentHealthBar.Value;
            }
            set
            {
                this.opponentHealthBar.Value = value;
            }
        }

        public UInt32 PlayerHealth
        {
            get
            {
                return this.playerHealthBar.Maximum;
            }
            set
            {
                this.playerHealthBar.Maximum = value;
            }
        }

        public UInt32 CurrentPlayerHealth
        {
            get
            {
                return this.playerHealthBar.Value;
            }
            set
            {
                this.playerHealthBar.Value = value;
            }
        }

        public UIButtonGrid MainMenu
        {
            get
            {
                return this.mainBattleMenu;
            }
        }

        public UIButtonGrid AttackMenu
        {
            get
            {
                return this.attackMenu;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BattleUI()
            : base("BattleUI")
        {
            this.opponentName = new StaticText(FSEGame.Singleton.DefaultFont);
            this.opponentName.Text = "<OPPONENT NAME>";
            this.opponentName.Position = new Vector2(50, 20);
            this.opponentName.TextColour = Color.Black;
            this.opponentName.HasShadow = false;

            this.playerName = new StaticText(FSEGame.Singleton.DefaultFont);
            this.playerName.Text = "Landor";
            this.playerName.Position = new Vector2(445, 385);
            this.playerName.TextColour = Color.Black;
            this.playerName.HasShadow = false;

            this.bossIcon = new StaticImage("Boss");
            this.bossIcon.Position = new Vector2(350, 20);
            this.bossIcon.Visible = false;

            this.opponentHealthBar = new UIProgressBar("ProgressBar", "ProgressBarFilled");
            this.opponentHealthBar.Position = new Vector2(35, 50);
            this.opponentHealthBar.Maximum = 100;
            this.opponentHealthBar.Value = 100;

            this.playerHealthBar = new UIProgressBar("ProgressBar", "ProgressBarFilled");
            this.playerHealthBar.Position = new Vector2(430, 415);
            this.playerHealthBar.Maximum = 100;
            this.playerHealthBar.Value = 100;

            this.mainBattleMenu = new UIButtonGrid(2, 2);
            this.mainBattleMenu.Visible = false;
            this.mainBattleMenu.Buttons[0, 0] = new UIGridButton("Attack", new Vector2(70, 500));
            this.mainBattleMenu.Buttons[0, 1] = new UIGridButton("Magic", new Vector2(500, 500));
            this.mainBattleMenu.Buttons[1, 0] = new UIGridButton("Items", new Vector2(70, 540));
            this.mainBattleMenu.Buttons[1, 1] = new UIGridButton("Skip?", new Vector2(500, 540));

            this.attackMenu = new UIButtonGrid(2, 2);
            this.attackMenu.Visible = false;
            this.attackMenu.Buttons[0, 0] = new UIGridButton("<MOVE 1>", new Vector2(70, 500));
            this.attackMenu.Buttons[0, 1] = new UIGridButton("<MOVE 2>", new Vector2(500, 500));
            this.attackMenu.Buttons[1, 0] = new UIGridButton("Back", new Vector2(70, 540));
            this.attackMenu.Buttons[1, 1] = new UIGridButton("<MOVE 3>", new Vector2(500, 540));

            base.Children.Add(this.opponentName);
            base.Children.Add(this.playerName);
            base.Children.Add(this.bossIcon);
            base.Children.Add(this.opponentHealthBar);
            base.Children.Add(this.playerHealthBar);
            base.Children.Add(this.mainBattleMenu);
            base.Children.Add(this.attackMenu);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::