using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HrothgarGame
{
    public class TmpGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D blackBlock;
        Texture2D redDot;

        TcpClient tcpClient;

        int? Id;

        Player player;

        List<Client> Clients = new List<Client>();

        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public TmpGame()
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
            blackBlock = Content.Load<Texture2D>("BlackBlock");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.Down))
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    player.Position.X += 5;
                }
                if (ks.IsKeyDown(Keys.Left))
                {
                    player.Position.X -= 5;
                }
                if(ks.IsKeyDown(Keys.Up))
                {
                    player.Position.Y -= 5;
                }
                if(ks.IsKeyDown(Keys.Down))
                {
                    player.Position.Y += 5;
                }
                Write(player.Position.ToString());
            }

            base.Update(gameTime);
        }

        public async Task Initialize(string ip, int port)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);

            Console.WriteLine("Connected to: {0}:{1}", ip, port);
        }

        public void Write(string data)
        {
            var buff = Encoding.ASCII.GetBytes(data);
            var ns = tcpClient.GetStream();
            ns.Write(buff, 0, buff.Length);
        }

        public async Task Read()
        {
            var buffer = new byte[4096];
            var ns = tcpClient.GetStream();
            try
            {
                while (true)
                {
                    var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) return; // Stream was closed
                    var readStr = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(readStr);
                    var data = readStr.Split('|');
                    switch (data[0])
                    {
                        case "0":
                            player = new Player()
                            {
                                Id = Convert.ToInt32(data[1]),
                                Position = Vector2.Zero,
                                Texture = Convert.ToInt32(data[2]) == 0 ? redDot : blackBlock
                            };
                            Console.WriteLine($"Connected: PlayerID: {player.Id}, Model: {data[2]}");
                            break;
                        case "1":
                            var newClient = new Client()
                            {
                                Id = Convert.ToInt32(data[1]),
                                Position = Vector2.Zero,
                                Texture = Convert.ToInt32(data[2]) == 0 ? redDot : blackBlock
                            };
                            Clients.Add(newClient);
                            Console.WriteLine($"Client added to scene: ID: {newClient.Id}");
                            break;
                        case "2":
                            Console.WriteLine("Update Client Position");
                            var id = Convert.ToInt32(data[1]);
                            var x = Convert.ToInt32(data[2]);
                            var y = Convert.ToInt32(data[3]);
                            var pos = new Vector2(x, y);
                            foreach (var client in Clients)
                            {
                                if(client.Id == id)
                                {
                                    client.Position = pos;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //var val = float.TryParse(readStr, out var x);
                    //redDotPos = new Vector2(x, 0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("It ended");
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if(player != null)
            {
                spriteBatch.Draw(player.Texture, player.Position, Color.White);
            }
            foreach(var client in Clients)
            {
                spriteBatch.Draw(client.Texture, client.Position, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
