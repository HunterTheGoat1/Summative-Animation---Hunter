using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Summative_Animation___Hunter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Random ranGen;
        MouseState _mouseState;
        SpriteFont smallFont;
        SpriteFont bigFont;
        Texture2D background;
        Texture2D bubble;
        Texture2D introBackground;
        int bubbleOneY;
        int bubbleTwoY;
        int bubbleThreeY;

        enum Screen
        {
            Intro,
            Main,
            Outro
        }
        Screen screen;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            ranGen = new Random();
            bubbleOneY = ranGen.Next(0, 200);
            bubbleTwoY = ranGen.Next(400, 600);
            bubbleThreeY = ranGen.Next(200, 400);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            smallFont = Content.Load<SpriteFont>("SmallFont");
            bigFont = Content.Load<SpriteFont>("BigFont");
            background = Content.Load<Texture2D>("background");
            bubble = Content.Load<Texture2D>("bubble");
            introBackground = Content.Load<Texture2D>("introBack");
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            this.Window.Title = $"Animation Summative | Hunter Wilson | {screen} | Mouse: X({_mouseState.X}), Y({_mouseState.Y})";

            if (screen == Screen.Intro)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    screen = Screen.Main;
            }
            else if (screen == Screen.Main)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    screen = Screen.Outro;
                bubbleOneY -= 5;
                if (bubbleOneY < 0 - bubble.Height)
                {
                    bubbleOneY = 600;
                }
                bubbleTwoY -= 5;
                if (bubbleTwoY < 0 - bubble.Height)
                {
                    bubbleTwoY = 600;
                }
                bubbleThreeY -= 5;
                if (bubbleThreeY < 0 - bubble.Height)
                {
                    bubbleThreeY = 600;
                }
            }
            else if (screen == Screen.Outro)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    Exit();
            }

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            GraphicsDevice.Clear(Color.DarkCyan);

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introBackground, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(bigFont, "The Deep Sea", new Vector2(120,130), Color.Black);
                _spriteBatch.DrawString(smallFont, "To enter the deep sea, please press 1 on your keyboard.", new Vector2(40, 250), Color.Black);
            }
            else if (screen == Screen.Main)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(smallFont, "Press 2 to leave.", new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(bubble, new Rectangle(0, bubbleOneY, 100, 100), Color.White);
                _spriteBatch.Draw(bubble, new Rectangle(350, bubbleTwoY, 100, 100), Color.White);
                _spriteBatch.Draw(bubble, new Rectangle(700, bubbleThreeY, 100, 100), Color.White);
            }
            else if (screen == Screen.Outro)
            {
                
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}