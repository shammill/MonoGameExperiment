using Engine.Content;
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
        List<RectangleF> gridLines;

        // Game Management Variables
        List<Tile> tiles;
        Tile selectedTile;
        Tile shadowTile;
        float lastZIndex = Constants.Depth.GameDepthVariance * 3f;
        float percentageComplete = 0f;

        // Game Settings
        TileGameSettings gameSettings;

        // Debugging objects
        RectangleF debugRectangle;
        bool debugMode = false;

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
            _image = Content.Load<Texture2D>(ContentPaths.Textures.Cat01);

            GetImageScaleForCurrentScreenResolution();
            tiles = TileHelper.GenerateTiles(_image, scaleX, scaleY, gameSettings.numberOfYTiles);
            gridLines = tiles.Select(x => x.GetBoundingBox()).ToList();

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
            if (gameSettings.tileGameType == TileGameMode.Scatter)
                HandleInputGameTypeScatter();
            if (gameSettings.tileGameType == TileGameMode.Shuffle)
                HandleInputGameTypeShuffle();
            if (gameSettings.tileGameType == TileGameMode.Swapper)
                HandleInputGameTypeSwapper();
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

            if (true)
            {
                foreach (var gridline in gridLines)
                {
                    _spriteBatch.DrawRectangle(gridline, gridlineColor, 0.5f, Constants.Depth.MinimumDepthVariance);
                }
            }

            foreach (var tile in tiles)
            {
                //draw underlying shadow first
                tile.sprite.DrawShadow(_spriteBatch, tile.Position.Offset(3f), tile.rotation, tile.scale, 0.7f, Color.DimGray, depthOffset: Constants.Depth.MinimumDepthVariance);

                //draw sprite/tile piece
                tile.sprite.Draw(_spriteBatch, tile.Position, tile.rotation, tile.scale);

                //draw subtle gridlines
                if (!tile.isHome)
                {
                    _spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), gridlineColor, 1f, tile.sprite.Depth + Constants.Depth.MinimumDepthVariance);
                }

                // draw coords and bounding box for debugging
                if (debugMode)
                {
                    _spriteBatch.DrawPoint(tile.Position.X, tile.Position.Y, Color.Magenta, 4f, depth: 1.0f);
                    _spriteBatch.DrawRectangle(tile.sprite.GetBoundingRectangle(tile.Position, tile.rotation, tile.scale), Color.Blue, 1f, depth: 1.0f);
                }
            }

            // Draw rectangle wherein randomly placed tiles can land.
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
                        lastZIndex = lastZIndex + Constants.Depth.GameDepthVariance;
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
            if (gameSettings.randomlyRotateTiles && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released && selectedTile == null)
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
                                tile.sprite.Depth = Constants.Depth.GameDepthVariance;
                                UpdatePercentageComplete();
                            }
                        }
                        break;
                    }
                }
            }

            // Right Click for Rotation while holding left click and having a tile selected
            if (gameSettings.randomlyRotateTiles && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && selectedTile != null
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
                        selectedTile.sprite.Depth = Constants.Depth.GameDepthVariance;
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

        private void HandleInputGameTypeShuffle()
        {
            var mouseState = Mouse.GetState();

            // if you've just clicked select the current tile
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.sprite.Depth))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position))
                    {
                        RectangleF shadowTileHitBox = shadowTile.GetBoundingBox();
                        // add bounds
                        //check hit.
                        if (true) // if close to shadowTile
                        {
                            Vector2 selectedTilePosition = tile.Position;
                            tile.Position = shadowTile.Position;
                            shadowTile.Position = selectedTilePosition;
                            selectedTile = null;
                        }
                        break;
                    }
                }
            }

            // Right Click for Rotation
            if (gameSettings.randomlyRotateTiles && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released && selectedTile == null)
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
                                tile.sprite.Depth = Constants.Depth.GameDepthVariance;
                                UpdatePercentageComplete();
                            }
                        }
                        break;
                    }
                }
            }

            // Right Click for Rotation while holding left click and having a tile selected
            if (gameSettings.randomlyRotateTiles && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && selectedTile != null
                && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                selectedTile.rotation = RotationHelper.Rotate90Degrees(selectedTile.rotation);
            }

            oldMouseState = mouseState;
        }

        private void HandleInputGameTypeSwapper()
        {
            var mouseState = Mouse.GetState();

            // if you've just clicked select the current tile
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var tile in tiles.Where(x => x.isHome == false).OrderByDescending(x => x.sprite.Depth))
                {
                    if (tile.GetBoundingBox().Contains(mouseState.Position))
                    {
                        // If No tile is selected, select the tile.
                        if (selectedTile == null)
                        {
                            selectedTile = tile;
                        }
                        else if (selectedTile.Position == tile.Position)
                        {
                            selectedTile = null;
                        }
                        else //swap tile positions and deselect Tile.
                        {
                            Vector2 selectedTilePosition = selectedTile.Position;
                            selectedTile.Position = tile.Position;
                            tile.Position = selectedTilePosition;
                            selectedTile = null;
                        }
                        break;
                    }
                }
            }

            // Right Click for Rotation
            if (gameSettings.randomlyRotateTiles && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released && selectedTile == null)
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
                                tile.sprite.Depth = Constants.Depth.GameDepthVariance;
                                UpdatePercentageComplete();
                            }
                        }
                        break;
                    }
                }
            }

            // Right Click for Rotation while holding left click and having a tile selected
            if (gameSettings.randomlyRotateTiles && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && selectedTile != null
                && mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                selectedTile.rotation = RotationHelper.Rotate90Degrees(selectedTile.rotation);
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
