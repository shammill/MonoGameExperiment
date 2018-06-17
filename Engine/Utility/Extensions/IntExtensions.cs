using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility.Extensions
{
    public static class IntExtensions
    {
        public static float PercentageToFloat(this int percentage)
        {
            float f = percentage / 100.0f;
            return f;
        }

    }
}
