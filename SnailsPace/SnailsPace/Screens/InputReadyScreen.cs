using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using A_Snail_s_Pace.Input;

namespace A_Snail_s_Pace.Screens
{
    abstract class InputReadyScreen : Screen
    {
        protected Dictionary<KeyCombination, ActionMapping> keyMapping;

        public InputReadyScreen( Game game ) : base( game )
        {
            keyMapping = new Dictionary<KeyCombination, ActionMapping>();
            initializeKeyMappings();
        }

        protected abstract void initializeKeyMappings();

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Dictionary<KeyCombination, ActionMapping>.Enumerator keyMapEnum = keyMapping.GetEnumerator();
            while (keyMapEnum.MoveNext())
            {
                KeyCombination keyCombination = keyMapEnum.Current.Key;
                ActionMapping actionMap = keyMapEnum.Current.Value;
                bool keyDown = true;
                Keys[] keys = keyCombination.getKeys();
                for (int keyIndex = 0; keyDown && keyIndex < keys.Length; keyIndex++)
                {
                    if (keyboardState.IsKeyUp(keys[keyIndex]))
                    {
                        keyDown = false;
                    }
                }
                if (keyDown)
                {
                    actionMap.keyDown(gameTime);
                }
                else
                {
                    actionMap.keyUp(gameTime);
                }
            }
        }

        public void assignKeyToAction(KeyCombination keyCombination, ActionMapping action)
        {
            if (keyMapping.ContainsKey(keyCombination))
            {
#if DEBUG
                ActionMapping oldAction;
                keyMapping.TryGetValue(keyCombination, out oldAction);
                SnailsPace.debug("Key Combination \"" + keyCombination.ToString() + "\" re-assigned from \"" +
                    oldAction.ToString() + "\" to \"" + action.ToString() + "\"");
#endif
                keyMapping.Remove(keyCombination);
            }
            keyMapping.Add(keyCombination, action);
        }
    }
}
