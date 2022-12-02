using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        Texture2D bubbleTexture;
        Texture2D introBackground;
        List<Rectangle> bubbleRects;
        List<Vector2> bubbleSpeeds;

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
            bubbleSpeeds = new List<Vector2>();
            bubbleRects = new List<Rectangle>();
            int width;

            for (int i = 0; i < 25; i++)
            {
                width = ranGen.Next(50, 101);
                bubbleRects.Add(new Rectangle(ranGen.Next(_graphics.PreferredBackBufferWidth - width), 2 * ranGen.Next(_graphics.PreferredBackBufferHeight), width, width));
                if (width< 60)
                    bubbleSpeeds.Add(new Vector2(0, -2));
                else if (width < 80)
                    bubbleSpeeds.Add(new Vector2(0, -4));
                else
                    bubbleSpeeds.Add(new Vector2(0, -6));
            }

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
            bubbleTexture = Content.Load<Texture2D>("bubble");
            introBackground = Content.Load<Texture2D>("introBack");
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            this.Window.Title = $"Animation Summative | Hunter Wilson | {screen}";

            if (screen == Screen.Intro)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    screen = Screen.Main;
            }
            else if (screen == Screen.Main)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    screen = Screen.Outro;
                Rectangle temp;
                for (int i = 0; i < bubbleRects.Count; i++)
                {
                    temp = bubbleRects[i];                   
                    temp.Y += (int)bubbleSpeeds[i].Y;
                    if (temp.Bottom < 0)
                    {
                        temp.Y = ranGen.Next(_graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferHeight + 100);
                        temp.X = ranGen.Next(_graphics.PreferredBackBufferWidth - temp.Width);
                    }
                    bubbleRects[i] = temp;
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
                foreach(Rectangle bubble in bubbleRects)
                    _spriteBatch.Draw(bubbleTexture, bubble, Color.White);
            }
            else if (screen == Screen.Outro)
            {
                
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}