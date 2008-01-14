using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace
{
    class GameConfig : LuaConfig
    {
        private String gameConfigFile = "Config/Game.lua";

        public GameConfig()
            : base()
        {
            this.readFile(gameConfigFile);
        }

        public void save()
        {
            this.writeFile(gameConfigFile);
        }
    }
}
