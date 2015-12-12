using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class EnemyGen
    {
        Rectangle SpawnBox { get; set; }
        Random POSVarance { get; set; }

        public List<Entity> EntityBag { get; set; }

        SimpleBadFish Fish1 { get; set; }

        public EnemyGen(GraphicsDevice gd, SimpleBadFish bf)
        {
            SpawnBox = new Rectangle(gd.Viewport.Width + 1, 0, gd.Viewport.Height, gd.Viewport.Height);
            POSVarance = new Random(DateTime.Now.Millisecond);

            Fish1 = bf;
        }

        public int CleanUp()
        {
            int finalPointValue = 0;

            for(int entIndex = 0; entIndex < EntityBag.Count; entIndex++)
            {
                if(EntityBag[entIndex].Dead)
                {
                    finalPointValue += EntityBag[entIndex].PointValue;
                    EntityBag.RemoveAt(entIndex);
                }
            }

            return finalPointValue;
        }

        public void Update(GameTime gameTime)
        {
            if (EntityBag == null) EntityBag = new List<Entity>();

            if(EntityBag.Count < 5)
            {
                //.Position = new Vector2(SpawnBox.Center.X, SpawnBox.Center.Y)
                float scale = ((float)POSVarance.NextDouble());
                if (scale < .25f) scale = .25f;
                EntityBag.Add(Fish1.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top+SpawnBox.Height)),
                    new Vector2(POSVarance.Next(-15, -10), POSVarance.Next(-3, 3)),scale));
            }

            foreach(Entity currentEnt in EntityBag)
            {
                currentEnt.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (Entity currentEnt in EntityBag)
            {
                currentEnt.Draw(gameTime, sb);
            }
        }
    }
}
