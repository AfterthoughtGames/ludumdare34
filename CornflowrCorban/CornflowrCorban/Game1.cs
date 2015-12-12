﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CornflowrCorban
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WhaleOfAPlayer Player { get; set; }

        Texture2D Background { get; set; }

        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            Player = new WhaleOfAPlayer( Content.Load<Texture2D>("Whale"));
            Background = RandomStaticStuff.GenerateBubuleField(GraphicsDevice, Content.Load<Texture2D>("Bubble"));
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            KeyboardState newState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newState.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W))
            {
                //do up
            }

            if (newState.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
            {
                //do down
            }

            if (newState.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A))
            {
                //do left
            }

            if (newState.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D))
            {
                //do right
            }

            oldState = newState;
            // TODO: Add your update logic here

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

            spriteBatch.Draw(Player.Image, Player.HitBox, Color.White);

            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
