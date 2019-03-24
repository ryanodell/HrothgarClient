using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HrothgarGame.Logic
{
    public class GameScreen : ScreenBase
    {
        public GameScreen(GraphicsDevice graphicsDevice, PresentationParameters presentationParameters, 
            ContentManager contentManager) : base(graphicsDevice, presentationParameters, contentManager)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
