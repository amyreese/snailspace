using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnailsPace.Input;

namespace SnailsPace.Screens.Menus
{
    class MenuItem : Microsoft.Xna.Framework.IDrawable
    {
        private String text;
        private MenuScreen screen;
        private Vector2 position;
        private ActionMapping.KeyAction action;

        public MenuItem(String text, MenuScreen screen, Vector2 position, ActionMapping.KeyAction action)
        {
            this.text = text;
            this.screen = screen;
            this.position = position;
            this.action = action;
        }

        public void Draw(GameTime time)
        {
            Color color = Selected ? Color.Yellow : Color.White;
            float scale = Selected ? 1.5f : 1f;
            SpriteBatch spriteBatch = screen.spriteBatch;
            SpriteFont font = screen.spriteFont;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, color, 0, origin, scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void Activate(GameTime gameTime)
        {
            action(gameTime);
        }
        private bool _selected = false;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        private bool _visible = true;
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                List<EventHandler>.Enumerator listnerEnumerator = _visibleChangedListners.GetEnumerator();
                while (listnerEnumerator.MoveNext())
                {
                    listnerEnumerator.Current.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _drawOrder = 0;
        public int DrawOrder
        {
            get
            {
                return _drawOrder;
            }
            set
            {
                _drawOrder = value;
                List<EventHandler>.Enumerator listnerEnumerator = _drawOrderChangedListners.GetEnumerator();
                while (listnerEnumerator.MoveNext())
                {
                    listnerEnumerator.Current.Invoke(this, EventArgs.Empty);
                }
            }
        }

        List<EventHandler> _drawOrderChangedListners = new List<EventHandler>();
        event EventHandler IDrawable.DrawOrderChanged
        {
            add
            {
                _drawOrderChangedListners.Add(value);
            }
            remove
            {
                _drawOrderChangedListners.Remove(value);
            }
        }

        List<EventHandler> _visibleChangedListners = new List<EventHandler>();
        event EventHandler IDrawable.VisibleChanged
        {
            add
            {
                _visibleChangedListners.Add(value);
            }
            remove
            {
                _visibleChangedListners.Remove(value);
            }
        }
    }
}
