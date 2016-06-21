using CorbanCore;
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
        int shootingDelay = 250;
        DateTime nextFrameFlip = DateTime.Now;
        DateTime lastShot = DateTime.Now;
        List<Texture2D> frames;
        int frameDelay = 250;
        int currentFrame = 0;


            

        public WhaleOfAPlayer(List<Texture2D> images) : base()
        {
            Image = images[0];
            this.Scale = 0.25f;
            this.Position = new Microsoft.Xna.Framework.Vector2(100, 100);
            this.HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            Health = 20;
            frames = images;
        }

        private void updatePosition()
        {
            float y = Position.Y;
            float x = Position.X;

            if (y < 0)
            {
                y = 0;
            }

            if (y > Game1.graphics.PreferredBackBufferHeight)
            {
                y = Game1.graphics.PreferredBackBufferHeight;
            }

            if(x<0)
            {
                x = 0;
            }

            if (x > Game1.graphics.PreferredBackBufferWidth)
            {
                x = Game1.graphics.PreferredBackBufferWidth;
            }

            Position = new Vector2(x, y);
        }

        public float Velocity
        { 
            get 
            {
                float speed = 7 * (2 - Scale);
                if(speed < 0)
                {
                    speed = 0;
                }

                if(speed > 7)
                {
                    speed = 7;
                }
                return speed; 
            } 
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

        public override void Update(GameTime gameTime)
        {
            //flip a frame?
            if (nextFrameFlip < DateTime.Now)
            {
                nextFrameFlip = DateTime.Now.AddMilliseconds(frameDelay);
                currentFrame++;
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
            }

            Image = frames[currentFrame];

            updateScale(gameTime);
            updatePosition();

            HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 2.5 * Scale),
                    (int)Position.Y - (int)(Image.Height / 6f * Scale), (int)(Image.Width /1.2 * Scale), (int)(Image.Height / 3 * Scale));
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
