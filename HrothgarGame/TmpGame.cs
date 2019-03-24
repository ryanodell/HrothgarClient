using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using HrothgarGame.Networking;
using HrothgarGame.Logic;

namespace HrothgarGame
{
    public class TmpGame : Game
    {
        NetworkClient networkClient;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D blackBlock;
        Texture2D redDot;
        Player player;
        List<Client> Clients = new List<Client>();
        SpriteFont font;
        List<string> Chat = new List<string>();
        string typeThing = string.Empty;

        public TmpGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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
                case "3":
                    var contents = data[1];
                    Chat.Add(contents);
                    break;
                default:
                    break;
            }
        }

        protected async override void Initialize()
        {
            base.Initialize();
            //networkClient = new NetworkClient("127.0.0.1", 23000);
            //networkClient.Packet_Reader.OnReceiveData += OnDataReceived;
            //await networkClient.Read();
            Global.MainGame = this;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            redDot = Content.Load<Texture2D>("RedDot");
            blackBlock = Content.Load<Texture2D>("BlackBlock");
            font = Content.Load<SpriteFont>("Fonts/MainFont");
            var pp = GraphicsDevice.PresentationParameters;            
            Global.Fonts.Add("SDS_6x6", Content.Load<SpriteFont>("Fonts/SDS_6x6"));
            Global.Fonts.Add("SDS_8x8", Content.Load<SpriteFont>("Fonts/SDS_8x8"));
            Global.Textures.Add("GUI", Content.Load<Texture2D>("UI/GUI"));

            ScreenManager.Instance.SetScreen(new LoginScreen(GraphicsDevice, pp, Content, Window));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ScreenManager.Instance.Update(gameTime);
            //var ks = Keyboard.GetState();
            //if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.Down))
            //{
            //    if (ks.IsKeyDown(Keys.Right))
            //    {
            //        player.Position.X += 5;
            //    }
            //    if (ks.IsKeyDown(Keys.Left))
            //    {
            //        player.Position.X -= 5;
            //    }
            //    if (ks.IsKeyDown(Keys.Up))
            //    {
            //        player.Position.Y -= 5;
            //    }
            //    if (ks.IsKeyDown(Keys.Down))
            //    {
            //        player.Position.Y += 5;
            //    }
            //    Write(player.Position.ToString());
            //}

            base.Update(gameTime);
        }

        public void Write(string data)
        {
            networkClient.Write(data);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Instance.Draw(spriteBatch, gameTime);
            //spriteBatch.Begin();
            //if (player != null)
            //{
            //    spriteBatch.Draw(player.Texture, player.Position, Color.White);
            //}
            //foreach (var client in Clients)
            //{
            //    spriteBatch.Draw(client.Texture, client.Position, Color.White);
            //}
            //string chatText = string.Empty;
            //foreach (var item in Chat)
            //{
            //    chatText += item + Environment.NewLine;
            //}
            //spriteBatch.DrawString(font, chatText, new Vector2(100, 100), Color.White);
            //spriteBatch.DrawString(font, typeThing, new Vector2(0, 200), Color.White);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
