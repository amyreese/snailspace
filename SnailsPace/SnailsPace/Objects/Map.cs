using System;
using System.Collections.Generic;
using System.Text;
using SnailsPace.Core;
using Microsoft.Xna.Framework;

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

		// Map bounds
		public Rectangle bounds;

        /**
         * Initialize the map, its interpreter, etc
         */
        public Map(String mapName)
        {
            name = mapName;
            
            objects = new List<GameObject>();
            characters = new List<Character>();
            triggers = new List<Trigger>();

            String filename = "Maps/" + name + "/" + name + ".lua";
            Engine.lua["map"] = this;
            Engine.lua.DoFile(filename);
        }
    }
}
