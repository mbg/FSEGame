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
            
            FSEGame.Singleton.OnInitialise += new GameEventDelegate(game_OnInitialise);
            FSEGame.Singleton.Run();
            
        }
        
        private void game_OnInitialise(Game sender)
        {
            FSEGame game = (FSEGame)sender;

            game.CurrentLevel.OnCreateActor += new CreateActorDelegate(CurrentLevel_OnCreateActor);
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