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

        public SimpleBadFish(Texture2D image, Vector2 startPOS, Vector2 velocity, float scale) : base()
        {
            Image = image;
            Scale = scale;
            Position = startPOS;
            Health = 2;
            PointValue = 2;
            Velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 4 * Scale),
                    (int)Position.Y - (int)(Image.Height /2.5f * Scale), (int)(Image.Width /3* Scale), (int)(Image.Height/1.2 * Scale));

            Position += Velocity + Game1.AdditionalVelocity/200;

            if(Position.X < -100)
            {
                Dead = true;
            }
        }

        public SimpleBadFish Clone()
        {
            return new SimpleBadFish(this.Image, this.Position, this.Velocity, this.Scale);
        }

        public SimpleBadFish Clone(Vector2 pos, Vector2 velocity, float scale)
        {
            SimpleBadFish tempFish = new SimpleBadFish(this.Image, this.Position, velocity, scale);
            tempFish.Position = pos;
            return tempFish;
        }
    }
}
