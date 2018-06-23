using Engine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class Tile
    {
        public RectangleF originRectangle; // from the origin texture size, position, etc.
        public RectangleF currentRectangle; // after scaling and moving.
        public Vector2 relativeOrigin;
        //public Vector2 originPosition;
        public RectangleF centerOriginRectangle; // after scaling and moving.

        public float rotation { get; set; } // 0f - 1f scaling. 0.25, 0.50, 0.75.
        public float scaleX { get; set; }
        public float scaleY { get; set; }


    }
}
