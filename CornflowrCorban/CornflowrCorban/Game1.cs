using Microsoft.Xna.Framework;
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

        KeyboardState oldState;

        static Texture2D Pixel;

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

            if (newState.IsKeyDown(Keys.W))
            {
                //do up
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y - 15));
            }

            if (newState.IsKeyDown(Keys.S))
            {
                //do down
                Player.Position = new Vector2(Player.Position.X, (Player.Position.Y + 15));
            }

            if (newState.IsKeyDown(Keys.A))
            {
                //do left
                Player.Position = new Vector2((Player.Position.X - 15), Player.Position.Y);
            }

            if (newState.IsKeyDown(Keys.D))
            {
                //do right
                Player.Position = new Vector2((Player.Position.X + 15), Player.Position.Y);
            }
            Player.Update(gameTime);
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
            int LineThickness = 3;
            int RedValue = 255;
            int BlueValue = 0;
            int GreenValue = 0;


            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Begin();

            Rectangle ObjectRect = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, Player.Image.Width, Player.Image.Height);

            spriteBatch.Draw(Pixel, new Rectangle(ObjectRect.Left - LineThickness, ObjectRect.Y, LineThickness, ObjectRect.Height),
                new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255));//This is the line on the Left

            spriteBatch.Draw(Pixel, new Rectangle(ObjectRect.Right, ObjectRect.Y, LineThickness, ObjectRect.Height),
                new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Right

            spriteBatch.Draw(Pixel, new Rectangle(ObjectRect.X, ObjectRect.Top - LineThickness, ObjectRect.Width, LineThickness),
                new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Top

            spriteBatch.Draw(Pixel, new Rectangle(ObjectRect.X, ObjectRect.Bottom, ObjectRect.Width, LineThickness),
                new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Bottom



            
            spriteBatch.Draw(Player.Image, Player.Position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
