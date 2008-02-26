using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace SnailsPace.Core
{
    class GameLua : Lua
    {
        /// <summary>
        /// Initialize a Lua interpreter with no map.
        /// </summary>
        public GameLua() : this("")
        {
        }

        /// <summary>
        /// Initialzie a Lua interpreter with a given map name.
        /// </summary>
        /// <param name="mapName">The map's folder name</param>
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
Random = import('System.Random');
MathHelper = import('Microsoft.Xna.Framework.MathHelper');
Matrix = import('Microsoft.Xna.Framework.Matrix');
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
Engine = Engine.GetInstance()
Renderer = import('SnailsPace.Core.Renderer');
            
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

        /// <summary>
        /// Call an arbitrary, global Lua function with the given name and parameters
        /// </summary>
        /// <param name="function">The global Lua function's name</param>
        /// <param name="args">Vararg parameters to the Lua function</param>
        public void Call(String function, params object[] args) 
        {
            try
            {
                String call = "if ( " + function + " ~= nil ) then\n\t";
                call += function + "(";

                for (int i = 0; i < args.Length; i++)
                {
                    String varname = "arg" + i;
                    this[varname] = args[i];

                    call += (i == 0 ? "" : ",") + varname;
                }

                call += ")\nend\n";
                
                DoString(call);
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);
            }
        }

        /// <summary>
        /// Call an arbitrary Lua table's method with the given name and parameters
        /// </summary>
        /// <param name="self">The LuaTable object with the target method</param>
        /// <param name="function">The target method's name</param>
        /// <param name="args">Vararg parameters to the Lua method</param>
        public void CallOn(LuaTable self, String function, params object[] args)
        {
            try
            {
                this["this"] = self;
                
                String call = "if ( this." + function + " ~= nil ) then\n\t";
                call += "this:" + function + "(";

                for (int i = 0; i < args.Length; i++)
                {
                    String varname = "arg" + i;
                    this[varname] = args[i];

                    call += (i == 0 ? "" : ",") + varname;
                }

                call += ")\nend\n";
            
                DoString(call);
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);
            }
        }
    }
}
