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
        public BitmapFont Font { get; set; }
        public float Scale { get; set; }
    }

    /// <summary>
    /// Helps choose a font closest to the required size/scale.
    /// </summary>
    public static class FontHelper
    {
        public static FontPackage GetFont(List<BitmapFont> fontList, float proposedHeightPercentage, Rectangle screenSpace)
        { 
            float proposedHeight = screenSpace.Height * proposedHeightPercentage;

            BitmapFont actualFont = null;
            float actualScale = 1f;

            float previousScale = 0f;
            float keptScale = 0f;
            foreach (BitmapFont font in fontList)
            {
                RectangleF rect = font.GetStringRectangle("X");
                float differenceWithProposed = rect.Height;
                float scale = rect.Height / proposedHeight;

                if (scale -)
            }

            return new FontPackage() { Font = actualFont, Scale = actualScale };
        }

    }
}
