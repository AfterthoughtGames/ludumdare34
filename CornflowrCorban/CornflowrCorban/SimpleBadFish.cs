using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class SimpleBadFish : Entity
    {
        public Vector2 Velocity;

        List<Texture2D> frames;
        int frameDelay = 0;
        int currentFrame = 0;
        DateTime nextFrameFlip = DateTime.Now;

        public SimpleBadFish(Texture2D image, Vector2 startPOS, Vector2 velocity, float scale) : base()
        {
            Image = image;
            Scale = scale;
            Position = startPOS;
            Health = 2;
            PointValue = 2;
            Velocity = velocity;

            frames = new List<Texture2D>();
            frames.Add(image);
        }

        public SimpleBadFish(List<Texture2D> images, Vector2 startPOS, Vector2 velocity, float scale, int frameDelay)
            : base()
        {
            //Image = image;
            Scale = scale;
            Position = startPOS;
            Health = 2;
            PointValue = 2;
            Velocity = velocity;

            frames = images;
            this.frameDelay = frameDelay;
        }



        public override void Update(GameTime gameTime)
        {

            
            //flip a frame?
            if(nextFrameFlip < DateTime.Now)
            {
                nextFrameFlip = DateTime.Now.AddMilliseconds(frameDelay);
                currentFrame++;
                if(currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
            }

            Image = frames[currentFrame];

            if (Image.Name.Contains("jellyfish"))
            {
                HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 4 * Scale),
                        (int)Position.Y - (int)(Image.Height / 2.5f * Scale), (int)(Image.Width / 3 * Scale), (int)(Image.Height / 1.2 * Scale));

            }
            else
            {
                HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 2 * Scale),
                       (int)Position.Y - (int)(Image.Height / 4.5f * Scale), (int)(Image.Width / 1.5 * Scale), (int)(Image.Height / 4 * Scale));
            }

            Position += Velocity + Game1.AdditionalVelocity/200;
            /*
            if(Position.X < -100)
            {
                Dead = true;
            }
            */
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            
            base.Draw(gameTime, batch);
        }

        public SimpleBadFish Clone()
        {
            return new SimpleBadFish(this.frames, this.Position, this.Velocity, this.Scale, this.frameDelay);
        }

        public SimpleBadFish Clone(Vector2 pos, Vector2 velocity, float scale)
        {
            SimpleBadFish tempFish = new SimpleBadFish(this.frames, this.Position, velocity, scale, this.frameDelay);
            tempFish.Position = pos;
            return tempFish;
        }
    }
}
