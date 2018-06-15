using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility
{
    public static class RectangleExtensions
    {
        public static Point CenterOfRectangle(this Rectangle rectangle)
        {
            var point = new Point();
            point.X = rectangle.Right / 2;
            point.Y = rectangle.Height / 2;

            return point;
        }

    }
}
