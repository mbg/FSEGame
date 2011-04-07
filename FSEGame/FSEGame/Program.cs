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
using System.Windows.Forms;
#endregion

namespace FSEGame
{
#if WINDOWS || XBOX
    public class Program
    {
        #region Instance Members
        private CommandLineParser commandLineParser;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Program()
        {
            
        }
        #endregion

        #region Entry Point
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Program app = null;

            app = new Program();
            app.Run(args);
        }

        
        #endregion

        #region Run
        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="arguments"></param>
        private void Run(String[] arguments)
        {
            try
            {
                this.commandLineParser = new CommandLineParser(arguments);

                FSEGame.Singleton.OnInitialise += new GameEventDelegate(game_OnInitialise);
                FSEGame.Singleton.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unhandled exception was caught:\n" +
                    ex.Message,
                    "FSEGame",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        private void game_OnInitialise(Game sender)
        {
            FSEGame game = (FSEGame)sender;

            game.ContentLoaded += new GameEventDelegate(game_ContentLoaded);
            game.CurrentLevel.OnCreateActor += new CreateActorDelegate(CurrentLevel_OnCreateActor);

        }

        void game_ContentLoaded(Game sender)
        {
            FSEGame game = (FSEGame)sender;

            if ((!String.IsNullOrWhiteSpace(this.commandLineParser.LevelToLoad)) &&
                (!String.IsNullOrWhiteSpace(this.commandLineParser.EntryPoint)))
            {
                game.NewGameWithLevel(
                    this.commandLineParser.LevelToLoad,
                    this.commandLineParser.EntryPoint);
            }
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
                case "BridgeNPC":
                    {
                        BridgeNPC bridgeNPC = new BridgeNPC(properties);
                        bridgeNPC.CellPosition = new Vector2(properties.X, properties.Y);
                        return bridgeNPC;
                    }
                case "Vernado":
                    {
                        Vernado vernado = new Vernado(properties);
                        vernado.CellPosition = new Vector2(properties.X, properties.Y);
                        return vernado;
                    }
                case "Markus":
                    {
                        Markus markus = new Markus(properties);
                        markus.CellPosition = new Vector2(properties.X, properties.Y);
                        return markus;
                    }
                case "Maro":
                    {
                        Maro markus = new Maro(properties);
                        markus.CellPosition = new Vector2(properties.X, properties.Y);
                        return markus;
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