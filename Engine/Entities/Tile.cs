using Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class Tile
    {
        public RectangleF originRectangle { get; set; } // from the origin texture size, position, etc.
        public float rotation { get; set; } // 0f - 1f scaling. 0.25, 0.50, 0.75.
        public float scaleX{ get; set; }
        public float scaleY { get; set; }
        public RectangleF currentRectangle { get; set; } // after scaling and moving.


    }
}
