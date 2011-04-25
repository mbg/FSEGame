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
using System.Collections.Generic;
using System.Reflection;
#endregion

namespace FSEGame
{
#if WINDOWS || XBOX
    public class Program
    {
        #region Instance Members
        private CommandLineParser commandLineParser;
        private Dictionary<String, Type> actorClasses;
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
#if !DEBUG
            try
            {
#endif
                this.commandLineParser = new CommandLineParser(arguments);

                FSEGame.Singleton.OnInitialise += new GameEventDelegate(game_OnInitialise);
                FSEGame.Singleton.Run();
#if !DEBUG
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
#endif
        }
        #endregion

        private void game_OnInitialise(Game sender)
        {
            FSEGame game = (FSEGame)sender;

            this.actorClasses = new Dictionary<String, Type>();
            this.actorClasses.Add("GenericNPC", typeof(GenericNPC));
            this.actorClasses.Add("BridgeNPC", typeof(BridgeNPC));
            this.actorClasses.Add("Vernado", typeof(Vernado));
            this.actorClasses.Add("Markus", typeof(Markus));
            this.actorClasses.Add("Maro", typeof(Maro));
            this.actorClasses.Add("ShopNPC", typeof(ShopNPC));
            this.actorClasses.Add("ChestActor", typeof(ChestActor));
            this.actorClasses.Add("ScriptedActor", typeof(ScriptedActor));

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

        #region OnCreateActor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private Actor CurrentLevel_OnCreateActor(ActorProperties properties)
        {
            GameBase.Singleton.Log.WriteLine("Game", "Attempting to create actor of type {0}", properties.Type);

            if (this.actorClasses.ContainsKey(properties.Type))
            {
                GameBase.Singleton.Log.WriteLine("Game", "Actor type found.");

                try
                {
                    Actor result = (Actor)Activator.CreateInstance(
                        this.actorClasses[properties.Type], properties);
                    result.CellPosition = new Vector2(properties.X, properties.Y);

                    return result;
                }
                catch (TargetInvocationException ex)
                {
                    GameBase.Singleton.Log.WriteLine(
                        "Game",
                        "Failed to create actor instance of type {0}: {1}",
                        properties.Type,
                        ex.InnerException.Message);
                }
                catch (Exception ex)
                {
                    GameBase.Singleton.Log.WriteLine(
                        "Game",
                        "Failed to create actor instance of type {0}: {1}",
                        properties.Type,
                        ex.Message);
                }
            }

            GameBase.Singleton.Log.WriteLine("Game", "Couldn't find actor type {0}", properties.Type);

            return null;
        }
        #endregion
    }
#endif
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::