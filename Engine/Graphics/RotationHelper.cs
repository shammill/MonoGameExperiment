﻿using Engine.Utility;
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
            float newRotation = rotation + Constants.Rotation.Degrees90;
            if (newRotation >= Constants.Rotation.Degrees360)
                newRotation = 0f;

            return newRotation;
        }

        public static float GetRandom90DegreeRotation()
        {
            int random = RandomHelper.Next(4);
            return Constants.Rotation.Degrees90 * random;
        }
    }
}
