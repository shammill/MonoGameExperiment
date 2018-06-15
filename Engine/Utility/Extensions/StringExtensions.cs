using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility
{
    public static class StringHelper
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Equals("");
        }

    }
}
