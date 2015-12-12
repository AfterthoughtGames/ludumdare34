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

        List<Laser> lasers;
        List<Bubble> bubbles;
        List<Bubble> topBubbles;
        

        Random rand = new Random(DateTime.Now.Millisecond);

        // enemy stuff
        EnemyGen Gen { get; set; }

        public static bool Debug = true;
        public static Texture2D Pixel;
        public static Texture2D LaserImage;
        public static Texture2D BubbleImage;
        public static Vector2 AdditionalVelocity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            
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

            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LaserImage = Content.Load<Texture2D>("Laser");
            BubbleImage = Content.Load<Texture2D>("Bubble");
            Player = new WhaleOfAPlayer( Content.Load<Texture2D>("Whale"));
            Background = RandomStaticStuff.GenerateBubuleField(GraphicsDevice, Content.Load<Texture2D>("Bubble"));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });

            lasers = new List<Laser>();
            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            createBubbles(100);

            Gen = new EnemyGen(GraphicsDevice,
                new SimpleBadFish(Content.Load<Texture2D>("Enemy1"), Vector2.Zero));

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
            AdditionalVelocity = new Vector2(-1000 * (Player.Position.X / graphics.PreferredBackBufferWidth), 0);

            KeyboardState newState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newState.IsKeyDown(Keys.W))
            {
                //do up
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y - Player.Velocity));
            }

            if (newState.IsKeyDown(Keys.S))
            {
                //do down
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y + Player.Velocity));
            }

            if (newState.IsKeyDown(Keys.A))
            {
                //do left
                Player.Position = new Vector2((Player.Position.X - Player.Velocity), Player.Position.Y);
            }

            if (newState.IsKeyDown(Keys.D))
            {
                //do right
                Player.Position = new Vector2((Player.Position.X + Player.Velocity), Player.Position.Y);
            }

            if (newState.IsKeyDown(Keys.F1) && !oldState.IsKeyDown(Keys.F1))
            {
                //debug toggle
                Debug = !Debug;
            }

            if (newState.IsKeyDown(Keys.Space))
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

            Gen.Update(gameTime);

            CollsionDetection();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            
            spriteBatch.Draw(Background, new Vector2(10, 10), Color.White);

            foreach (Bubble b in bubbles)
            {
                b.Draw(spriteBatch);
            }

            foreach (Laser l in lasers)
            {
                l.Draw(spriteBatch);
            }

            

            Player.Draw(gameTime, spriteBatch);
            Gen.Draw(gameTime, spriteBatch);
            

            foreach (Bubble b in topBubbles)
            {
                b.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void createBubbles(int total)
        {
            int topTotal = total;
            while(total > 0)
            {
                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2*graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
                    (float)rand.NextDouble()/2, BubbleImage, new Vector2(-300, 0),new Color(100,100,100,100));
                bubbles.Add(bubble);
                total--;
            }

            while (topTotal > 0)
            {
                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2*graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
                    (float)rand.NextDouble() / 2, BubbleImage, new Vector2(-300, 0),Color.White);
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
                    Player.Damage(1);
                    currentEn.Damage(currentEn.Health);
                }

                foreach(Laser currentBeam in lasers)
                {
                    if(currentEn.HitBox.Intersects(currentBeam.HitBox))
                    {
                         currentEn.Damage(currentBeam.DamageValue);
                        currentBeam.Damage(1);
                    }
                }
            }

        }
    }
}
