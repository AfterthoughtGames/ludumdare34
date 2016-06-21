using CorbanCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    class ComicHit : Entity
    {
        public float Life { get; set; }
        Random rand = new Random(DateTime.Now.Millisecond);
        float maxScale;
        float startingLife;
        float maxRotation;
        float rotation;

        public ComicHit(float life, Vector2 position, Texture2D image = null)
        {
            Life = life;
            startingLife = life;
            maxScale = .25f + (float)rand.NextDouble();
            if (maxScale > .75f) maxScale = .75f;
            maxRotation = rand.Next(-45, 45);
            Position = position;

            double nbr = rand.NextDouble();
            if (image == null)
            {
                if (nbr < .3)
                {
                    Image = Game1.ComicHit1;
                }
                else if (nbr < .6)
                {
                    Image = Game1.ComicHit2;
                }
                else
                {
                    Image = Game1.ComicHit3;
                }
            }
            else
            {
                Image = image;
            }
            
            
            if (maxScale > 1) maxScale = 1;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Life -= 250 * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            Scale = maxScale * ((startingLife - Life) / startingLife);
            rotation = maxRotation * 0.0174533f * ((startingLife - Life) / startingLife);

            if(Life < 0)
            {
                Dead = true;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            if (Dead == false)
                batch.Draw(Image, Position, null, Color.White, rotation, new Vector2(Image.Width / 2, Image.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
