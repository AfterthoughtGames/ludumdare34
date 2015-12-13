using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

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

        public List<Laser> lasers;
        List<Bubble> bubbles;
        List<Bubble> topBubbles;
        List<ComicHit> comicHits;

        Title TitleScreen;

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
        public static Texture2D LaserImage2;
        public static Texture2D BubbleImage;
        public static Texture2D BubbleImage2;
        public static Texture2D BubbleImage3;
        public static Texture2D ComicHit1;
        public static Texture2D ComicHit2;
        public static Texture2D ComicHit3;

        public static Texture2D ComicKrill;
        public static Vector2 AdditionalVelocity;
        public static SpriteFont GUIFont;


        public static bool InMenu = true;
        public static bool StartNewGame = false;
        public static bool ExitGame = false;

        public static Texture2D Shark1;
        public static Texture2D Shark2;
        public static Texture2D Shark3;

        public static Texture2D LaserShark1;
        public static Texture2D LaserShark2;
        public static Texture2D LaserShark3;

        public static Texture2D Whale1;
        public static Texture2D Whale2;
        public static Texture2D Whale3;

        public static Texture2D Jelly1;
        public static Texture2D Jelly2;
        public static Texture2D Jelly3;
        public static Texture2D Jelly4;
        public static Texture2D Jelly5;
        public static Texture2D Jelly6;

        public static Texture2D Fish;

        public static SoundEffect LaserSound;
        public static SoundEffect BloopSound;

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
            
            this.Window.Title = "Cornflower Corban - Violent Sol Team 12/2015 - " + GraphicsDevice.Viewport.Width + " x " + GraphicsDevice.Viewport.Height;

            oldState = Keyboard.GetState();

            Score = 0;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Fish = Content.Load<Texture2D>("FishSchool");

            LaserSound = Content.Load<SoundEffect>("wahwahlaser");
            BloopSound = Content.Load<SoundEffect>("bloopbloop");

            Jelly1 = Content.Load<Texture2D>("jellyfishAnimation_0000_tentacles-6");
            Jelly2 = Content.Load<Texture2D>("jellyfishAnimation_0001_tentacles-5");
            Jelly3 = Content.Load<Texture2D>("jellyfishAnimation_0002_tentacles-4");
            Jelly4 = Content.Load<Texture2D>("jellyfishAnimation_0003_tentacles-3");
            Jelly5 = Content.Load<Texture2D>("jellyfishAnimation_0004_tentacles-2");
            Jelly6 = Content.Load<Texture2D>("jellyfishAnimation_0005_tentacles-1");

            Shark1 = Content.Load<Texture2D>("shark_0000_3");
            Shark2 = Content.Load<Texture2D>("shark_0001_2");
            Shark3 = Content.Load<Texture2D>("shark_0002_1");

            LaserShark3 = Content.Load<Texture2D>("laserShark3");
            LaserShark2 = Content.Load<Texture2D>("laserShark2");
            LaserShark1 = Content.Load<Texture2D>("laserShark1");

            Whale3 = Content.Load<Texture2D>("Whale3");
            Whale2 = Content.Load<Texture2D>("Whale2");
            Whale1 = Content.Load<Texture2D>("Whale");

            ComicHit1 = Content.Load<Texture2D>("ComicPow");
            ComicHit2 = Content.Load<Texture2D>("ComicBam");
            ComicHit3 = Content.Load<Texture2D>("ComicZap");

            ComicKrill = Content.Load<Texture2D>("ComicKrill");

            LaserImage = Content.Load<Texture2D>("Laser");
            LaserImage2 = Content.Load<Texture2D>("Laser2");
            BubbleImage = Content.Load<Texture2D>("Bubble");
            BubbleImage2 = Content.Load<Texture2D>("Bubble2");
            BubbleImage3 = Content.Load<Texture2D>("Bubble3");
            GUIFont = Content.Load<SpriteFont>("GUIFont");
            Background = Content.Load<Texture2D>("water");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });

            TitleScreen = new Title(Content.Load<Texture2D>("Title"), GUIFont);
            lasers = new List<Laser>();
            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            comicHits = new List<ComicHit>();
            createBubbles(100);


            topBubbles = new List<Bubble>();
            bubbles = new List<Bubble>();
            lasers = new List<Laser>();

       


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
            if (ExitGame) Exit();

            if (InMenu)
            {
                TitleScreen.Update(gameTime);
            }
            else if(Gen != null)
            {
                if (StartTime == null)
                {
                    StartTime = gameTime.TotalGameTime;
                }

                AdditionalVelocity = new Vector2(-1000 * (Player.Position.X / graphics.PreferredBackBufferWidth), 0);

                KeyboardState newState = Keyboard.GetState();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    InMenu = true;//Exit();

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
                    if (laser != null)
                    {
                        lasers.Add(laser);
                        LaserSound.Play(0.1f, 0f, 0f);
                    }
                }

                foreach (Laser l in lasers)
                {
                    l.Update(gameTime);
                }

                for (int i = 0; i < lasers.Count; i++)
                {
                    if (lasers[i].Position.X < -500)
                    {
                        lasers.RemoveAt(i);
                    }

                    if (lasers.Count > i && lasers[i].Position.X > graphics.PreferredBackBufferWidth + 500)
                    {
                        lasers.RemoveAt(i);
                    }
                }

                if (Player != null) Player.Update(gameTime);
                updateBubbles(gameTime);
                oldState = newState;
                oldStatePad = GamePad.GetState(PlayerIndex.One);

                Gen.Update(gameTime);

                CollsionDetection();

                Score += Gen.CleanUp();

                currentTime = gameTime.TotalGameTime;

                if (Player.Dead) //bloopbloop
                {
                    BloopSound.Play();
                    SaveScore();
                    InMenu = true;
                }

                foreach (ComicHit hit in comicHits)
                {
                    hit.Update(gameTime);
                }

                foreach (Entity e in Gen.EventEntities)
                {
                    e.Update(gameTime);
                }
            }
            else
            {
                // Start a new game
                if (StartNewGame)
                {
                    NewGame(gameTime);
                }
            }


            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (InMenu)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
                TitleScreen.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
            else if (Gen != null)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                backgroundParallax += new Vector2(-50, -19) * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                //spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width + (int)backgroundParallax.X, GraphicsDevice.Viewport.Height), new Color(255,255,255,.15f), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                //spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.DarkGreen);
                spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X * 4, (int)backgroundParallax.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Color(.5f, .5f, 5f, .25f));
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

                if (Player != null) Player.Draw(gameTime, spriteBatch);
                if (Player != null) Gen.Draw(gameTime, spriteBatch);

                foreach (Entity e in Gen.EventEntities)
                {
                    e.Draw(gameTime, spriteBatch);
                }

                foreach (Bubble b in topBubbles)
                {
                    b.Draw(spriteBatch);
                }

                foreach (ComicHit hit in comicHits)
                {
                    hit.Draw(gameTime, spriteBatch);
                }

                spriteBatch.DrawString(GUIFont, "Score: " + Score, new Vector2(GraphicsDevice.Viewport.Width - 90, 10), Color.Red);
                if(Player != null) spriteBatch.DrawString(GUIFont, "Health: " + Player.Health, new Vector2(10, 10), Color.Red);
                spriteBatch.DrawString(GUIFont, currentTime.Add(-StartTime).ToString(@"mm\:ss"), new Vector2(GraphicsDevice.Viewport.Width - 700, 10), Color.Red);
                spriteBatch.DrawString(GUIFont, "Cornflower Corban - Violent Sol Team 12/2015", new Vector2(GraphicsDevice.Viewport.Width - 396, GraphicsDevice.Viewport.Height - 30), Color.Red);

                spriteBatch.DrawString(GUIFont, "Score: " + Score, new Vector2(GraphicsDevice.Viewport.Width - 90, 10), Color.Red);
                if(Player != null) spriteBatch.DrawString(GUIFont, "Health: " + Player.Health, new Vector2(10, 10), Color.Red);
                spriteBatch.DrawString(GUIFont, currentTime.Add(-StartTime).ToString(@"mm\:ss"), new Vector2(GraphicsDevice.Viewport.Width - 700, 10), Color.Red);
                spriteBatch.DrawString(GUIFont, "Cornflower Corban - Violent Sol Team 12/2015", new Vector2(GraphicsDevice.Viewport.Width - 396, GraphicsDevice.Viewport.Height - 30), Color.Red);

                spriteBatch.End();
            }

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
                        if (currentBeam.PlayerShoot)
                        {
                            comicHits.Add(new ComicHit(50, new Vector2((currentEn.Position.X + currentBeam.Position.X) / 2f, (currentEn.Position.Y + currentBeam.Position.Y) / 2f)));
                            currentEn.Damage(currentBeam.DamageValue);
                            currentBeam.Damage(1);
                            Score += 1;
                        }
                        
                    }
                }
            }

            foreach(Laser l in lasers)
            {
                //shark shot it
                if(l.PlayerShoot == false && Player.HitBox.Intersects(l.HitBox))
                {
                    comicHits.Add(new ComicHit(150, new Vector2((l.Position.X + Player.Position.X) / 2f, (l.Position.Y + Player.Position.Y) / 2f)));
                    Player.Damage(2);
                    l.Damage(l.Health);
                }
            }

            foreach (Pickup p in Gen.Pickups)
            {
                //shark shot it
                if (Player.HitBox.Intersects(p.HitBox))
                {
                    comicHits.Add(new ComicHit(150, new Vector2((p.Position.X + Player.Position.X) / 2f, (p.Position.Y + Player.Position.Y) / 2f),ComicKrill));
                    Score += p.Score;
                    p.Damage(p.Health);
                }
            }

            // clean up lasers might need to move
            for(int laserIndex = 0; laserIndex < lasers.Count; laserIndex++)
            {
                if (lasers[laserIndex].Dead) lasers.RemoveAt(laserIndex);
            }

        }

        private void NewGame(GameTime gameTime)
        {
            if(Player != null)
            {
                Player.Dead = false;
                Player.Health = 10;
            }
            Score = 0;
            List<Texture2D> whaleFrames = new List<Texture2D>();
            whaleFrames.Add(Whale1);
            whaleFrames.Add(Whale2);
            whaleFrames.Add(Whale3);
            whaleFrames.Add(Whale2);
            Player = new WhaleOfAPlayer(whaleFrames);

            lasers = new List<Laser>();
            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            createBubbles(100);

            updateBubbles(gameTime);

            List<Texture2D> jellyFrames = new List<Texture2D>();
            jellyFrames.Add(Jelly1);
            jellyFrames.Add(Jelly2);
            jellyFrames.Add(Jelly3);
            jellyFrames.Add(Jelly4);
            jellyFrames.Add(Jelly5);
            jellyFrames.Add(Jelly6);

            List<Texture2D> sharkFrames = new List<Texture2D>();
            sharkFrames.Add(Shark1);
            sharkFrames.Add(Shark2);
            sharkFrames.Add(Shark3);

            List<Texture2D> laserSharkFrames = new List<Texture2D>();
            laserSharkFrames.Add(LaserShark1);
            laserSharkFrames.Add(LaserShark2);
            laserSharkFrames.Add(LaserShark3);

            Gen = new EnemyGen(GraphicsDevice,
                new SimpleBadFish(jellyFrames, Vector2.Zero, new Vector2(-100, 0), 1, 250,this),
                new SimpleBadFish(sharkFrames, Vector2.Zero, new Vector2(-100, 0), 1,250,this),
                new SimpleBadFish(laserSharkFrames, Vector2.Zero, new Vector2(-100, 0), 1,250,this),
                new Pickup(Vector2.Zero,1,Content.Load<Texture2D>("Krill"),new Vector2(-500,0),5));

            StartNewGame = false;
        }

        private void SaveScore()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\score.txt")) File.Delete(Directory.GetCurrentDirectory() + "\\score.txt");
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\score.txt", Score.ToString(), System.Text.Encoding.ASCII);
        }

        public static int ReadScore()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\score.txt"))
            {
                String line = string.Empty;

                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\score.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }

                return Convert.ToInt32(line);
            }

            return 0;
        }
    }
}
