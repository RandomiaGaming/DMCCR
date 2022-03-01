using System;
using System.Collections.Generic;
using System.Reflection;
namespace EpsilonEngine
{
    public static class Modloader
    {
        #region Public Variables
        public static bool ProfilerEnabled { get; private set; } = false;
        #endregion
        #region Private Variables
        private static List<Assembly> _loadedMods = new List<Assembly>();
        #endregion
        #region Constructors
        static Modloader()
        {
            Load(typeof(Modloader).Assembly);
        }
        #endregion
        #region Public Methods
        public static void Load(Assembly mod)
        {
            if(mod is null)
            {
                throw new Exception("mod cannot be null.");
            }

            int loadedModCount = _loadedMods.Count;
            for (int i = 0; i < loadedModCount; i++)
            {
                if(_loadedMods[i] == mod)
                {
                    throw new Exception("mod has already been loaded.");
                }
            }

            _loadedMods.Add(mod);

            if (mod.GetCustomAttribute<EnableProfilerAttribute>() is not null)
            {
                ProfilerEnabled = true;
            }
        }
        #endregion
    }
}