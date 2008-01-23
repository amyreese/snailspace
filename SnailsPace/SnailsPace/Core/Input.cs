using System;
using System.Collections.Generic;
using System.Text;
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

		private int currentMouseX;
		private int currentMouseY;
		public int MouseX { get { return currentMouseX; } }
		public int MouseY { get { return currentMouseY; } }

        /**
         * Initialize the InputManager object and configuration.
         */
        public Input()
        {
            inputKeys = new Dictionary<String, String>();
            keyStates = new Dictionary<String,KeyState>();
            keyPresses = new Dictionary<String, Boolean>();

            // Default action assignments
            inputKeys.Add("MenuUp", "Up");
            inputKeys.Add("MenuDown", "Down");
            inputKeys.Add("MenuLeft", "Left");
            inputKeys.Add("MenuRight", "Right");
            inputKeys.Add("MenuSelect", "Enter");
            inputKeys.Add("MenuToggle", "Escape");
            inputKeys.Add("Up", "W");
            inputKeys.Add("Down", "S");
            inputKeys.Add("Left", "A");
            inputKeys.Add("Right", "D");
            inputKeys.Add("Fire", "Mouse1");
            inputKeys.Add("Pause", "P");
            
            // Read from the user's config file
            inputConfig = new LuaConfig(new Dictionary<string,double>(), inputKeys);
            inputConfig.readFile(inputConfigFile);
            inputKeys = inputConfig.getStrings();

            this.reset();
        }

        /**
         * Check the state of an input action.
         */
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

        /**
         * Check if the action's input has been pressed.  
         * Distinct from inputState() in that it only returns True once for each time the key goes down.
         */
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
        
        /**
         * Update the input states.
         */
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

			for( int pressedKeyIndex = 0; pressedKeyIndex < pressedKeys.Length; pressedKeyIndex++ ) {
				String keyName = pressedKeys[pressedKeyIndex].ToString();
				if (keyStates.ContainsKey(keyName))
				{
					keyStates[keyName] = KeyState.Down;
				}
			}

            // Check mouse inputs
            MouseState mouseState = Mouse.GetState();

			currentMouseX = mouseState.X;
			currentMouseY = mouseState.Y;

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

        /**
         * Reset keyStates and keyPresses.
         */
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
		
		/**
		 * Get the key binding that corresponds to the given action.
		 */

		public String getKeyBinding(String action)
		{
			return inputKeys[action];
		}

        /**
         * Write the new user config.
         */
        public void saveConfig()
        {
            inputConfig.writeFile(inputConfigFile);
        }
    }
}
