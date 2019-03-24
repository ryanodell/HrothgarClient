using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HrothgarGame.Logic.UI
{
    public class Label : Control
    {
        public string Text { get; set; }

        public Label(SpriteFont font, Vector2 position) : base(position)
        {
            Font = font;
            Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void OnFocus()
        {
            base.OnFocus();
        }

        public override void Update(GameTime gameTime, ScreenBase parent)
        {
            base.Update(gameTime, parent);
        }
    }
}
