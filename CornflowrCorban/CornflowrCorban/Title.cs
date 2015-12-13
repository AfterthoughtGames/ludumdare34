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

        public Title(Texture2D image)
        {
            titleImage = image;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                Game1.StartNewGame = true;
                Game1.InMenu = false;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(titleImage, Vector2.Zero, Color.White);
        }
    }
}
