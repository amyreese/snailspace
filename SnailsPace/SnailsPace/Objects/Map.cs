using System;
using System.Collections.Generic;
using System.Text;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Map
    {
        // Map properties
        public String name;

        // Map background, foreground, and scenery objects.
        public List<GameObject> objects;

        // Characters and triggers.
        public List<Character> characters;
        public List<Trigger> triggers;

        // Map interpreter
        private GameLua lua;

        /**
         * Initialize the map, its interpreter, etc
         */
        public Map(String mapName)
        {
            name = mapName;
            lua = new GameLua();

            objects = new List<GameObject>();
            characters = new List<Character>();
            triggers = new List<Trigger>();

            String filename = "Maps/" + name + "/" + name + ".lua";
            lua["map"] = this;
            lua.DoFile(filename);
        }
    }
}
