using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Entity
    {
        public bool Dead { get; set; }
        public Texture2D Image { get; set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public int Health { get; set; }

        public Rectangle HitBox { get; set; }

        public List<Point> AIPath { get; set; }

        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        int LineThickness = 3;

        public Entity()
        {
            Dead = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 2 * Scale),
                    (int)Position.Y - (int)(Image.Height / 2 * Scale), (int)(Image.Width * Scale), (int)(Image.Height * Scale));
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if(Health <= 0)
            {
                Dead = true;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
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

            if(Dead == false)
            batch.Draw(Image, Position, null, Color.White, 0, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
