using Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics
{
    public static class RotationHelper
    {
        public static float Rotate90Degrees(float rotation)
        {
            float newRotation = rotation + Constants.Degrees90;
            if (newRotation >= Constants.Degrees360)
                newRotation = 0f;
             
            return newRotation;
        }

    }
}
