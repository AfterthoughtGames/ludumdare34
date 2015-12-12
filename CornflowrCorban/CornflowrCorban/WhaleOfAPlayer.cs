using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban 
{
    public class WhaleOfAPlayer : Entity
    {
        private float normalScale = .25f;
        int LineThickness = 3;
        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        int shootingDelay = 250;
        DateTime lastShot = DateTime.Now;


            

        public WhaleOfAPlayer(Texture2D image)
        {
            Image = image;
            this.Scale = 1;
            this.Position = new Microsoft.Xna.Framework.Vector2(100, 100);
            this.HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
        }

        public Laser Shoot(GameTime gameTime)
        {
            if (lastShot.AddMilliseconds(250) < DateTime.Now)
            {
                lastShot = DateTime.Now;
                Scale += .04f;
                return new Laser(Position + new Vector2(200,-75)*Scale, Scale, Game1.LaserImage, new Vector2(2000, 0));
                
            }
            else
            {
                return null;
            }
        }

        public void Update(GameTime gameTime)
        {
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);

            updateScale(gameTime);
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

        private void updateScale(GameTime gameTime)
        {
            //TODO: this needs to be tied to shooting
            //shrink him
            if(Scale > normalScale)
            {
                Scale -= .1f * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
        }
    }
}
