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
        public int PointValue { get; set; }
        public Vector2 Velocity { get; set; }

        public Rectangle HitBox { get; set; }

        public List<Point> AIPath { get; set; }

        public bool QuietDeath { get; set; }

        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        int LineThickness = 3;
        float rotation = 0;
        Color color = Color.White;

        public Entity()
        {
            Dead = false;
            QuietDeath = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if(Velocity != null)
            {
                Position += Velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }

            HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 2 * Scale),
                    (int)Position.Y - (int)(Image.Height / 2 * Scale), (int)(Image.Width * Scale), (int)(Image.Height * Scale));

            if(Image.Name.Contains("ink"))
            {
                rotation += 2 * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
                //Scale += 2 * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
                //color = new Color(Color.White, 1-3 * (gameTime.ElapsedGameTime.Milliseconds / 1000f) / 3f);
            }
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
                batch.Draw(Game1.Pixel, new Rectangle(HitBox.Left - LineThickness, HitBox.Y, LineThickness, HitBox.Height),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255));//This is the line on the Left

                batch.Draw(Game1.Pixel, new Rectangle(HitBox.Right, HitBox.Y, LineThickness, HitBox.Height),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Right

                batch.Draw(Game1.Pixel, new Rectangle(HitBox.X, HitBox.Top - LineThickness, HitBox.Width, LineThickness),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Top

                batch.Draw(Game1.Pixel, new Rectangle(HitBox.X, HitBox.Bottom, HitBox.Width, LineThickness),
                    new Color((byte)RedValue, (byte)GreenValue, (byte)BlueValue, (byte)255)); //This is the line on the Bottom

            }

            if(Dead == false)
            batch.Draw(Image, Position, null, color, rotation, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }

        public void DieQuietly()
        {
            //ssshhhhhhhhhhhhhhhhhh
            QuietDeath = true;
            Dead = true;
        }
    }
}
