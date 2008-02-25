using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace
{
    class GameConfig : LuaConfig
    {
        private String gameConfigFile = "Config/Game.lua";

        // Load a LuaConfig specific to game configs
        public GameConfig()
            : base()
        {
            #region Default values
            // Default string values
            Dictionary<String, String> strings = new Dictionary<String, String>();
            strings.Add("levels", "Garden:Garden;Garden #2:Garden2");
            strings.Add("levelSplit", ";");
            strings.Add("levelSubsplit", ":");
            setDefaults(strings);

            #endregion

            // Load user preferences
            this.readFile(gameConfigFile);

        }

        // Write changes to a file
        public void save()
        {
            this.writeFile(gameConfigFile);
        }
    }
}
