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

        public Title(Texture2D image, SpriteFont gui)
        {
            titleImage = image;
            guiText = gui;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                Game1.StartNewGame = true;
                Game1.InMenu = false;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game1.ExitGame = true;//Exit();
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(titleImage, Vector2.Zero, Color.White);
            sb.DrawString(guiText, "Previous High Score: " + ScoreSystem.ReadScore().ToString(), new Vector2(sb.GraphicsDevice.Viewport.Width - 214, 10), Color.Black);
            sb.DrawString(guiText, "Previous Score: " + Game1.Score.ToString(), new Vector2(sb.GraphicsDevice.Viewport.Width - 170, 25), Color.Black);
            sb.DrawString(guiText, "Press Enter or Start Button to Play", new Vector2((sb.GraphicsDevice.Viewport.Width/ 2) - 100, sb.GraphicsDevice.Viewport.Height -250), Color.Red);
            sb.DrawString(guiText, "Press Esc or Back Button to Exit", new Vector2((sb.GraphicsDevice.Viewport.Width / 2) - 88, sb.GraphicsDevice.Viewport.Height - 224), Color.Red);
        }
    }
}
