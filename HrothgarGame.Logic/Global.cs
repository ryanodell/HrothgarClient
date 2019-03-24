using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HrothgarGame.Logic
{
    public static class Global
    {
        public static NetworkManager NetworkManager;
        public static Game MainGame;
        public static string IpAddress = "127.0.0.1";
        public static int Port = 23000;
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
    }
}
