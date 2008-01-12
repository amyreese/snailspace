using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SnailsPace.Input;

namespace SnailsPace.Screens
{
    abstract class InputReadyScreen : Screen
    {
        protected Dictionary<KeyCombination, ActionMapping> keyMapping;

        public InputReadyScreen(SnailsPace game)
            : base(game)
        {
            keyMapping = new Dictionary<KeyCombination, ActionMapping>();
            initializeKeyMappings();
        }

        protected abstract void initializeKeyMappings();

        private TimeSpan timeWhenUpdatesStart = new TimeSpan();
        private int millisecondsBeforeInputAccepted = 250;
        public override void Update(GameTime gameTime)
        {
			if (timeWhenUpdatesStart.TotalMilliseconds == 0)
			{
				timeWhenUpdatesStart = gameTime.TotalGameTime.Add(new TimeSpan(0, 0, 0, 0, millisecondsBeforeInputAccepted));
			}
			if (gameTime.TotalGameTime.Subtract(timeWhenUpdatesStart).TotalMilliseconds > 0)
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
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {

			timeWhenUpdatesStart = new TimeSpan();
            base.OnEnabledChanged(sender, args);
        }

        public void assignKeyToAction(KeyCombination keyCombination, ActionMapping action)
        {
            if (keyMapping.ContainsKey(keyCombination))
            {
#if DEBUG
				if (SnailsPace.debugKeyAssignments)
				{
					ActionMapping oldAction;
					keyMapping.TryGetValue(keyCombination, out oldAction);
					SnailsPace.debug("Key Combination \"" + keyCombination.ToString() + "\" re-assigned from \"" +
						oldAction.ToString() + "\" to \"" + action.ToString() + "\"");
				}
#endif
                keyMapping.Remove(keyCombination);
            }
            keyMapping.Add(keyCombination, action);
        }
    }
}
