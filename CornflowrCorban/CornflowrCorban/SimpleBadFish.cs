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
        public SimpleBadFish(Texture2D image, Vector2 startPOS) : base()
        {
            Image = image;
            Scale = 1;
            Position = startPOS;
            Health = 2;
            PointValue = 2;
        }

        public override void Update(GameTime gameTime)
        {
            HitBox = new Rectangle((int)Position.X - (int)(Image.Width / 4 * Scale),
                    (int)Position.Y - (int)(Image.Height /2.5f * Scale), (int)(Image.Width /3* Scale), (int)(Image.Height/1.2 * Scale));

            Position = new Vector2(Position.X - 10, Position.Y);
        }

        public SimpleBadFish Clone()
        {
            return new SimpleBadFish(this.Image, this.Position);
        }

        public SimpleBadFish Clone(Vector2 pos)
        {
            SimpleBadFish tempFish = new SimpleBadFish(this.Image, this.Position);
            tempFish.Position = pos;
            return tempFish;
        }
    }
}
