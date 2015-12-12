using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Laser : Entity
    {
        private Vector2 velocity;
        int LineThickness = 3;
        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        public int DamageValue = 1;

        public Laser(Vector2 position, float scale, Texture2D image,Vector2 velocity)
        {
            Image = image;
            Scale = scale;
            Position = position;

            this.velocity = velocity;
            Health = 1;
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            if (Game1.Debug)
            {
                Rectangle ObjectRect = new Rectangle((int)Position.X - (int)(Image.Width / 2 * Scale),
                    (int)Position.Y - (int)(Image.Height / 2 * Scale), (int)(Image.Width * Scale), (int)(Image.Height * Scale));

                batch.Draw(Game1.Pixel, new Rectangle(ObjectRect.Left - LineThickness, ObjectRect.Y, LineThickness, ObjectRect.Height),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255));//This is the line on the Left

                batch.Draw(Game1.Pixel, new Rectangle(ObjectRect.Right, ObjectRect.Y, LineThickness, ObjectRect.Height),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Right

                batch.Draw(Game1.Pixel, new Rectangle(ObjectRect.X, ObjectRect.Top - LineThickness, ObjectRect.Width, LineThickness),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Top

                batch.Draw(Game1.Pixel, new Rectangle(ObjectRect.X, ObjectRect.Bottom, ObjectRect.Width, LineThickness),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Bottom

            }

            batch.Draw(Image, Position, null, Color.White, 0, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
