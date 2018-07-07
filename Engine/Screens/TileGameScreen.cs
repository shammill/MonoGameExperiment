using Engine.Entities;
using Engine.Graphics;
using Engine.Graphics.Functions;
using Engine.Graphics.Sprites;
using Engine.Graphics.TextureAtlases;
using Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Screens
{
    public class TileGameScreen : Screen
    {
        // Core Components
        Game _game;
        private SpriteBatch _spriteBatch;
        MouseState oldMouseState;

        // Content
        private Texture2D _image;

        // Scale variables
        float scaleX = 1f;
        float scaleY = 1f;
        float combinedScale = 1f;

        // Visual Variables
        Color gridlineColor = new Color(Color.Black, 0.15f);

        // Game Management Variables
        List<Tile> tiles;
        Tile selectedTile;
        float lastZIndex = Constants.GameDepthVariance * 3f;
        float percentageComplete = 0f;

        // Game Settings
        TileGameSettings gameSettings;

        // Debugging objects
        RectangleF debugRectangle;
        bool debugMode = true;

        public TileGameScreen(Game game, TileGameSettings gameSettings) : base(game)
        {
            _game = game;
            this.gameSettings = gameSettings;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _image = Content.Load<Texture2D>("Images/01");

            GetImageScaleForCurrentScreenResolution();
            tiles = TileHelper.GenerateTiles(_image, scaleX, scaleY, gameSettings.numberOfYTiles);

            if (gameSettings.randomlyRotateTiles)
            {
                TileHelper.RandomlyRotateTiles(tiles);
            }
            if (gameSettings.randomlySwapTilePositions)
            {
                TileHelper.ShuffleTileLocations(tiles);
            }
            if (gameSettings.randomlyPlaceTiles)
            {
                TileHelper.RandomlyPlaceTiles(tiles, GraphicsDevice.Viewport.Bounds, ref lastZIndex, out debugRectangle);
            }
        }


        /// <summary>
        /// Gets the scale required to fit the game image to the current screen resolution.
        /// </summary>
        public void GetImageScaleForCurrentScreenResolution()
        {
            if (_image.Bounds.Width > GraphicsDevice.Viewport.Width)
            {
                scaleX = (float)GraphicsDevice.Viewport.Width / (float)_image.Bounds.Width;
            }
            else
            {
                scaleX = (float)_image.Bounds.Width / (float)GraphicsDevice.Viewport.Width;
            }
            if (_image.Bounds.Height > GraphicsDevice.Viewport.Height)
            {
                scaleY = (float)GraphicsDevice.Viewport.Height / (float)_image.Bounds.Height;
            }
            else
            {
                scaleY = (float)_image.Bounds.Height / (float)GraphicsDevice.Viewport.Height;
            }
            combinedScale = (scaleX + scaleY) / 2f;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInputGameTypeScatter();
        }

        public float UpdatePercentageComplete()
        {
            var numberHome = tiles.Where(x => x.isHome).Count();
            var numberOfTiles = (float)tiles.Count();

            percentageComplete = numberHome / numberOfTiles * 100;

            return percentageComplete;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var tile in tiles)
            {
                //draw underlying shadow first
                tile.sprite.DrawShadow(_spriteBatch, tile.Position.Offset(3f), tile.rotation, tile.scale, 0.7f, Color.DimGray, depthOffset: Constants.MinimumDepthVariance);

                //draw sprite/tile piece
                tile.sprite.Draw(_spriteBatch, tile.Position, tile.rotation, tile.scale);

                //draw subtle gridlines
                if (!tile.isHome)
                {
                    _spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), gridlineColor, 1f, tile.sprite.Depth + Constants.MinimumDepthVariance);
                }

                // draw coords and bounding box for debugging
                if (debugMode)
                {
                    _spriteBatch.DrawPoint(tile.Position.X, tile.Position.Y, Color.Magenta, 4f);
                    _spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), Color.Blue, 1f);
                }
            }

            if (debugMode)
            {
                _spriteBatch.DrawRectangle(debugRectangle, Color.Blue, 2f);
            }

            _spriteBatch.End();
        }


        private void HandleInputGameTypeScatter()
        {
            var mouseState = Mouse.GetState();

            // if you've just clicked select the current tile
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.sprite.Depth))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position))
                    {
                        selectedTile = tile;
                        tile.Position = mouseState.Position.ToVector2();
                        tile.sprite.Depth = lastZIndex;
                        lastZIndex = lastZIndex + Constants.GameDepthVariance;
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
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.sprite.Depth))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position) && tile.isHome == false)
                    {
                        tile.rotation = RotationHelper.Rotate90Degrees(tile.rotation);

                        // Handle Snapping
                        if (tile.rotation == 0f)
                        {
                            CircleF circle = new CircleF(tile.homePosition, 20f);
                            if (circle.Contains(tile.Position))
                            {
                                tile.Position = tile.homePosition;
                                tile.isHome = true;
                                tile.sprite.Depth = Constants.GameDepthVariance;
                                UpdatePercentageComplete();
                            }
                        }
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
                        selectedTile.sprite.Depth = Constants.GameDepthVariance;
                        UpdatePercentageComplete();
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


        public override void UnloadContent()
        {
            Content.Unload();
            _image.Dispose();
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
