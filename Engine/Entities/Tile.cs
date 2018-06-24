using Engine.Graphics;
using Engine.Graphics.Sprites;
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
        public Sprite sprite;
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        // Game Logic
        public Vector2 homePosition;
        public bool isMovable;
        public bool isHome;


        public RectangleF GetBoundingBox()
        {
            return this.sprite.GetBoundingRectangle(position, rotation, scale);
        }

        public bool CheckIsHome()
        {
            return (position == homePosition && rotation == 0f);
        }

    }
}
