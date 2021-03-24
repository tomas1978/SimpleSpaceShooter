using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D player;
        Vector2 playerPos = new Vector2(300, 350);

        Texture2D enemy;
        Vector2 enemyPos = new Vector2(300, 100);
        int enemyDirection = -1;

        Texture2D shot;
        Vector2 shotPos = new Vector2(0, 0);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
                playerPos.X--;
            if (kstate.IsKeyDown(Keys.Right))
                playerPos.X++;

            if (enemyPos.X > 700 || enemyPos.X < 0)
                enemyDirection *= -1;
            enemyPos.X += enemyDirection;

            if(kstate.IsKeyDown(Keys.Space)) {
                shotPos.X = playerPos.X;
                shotPos.Y = playerPos.Y;
            }

            shotPos.Y-=8;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(player, playerPos, Color.White);
            _spriteBatch.Draw(enemy, enemyPos, Color.White);
            _spriteBatch.Draw(shot, shotPos, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
