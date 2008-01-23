using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Objects
{
    class Helix : Character
    {
        // Jetpack fuel
        public int fuel;

		public void setSprite(String sprtName, String aSprtName)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = this.sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
			this.sprites[sprtName].visible = true;
			this.sprites[aSprtName].visible = true;
		}
    }
}
