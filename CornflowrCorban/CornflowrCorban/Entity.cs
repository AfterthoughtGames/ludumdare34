using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornflowrCorban
{
    public class Entity
    {
        public Texture2D Image { get; set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle HitBox { get; set; }

        public List<Point> AIPath { get; set; }
    }
}
