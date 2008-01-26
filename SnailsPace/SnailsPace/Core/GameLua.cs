using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace SnailsPace.Core
{
    class GameLua : Lua
    {
        public GameLua() : this("")
        {
        }

        public GameLua(String mapName)
            : base()
        {
            #region Lua initialization of C# classes
            String mapPath = (mapName.Length > 0) ? ("Maps/" + mapName + "/") : ("");
            String initCode = @" 

using = luanet.load_assembly;
import = luanet.import_type;
function include( filename )
    dofile('" + mapPath + @"' .. filename)
end

using('Microsoft.Xna.Framework');

Rectangle = import('Microsoft.Xna.Framework.Rectangle');
Vector2 = import('Microsoft.Xna.Framework.Vector2');
Vector3 = import('Microsoft.Xna.Framework.Vector3');

using('SnailsPace');

SnailsPace = import('SnailsPace.SnailsPace')
SnailsPace = SnailsPace.getInstance()

Trigger = import('SnailsPace.Objects.Trigger');

Image = import('SnailsPace.Objects.Image');
Sprite = import('SnailsPace.Objects.Sprite');
Text = import('SnailsPace.Objects.Text');

GameObject = import('SnailsPace.Objects.GameObject');
GameObjectBounds = import('SnailsPace.Objects.GameObjectBounds');
Bullet = import('SnailsPace.Objects.Bullet');
Character = import('SnailsPace.Objects.Character');
Helix = import('SnailsPace.Objects.Helix');

Map = import('SnailsPace.Objects.Map');
            
            ";
            this.DoString(initCode);
            #endregion

            SnailsPace snailsPace = SnailsPace.getInstance();
            this.RegisterFunction("sdebug", snailsPace, snailsPace.GetType().GetMethod("debug"));
        }

        public void Call(String function, params object[] args) 
        {
            String call = "if ( " + function + " ~= nil ) then\n\t" + function + "(";

            for (int i = 0; i < args.Length; i++)
            {
                String varname = "arg" + i;
                this[varname] = args[i];

                call += (i == 0 ? "" : ",") + varname;
            }

            call += ")\nend";
            DoString(call);
        }
    }
}
