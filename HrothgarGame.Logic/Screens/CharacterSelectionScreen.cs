using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HrothgarGame.Logic
{
    public class CharacterSelectionScreen : ScreenBase
    {
        
        public CharacterSelectionScreen(GraphicsDevice graphicsDevice, PresentationParameters presentationParameters, ContentManager content) : base(graphicsDevice, presentationParameters, content)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            //throw new NotImplementedException();
            Global.NetworkManager.WriteData(((int)eClientRequest.CharacterSelectScreen).ToString());
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
