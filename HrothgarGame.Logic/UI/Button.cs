using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace HrothgarGame.Logic.UI
{
    public class Button : Control
    {
        MouseState oldState;
        private Rectangle LeftRectangle = new Rectangle(27 * Constants.TileSize, 1 * Constants.TileSize, 
            Constants.TileSize, Constants.TileSize);
        private Rectangle CenterRectangle = new Rectangle(28 * Constants.TileSize, 1 * Constants.TileSize,
            Constants.TileSize, Constants.TileSize);
        private Rectangle RightRectangle = new Rectangle(29 * Constants.TileSize, 1 * Constants.TileSize, 
            Constants.TileSize, Constants.TileSize);
        public string Text { get; set; }
        public int Size { get; set; }

        public event EventHandler<EventArgs> OnClick;

        public Button(Vector2 position, Texture2D texture, SpriteFont spriteFont) : base(position)
        {
            Font = spriteFont;
            Texture = texture;
            Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, LeftRectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1f);
            Vector2 finalPos = Position;
            if (Size > 1)
            {
                for (int i = 1; i < Size; i++)
                {
                    var xFactor = i * Constants.TileSize;
                    var newPos = new Vector2(Position.X + xFactor, Position.Y);
                    spriteBatch.Draw(Texture, newPos, CenterRectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1f);
                    finalPos = newPos;
                }
            }
            var lastVector = new Vector2(finalPos.X + Constants.TileSize, finalPos.Y);
            spriteBatch.Draw(Texture, lastVector, RightRectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1f);
            if (!string.IsNullOrEmpty(Text))
            {
                spriteBatch.DrawString(Font, Text, new Vector2(Position.X + 15, Position.Y + 4), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        public override void Update(GameTime gameTime, ScreenBase parent)
        {
            MouseState mState = Mouse.GetState();
            if(mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(parent.Camera.GetViewMatrix()));
                var buttonMaxX = Position.X + ((Size + 1) * Constants.TileSize);
                var buttonMaxY = Position.Y + Constants.TileSize;
                if(mousePosition.X >= Position.X && mousePosition.X <= buttonMaxX 
                    && mousePosition.Y >= Position.Y && mousePosition.Y <= buttonMaxY)
                {
                    OnClick(this, null);
                }                
            }
            oldState = mState;
        }
    }
}
