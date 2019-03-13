using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using HrothgarGame.Networking;


namespace HrothgarGame
{
    public class MainGame : Game
    {
        NetworkClient networkClient;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D blackBlock;
        Texture2D redDot;
        TcpClient tcpClient;
        Player player;
        List<Client> Clients = new List<Client>();

        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void OnDataReceived(object obj, EventArgs args)
        {
            var readStr = ((DatReceivedEventArgs)args).Data;
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
                        if (client.Id == id)
                        {
                            client.Position = pos;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        protected async override void Initialize()
        {
            base.Initialize();
            networkClient = new NetworkClient("127.0.0.1", 23000);
            networkClient.Packet_Reader.OnReceiveData += OnDataReceived;
            networkClient.Read();
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
                if (ks.IsKeyDown(Keys.Up))
                {
                    player.Position.Y -= 5;
                }
                if (ks.IsKeyDown(Keys.Down))
                {
                    player.Position.Y += 5;
                }
                Write(player.Position.ToString());
            }

            base.Update(gameTime);
        }

        public void Write(string data)
        {
            networkClient.Write(data);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (player != null)
            {
                spriteBatch.Draw(player.Texture, player.Position, Color.White);
            }
            foreach (var client in Clients)
            {
                spriteBatch.Draw(client.Texture, client.Position, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
