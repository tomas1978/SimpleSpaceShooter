using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SimpleSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int score = 0;
        SpriteFont scoreFont;
        Vector2 scorePos;
        SpriteFont gameOverFont;
        Vector2 gameOverPos;

        Sprite player;
        bool gameOver = false;

        Vector2 enemySpeed;
        List<Sprite> enemyFleet;

        Texture2D shotTexture;
        List<Sprite> playerShots;
        List<Sprite> enemyShots;

        Texture2D enemyShotTexture;

        SoundEffect explosion;

        GameTime gameTime;

        Random rand;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            playerShots = new List<Sprite>();
            enemyShots = new List<Sprite>();
            gameTime = new GameTime();
            rand = new Random();
        }

        protected override void Initialize()
        {
            scorePos = new Vector2(5, 5);
            gameOverPos = new Vector2(100, 100);
            enemySpeed = new Vector2(1, 0);
            enemyFleet = new List<Sprite>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = Content.Load<Texture2D>("spaceship96");
            Texture2D enemyTexture = Content.Load<Texture2D>("alien");
            enemyShotTexture = Content.Load<Texture2D>("blasterboltEnemy");
            shotTexture = Content.Load<Texture2D>("blasterbolt");
            scoreFont = Content.Load<SpriteFont>("scorefont");
            gameOverFont = Content.Load<SpriteFont>("gameover.spritefont");
            explosion = Content.Load<SoundEffect>("shortExplosion");
            player = new Sprite(300, 350, 20, 5, 10, playerTexture);
            for(int i=0;i<5;i++) {
                enemyFleet.Add(new Sprite(300+50*i, 100, 4, 1, 30+rand.Next(0,100), enemyTexture));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left) && player.spriteRect.X>0 && !gameOver) {
                player.Move(-1,0);
            }
            if (kstate.IsKeyDown(Keys.Right) && player.spriteRect.X<
                _graphics.PreferredBackBufferWidth - player.spriteRect.Width && !gameOver) {
                player.Move(1, 0);
            }

            if(gameOver)
            {
                if(kstate.IsKeyDown(Keys.Home)) {
                    score = 0;
                    gameOver = false;
                    player.Energy = 20;
                }
            }

            player.LastShotTime++;
            
            if (kstate.IsKeyDown(Keys.Space) && player.ReadyToFire() && !gameOver) {
                Sprite leftShot = new Sprite(player.spriteRect.X, player.spriteRect.Y + 20, 1, 0, 0, shotTexture);
                Sprite rightShot = new Sprite(player.spriteRect.X + player.spriteRect.Width - 29, 
                                                player.spriteRect.Y + 20, 1, 0, 0, shotTexture);

                playerShots.Add(leftShot);
                playerShots.Add(rightShot);
            }

            foreach (Sprite e in enemyFleet) {
                e.LastShotTime++;
                if (e.ReadyToFire())
                    enemyShots.Add(new Sprite(e.spriteRect.X, e.spriteRect.Y, 1, -1, 0, enemyShotTexture));
            }

            foreach (Sprite s in enemyShots) {
                s.Move(0, 1);
            }

            foreach (Sprite s in enemyFleet) {
                if (s.spriteRect.X > _graphics.PreferredBackBufferWidth || s.spriteRect.X < 0)
                    s.Direction *= -1;
                s.Move(s.Direction*(int)enemySpeed.X, 0);
            }

            foreach(Sprite s in playerShots) {
                s.Move(0, -3);
            }

            Sprite shotToRemove = null;
            if (!gameOver) {
                foreach (Sprite s in enemyShots) {
                    if (s.spriteRect.Intersects(player.spriteRect)) {
                        player.Energy--;
                        score -= 1;
                        player.Move(0, 1);
                        shotToRemove = s;
                    }
                }
                if (player.Energy < 1) {
                    gameOver = true;
                    explosion.Play();
                }
                if (shotToRemove != null)
                    enemyShots.Remove(shotToRemove);
            }
           
            Sprite enemyToRemove = null;

            foreach(Sprite s in playerShots) {
                foreach(Sprite e in enemyFleet) {
                    if(s.spriteRect.Intersects(e.spriteRect)) {
                        e.Move(0, -1);
                        shotToRemove = s;
                        e.Energy--;
                        if (e.Energy < 0) {
                            score += 1;
                            enemyToRemove = e;
                        }
                    }
                }
            }

            if(shotToRemove!=null) {
                playerShots.Remove(shotToRemove);
            }

            if(enemyToRemove!=null) {
                enemyFleet.Remove(enemyToRemove);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(scoreFont, "Score: "+score, scorePos, Color.White);
            if(!gameOver)
                _spriteBatch.Draw(player.spriteTexture, player.spriteRect, Color.White);
            if (gameOver) {
                _spriteBatch.DrawString(gameOverFont, "Game Over", gameOverPos, Color.White);
                _spriteBatch.DrawString(gameOverFont, "Press Home to restart", 
                    new Vector2(60, 140), Color.White);
            }
            foreach(Sprite s in playerShots)
                _spriteBatch.Draw(s.spriteTexture, s.spriteRect, Color.White);
            foreach (Sprite s in enemyShots)
                _spriteBatch.Draw(s.spriteTexture, s.spriteRect, Color.White);
            foreach (Sprite s in enemyFleet)
                _spriteBatch.Draw(s.spriteTexture, s.spriteRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
