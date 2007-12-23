using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace A_Snail_s_Pace
{
    abstract class Screen
    {
        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
        public abstract void LoadGraphicsContent(bool loadAllContent);
        public abstract void UnloadGraphicsContent(bool unloadAllContent);
        public abstract bool ready();
    }
}
