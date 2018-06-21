using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Graphics
{
    public class Path
    {
        public List<Segment2> path = new List<Segment2>();

        public void AddLine(Segment2 line)
        {
            path.Add(line);
        }
    }
}
