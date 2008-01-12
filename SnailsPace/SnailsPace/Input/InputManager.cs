using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace A_Snail_s_Pace.Input
{
    class InputManager
    {
        private LuaConfig inputConfig;
        private String inputConfigFile = "config/inputs.lua";

        private Dictionary<String, String> inputKeys;
        private Dictionary<String, KeyState> keyStates;

        /**
         * Initialize the InputManager object and configuration.
         */
        public InputManager()
        {
            inputKeys = new Dictionary<String, String>();
            keyStates = new Dictionary<String,KeyState>();

            // Default action assignments
            inputKeys.Add("MenuUp", "Up");
            inputKeys.Add("MenuDown", "Down");
            inputKeys.Add("MenuLeft", "Left");
            inputKeys.Add("MenuRight", "Right");
            inputKeys.Add("MenuSelect", "Enter");
            inputKeys.Add("Up", "W");
            inputKeys.Add("Down", "S");
            inputKeys.Add("Left", "A");
            inputKeys.Add("Right", "D");
            inputKeys.Add("Fire", "Mouse1");

            
            // Read from the user's config file
            inputConfig = new LuaConfig(new Dictionary<string,double>(), inputKeys);
            inputConfig.readFile(inputConfigFile);
            inputKeys = inputConfig.getStrings();
        }

        /**
         * Check the state of an input action.
         */
        public KeyState inputState(String action)
        {
            if (inputKeys.ContainsKey(action))
            {
                return keyStates[inputKeys[action]];
            }
            else
            {
                return KeyState.Up;
            }
        }

        /**
         * Update the input states.
         */
        public void update()
        {
            // Set all inputs to up
            keyStates = new Dictionary<String, KeyState>();
            foreach (String key in inputKeys.Values)
            {
                keyStates.Add(key, KeyState.Up);
            }
            
            // Check keyboard inputs
            KeyboardState keyboardState = new KeyboardState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            foreach (Keys pressedKey in pressedKeys)
            {
                String keyName = pressedKey.ToString();
                if (keyStates.ContainsKey(keyName))
                {
                    keyStates[keyName] = KeyState.Down;
                }
            }

            // Check mouse inputs
            MouseState mouseState = new MouseState();

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
