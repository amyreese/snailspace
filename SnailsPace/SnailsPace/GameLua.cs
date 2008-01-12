using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace A_Snail_s_Pace
{
    class GameLua : Lua
    {
        public GameLua()
            : base()
        {
            #region Lua initialization of C# classes
            String initCode = @" 

load_assembly('SnailsPace'); 
Bullet = import_type('SnailsPace.Objects.Bullet');
            
            ";
            #endregion

            this.DoString(initCode);

        }
    }
}
