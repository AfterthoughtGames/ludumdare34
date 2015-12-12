using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Bubble : Entity
    {
        public Vector2 Velocity;

        int LineThickness = 3;
        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        Color color;

        public Bubble(Vector2 position, float scale, Texture2D image, Vector2 velocity, Color color)
        {
            Image = image;
            Scale = scale;
            Position = position;

            Velocity = velocity;
            this.color = color;
        }

        public void Update(GameTime gametime)
        {
            Position += Velocity * (gametime.ElapsedGameTime.Milliseconds / 1000f);
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

            batch.Draw(Image, Position, null, color, 0, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
