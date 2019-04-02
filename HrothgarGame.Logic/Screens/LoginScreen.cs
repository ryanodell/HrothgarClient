using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HrothgarGame.Logic.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace HrothgarGame.Logic
{
    public class LoginScreen : ScreenBase
    {
        private List<Control> Controls = new List<Control>();
        Camera2D backgroundCam;

        public LoginScreen(GraphicsDevice graphicsDevice, PresentationParameters presentationParameters, 
            ContentManager content, GameWindow gameWindow) : base(graphicsDevice, presentationParameters, content)
        {
            _initializeCamera();
            gameWindow.TextInput += TextInputeHandler;
            backgroundCam = new Camera2D(graphicsDevice);
            backgroundCam.Zoom = 3.5f;
        }

        private void TextInputeHandler(object sender, TextInputEventArgs args)
        {
            foreach (var control in Controls)
            {
                if (control.HasFocus && control is TextBox)
                {
                    var textBox = (TextBox)control;
                    var pressedKey = args.Key;
                    var character = args.Character;
                    if (pressedKey == Keys.Back)
                    {
                        if (!string.IsNullOrEmpty(textBox.Text))
                        {
                            textBox.Text = textBox.Text.TrimEnd(textBox.Text[textBox.Text.Length - 1]);
                        }
                        return;
                    }
                    if (pressedKey == Keys.Enter)
                    {
                        //networkClient.Write(typeThing);
                        return;
                    }
                    if(pressedKey == Keys.Tab)
                    {
                        foreach (var item in Controls)
                        {
                            if (item is TextBox && !item.HasFocus)
                            {
                                control.HasFocus = false;
                                item.HasFocus = true;
                                return;
                            }
                        }
                    }
                    textBox.Text += character;
                }
            }
        }

        public override void LoadContent()
        {
            int offset = 125;
            var userNameTextBox = new TextBox(Global.Textures[Constants.Textures.Gui], new Vector2(250 + offset, 250),
                Global.Fonts[Constants.Fonts.SDS_6x6], 5, true) { Id = "txtUsername" };
            Controls.Add(userNameTextBox);
            var passwordTextBox = new TextBox(Global.Textures[Constants.Textures.Gui], new Vector2(250 + offset, 275),
                Global.Fonts[Constants.Fonts.SDS_6x6], 5) { Id = "txtPassword" };
            Controls.Add(passwordTextBox);
            var userNameLabel = new Label(Global.Fonts[Constants.Fonts.SDS_6x6], new Vector2(195 + offset, 254));
            var passwordLabel = new Label(Global.Fonts[Constants.Fonts.SDS_6x6], new Vector2(195 + offset, 279));
            userNameLabel.Text = "Username:";
            passwordLabel.Text = "Password:";

            var loginButton = new Button(new Vector2(250 + offset, 300), Global.Textures[Constants.Textures.Gui], 
                Global.Fonts[Constants.Fonts.SDS_6x6]);
            loginButton.Size = 3;
            loginButton.Text = "LOGIN";
            loginButton.OnClick += Login_onClick;


            var testSendDataButton = new Button(new Vector2(250 + offset, 400), Global.Textures[Constants.Textures.Gui], 
                Global.Fonts[Constants.Fonts.SDS_6x6]);
            testSendDataButton.Size = 3;
            testSendDataButton.Text = "Test";
            testSendDataButton.OnClick += SendTestData;

            Controls.Add(userNameLabel);
            Controls.Add(passwordLabel);
            Controls.Add(loginButton);
            Controls.Add(testSendDataButton);
        }

        private void SendTestData(object sender, EventArgs args)
        {
            Global.NetworkManager.WriteData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var control in Controls)
            {
                control.Update(gameTime, this);
            }
        }

        protected void Login_onClick(object sender, EventArgs args)
        {
            var userName = ((TextBox)Controls.FirstOrDefault(x => x.Id == "txtUsername")).Text;
            var password = ((TextBox)Controls.FirstOrDefault(x => x.Id == "txtPassword")).Text;
            Console.WriteLine($"Username: {userName} Password: {password}");
            //Global.NetworkManager.WriteData($"{userName}|{password}");
            Global.NetworkManager.ConnectToServer(userName, password);
            Global.NetworkManager.OnAuthenticated += OnAuthenticated;
        }

        protected void OnAuthenticated(object sender, EventArgs args)
        {
            //ScreenManager.Instance.SetScreen(new CharacterSelectionScreen(GraphicsDevice, PresentationParameters, Content));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, backgroundCam.GetViewMatrix());
            spriteBatch.Draw(Global.Textures["Mine"], new Vector2(250, 150), Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.GetViewMatrix());
            
            foreach (var control in Controls)
            {
                control.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        private void _initializeCamera()
        {
            Camera = new Camera2D(GraphicsDevice);
            Camera.Zoom = 2f;            
        }

    }
}
