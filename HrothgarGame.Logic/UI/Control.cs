using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HrothgarGame.Logic.UI
{
    public abstract class Control
    {
        public Texture2D Texture;
        public bool HasFocus;
        public Vector2 Position;
        public SpriteFont Font;
        public string Id;

        public Control(Vector2 position)
        {
            Position = position;
        }

        public virtual void OnFocus()
        {
            HasFocus = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void Update(GameTime gameTime, ScreenBase parent) { }

    }
}
