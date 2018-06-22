using Engine.Entities;
using Engine.Graphics;
using Engine.Graphics.Functions;
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

            Size2 tileSize = TileHelper.GetTileSize(_background.Bounds, 10);
            Size2 numberOfPieces = TileHelper.GetTotalNumberOfTiles(_background.Bounds, 10);
            tileOrigins = TileHelper.GetTilePositions(_background.Bounds, numberOfPieces, tileSize);

            if (_background.Bounds.Width > GraphicsDevice.Viewport.Width)
            {
                scaleX = (float)GraphicsDevice.Viewport.Width / (float)_background.Bounds.Width;
                scaleY = (float)GraphicsDevice.Viewport.Height/ (float)_background.Bounds.Height;
            }
            else { 
                scaleX = (float)_background.Bounds.Width / (float)GraphicsDevice.Viewport.Width;
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

                tile.currentRectangle = new RectangleF(scaledXPosition, scaledYPosition, scaledWidth, scaledHeight);
                tiles.Add(tile);
            }

        }

        public override void Update(GameTime gameTime)
        {

            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("MousePosition: " + mouseState.Position.X.ToString() + "-" + mouseState.Position.Y.ToString());
                foreach (var tile in tiles)
                {
                    Console.WriteLine("On Tile: " + tile.currentRectangle.Center.X.ToString() + "-" + tile.currentRectangle.Center.Y.ToString());
                    if (tile.currentRectangle.Contains(mouseState.Position));
                    {
                        Console.WriteLine("I hit a tile:");
                        Console.Write("X:");
                        Console.Write(tile.currentRectangle.X.ToString());
                        Console.Write(" - ");
                        Console.Write("Y:");
                        Console.Write(tile.currentRectangle.Y.ToString());
                        Console.WriteLine("");
                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            foreach (var tile in tiles)
            {
                _spriteBatch.Draw(_background, new Vector2((tile.currentRectangle.X), (tile.currentRectangle.Y)), tile.originRectangle.ToRectangle(), Color.White, 1f, new Vector2(), new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);
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
