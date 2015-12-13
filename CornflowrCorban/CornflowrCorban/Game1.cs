using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CornflowrCorban
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WhaleOfAPlayer Player { get; set; }

        Texture2D Background { get; set; }

        KeyboardState oldState;
        GamePadState oldStatePad;

        List<Laser> lasers;
        List<Bubble> bubbles;
        List<Bubble> topBubbles;
        List<ComicHit> comicHits;

        int Score { get; set; }

        private Vector2 backgroundParallax;

        TimeSpan StartTime { get; set; }
        TimeSpan currentTime { get; set; }

        Random rand = new Random(DateTime.Now.Millisecond);

        // enemy stuff
        EnemyGen Gen { get; set; }

        public static bool Debug = false;
        public static Texture2D Pixel;
        public static Texture2D LaserImage;
        public static Texture2D BubbleImage;
        public static Texture2D BubbleImage2;
        public static Texture2D BubbleImage3;
        public static Texture2D ComicHit1;
        public static Texture2D ComicHit2;
        public static Texture2D ComicHit3;
        public static Vector2 AdditionalVelocity;
        public static SpriteFont GUIFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            backgroundParallax = new Vector2(0,0);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            
            this.Window.Title = "Cornflower Corban - Violent Sol Team 12/2015";

            oldState = Keyboard.GetState();

            Score = 0;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ComicHit1 = Content.Load<Texture2D>("ComicPow");
            ComicHit2 = Content.Load<Texture2D>("ComicBam");
            ComicHit3 = Content.Load<Texture2D>("ComicZap");

            LaserImage = Content.Load<Texture2D>("Laser");
            BubbleImage = Content.Load<Texture2D>("Bubble");
            BubbleImage2 = Content.Load<Texture2D>("Bubble2");
            BubbleImage3 = Content.Load<Texture2D>("Bubble3");
            Player = new WhaleOfAPlayer( Content.Load<Texture2D>("Whale"));
            GUIFont = Content.Load<SpriteFont>("GUIFont");
            Background = Content.Load<Texture2D>("water");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });

            lasers = new List<Laser>();
            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            comicHits = new List<ComicHit>();
            createBubbles(100);

            Gen = new EnemyGen(GraphicsDevice,
                new SimpleBadFish(Content.Load<Texture2D>("Enemy1"), Vector2.Zero,new Vector2(-100,0),1),
                new SimpleBadFish(Content.Load<Texture2D>("shark_0000_3"), Vector2.Zero, new Vector2(-100, 0), 1));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(StartTime == null)
            {
                StartTime = gameTime.TotalGameTime;
            }

            AdditionalVelocity = new Vector2(-1000 * (Player.Position.X / graphics.PreferredBackBufferWidth), 0);

            KeyboardState newState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newState.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
            {
                //do up
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y - Player.Velocity));
            }

            if (newState.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
            {
                //do down
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y + Player.Velocity));
            }

            if (newState.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                //do left
                Player.Position = new Vector2((Player.Position.X - Player.Velocity), Player.Position.Y);
            }

            if (newState.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            {
                //do right
                Player.Position = new Vector2((Player.Position.X + Player.Velocity), Player.Position.Y);
            }

            if ((newState.IsKeyDown(Keys.F1) && !oldState.IsKeyDown(Keys.F1)) || (GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed && oldStatePad.Buttons.RightStick == ButtonState.Released))
            {
                //debug toggle
                Debug = !Debug;
            }

            if (newState.IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                //shoot
                Laser laser = Player.Shoot(gameTime);
                if(laser != null)
                {
                    lasers.Add(laser);
                }
            }

            foreach(Laser l in lasers)
            {
                l.Update(gameTime);
            }

            for (int i = 0; i < lasers.Count;i++ )
            {
                if (lasers[i].Position.X < -500)
                {
                    lasers.RemoveAt(i);
                }

                if (lasers[i].Position.X > graphics.PreferredBackBufferWidth + 500)
                {
                    lasers.RemoveAt(i);
                }
            }

            Player.Update(gameTime);
            updateBubbles(gameTime);
            oldState = newState;
            oldStatePad = GamePad.GetState(PlayerIndex.One);

            Gen.Update(gameTime);

            CollsionDetection();

            Score += Gen.CleanUp();

            currentTime = gameTime.TotalGameTime;

            foreach (ComicHit hit in comicHits)
            {
                hit.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            backgroundParallax += new Vector2(-50, -19) * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                spriteBatch.Draw(Background,Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                //spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width + (int)backgroundParallax.X, GraphicsDevice.Viewport.Height), new Color(255,255,255,.15f), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                //spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.DarkGreen);
            spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X*4, (int)backgroundParallax.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Color(.5f,.5f,5f,.25f));
            spriteBatch.End();
            
            spriteBatch.Begin();

            foreach (Bubble b in bubbles)
            {
                b.Draw(spriteBatch);
            }

            foreach (Laser l in lasers)
            {
                l.Draw(gameTime, spriteBatch);
            }

            

            Player.Draw(gameTime, spriteBatch);
            Gen.Draw(gameTime, spriteBatch);
            

            foreach (Bubble b in topBubbles)
            {
                b.Draw(spriteBatch);
            }

            foreach (ComicHit hit in comicHits)
            {
                hit.Draw(gameTime, spriteBatch);
            }

            spriteBatch.DrawString(GUIFont, "Score: " + Score, new Vector2(GraphicsDevice.Viewport.Width - 90, 10), Color.Red);
            spriteBatch.DrawString(GUIFont, "Health: " + Player.Health, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(GUIFont, currentTime.Add(-StartTime).ToString(@"mm\:ss"), new Vector2(GraphicsDevice.Viewport.Width - 700, 10), Color.Red);
            spriteBatch.DrawString(GUIFont, "Cornflower Corban - Violent Sol Team 12/2015", new Vector2(GraphicsDevice.Viewport.Width - 396, GraphicsDevice.Viewport.Height - 30), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void createBubbles(int total)
        {
            int topTotal = total;
            while(total > 0)
            {
                Texture2D image;
                double number = rand.NextDouble();

                if(number >.66)
                {
                    image = BubbleImage;
                }
                else if(number > .33)
                {
                    image = BubbleImage2;
                }
                else
                {
                    image = BubbleImage3;
                }

                

                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2*graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
                    (float)rand.NextDouble() / 3, image, new Vector2(rand.Next(-200, -100), 0), new Color(100, 100, 100, 100));
                bubbles.Add(bubble);
                total--;
            }

            while (topTotal > 0)
            {
                Texture2D image;
                double number = rand.NextDouble();

                if (number > .66)
                {
                    image = BubbleImage;
                }
                else if (number > .33)
                {
                    image = BubbleImage2;
                }
                else
                {
                    image = BubbleImage3;
                }


                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2*graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
                    (float)rand.NextDouble() / 2, image, new Vector2(rand.Next(-300,-200), 0),Color.White);
                topBubbles.Add(bubble);
                topTotal--;
            }
        }

        private void updateBubbles(GameTime gameTime)
        {
            foreach(Bubble b in bubbles)
            {
                b.Update(gameTime);

                if(b.Position.X < 0)
                {
                    b.Position = new Vector2(graphics.PreferredBackBufferWidth + rand.Next(0,graphics.PreferredBackBufferWidth),rand.Next(0,graphics.PreferredBackBufferHeight));
                }
            }

            foreach (Bubble b in topBubbles)
            {
                b.Update(gameTime);

                if (b.Position.X < 0)
                {
                    b.Position = new Vector2(graphics.PreferredBackBufferWidth + rand.Next(0, graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight));
                }
            }
        }

        private void CollsionDetection()
        {
            // player
            // projectiles
            // eninmies 

            foreach(Entity currentEn in Gen.EntityBag)
            {
                if(Player.HitBox.Intersects(currentEn.HitBox) && currentEn.Dead == false)
                {
                    comicHits.Add(new ComicHit(150,new Vector2((currentEn.Position.X + Player.Position.X)/2f,(currentEn.Position.Y + Player.Position.Y)/2f)));
                    Player.Damage(1);
                    currentEn.Damage(currentEn.Health);
                }

                foreach(Laser currentBeam in lasers)
                {
                    if(currentEn.HitBox.Intersects(currentBeam.HitBox))
                    {
                        comicHits.Add(new ComicHit(50, new Vector2((currentEn.Position.X + currentBeam.Position.X) / 2f, (currentEn.Position.Y + currentBeam.Position.Y) / 2f)));
                         currentEn.Damage(currentBeam.DamageValue);
                         currentBeam.Damage(1);
                         if (currentBeam.PlayerShoot) Score += 1;
                    }
                }
            }

            // clean up lasers might need to move
            for(int laserIndex = 0; laserIndex < lasers.Count; laserIndex++)
            {
                if (lasers[laserIndex].Dead) lasers.RemoveAt(laserIndex);
            }

        }
    }
}
