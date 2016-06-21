using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class RandomStaticStuff
    {
        public static Texture2D GenerateBubuleField(GraphicsDevice gd, Texture2D image)
        {
            RenderTarget2D target = new RenderTarget2D(gd, 800, 800);

            gd.SetRenderTarget(target);
            gd.Clear(Color.Transparent);
            using(SpriteBatch b = new SpriteBatch(gd))
            {
               b.Begin();

               
               for(int x = 0; x < 10; x++)
                 for(int y = 0; y < 10; y++)
                   b.Draw(image, new Rectangle(x, y, 10, 10),null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

               b.End();
            }

            gd.SetRenderTarget(null);

             
            return target; 
        }
    }
}
