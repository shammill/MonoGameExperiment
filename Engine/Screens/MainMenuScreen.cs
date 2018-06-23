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
        Game _game;
        float scaleX = 1f;
        float scaleY = 1f;
        float combinedScale = 1f;
        MouseState oldMouseState;

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

            TextureRegion2D t2D = new TextureRegion2D(_background);
            Sprite sprite = new Sprite(t2D);

            //Font = Content.Load<BitmapFont>("Fonts/montserrat-32");

            Size2 tileSize = TileHelper.GetTileSize(_background.Bounds, 12);
            Size2 numberOfPieces = TileHelper.GetTotalNumberOfTiles(_background.Bounds, 12);
            tileOrigins = TileHelper.GetTilePositions(_background.Bounds, numberOfPieces, tileSize);

            if (_background.Bounds.Width > GraphicsDevice.Viewport.Width)
            {
                scaleX = (float)GraphicsDevice.Viewport.Width / (float)_background.Bounds.Width;
                //scaleY = (float)GraphicsDevice.Viewport.Height/ (float)_background.Bounds.Height;
            }
            else { 
                scaleX = (float)_background.Bounds.Width / (float)GraphicsDevice.Viewport.Width;
                //scaleY = (float)_background.Bounds.Height / (float)GraphicsDevice.Viewport.Height;
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

            tiles = new List<Tile>();
            foreach (var tileOrigin in tileOrigins)
            {
                var tile = new Tile();
                tile.originRectangle = tileOrigin;
                tile.rotation = 0f;
                tile.scaleX = scaleX;
                tile.scaleY = scaleY;

                float scaledXPosition = tileOrigin.X * scaleX;
                float scaledYPosition = tileOrigin.Y * scaleY;
                float scaledWidth = tileOrigin.Width * scaleX;
                float scaledHeight = tileOrigin.Height * scaleY;

                tile.relativeOrigin.X = tileOrigin.Width * 0.5f;
                tile.relativeOrigin.Y = tileOrigin.Height * 0.5f;

                tile.centerOriginRectangle = new RectangleF(scaledXPosition + scaledWidth / 2f, scaledYPosition + scaledHeight / 2f, scaledWidth, scaledHeight);
                tile.currentRectangle = new RectangleF(scaledXPosition, scaledYPosition, scaledWidth, scaledHeight);
                tiles.Add(tile);
            }

        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                foreach (var tile in tiles)
                {
                    if (tile.currentRectangle.Contains(mouseState.Position))
                    {
                        float widthHolder = tile.currentRectangle.Width;
                        tile.currentRectangle.Width = tile.currentRectangle.Height;
                        tile.currentRectangle.Height = widthHolder;
                        //tile.currentRectangle.

                        var previousRotation = tile.rotation;
                        tile.rotation = RotationHelper.Rotate90Degrees(tile.rotation);

                        Matrix rotate = Matrix.CreateRotationZ(tile.rotation - previousRotation);
                        //
                        //tile.currentRectangle.X

                        break;
                    }
                }
            }

                oldMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            _spriteBatch.Begin();

            //_spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            foreach (var tile in tiles)
            {
                _spriteBatch.Draw(_background,
                    tile.centerOriginRectangle.ToRectangle(),
                    tile.originRectangle.ToRectangle(),
                    Color.White,
                    tile.rotation,
                    //Vector2.Zero,
                    tile.relativeOrigin,
                    SpriteEffects.None, 0f);


                //_spriteBatch.Draw(_background,
                //    //new Vector2((tile.currentRectangle.X + tile.currentRectangle.Width / 5f), (tile.currentRectangle.Y + tile.currentRectangle.Height / 6f)),
                //    new Vector2((tile.currentRectangle.X), (tile.currentRectangle.Y)),
                //    tile.originRectangle.ToRectangle(),
                //    Color.White,
                //    tile.rotation,
                //    //Vector2.Zero,
                //    new Vector2(tile.originRectangle.Width / 2f, tile.originRectangle.Height / 2f),
                //    new Vector2(scaleX, scaleY),
                //    SpriteEffects.None,
                //    0f);
            }

            foreach (var tile in tiles)
            {
                //_spriteBatch.DrawPoint(tile.currentRectangle.X, tile.currentRectangle.Y, Color.Red, 6f);
                _spriteBatch.DrawPoint(tile.centerOriginRectangle.X, tile.centerOriginRectangle.Y, Color.Magenta, 6f);
                _spriteBatch.DrawRectangle(tile.currentRectangle, Color.Blue, 1f);
                //_spriteBatch.DrawRectangle(tile.centerOriginRectangle, Color.Green, 1f);
                //_spriteBatch.DrawPoint(tile.relativeOrigin, Color.Yellow, 2f); 
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
