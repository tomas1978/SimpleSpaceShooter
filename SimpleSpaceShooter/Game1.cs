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

        int score = 0;
        SpriteFont scoreFont;
        Vector2 scorePos = new Vector2(5, 5);

        Sprite player;

        Sprite enemy;
        Vector2 enemySpeed = new Vector2(1, 0);
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
            Texture2D playerTexture = Content.Load<Texture2D>("spaceship96");
            Texture2D enemyTexture = Content.Load<Texture2D>("alien");
            shot = Content.Load<Texture2D>("blasterbolt");
            scoreFont = Content.Load<SpriteFont>("scorefont");
            player = new Sprite(300, 350, playerTexture);
            enemy = new Sprite(300, 100, enemyTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
            {
                player.Move(-1,0);
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                player.Move(1, 0);
            }

            if (enemy.spriteRect.X > 700 || enemy.spriteRect.X < 0)
                enemySpeed.X *= -1;
            enemy.Move((int)enemySpeed.X, 0);
     
            if (kstate.IsKeyDown(Keys.Space)) {
                Rectangle shotRect1 = new Rectangle(player.spriteRect.X + 8, 
                                        player.spriteRect.Y + 20, 5, 20);
                Rectangle shotRect2 = new Rectangle(player.spriteRect.X + player.spriteRect.Width-15, 
                                        player.spriteRect.Y+20, 5, 20);
                shotRectangles.Add(shotRect1);
                shotRectangles.Add(shotRect2);
            }

            for (int i=0;i<shotRectangles.Count;i++)
            {
                Rectangle r = shotRectangles[i];
                shotRectangles[i] = new Rectangle(r.X, r.Y - 5, r.Width, r.Height);
                if (shotRectangles[i].Intersects(enemy.spriteRect))
                {
                    enemy.Move(0, -1);
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
            _spriteBatch.Draw(player.spriteTexture, player.spriteRect, Color.White);
            _spriteBatch.Draw(enemy.spriteTexture, enemy.spriteRect, Color.White);
            foreach(Rectangle shotRect in shotRectangles)
                _spriteBatch.Draw(shot, shotRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
