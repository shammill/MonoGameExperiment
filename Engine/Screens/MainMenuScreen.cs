using Engine.Entities;
using Engine.Graphics;
using Engine.Graphics.Functions;
using Engine.Graphics.Sprites;
using Engine.Graphics.TextureAtlases;
using Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Screens
{
    public class MainMenuScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        List<RectangleF> tileOrigins;
        List<Tile> tiles;
        Tile selectedTile;
        Game _game;
        float scaleX = 1f;
        float scaleY = 1f;
        float combinedScale = 1f;
        MouseState oldMouseState;
        int lastZIndex = 2;

        public MainMenuScreen(Game game) : base(game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("01");

            //Font = Content.Load<BitmapFont>("Fonts/montserrat-32");

            Size2 tileSize = TileHelper.GetTileSize(_background.Bounds, 12);
            Size2 numberOfPieces = TileHelper.GetTotalNumberOfTiles(_background.Bounds, 12);
            tileOrigins = TileHelper.GetTilePositions(_background.Bounds, numberOfPieces, tileSize);

            GetScale();
            GenerateTiles();
        }

        public void GetScale()
        {
            if (_background.Bounds.Width > GraphicsDevice.Viewport.Width)
            {
                scaleX = (float)GraphicsDevice.Viewport.Width / (float)_background.Bounds.Width;
            }
            else
            {
                scaleX = (float)_background.Bounds.Width / (float)GraphicsDevice.Viewport.Width;
            }
            if (_background.Bounds.Height > GraphicsDevice.Viewport.Height)
            {
                scaleY = (float)GraphicsDevice.Viewport.Height / (float)_background.Bounds.Height;
            }
            else
            {
                scaleY = (float)_background.Bounds.Height / (float)GraphicsDevice.Viewport.Height;
            }
            combinedScale = (scaleX + scaleY) / 2f;
        }

        public List<Tile> GenerateTiles()
        {
            tiles = new List<Tile>();
            foreach (var tileOrigin in tileOrigins)
            {
                float scaledXPosition = tileOrigin.X * scaleX;
                float scaledYPosition = tileOrigin.Y * scaleY;
                float scaledWidth = tileOrigin.Width * scaleX;
                float scaledHeight = tileOrigin.Height * scaleY;

                var tile = new Tile();
                TextureRegion2D newt2ds = new TextureRegion2D(_background, tileOrigin.ToRectangle());
                tile.sprite = new Sprite(newt2ds);

                tile.rotation = 0f;
                tile.Position = new Vector2(scaledXPosition + scaledWidth * 0.5f, scaledYPosition + scaledHeight * 0.5f);
                tile.scale = new Vector2(scaleX, scaleY);
                tile.homePosition = tile.Position;

                tiles.Add(tile);
            }
            return tiles;

        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            // if you've just clicked select the current tile
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.zIndex))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position))
                    {
                        selectedTile = tile;
                        tile.Position = mouseState.Position.ToVector2();
                        tile.zIndex = lastZIndex++;
                        
                        break;
                    }
                }
            }

            // Holding left click keeps the tile on the mouse
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && selectedTile != null)
            {
                selectedTile.Position = mouseState.Position.ToVector2();
            }

            // Right Click for Rotation
            if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released && selectedTile == null)
            {
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.zIndex))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position) && tile.isHome == false)
                    {
                        tile.rotation = RotationHelper.Rotate90Degrees(tile.rotation);
                        break;
                    }
                }
            }

            // Right Click for Rotation while holding left click and having a tile selected
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && selectedTile != null 
                && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                selectedTile.rotation = RotationHelper.Rotate90Degrees(selectedTile.rotation);
            }

            // Deselect a tile when releasing a left click
            if (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                // Handle snapping
                if (selectedTile != null && selectedTile.rotation == 0f)
                {
                    CircleF circle = new CircleF(selectedTile.homePosition, 20f);
                    if (circle.Contains(selectedTile.Position))
                    {
                        selectedTile.Position = selectedTile.homePosition;
                        selectedTile.isHome = true;
                        selectedTile.zIndex = 1;
                    }
                }
                else if (selectedTile != null)
                {
                    selectedTile.isHome = false;
                }

                selectedTile = null;
            }

            oldMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            _spriteBatch.Begin();

            List<int> zIndexList = tiles.Select(x => x.zIndex).Distinct().OrderBy(x => x).ToList();

            foreach (var zIndex in zIndexList)
            {
                foreach (var tile in tiles.Where(x => x.zIndex == zIndex))
                {

                    tile.sprite.DrawShadow(_spriteBatch, tile.Position.Offset(2f), tile.rotation, tile.scale, 0.7f, Color.DimGray);

                    //draw sprite
                    tile.sprite.Color = Color.White;
                    tile.sprite.Draw(_spriteBatch, tile.Position, tile.rotation, tile.scale);

                    //draw subtle gridlines // TODO: Stop using Rectangle, a jigsaw piece will need to be shaded the same.
                    var newColor = new Color(Color.Black, 0.15f);
                    _spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), newColor, 0.5f);

                    // draw coords for debugging
                    //_spriteBatch.DrawPoint(tile.Position.X, tile.Position.Y, Color.Magenta, 6f);
                    //_spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), Color.Blue, 1f);
                }
            }

            _spriteBatch.End();
        }


        public override void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();

            base.UnloadContent();
        }

        public override void Dispose()
        {
            base.Dispose();

            _spriteBatch.Dispose();
        }

    }
}
