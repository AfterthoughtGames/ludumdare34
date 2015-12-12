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
        public WhaleOfAPlayer(Texture2D image)
        {
            Image = image;
            this.Scale = 1;
            this.Position = new Microsoft.Xna.Framework.Vector2(10, 10);
            this.HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
        }

        public void Update(GameTime gameTime)
        {
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
        }
    }
}
