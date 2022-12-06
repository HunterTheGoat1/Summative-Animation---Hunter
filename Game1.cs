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
        Texture2D fishOne;
        Texture2D fishTwo;
        Texture2D endScreen;
        Texture2D shark;
        Texture2D fishOneBone;
        Texture2D fishTwoBone;
        Texture2D flipShark;
        Rectangle sharkRect;
        Rectangle fishOneRect;
        Rectangle fishTwoRect;
        List<Rectangle> bubbleRects;
        List<Vector2> bubbleSpeeds;
        List<int> bubbleHMovement;
        bool hasFishOne;
        bool hasFishTwo;
        bool loadShark;
        bool isFlipped;

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
            this.Window.Title = $"Animation Summative | Hunter Wilson";
            screen = Screen.Intro;
            ranGen = new Random();
            bubbleSpeeds = new List<Vector2>();
            bubbleRects = new List<Rectangle>();
            bubbleHMovement = new List<int>();
            int width;
            int sideMove;
            hasFishOne = false;
            hasFishTwo = false;
            loadShark = false;

            for (int i = 0; i < 25; i++)
            {
                width = ranGen.Next(50, 101);
                sideMove = ranGen.Next(0, 50);
                bubbleHMovement.Add(sideMove);
                bubbleRects.Add(new Rectangle(ranGen.Next(_graphics.PreferredBackBufferWidth - width), 2 * ranGen.Next(_graphics.PreferredBackBufferHeight), width, width));
                if (width < 60)
                    bubbleSpeeds.Add(new Vector2(ranGen.Next(-1, 2), -2));
                else if (width < 80)
                    bubbleSpeeds.Add(new Vector2(ranGen.Next(-1, 2), -4));
                else
                    bubbleSpeeds.Add(new Vector2(ranGen.Next(-1, 2), -6));
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
            fishOne = Content.Load<Texture2D>("fish1");
            fishTwo = Content.Load<Texture2D>("fish2");
            fishOneRect = new Rectangle(ranGen.Next(300, 650), ranGen.Next(100, 500), 150, 100);
            fishTwoRect = new Rectangle(ranGen.Next(0, 300), ranGen.Next(100, 500), 150, 100);
            sharkRect = new Rectangle(1000, 1000, 300, 200);
            endScreen = Content.Load<Texture2D>("EndScreen");
            shark = Content.Load<Texture2D>("shark");
            fishOneBone = Content.Load<Texture2D>("fishOneBone");
            fishTwoBone = Content.Load<Texture2D>("fishTwoBone");
            flipShark = Content.Load<Texture2D>("flipShark");
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();


            if (screen == Screen.Intro)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    screen = Screen.Main;
            }
            else if (screen == Screen.Main)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    loadShark = true;

                if (Keyboard.GetState().IsKeyDown(Keys.D2) && loadShark)
                    screen = Screen.Outro;
                Rectangle temp;
                for (int i = 0; i < bubbleRects.Count; i++)
                {
                    temp = bubbleRects[i];
                    Vector2 tempSpeeds = bubbleSpeeds[i];
                    temp.Y += (int)bubbleSpeeds[i].Y;
                    temp.X += (int)bubbleSpeeds[i].X;
                    int moveHcount = bubbleHMovement[i];
                    moveHcount++;
                    if (moveHcount > 50)
                    {
                        tempSpeeds.X *= -1;
                        moveHcount = 0;
                    }
                    bubbleHMovement[i] = moveHcount;
                    if (temp.Bottom < 0)
                    {
                        temp.Y = ranGen.Next(_graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferHeight + 100);
                        temp.X = ranGen.Next(_graphics.PreferredBackBufferWidth - temp.Width);
                    }
                    bubbleRects[i] = temp;
                    bubbleSpeeds[i] = tempSpeeds;
                }
                if (loadShark)
                {
                    if (hasFishOne)
                    {
                        fishOneRect.Y--;
                        if (hasFishTwo)
                        {
                            sharkRect.X -= 5;
                            fishTwoRect.Y--;
                            isFlipped = false;
                        }
                        else
                        {
                            fishTwoRect.X += 2;
                            if (fishTwoRect.X > 820)
                            {
                                fishTwoRect.X = -150;
                                fishTwoRect.Y = ranGen.Next(100, 500);
                            }
                            if (fishTwoRect.X > sharkRect.X)
                            {
                                sharkRect.X += 2;
                                isFlipped = true;
                            }
                            if (fishTwoRect.X < sharkRect.X)
                            {
                                sharkRect.X -= 2;
                                isFlipped = false;
                            }
                            if (fishTwoRect.Y > sharkRect.Y)
                            {
                                sharkRect.Y += 2;
                            }
                            if (fishTwoRect.Y < sharkRect.Y)
                            {
                                sharkRect.Y -= 2;
                            }
                            if (((fishTwoRect.X - sharkRect.X < 3) && (fishTwoRect.X - sharkRect.X > -3)) && ((fishTwoRect.Y - sharkRect.Y < 3) && (fishTwoRect.Y - sharkRect.Y > -3)))
                            {
                                hasFishTwo = true;
                            }
                        }
                    }
                    else
                    {
                        fishOneRect.X -= 2;
                        if (fishOneRect.X < -160)
                        {
                            fishOneRect.X = 850;
                            fishOneRect.Y = ranGen.Next(100, 500);
                        }
                        fishTwoRect.X += 2;
                        if (fishTwoRect.X > 820)
                        {
                            fishTwoRect.X = -150;
                            fishTwoRect.Y = ranGen.Next(100, 500);
                        }
                        if (fishOneRect.X > sharkRect.X)
                        {
                            sharkRect.X += 2;
                            isFlipped = true;
                        }
                        if (fishOneRect.X < sharkRect.X)
                        {
                            sharkRect.X -= 2;
                            isFlipped = false;
                        }
                        if (fishOneRect.Y > sharkRect.Y)
                        {
                            sharkRect.Y += 2;
                        }
                        if (fishOneRect.Y < sharkRect.Y)
                        {
                            sharkRect.Y -= 2;
                        }
                        if (((fishOneRect.X - sharkRect.X < 3) && (fishOneRect.X - sharkRect.X > -3)) && ((fishOneRect.Y - sharkRect.Y < 3) && (fishOneRect.Y - sharkRect.Y > -3)))
                        {
                            hasFishOne = true;
                        }
                    }
                }
                else
                {
                    fishOneRect.X -= 2;
                    if (fishOneRect.X < -160)
                    {
                        fishOneRect.X = 850;
                        fishOneRect.Y = ranGen.Next(100, 500);
                    }
                    fishTwoRect.X += 2;
                    if (fishTwoRect.X > 820)
                    {
                        fishTwoRect.X = -150;
                        fishTwoRect.Y = ranGen.Next(100, 500);
                    }
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
                _spriteBatch.DrawString(bigFont, "The Deep Sea", new Vector2(120, 130), Color.Black);
                _spriteBatch.DrawString(smallFont, "To enter the deep sea, please press 1 on your keyboard.", new Vector2(40, 250), Color.Black);
            }
            else if (screen == Screen.Main)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);
                if (loadShark)
                    _spriteBatch.DrawString(smallFont, "Press 2 to leave.", new Vector2(10, 0), Color.White);
                else
                    _spriteBatch.DrawString(smallFont, "Press S to summon the shark.", new Vector2(0, 0), Color.White);

                if (hasFishOne)
                    _spriteBatch.Draw(fishOneBone, fishOneRect, Color.White);
                else
                    _spriteBatch.Draw(fishOne, fishOneRect, Color.White);

                if (hasFishTwo)
                    _spriteBatch.Draw(fishTwoBone, fishTwoRect, Color.White);
                else
                    _spriteBatch.Draw(fishTwo, fishTwoRect, Color.White);

                if (loadShark)
                    if (isFlipped)
                        _spriteBatch.Draw(flipShark, sharkRect, Color.White);
                    else
                        _spriteBatch.Draw(shark, sharkRect, Color.White);

                foreach (Rectangle bubble in bubbleRects)
                    _spriteBatch.Draw(bubbleTexture, bubble, Color.White);
            }
            else if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(endScreen, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(smallFont, "Press 3 to close.", new Vector2(10, 0), Color.Black);
                _spriteBatch.DrawString(bigFont, "     The End", new Vector2(120, 130), Color.Black);
                _spriteBatch.DrawString(smallFont, "By: Hunter Wilson", new Vector2(10, 50), Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}