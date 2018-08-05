using Engine.BitmapFonts;
using Engine.Entities;
using Engine.Graphics.Sprites;
using Engine.Graphics.TextureAtlases;
using Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics.Fonts
{
    public class FontPackage
    {
        public BitmapFont font { get; set; }
        public float scale { get; set; }
    }
    /// <summary>
    /// Helps choose a font closest to the required size/scale.
    /// </summary>
    public static class FontHelper
    {
        public static FontPackage GetFont(List<BitmapFont> fontList, float proposed)
        {
            throw new NotImplementedException();
            return new FontPackage();
        }

    }
}
