using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility
{
    public static class Constants
    {
        /// <summary>
        /// Rotational constannts. Float Rotation (radians)
        /// </summary>
        public static class Rotation
        {
            public const float Degrees0 = 0f;
            public const float Degrees90 = 1.5708f;
            public const float Degrees180 = 3.1416f;
            public const float Degrees270 = 4.7124f;
            public const float Degrees360 = 6.2832f;
        }

        public static class Depth
        {
            /// <summary>
            /// The very back layer. Base layer.
            /// </summary>
            public const float MinimumDepth = 0.0f;


            /// <summary>
            /// Typical distance between different 2D objects.
            /// </summary>
            public const float GameDepthVariance = 0.000000001f;

            /// <summary>
            /// a very small difference. to display a dropshadow, or render subtle lines on top of the edge of a shape.
            /// </summary>
            public const float MinimumDepthVariance = 0.0000000001f;
        }

//        public static class 


    }
}
