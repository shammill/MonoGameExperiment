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

namespace Engine.Graphics.Functions
{
    /// <summary>
    /// Generates Tiles from a Parent Image
    /// </summary>
    public static class TileHelper
    {
        public static Size2 GetTotalNumberOfTiles(RectangleF textureBounds, int numberOfTilesVertical)
        {
            float pixelsY = textureBounds.Height / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = textureBounds.Width / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));

            return new Size2(roundednumberOfHorizontalPieces, numberOfTilesVertical);
        }

        public static Size2 GetTileSize(RectangleF textureBounds, int numberOfTilesVertical)
        {
            float pixelsY = textureBounds.Height / numberOfTilesVertical; // get the number of pixels per piece
            float numberOfHorizontalPieces = textureBounds.Width / pixelsY; // keep the X size the same as Y as it is square.
            int roundednumberOfHorizontalPieces = Convert.ToInt32(Math.Round(numberOfHorizontalPieces));
            float pixelsX = textureBounds.Width / roundednumberOfHorizontalPieces;

            return new Size2(Convert.ToInt32(Math.Round(pixelsX)), Convert.ToInt32(Math.Round(pixelsY)));
        }

        public static List<RectangleF> GetTilePositions(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        {
            List<RectangleF> tiles = new List<RectangleF>();

            for (var indexX = 0; indexX < numberOfTilesXY.Width; indexX++)
            {
                for (var indexY = 0; indexY < numberOfTilesXY.Height; indexY++)
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

        public static List<Tile> GenerateTiles(Texture2D image, float scaleX, float scaleY, int numberOfTilesY)
        {
            Size2 tileSize = GetTileSize(image.Bounds, numberOfTilesY);
            Size2 numberOfPieces = GetTotalNumberOfTiles(image.Bounds, numberOfTilesY);
            List<RectangleF> tileOrigins = GetTilePositions(image.Bounds, numberOfPieces, tileSize);

            return CreateTiles(image, tileOrigins, scaleX, scaleY);
        }

        public static List<Tile> CreateTiles(Texture2D image, List<RectangleF> tileOrigins, float scaleX, float scaleY)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (var tileOrigin in tileOrigins)
            {
                float scaledXPosition = tileOrigin.X * scaleX;
                float scaledYPosition = tileOrigin.Y * scaleY;
                float scaledWidth = tileOrigin.Width * scaleX;
                float scaledHeight = tileOrigin.Height * scaleY;

                var tile = new Tile();
                TextureRegion2D newt2ds = new TextureRegion2D(image, tileOrigin.ToRectangle());
                tile.sprite = new Sprite(newt2ds);
                tile.sprite.Depth = Constants.GameDepthVariance * 2;

                tile.rotation = 0f;
                tile.Position = new Vector2(scaledXPosition + scaledWidth * 0.5f, scaledYPosition + scaledHeight * 0.5f);
                tile.scale = new Vector2(scaleX, scaleY);
                tile.homePosition = tile.Position;

                tiles.Add(tile);
            }
            return tiles;
        }

        public static void RandomlyRotateTiles(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.rotation = RotationHelper.GetRandom90DegreeRotation();
            }
        }

        public static void ShuffleTileLocations(List<Tile> tiles)
        {
            int n = tiles.Count();
            while (n > 1)
            {
                n--;
                int randomIndex = RandomHelper.Next(n + 1);
                Vector2 value = tiles[randomIndex].Position;
                tiles[randomIndex].Position = tiles[n].Position;
                tiles[n].Position = value;
            }
        }

        public static void RandomlyPlaceTiles(List<Tile> tiles, RectangleF bounds, ref float maxDepth, out RectangleF outRect)
        {
            var tileBounds = tiles.FirstOrDefault().GetBoundingBox();

            var xBoundsMin = bounds.X + (tileBounds.Width / 2);
            var yBoundsMin = bounds.Y + (tileBounds.Height /2);
            var xBoundsMax = bounds.Width - (tileBounds.Height / 2);
            var yBoundsMax = bounds.Height - (tileBounds.Height / 2);
            RectangleF boundsWithMargin = new RectangleF(xBoundsMin, yBoundsMin, xBoundsMax - (tileBounds.Height / 2), yBoundsMax - (tileBounds.Height / 2));
            outRect = boundsWithMargin;

            // Adjust depth so randomly placed tiles arent on the exact same level.
            var depth = Constants.GameDepthVariance;
            foreach (var tile in tiles)
            {
                var randomX = RandomHelper.Next((int)xBoundsMin, (int)xBoundsMax);
                var randomY = RandomHelper.Next((int)yBoundsMin, (int)yBoundsMax);
                tile.Position = new Vector2(randomX, randomY);
                tile.sprite.Depth = depth;
                depth += Constants.GameDepthVariance;
            }
            maxDepth = depth;
        }


        //public static List<Segment2> GetStraightHorizontalLines(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        //{
        //    List<Segment2> horizontalLines = new List<Segment2>();
        //    for (var index = 0; index <= numberOfTilesXY.Height; index++)
        //    {
        //        float XPosition = 0;
        //        float YPosition = tileSize.Height * index;
        //        Point2 startPoint = new Point2(XPosition, YPosition);
        //        Point2 endPoint = new Point2(textureSize.Width, YPosition);
        //        Segment2 lineSegment = new Segment2(startPoint, endPoint);
        //        horizontalLines.Add(lineSegment);
        //    }
        //    return horizontalLines;
        //}

        //public static List<Segment2> GetStraightVerticalLines(RectangleF textureSize, Size2 numberOfTilesXY, Size2 tileSize)
        //{
        //    List<Segment2> verticalLines = new List<Segment2>();

        //    for (var index = 0; index <= numberOfTilesXY.Width; index++)
        //    {
        //        float YPosition = 0;
        //        float XPosition = tileSize.Width * index;
        //        Point2 startPoint = new Point2(XPosition, YPosition);
        //        Point2 endPoint = new Point2(textureSize.Height, YPosition);
        //        Segment2 lineSegment = new Segment2(startPoint, endPoint);
        //        verticalLines.Add(lineSegment);
        //    }
        //    return verticalLines;
        //}

        //public static List<RectangleF> GenerateLineSegments(Texture2D texture, int numberOfTiles)
        //{
        //    RectangleF textureSize = texture.Bounds;
        //    Size2 numberOfTilesXY = GetTotalNumberOfTiles(textureSize, numberOfTiles);
        //    Size2 tileSize = GetTileSize(textureSize, numberOfTiles);

        //    // Generate Line Segments
        //    List<Segment2> horizontalLines = GetStraightHorizontalLines(textureSize, numberOfTilesXY, tileSize);
        //    List<Segment2> verticalLines = GetStraightVerticalLines(textureSize, numberOfTilesXY, tileSize);

        //    foreach (var horizontalLine in horizontalLines)
        //    {
        //        foreach (var verticalLine in verticalLines)
        //        {

        //        }
        //    }

        //    // loop through lines and catch intersections.

        //    return new List<RectangleF>();
        //}

        //public static List<RectangleF> GenerateSquareTiles(Texture2D texture, int numberOfTiles)
        //{
        //    RectangleF textureSize = texture.Bounds;
        //    Size2 numberOfTilesXY = GetTotalNumberOfTiles(textureSize, numberOfTiles);
        //    Size2 tileSize = GetTileSize(textureSize, numberOfTiles);

        //    // Generate Tiles
        //    List<RectangleF> tiles = GetTilePositions(textureSize, numberOfTilesXY, tileSize);

        //    return tiles;
        //}

    }
}
