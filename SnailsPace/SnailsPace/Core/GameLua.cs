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

using('Microsoft.Xna.Framework');

MSMath = import('System.Math');
MathHelper = import('Microsoft.Xna.Framework.MathHelper');
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
GameObjectBoundsBuilder = import('SnailsPace.Objects.GameObjectBoundsBuilder');
Weapon = import('SnailsPace.Objects.Weapon');
Bullet = import('SnailsPace.Objects.Bullet');
Explosion = import('SnailsPace.Objects.Explosion');
Character = import('SnailsPace.Objects.Character');
Helix = import('SnailsPace.Objects.Helix');

Map = import('SnailsPace.Objects.Map');
Player = import('SnailsPace.Core.Player');
Engine = import('SnailsPace.Core.Engine');
            
            ";

            try
            {
                DoString(initCode);
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);
            }
            #endregion

            #region Lua helper functions
            String funcCode = @"

Libraries = {}
function library( filename )
    if ( Libraries.filename == nil ) then
        dofile('Lua/' .. filename .. '.lua')
    end
end

function include( filename )
    dofile('" + mapPath + @"' .. filename)
end

            ";
            
            try
            {
                DoString(funcCode);
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);
            }
            #endregion
        }

        public Object Call(String function, params object[] args) 
        {
            try
            {
                this["retval"] = null;

                String call = "if ( " + function + " ~= nil ) then\n\t";
                call += "retval = " + function + "(";

                for (int i = 0; i < args.Length; i++)
                {
                    String varname = "arg" + i;
                    this[varname] = args[i];

                    call += (i == 0 ? "" : ",") + varname;
                }

                call += ")\nend\n";
                call += "retval = retval or True";

                DoString(call);

                return this["retval"];
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);

                return null;
            }
        }

        public Object CallOn(LuaTable self, String function, params object[] args)
        {
            try
            {
                this["this"] = self;
                this["retval"] = null;

                String call = "if ( this." + function + " ~= nil ) then\n\t";
                call += "retval = this:" + function + "(";

                for (int i = 0; i < args.Length; i++)
                {
                    String varname = "arg" + i;
                    this[varname] = args[i];

                    call += (i == 0 ? "" : ",") + varname;
                }

                call += ")\nend\n";
                call += "retval = retval or True";
            
                DoString(call);

                return this["retval"];
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);

                return null;
            }
        }
    }
}
