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

        List<Entity> EntityBag { get; set; }

        SimpleBadFish Fish1 { get; set; }

        public EnemyGen(GraphicsDevice gd, SimpleBadFish bf)
        {
            SpawnBox = new Rectangle(gd.Viewport.Width + 1, 0, gd.Viewport.Height, gd.Viewport.Height);
            POSVarance = new Random(1337);

            Fish1 = bf;
        }

        public void Update(GameTime gameTime)
        {
            if (EntityBag == null) EntityBag = new List<Entity>();

            if(EntityBag.Count < 1)
            {
                //.Position = new Vector2(SpawnBox.Center.X, SpawnBox.Center.Y)
                EntityBag.Add(Fish1.Clone(new Vector2(SpawnBox.Center.X, SpawnBox.Center.Y)));
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
