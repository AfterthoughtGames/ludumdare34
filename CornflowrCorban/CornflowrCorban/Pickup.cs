using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Pickup : Entity
    {
        private Vector2 velocity;
        int LineThickness = 3;
        int RedValue = 255;
        int BlueValue = 0;
        int GreenValue = 0;
        public int Score { get; set; }

        public Pickup(Vector2 position, float scale, Texture2D image, Vector2 velocity, int score)
        {
            Image = image;
            Scale = scale;
            Position = position;

            this.velocity = velocity;
            Score = score;
            Health = 1;
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);

            base.Update(gameTime);
        }

        public Pickup Clone(Vector2 position, float scale, Texture2D image, Vector2 velocity)
        {
            Pickup tempPickup = new Pickup(position, scale, image, velocity, this.Score);
            return tempPickup;
        }
    }
}
