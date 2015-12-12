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
        public SimpleBadFish(Texture2D image, Vector2 startPOS)
        {
            Image = image;
            Scale = 1;
            Position = startPOS;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Position = new Vector2(Position.X - 1, Position.Y);
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
