using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Objects
{
    class GameObject
    {
        // The game object's position and velocity.
        public Vector2 position;
        public Vector2 rotation;
        public Vector2 velocity;
       
        // The sprite's layer and parallax.
        public int layer;       // 0: background ... 5: foreground
        
        // Sprites that make up the game object.
        public Dictionary<String, Sprite> sprites;
        
    }
}
