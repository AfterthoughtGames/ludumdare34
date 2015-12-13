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
        public List<Entity> EventEntities { get; set; }

        SimpleBadFish Fish1 { get; set; }
        SimpleBadFish Shark1 { get; set; }
        SimpleBadFish LaserShark { get; set; }
        SimpleBadFish Octo { get; set; }
        Pickup Krill { get; set; }

        DateTime lastPickup = DateTime.Now;
        int pickupDelay = 20000;
        int Difficulty = 2;

        DateTime lastEvent = DateTime.Now;
        DateTime lastEventSpawn = DateTime.Now;
        int eventDelay;
        int eventDuration;

        DateTime lastDifficultyUpgrade = DateTime.Now;
        int difficultyDelay = 10000;

        public EnemyGen(GraphicsDevice gd, SimpleBadFish bf, SimpleBadFish shark, SimpleBadFish laserShark, Pickup krill, SimpleBadFish octo)
        {
            SpawnBox = new Rectangle(gd.Viewport.Width + 1, 0, gd.Viewport.Height, gd.Viewport.Height);
            POSVarance = new Random(DateTime.Now.Millisecond);

            Fish1 = bf;
            Fish1.PointValue = 2;
            Shark1 = shark;
            Shark1.PointValue = 4;
            LaserShark = laserShark;
            LaserShark.PointValue = 10;
            Krill = krill;
            Krill.PointValue = 15;
            Octo = octo;
            octo.PointValue = 8;

            Pickups = new List<Pickup>();
            EventEntities = new List<Entity>();
            EntityBag = new List<Entity>();

            eventDelay = POSVarance.Next(20000, 40000);
            eventDuration = 0;
            lastEvent = DateTime.Now;
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
                        finalPointValue += EntityBag[entIndex].PointValue;
                    }

                    EntityBag.RemoveAt(entIndex);
                }
            }

            for (int entIndex = 0; entIndex < EntityBag.Count; entIndex++)
            {
                if (EntityBag[entIndex].Position.X < -100)
                {
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

            for (int i = 0; i < EventEntities.Count; i++ )
            {
                if(EventEntities[i].Position.X < -100)
                {
                    EventEntities.RemoveAt(i);
                }
            }

                return finalPointValue;
        }

        public void Update(GameTime gameTime)
        {
            if (EntityBag == null) EntityBag = new List<Entity>();

            if (lastEvent.AddMilliseconds(eventDelay) < DateTime.Now && eventDuration <= 0)
            {
                eventDuration = POSVarance.Next(2000, 7000);
            }

            if(lastDifficultyUpgrade.AddMilliseconds(difficultyDelay) < DateTime.Now)
            {
                Difficulty += 1;
                lastDifficultyUpgrade = DateTime.Now;
            }

            if(eventDuration >= 0)
            {
                if (lastEventSpawn.AddMilliseconds(100) < DateTime.Now)
                {
                    lastEventSpawn = DateTime.Now;
                    Entity entity = new Entity();
                    entity.Health = 1;
                    entity.Image = Game1.Fish;
                    entity.PointValue = 0;
                    entity.Position = new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height));
                    entity.Scale = ((float)POSVarance.NextDouble());
                    entity.Velocity = new Vector2(POSVarance.Next(-1000, -500),0);
                    if (entity.Scale < .25f) entity.Scale = .25f;

                    EventEntities.Add(entity);
                        
                }
                eventDuration -= gameTime.ElapsedGameTime.Milliseconds;

                if (eventDuration < 0)
                {
                    lastEvent = DateTime.Now;
                }
            }

            if(lastPickup.AddMilliseconds(pickupDelay) < DateTime.Now && Pickups.Count < 1)
            {
                //drop a pickup of Krill
                float scale = ((float)POSVarance.NextDouble()/2);
                    if (scale < .25f) scale = .25f;
                    Pickups.Add(Krill.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                       scale, Krill.Image, new Vector2(POSVarance.Next(-1000, -500), POSVarance.Next(-3, 3))));
                    lastPickup = DateTime.Now;
            }

            if(EntityBag.Count < Difficulty)
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
                else if (POSVarance.NextDouble() > .33)
                {
                    //Octo
                    float scale = ((float)POSVarance.NextDouble());
                    if (scale < .5f) scale = .5f;
                    EntityBag.Add(Octo.Clone(new Vector2(POSVarance.Next(SpawnBox.Left, SpawnBox.Left + SpawnBox.Width), POSVarance.Next(SpawnBox.Top, SpawnBox.Top + SpawnBox.Height)),
                        new Vector2(POSVarance.Next(-10, -10), POSVarance.Next(-3, 3)), scale));
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
