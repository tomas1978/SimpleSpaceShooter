﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SimpleSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int score = 0;
        SpriteFont scoreFont;
        Vector2 scorePos = new Vector2(5, 5);

        Texture2D player;
        Rectangle playerRect = new Rectangle(300,350,80,80);

        Texture2D enemy;
        int enemyDirection = -1;
        Rectangle enemyRect = new Rectangle(300, 100, 50, 50);

        Texture2D shot;
         List<Rectangle> shotRectangles;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            shotRectangles = new List<Rectangle>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player = Content.Load<Texture2D>("spaceship96");
            enemy = Content.Load<Texture2D>("alien");
            shot = Content.Load<Texture2D>("blasterbolt");
            scoreFont = Content.Load<SpriteFont>("scorefont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
                playerRect.X--;
            if (kstate.IsKeyDown(Keys.Right))
                playerRect.X++;

            if (enemyRect.X > 700 || enemyRect.X < 0)
                enemyDirection *= -1;
            enemyRect.X += enemyDirection;
            enemyRect.X += enemyDirection;
            
            if (kstate.IsKeyDown(Keys.Space)) {
                Rectangle shotRect1 = new Rectangle(playerRect.X+8, playerRect.Y+20, 5, 20);
                Rectangle shotRect2 = new Rectangle(playerRect.X+playerRect.Width-15, playerRect.Y+20, 5, 20);
                shotRectangles.Add(shotRect1);
                shotRectangles.Add(shotRect2);

            }

            for (int i=0;i<shotRectangles.Count;i++)
            {
                Rectangle r = shotRectangles[i];
                shotRectangles[i] = new Rectangle(r.X, r.Y - 5, r.Width, r.Height);
                if (shotRectangles[i].Intersects(enemyRect))
                {
                    enemyRect.Y -= 1;
                    score++;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(scoreFont, "Score: "+score, scorePos, Color.White);
            _spriteBatch.Draw(player, playerRect, Color.White);
            _spriteBatch.Draw(enemy, enemyRect, Color.White);
            foreach(Rectangle shotRect in shotRectangles)
                _spriteBatch.Draw(shot, shotRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
