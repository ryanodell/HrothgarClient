﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace HrothgarGame.Logic
{
    public abstract class ScreenBase
    {
        public GraphicsDevice GraphicsDevice;
        public PresentationParameters PresentationParameters;
        public ContentManager Content;
        public Camera2D Camera;

        public ScreenBase(GraphicsDevice graphicsDevice, PresentationParameters presentationParameters, ContentManager content)
        {
            GraphicsDevice = graphicsDevice;
            PresentationParameters = presentationParameters;
            Content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
