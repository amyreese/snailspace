using System;
using System.Collections.Generic;
using System.Text;

namespace A_Snail_s_Pace
{
    class GameEngine
    {
        // Game map
        public Objects.Map map;

        // Player
        public Objects.Helix helix;

        // Bullets
        public List<Objects.Bullet> bullets;

        // Constructors
        public GameEngine(String map)
        {
            // TODO: Load the map object from Lua

            // TODO: Initialize Helix;
        }

        public void think()
        {
            // TODO: iterate through map.characters calling think() on each one.

            // TODO: handle player inputs to change Helix's attributes.
        }

        public void physics()
        {
            // TODO: iterate through map.characters and this.bullets using collision detection to move everything.

            // TODO: iterate through map.triggers and map.characters to find which triggers to execute
        }

        public void render()
        {
            // TODO: iterate through map.objects, map.characters, and this.bullets to gather all visible sprites
            // and then send the list of sprites to the rendering system.
        }
    }
}
