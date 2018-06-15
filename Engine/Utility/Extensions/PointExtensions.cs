using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Engine.Utility
{
    public static class PointExtensions
    {
        public static Microsoft.Xna.Framework.Point ToXnaPoint(this System.Drawing.Point point)
        {
            return new Microsoft.Xna.Framework.Point(point.X, point.Y);
        }

    }
}
