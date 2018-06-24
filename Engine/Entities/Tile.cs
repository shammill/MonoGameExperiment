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
    public class Tile : IMovable
    {
        public Sprite sprite;
        public Vector2 Position { get; set; }
        public float rotation;
        public Vector2 scale;
        public int zIndex = 0;

        // Game Logic
        public Vector2 homePosition;
        public bool isMovable;
        public bool isHome;

        public RectangleF GetBoundingBox()
        {
            return this.sprite.GetBoundingRectangle(Position, rotation, scale);
        }

        public bool CheckIsHome()
        {
            return (Position == homePosition && rotation == 0f);
        }

    }
}
