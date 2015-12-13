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
        public List<Pickup> Pickups { get; set; }

        SimpleBadFish Fish1 { get; set; }
        SimpleBadFish Shark1 { get; set; }
        SimpleBadFish LaserShark { get; set; }
        Pickup Krill { get; set; }

        DateTime lastPickup = DateTime.Now;
        int pickupDelay = 20000;

        public EnemyGen(GraphicsDevice gd, SimpleBadFish bf, SimpleBadFish shark, SimpleBadFish laserShark, Pickup krill)
        {
            SpawnBox = new Rectangle(gd.Viewport.Width + 1, 0, gd.Viewport.Height, gd.Viewport.Height);
            POSVarance = new Random(DateTime.Now.Millisecond);

            Fish1 = bf;
            Shark1 = shark;
            LaserShark = laserShark;
            Krill = krill;

            Pickups = new List<Pickup>();
        }

        public int CleanUp()
        {
            int finalPointValue = 0;

            for(int entIndex = 0; entIndex < EntityBag.Count; entIndex++)
            {
                if (EntityBag[entIndex].Dead) //bloopbloop
                {
                    if (!EntityBag[entIndex].QuietDeath)
                    {
                        Game1.BloopSound.Play();
                    }
                    finalPointValue += EntityBag[entIndex].PointValue;
                    EntityBag.RemoveAt(entIndex);
                    return 0;
                }

                if(EntityBag[entIndex].Position.X < -100)
                {
                    //EntityBag.RemoveAt(entIndex);
                    EntityBag[entIndex].DieQuietly();
                }
            }

            for (int i = 0; i < Pickups.Count;i++ )
            {
                if(Pickups[i].Dead)
                {
                    Pickups.RemoveAt(i);
                }

                if(Pickups.Count > i && Pickups[i].Position.X < -50)
                {
                    Pickups.RemoveAt(i);
                }
            }

                return finalPointValue;
        }

        public void Update(GameTime gameTime)
        {
            if (EntityBag == null) EntityBag = new List<Entity>();

            if(lastPickup.AddMilliseconds(pickupDelay) < DateTime.Now && Pickups.Count < 1)
            {
                //drop a pickup of Krill
                float scale = ((float)POSVarance.NextDouble()/2);
                    if (scale < .25f) scale = .25f;
                    Pickups.Add(Krill.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                       scale, Krill.Image, new Vector2(POSVarance.Next(-1000, -500), POSVarance.Next(-3, 3))));
                    lastPickup = DateTime.Now;
            }

            if(EntityBag.Count < 5)
            {
                if (POSVarance.NextDouble() > .33)
                {
                    //Jelly
                    //.Position = new Vector2(SpawnBox.Center.X, SpawnBox.Center.Y)
                    float scale = ((float)POSVarance.NextDouble());
                    if (scale < .25f) scale = .25f;
                    EntityBag.Add(Fish1.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                        new Vector2(POSVarance.Next(-10, -5), POSVarance.Next(-3, 3)), scale));
                }
                else if (POSVarance.NextDouble() > .33)
                {
                    //Shark
                    float scale = ((float)POSVarance.NextDouble());
                    if (scale < .5f) scale = .5f;
                    EntityBag.Add(Shark1.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                        new Vector2(POSVarance.Next(-15, -10), POSVarance.Next(-3, 3)), scale));
                }
                else
                {
                    //Shark
                    float scale = ((float)POSVarance.NextDouble());
                    if (scale < .5f) scale = .5f;
                    EntityBag.Add(LaserShark.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                        new Vector2(POSVarance.Next(-15, -10), POSVarance.Next(-3, 3)), scale));
                }
            }

            foreach(Entity currentEnt in EntityBag)
            {
                currentEnt.Update(gameTime);
            }

            foreach (Pickup p in Pickups)
            {
                p.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (Entity currentEnt in EntityBag)
            {
                currentEnt.Draw(gameTime, sb);
            }

            foreach(Pickup p in Pickups)
            {
                p.Draw(gameTime, sb);
            }

        }
    }
}
