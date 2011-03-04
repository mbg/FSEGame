// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   This file was generated automatically by Visual Studio.
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using FSEGame.Engine;
using Microsoft.Xna.Framework;
using FSEGame.Actors;
#endregion

namespace FSEGame
{
#if WINDOWS || XBOX
    public class Program
    {
        private MainMenuScreen mainMenu;

        public Program()
        {
            
        }

        #region Entry Point
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Program app = null;

            app = new Program();
            app.Run();
        }

        
        #endregion

        private void Run()
        {
            using (FSEGame game = new FSEGame())
            {
                game.OnInitialise += new GameEventDelegate(game_OnInitialise);
                game.Run();
            }
        }
        
        private void game_OnInitialise(Game sender)
        {
            FSEGame game = (FSEGame)sender;
            game.State = GameState.Menu;

            game.CurrentLevel.OnCreateActor += new CreateActorDelegate(CurrentLevel_OnCreateActor);

            this.mainMenu = new MainMenuScreen();

            game.UIElements.Add(this.mainMenu);
        }
        
        private Actor CurrentLevel_OnCreateActor(ActorProperties properties)
        {
            switch (properties.Type)
            {
                case "GenericNPC":
                    {
                        GenericNPC genericNPC = new GenericNPC(properties);
                        genericNPC.CellPosition = new Vector2(properties.X, properties.Y);
                        return genericNPC;
                    }
            }

            return null;
        }
    }
#endif
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::