using System;
using System.Collections.Generic;
using System.Text;

namespace A_Snail_s_Pace
{
    class ActionMapping
    {
        /// <summary>
        /// During what situation should the action occur?
        /// </summary>
        public enum Perform {
            OnKeyUp,
            OnKeyDown,
            WhileKeyDown
        };

        public delegate void KeyAction();

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
        public void keyDown()
        {
            if (!isKeyDown && actionTiming == Perform.OnKeyDown)
            {
                actionCall();
            } else if (actionTiming == Perform.WhileKeyDown)
            {
                actionCall();
            }
            isKeyDown = true;
        }

        /// <summary>
        /// The key(s) this action mapping is assigned to is up.
        /// </summary>
        public void keyUp()
        {
            if( isKeyDown && actionTiming == Perform.OnKeyUp )
            {
                actionCall();
            }
            isKeyDown = false;
        }
    }
}
