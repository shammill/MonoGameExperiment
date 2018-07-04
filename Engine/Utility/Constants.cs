using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility
{
    public class Constants
    {
        public const float Degrees0 = 0f;
        public const float Degrees90 = 1.5708f;
        public const float Degrees180 = 3.1416f;
        public const float Degrees270 = 4.7124f;
        public const float Degrees360 = 6.2832f;

        public const float GameDepthVariance = 0.0001f;
        public const float MinimumDepthVariance = 0.00001f; // doesnt seem to have a limit.

    }
}
