using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Graphics.Functions
{
    /// <summary>
    /// Generates Tiles from a Parent Image
    /// </summary>
    public static class TileHelper
    {
        public static float GetTotalNumberOfTiles(Texture2D texture, int numberOfTilesVertical)
        {
            float pixelsY = texture.Bounds.Y / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = texture.Bounds.X / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));
            float pixelsX = texture.Bounds.X / roundednumberOfHorizontalPieces;

            return numberOfTilesVertical * roundednumberOfHorizontalPieces;
        }

        public static Size2 GetPieceSize(Texture2D texture, int numberOfTilesVertical)
        {
            float pixelsY = texture.Bounds.Y / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = texture.Bounds.X / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));
            float pixelsX = texture.Bounds.X / roundednumberOfHorizontalPieces;

            return new Size2(pixelsX, pixelsY);
        }


        public static List<RectangleF> GenerateSquareTiles(Texture2D texture, int numberOfTiles)
        {
            RectangleF textureSize = texture.Bounds;



            return new List<RectangleF>();
        }

    }
}
