using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Input
{
    public class ActionMapping
    {
        /// <summary>
        /// During what situation should the action occur?
        /// </summary>
        public enum Perform
        {
            OnKeyUp,
            OnKeyDown,
            WhileKeyDown
        };

        public delegate void KeyAction(GameTime gameTime);

        protected KeyAction actionCall;
        protected Perform actionTiming;
        protected bool isKeyDown;

        /// <summary>
        /// Defines a mapping of an action to key up
        /// </summary>
        /// <param name="action"></param>
        /// <param name="performOnKeyUp"></param>
        public ActionMapping(KeyAction action, Perform when)
        {
            actionCall = action;
            actionTiming = when;
            isKeyDown = false;
        }

        /// <summary>
        /// The key(s) this action mapping is assigned to is down.
        /// </summary>
        public void keyDown(GameTime gameTime)
        {
            if (!isKeyDown && actionTiming == Perform.OnKeyDown)
            {
                actionCall(gameTime);
            }
            else if (actionTiming == Perform.WhileKeyDown)
            {
                actionCall(gameTime);
            }
            isKeyDown = true;
        }

        /// <summary>
        /// The key(s) this action mapping is assigned to is up.
        /// </summary>
        public void keyUp(GameTime gameTime)
        {
            if (isKeyDown && actionTiming == Perform.OnKeyUp)
            {
                actionCall(gameTime);
            }
            isKeyDown = false;
        }

        /// <summary>
        /// A description of the action mapping
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return actionCall.Method.ToString() + " " + actionTiming.ToString();
        }
    }
}
