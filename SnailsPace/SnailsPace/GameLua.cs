using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace SnailsPace
{
    class GameLua : Lua
    {
        public GameLua()
            : base()
        {
        }

        public void init()
        {
            #region Lua initialization of C# classes
            String initCode = @" 

using = luanet.load_assembly;
import = luanet.import_type;

using('SnailsPace');

Trigger = import('SnailsPace.Objects.Trigger');

Image = import('SnailsPace.Objects.Image');
Sprite = import('SnailsPace.Objects.Sprite');
Text = import('SnailsPace.Objects.Text');

GameObject = import('SnailsPace.Objects.GameObject');
Bullet = import('SnailsPace.Objects.Bullet');
Character = import('SnailsPace.Objects.Character');
Helix = import('SnailsPace.Objects.Helix');

Map = import('SnailsPace.Objects.Map');
            
            ";
            #endregion

            this.DoString(initCode);
        }
    }
}
