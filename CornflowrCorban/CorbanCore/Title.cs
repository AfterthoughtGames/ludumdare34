using CorbanCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Title
    {
        Texture2D titleImage { get; set; }
        SpriteFont guiText { get; set; }
        Random rand = new Random(DateTime.Now.Millisecond);

        public List<Laser> lasers;
        List<Bubble> bubbles;
        List<Bubble> topBubbles;
        EnemyGen Gen;
        private Vector2 backgroundParallax;

        public Title(Texture2D image, SpriteFont gui, GraphicsDeviceManager gd)
        {
            titleImage = image;
            guiText = gui;

            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            createBubbles(100, gd);

            
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager gd)
        {
            TitleGame(gameTime, gd);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                Game1.StartNewGame = true;
                Game1.InMenu = false;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game1.ExitGame = true;//Exit();

            updateBubbles(gameTime, gd);
            //Gen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //SpriteBatch spriteBatch = sb;

            ////GraphicsDevice.Clear(Color.CornflowerBlue);
            //backgroundParallax += new Vector2(-50, -19) * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            
            //spriteBatch.Draw(Game1.Background, Vector2.Zero, new Rectangle(0, 0, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            ////spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width + (int)backgroundParallax.X, GraphicsDevice.Viewport.Height), new Color(255,255,255,.15f), 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            //spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            ////spriteBatch.Draw(Background, Vector2.Zero, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(Game1.Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X, 0, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), Color.DarkGreen);
            //spriteBatch.Draw(Game1.Background, Vector2.Zero, new Rectangle(-(int)backgroundParallax.X * 4, (int)backgroundParallax.Y, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), new Color(.5f, .5f, 5f, .25f));
            //spriteBatch.End();

            //spriteBatch.Begin();

            //foreach (Bubble b in bubbles)
            //{
            //    b.Draw(spriteBatch);
            //}

            ////foreach (Entity e in Gen.EventEntities)
            ////{
            ////    e.Draw(gameTime, spriteBatch);
            ////}

            //foreach (Bubble b in topBubbles)
            //{
            //    b.Draw(spriteBatch);
            //}

            sb.Draw(titleImage, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.87f, SpriteEffects.None, 0f);
            //sb.Draw(Game1.Whale1, new Vector2((sb.GraphicsDevice.Viewport.Width / 2) - 400,(sb.GraphicsDevice.Viewport.Height / 2)- 200 ), Color.White);
            sb.DrawString(guiText, "Previous High Score: " + ScoreSystem.ReadScore().ToString(), new Vector2(sb.GraphicsDevice.Viewport.Width - 214, 10), Color.Green);
            sb.DrawString(guiText, "Previous Score: " + Game1.Score.ToString(), new Vector2(sb.GraphicsDevice.Viewport.Width - 170, 28), Color.Green);
            sb.DrawString(guiText, "Press Enter or Start Button to Play", new Vector2((sb.GraphicsDevice.Viewport.Width / 2) - 100, sb.GraphicsDevice.Viewport.Height - 270), Color.White);
            sb.DrawString(guiText, "Press Esc or Back Button to Exit", new Vector2((sb.GraphicsDevice.Viewport.Width / 2) - 88, sb.GraphicsDevice.Viewport.Height - 244), Color.White);

            //spriteBatch.End();
        }

        private void TitleGame(GameTime gameTime, GraphicsDeviceManager gd)
        {
           
            List<Texture2D> whaleFrames = new List<Texture2D>();
            whaleFrames.Add(Game1.Whale1);
            whaleFrames.Add(Game1.Whale2);
            whaleFrames.Add(Game1.Whale3);
            whaleFrames.Add(Game1.Whale2);

            lasers = new List<Laser>();
            bubbles = new List<Bubble>();
            topBubbles = new List<Bubble>();
            createBubbles(100, gd);

            updateBubbles(gameTime, gd);

            

            //Gen = new EnemyGen(gd.GraphicsDevice,
            //    new SimpleBadFish(jellyFrames, Vector2.Zero, new Vector2(-100, 0), 1f, 250, 1, this),
            //    new SimpleBadFish(sharkFrames, Vector2.Zero, new Vector2(-100, 0), 1, 250, 2, this),
            //    new SimpleBadFish(laserSharkFrames, Vector2.Zero, new Vector2(-100, 0), 1, 250, 4, this),
            //    new Pickup(Vector2.Zero, 1, Content.Load<Texture2D>("Krill"), new Vector2(-500, 0), 5));

           
        }

        private void createBubbles(int total, GraphicsDeviceManager graphics)
        {
            int topTotal = total;
            while (total > 0)
            {
                Texture2D image;
                double number = rand.NextDouble();

                if (number > .66)
                {
                    image = Game1.BubbleImage;
                }
                else if (number > .33)
                {
                    image = Game1.BubbleImage2;
                }
                else
                {
                    image = Game1.BubbleImage3;
                }



                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2 * graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
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
                    image = Game1.BubbleImage;
                }
                else if (number > .33)
                {
                    image = Game1.BubbleImage2;
                }
                else
                {
                    image = Game1.BubbleImage3;
                }


                Bubble bubble = new Bubble(new Vector2(-graphics.PreferredBackBufferWidth + rand.Next(0, 2 * graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)),
                    (float)rand.NextDouble() / 2, image, new Vector2(rand.Next(-300, -200), 0), Color.White);
                topBubbles.Add(bubble);
                topTotal--;
            }
        }

        private void updateBubbles(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (Bubble b in bubbles)
            {
                b.Update(gameTime);

                if (b.Position.X < 0)
                {
                    b.Position = new Vector2(graphics.PreferredBackBufferWidth + rand.Next(0, graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight));
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
    }
}
