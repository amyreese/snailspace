using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SnailsPace.Core
{
    class Input
    {
        private LuaConfig inputConfig;
        private String inputConfigFile = "Config/Input.lua";

        private Dictionary<String, String> inputKeys;
        private Dictionary<String, KeyState> keyStates;
        private Dictionary<String, KeyState> keyStatesOld;
        private Dictionary<String, Boolean> keyPresses;

        public Vector2 mousePosition;

		/// <summary>
		/// Initialize the InputManager object and configuration.
		/// </summary>
        public Input()
        {
            inputKeys = new Dictionary<String, String>();
            keyStates = new Dictionary<String,KeyState>();
            keyPresses = new Dictionary<String, Boolean>();

            // Default action assignments
            inputKeys.Add("MenuUp", "up");
            inputKeys.Add("MenuDown", "down");
            inputKeys.Add("MenuLeft", "left");
            inputKeys.Add("MenuRight", "right");
            inputKeys.Add("MenuSelect", "enter");
            inputKeys.Add("MenuToggle", "escape");

            inputKeys.Add("Up", "w");
            inputKeys.Add("Down", "s");
            inputKeys.Add("Left", "a");
            inputKeys.Add("Right", "d");
            inputKeys.Add("WeaponNext", "q");
            inputKeys.Add("Fire", "Mouse1");
			inputKeys.Add("Camera", "Mouse2");
            inputKeys.Add("Pause", "p");
#if DEBUG
            inputKeys.Add("DebugFramerate", "control+f");
            inputKeys.Add("DebugCollisions", "control+c");
            inputKeys.Add("DebugCulling", "control+u");
            inputKeys.Add("DebugBoundingBoxes", "control+b");
            inputKeys.Add("DebugFlying", "control+y");
            inputKeys.Add("DebugCameraPosition", "control+alt+c");
            inputKeys.Add("DebugHelixPosition", "control+alt+h");
            inputKeys.Add("DebugZoomIn", "control+pageup");
            inputKeys.Add("DebugZoomOut", "control+pagedown");
            inputKeys.Add("DebugTriggers", "control+t");
#endif
            
            // Read from the user's config file
            inputConfig = new LuaConfig(new Dictionary<string,double>(), inputKeys);
            inputConfig.readFile(inputConfigFile);
            inputKeys = inputConfig.getStrings();

            this.reset();
        }

        /// <summary>
		/// Check the state of an input action.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <returns>Whether or not the key that performs this action is down.</returns>
        public Boolean inputDown(String action)
        {
            if (inputKeys.ContainsKey(action))
            {
                return keyStates[inputKeys[action]] == KeyState.Down;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the action's input has been pressed.  
        /// Distinct from inputState() in that it only returns True once for each time the key goes down.
        /// </summary>
		/// <param name="action">The input action.</param>
		/// <returns>Whether or not the key that performs this action is pressed.</returns>
        public Boolean inputPressed(String action)
        {
            if (inputKeys.ContainsKey(action))
            {
                Boolean pressed = keyPresses[inputKeys[action]];
                keyPresses[inputKeys[action]] = false;
                return pressed;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
		/// Update the input states.
        /// </summary>
        public void update()
        {
            // Shift keyStates to keyStatesOld
			Dictionary<String, String>.ValueCollection.Enumerator keyValueEnumerator = inputKeys.Values.GetEnumerator();
			while( keyValueEnumerator.MoveNext() )
            {
                keyStatesOld[keyValueEnumerator.Current] = keyStates[keyValueEnumerator.Current];
                keyStates[keyValueEnumerator.Current] = KeyState.Up;
            }
			keyValueEnumerator.Dispose();

            // Check keyboard inputs
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            bool control = false;
            bool shift = false;
            bool alt = false;

            // Handle modifier keys
            for (int pressedKeyIndex = 0; pressedKeyIndex < pressedKeys.Length; pressedKeyIndex++)
            {
                String keyName = pressedKeys[pressedKeyIndex].ToString();
                
                if (keyName == "LeftAlt" || keyName == "RightAlt")
                {
                    alt = true;
                }

                if (keyName == "LeftShift" || keyName == "RightShift")
                {
                    shift = true;
                }

                if (keyName == "LeftControl" || keyName == "RightControl")
                {
                    control = true;
                }
            }

            // Handle stupid MS key names
			for( int pressedKeyIndex = 0; pressedKeyIndex < pressedKeys.Length; pressedKeyIndex++ ) {
				String keyName = pressedKeys[pressedKeyIndex].ToString().ToLower();
                if (keyName == "back")
                {
                    keyName = "backspace";
                }
                if (keyName == "oemcomma")
                {
                    keyName = "comma";
                }

                String modName = (control ? "control+" : "") + (alt ? "alt+" : "") + (shift ? "shift+" : "") + keyName;
                if (keyStates.ContainsKey(keyName))
				{
					keyStates[keyName] = KeyState.Down;
				}
                if (keyStates.ContainsKey(modName))
                {
                    keyStates[modName] = KeyState.Down;
                }
			}

            // Check mouse inputs
            MouseState mouseState = Mouse.GetState();

			mousePosition = new Vector2(mouseState.X,mouseState.Y);
			
            if (keyStates.ContainsKey("Mouse1"))
            {
                keyStates["Mouse1"] = (mouseState.LeftButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up);
            }

            if (keyStates.ContainsKey("Mouse2"))
            {
                keyStates["Mouse2"] = (mouseState.RightButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up);
            }

            if (keyStates.ContainsKey("Mouse3"))
            {
                keyStates["Mouse3"] = (mouseState.MiddleButton == ButtonState.Pressed ? KeyState.Down : KeyState.Up);
            }

            // Find new key presses
			keyValueEnumerator = inputKeys.Values.GetEnumerator();
			while (keyValueEnumerator.MoveNext())
			{
				keyPresses[keyValueEnumerator.Current] =
					(keyPresses[keyValueEnumerator.Current] || (keyStates[keyValueEnumerator.Current] == KeyState.Down) && (keyStatesOld[keyValueEnumerator.Current] == KeyState.Up))
					&& (keyStates[keyValueEnumerator.Current] == KeyState.Down);
            }
			keyValueEnumerator.Dispose();
        }

        /// <summary>
		/// Reset keyStates and keyPresses.
        /// </summary>
        public void reset()
        {
            // Reset input dictionaries
            keyStates = new Dictionary<String, KeyState>();
            keyStatesOld = new Dictionary<String, KeyState>();
            keyPresses = new Dictionary<String, Boolean>();

            // Loop through all defined input actions
			Dictionary<String, String>.ValueCollection.Enumerator keyValueEnumerator = inputKeys.Values.GetEnumerator();
			while (keyValueEnumerator.MoveNext())
			{
                // Set all inputs to up
				keyStates.Add(keyValueEnumerator.Current, KeyState.Up);
				keyStatesOld.Add(keyValueEnumerator.Current, KeyState.Up);

                // Set all inputs as not pressed
				keyPresses.Add(keyValueEnumerator.Current, false);
            }
			keyValueEnumerator.Dispose();
        }
		
		/// <summary>
		/// Get the key binding that corresponds to the given action.
		/// </summary>
		/// <param name="action">An action.</param>
		/// <returns>The key binding.</returns>
		public String getKeyBinding(String action)
		{
			return inputKeys[action];
		}

        /// <summary>
		/// Write the new user config.
        /// </summary>
        public void saveConfig()
        {
            inputConfig.writeFile(inputConfigFile);
        }
    }
}
