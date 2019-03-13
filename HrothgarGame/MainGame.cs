using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HrothgarGame
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D blackBlock;
        Texture2D redDot;

        Vector2 blackBlockPos;
        Vector2 redDotPos;
        TcpClient tcpClient;



        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected async override void Initialize()
        {
            base.Initialize();
            await Initialize("127.0.0.1", 23000);
            //await Initialize("178.128.231.216", 23000);
            Task.Run(() => Read());
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            redDot = Content.Load<Texture2D>("RedDot");
            redDotPos = Vector2.Zero;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Right))
            {

            }
            if(ks.IsKeyDown(Keys.Left))
            {

            }

            base.Update(gameTime);
        }

        public async Task Initialize(string ip, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);

            Console.WriteLine("Connected to: {0}:{1}", ip, port);
        }

        public async Task Read()
        {
            var buffer = new byte[4096];
            var ns = tcpClient.GetStream();
            while (true)
            {
                var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) return; // Stream was closed
                var readStr = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine(readStr);
                var val = float.TryParse(readStr, out var x);
                redDotPos = new Vector2(x, 0);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(redDot, redDotPos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
