using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Bubble : Entity
    {
        public Vector2 Velocity;
        Color color;

        public Bubble(Vector2 position, float scale, Texture2D image, Vector2 velocity, Color color)
        {
            Image = image;
            Scale = scale;
            Position = position;

            Velocity = velocity;
            this.color = color;
        }

        public override void Update(GameTime gametime)
        {
            Position += (Velocity + Game1.AdditionalVelocity) * (gametime.ElapsedGameTime.Milliseconds / 1000f);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Image, Position, null, color, 0, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
