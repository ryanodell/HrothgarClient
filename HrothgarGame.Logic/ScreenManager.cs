using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HrothgarGame.Logic
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public ScreenBase CurrentScreen { private set; get; }
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public void SetScreen(ScreenBase screen)
        {
            CurrentScreen = screen;
            CurrentScreen.LoadContent();
        }

        public ScreenManager() { }

        public void LoadContent()
        {
            CurrentScreen?.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen?.Draw(spriteBatch);
        }
    }
}
