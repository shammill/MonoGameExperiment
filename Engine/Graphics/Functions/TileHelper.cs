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
        public static Size2 GetTotalNumberOfTiles(RectangleF textureBounds, int numberOfTilesVertical)
        {
            float pixelsY = textureBounds.Y / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = textureBounds.X / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));

            return new Size2(roundednumberOfHorizontalPieces, numberOfTilesVertical);
        }

        public static Size2 GetTileSize(RectangleF textureBounds, int numberOfTilesVertical)
        {
            float pixelsY = textureBounds.Y / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = textureBounds.X / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));
            float pixelsX = textureBounds.X / roundednumberOfHorizontalPieces;

            return new Size2(pixelsX, pixelsY);
        }

        public static List<Segment2> GetStraightHorizontalLines(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        {
            List<Segment2> horizontalLines = new List<Segment2>();
            for (var index = 0; index <= numberOfTilesXY.Height; index++)
            {
                float XPosition = 0;
                float YPosition = tileSize.Height * index;
                Point2 startPoint = new Point2(XPosition, YPosition);
                Point2 endPoint = new Point2(textureSize.Width, YPosition);
                Segment2 lineSegment = new Segment2(startPoint, endPoint);
                horizontalLines.Add(lineSegment);
            }
            return horizontalLines;
        }

        public static List<Segment2> GetStraightVerticalLines(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        {
            List<Segment2> verticalLines = new List<Segment2>();

            for (var index = 0; index <= numberOfTilesXY.Width; index++)
            {
                float YPosition = 0;
                float XPosition = tileSize.Width * index;
                Point2 startPoint = new Point2(XPosition, YPosition);
                Point2 endPoint = new Point2(textureSize.Height, YPosition);
                Segment2 lineSegment = new Segment2(startPoint, endPoint);
                verticalLines.Add(lineSegment);
            }
            return verticalLines;
        }

        public static List<RectangleF> GenerateLineSegments(Texture2D texture, int numberOfTiles)
        {
            RectangleF textureSize = texture.Bounds;
            Size2 numberOfTilesXY = GetTotalNumberOfTiles(textureSize, numberOfTiles);
            Size2 tileSize = GetTileSize(textureSize, numberOfTiles);

            // Generate Line Segments
            List<Segment2> horizontalLines = GetStraightHorizontalLines(textureSize, numberOfTilesXY, tileSize);
            List<Segment2> verticalLines = GetStraightVerticalLines(textureSize, numberOfTilesXY, tileSize);

            foreach (var horizontalLine in horizontalLines)
            {
                foreach (var verticalLine in verticalLines)
                {

                }
            }

            // loop through lines and catch intersections.

            return new List<RectangleF>();
        }

        public static List<RectangleF> GenerateSquareTiles(Texture2D texture, int numberOfTiles)
        {
            RectangleF textureSize = texture.Bounds;
            Size2 numberOfTilesXY = GetTotalNumberOfTiles(textureSize, numberOfTiles);
            Size2 tileSize = GetTileSize(textureSize, numberOfTiles);

            // Generate Tiles
            List<RectangleF> tiles = GetTilePositions(textureSize, numberOfTilesXY, tileSize);

            return tiles;
        }

        public static List<RectangleF> GetTilePositions(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        {
            List<RectangleF> tiles = new List<RectangleF>();

            for (var indexX = 0; indexX < numberOfTilesXY.Width; indexX++)
            {
                for (var indexY = 0; indexX < numberOfTilesXY.Height; indexY++)
                {
                    float startingX = tileSize.Width * indexX;
                    float startingY = tileSize.Height * indexY;
                    float width = tileSize.Width;
                    float height = tileSize.Height;

                    RectangleF newTile = new RectangleF(startingX, startingY, width, height);
                    tiles.Add(newTile);
                }
            }
            return tiles;
        }
    }
}
