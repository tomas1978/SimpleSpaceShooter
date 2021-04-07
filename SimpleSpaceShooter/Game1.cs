using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SimpleSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D player;
        Rectangle playerRect = new Rectangle(300,350,80,80);

        Texture2D enemy;
        int enemyDirection = -1;
        Rectangle enemyRect = new Rectangle(300,100, 50, 50);

        Texture2D shot;
        Rectangle shotRect = new Rectangle(0, 0, 5, 20);

        List<Texture2D> shots;
        List<Vector2> shotPositions;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            shots = new List<Texture2D>();
            shotPositions = new List<Vector2>();
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
            //playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, player.Width, player.Height);
            //shotRect = new Rectangle((int)shotPos.X, (int)shotPos.Y, shot.Width, shot.Height);
            //enemyRect = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemy.Width, enemy.Height);
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
                shotRect.X = playerRect.X;
                shotRect.Y = playerRect.Y;
            }

            shotRect.Y-=8;
            shotRect.Y -= 8;

            if(shotRect.Intersects(enemyRect)) {
                enemyRect.Y -= 1;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(player, playerRect, Color.White);
            _spriteBatch.Draw(enemy, enemyRect, Color.White);
            _spriteBatch.Draw(shot, shotRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
