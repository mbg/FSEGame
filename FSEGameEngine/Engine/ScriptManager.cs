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
using LuaInterface;
using System.IO;
using System.Reflection;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ScriptManager
    {
        #region Instance Members
        /// <summary>
        /// 
        /// </summary>
        private Lua state;

        private String scriptPath;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Lua state instance used by the script manager.
        /// </summary>
        public Lua State
        {
            get
            {
                return this.state;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ScriptManager(String scriptPath)
        {
            this.state = new Lua();
            this.scriptPath = scriptPath;

            this.LoadCoreScripts();
        }
        #endregion

        #region LoadCoreScripts
        /// <summary>
        /// 
        /// </summary>
        private void LoadCoreScripts()
        {
            String filename = Path.Combine(this.scriptPath, @"Core.ini");

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    String scriptFilename = reader.ReadLine();

                    this.state.LoadFile(scriptFilename);
                }
            }
        }
        #endregion

        #region RegisterTypeInstance
        /// <summary>
        /// Registers methods marked with the ScriptFunctionAttribute using
        /// the specified instance of a type.
        /// </summary>
        /// <param name="instance"></param>
        public void RegisterTypeInstance(Object instance)
        {
            // :: Iterate through all methods in the type of the specified
            // :: object, then iterate through all attributes to find the
            // :: ScriptFunction attribute and register methods where
            // :: appropriate.
            foreach (MethodBase method in instance.GetType().GetMethods())
            {
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    if (attribute.GetType() == typeof(ScriptFunctionAttribute))
                    {
                        ScriptFunctionAttribute scriptFunctionAttribute =
                            (ScriptFunctionAttribute)attribute;

                        this.state.RegisterFunction(
                            scriptFunctionAttribute.Name,
                            instance,
                            method);

                    }
                }
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::