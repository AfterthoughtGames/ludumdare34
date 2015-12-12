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
    }
}
