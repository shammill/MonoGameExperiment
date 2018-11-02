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

    public enum VerticalPosition
    {
        Top = 0,
        Centered = 1,
        Bottom = 2,
    }

    public enum HorizontalPosition
    {
        Left = 0,
        Centered = 1,
        Right = 2,
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
            float actualScale = 0f;

            float bestHeightDifferenceFound = 1000f;
            foreach (BitmapFont font in fontList)
            {
                RectangleF rect = font.GetStringRectangle("X");
                float differenceWithProposed = rect.Height;

                if (rect.Height > proposedHeight)
                    differenceWithProposed = rect.Height - proposedHeight;
                else if (rect.Height < proposedHeight)
                    differenceWithProposed = proposedHeight - rect.Height;

                if (bestHeightDifferenceFound > differenceWithProposed)
                {
                    actualFont = font;
                    bestHeightDifferenceFound = differenceWithProposed;
                    actualScale = proposedHeight / rect.Height;
                }
            }

            return new FontPackage() { Font = actualFont, Scale = actualScale };
        }

        public static Textbox SetTextBoxPosition(Textbox tb, FontPackage fontPackage, Rectangle viewportBounds, VerticalPosition verticalPosition, float verticalDistance, HorizontalPosition horizontalPosition, float horizontalDistance)
        {
            RectangleF textBoxRectangle = fontPackage.Font.GetStringRectangle(tb.Value);

            float xPosition = 0f;
            if (horizontalPosition.Equals(HorizontalPosition.Centered))
                xPosition = viewportBounds.Width / 2f - (textBoxRectangle.Width / 2f);

            if (horizontalPosition.Equals(HorizontalPosition.Left))
                xPosition = 0f + viewportBounds.Width * horizontalDistance;

            if (horizontalPosition.Equals(HorizontalPosition.Right))
                xPosition = viewportBounds.Width - (textBoxRectangle.Width) - viewportBounds.Width * horizontalDistance;


            float yPosition = viewportBounds.Height * 0.25f;
            tb.Location = new Vector2(xPosition, yPosition);


            textBoxRectangle.Position = new Point2(tb.Location.X, tb.Location.Y);
            tb.BoundingBox = textBoxRectangle;

            return tb;
        }







    }
}
