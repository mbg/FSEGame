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
    static class Program
    {
        #region Entry Point
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FSEGame game = new FSEGame())
            {
                game.OnInitialise += new GameEventDelegate(game_OnInitialise);
                game.Run();
            }
        }

        
        #endregion
        
        private static void game_OnInitialise(Game sender)
        {
            FSEGame game = (FSEGame)sender;
            game.CurrentLevel.OnCreateActor += new CreateActorDelegate(CurrentLevel_OnCreateActor);
        }
        
        private static Actor CurrentLevel_OnCreateActor(String type, Vector2 position)
        {
            switch (type)
            {
                case "GenericNPC":
                    {
                        GenericNPC genericNPC = new GenericNPC();
                        genericNPC.CellPosition = position;
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