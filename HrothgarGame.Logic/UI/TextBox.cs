using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HrothgarGame.Logic.UI
{
    public class TextBox : Control
    {
        private Rectangle LeftRectangle = new Rectangle(27 * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize);
        private Rectangle CenterRectangle = new Rectangle(28 * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize);
        private Rectangle RightRectangle = new Rectangle(29 * Constants.TileSize, 0, Constants.TileSize, Constants.TileSize);
        public string Text;
        public string Value;
        public int Size;

        public TextBox(Texture2D texture, Vector2 position, SpriteFont spriteFont, int size, bool hasFocus = false) : base(position)
        {
            Texture = texture;
            Size = size;
            HasFocus = hasFocus;
            Font = spriteFont;
        }

        public override void Update(GameTime gameTime, ScreenBase parent) { }

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
                spriteBatch.DrawString(Font, Text, new Vector2(Position.X + 4, Position.Y + 4), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
