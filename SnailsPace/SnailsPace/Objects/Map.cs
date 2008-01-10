using System;
using System.Collections.Generic;
using System.Text;

namespace A_Snail_s_Pace.Objects
{
    class Map
    {
        // Map background, foreground, and scenery objects.
        public List<GameObject> objects;

        // Characters and triggers.
        public List<Character> characters;
        public List<Trigger> triggers;
    }
}
