﻿using Engine.Graphics;
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
        List<RectangleF> tiles;

        public MainMenuScreen(Game game) : base(game)
        {

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

            Size2 tileSize = TileHelper.GetTileSize(_background.Bounds, 4);
            Size2 numberOfPieces = TileHelper.GetTotalNumberOfTiles(_background.Bounds, 4);
            tiles = TileHelper.GetTilePositions(_background.Bounds, numberOfPieces, tileSize);

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            foreach (var tile in tiles)
            {
                _spriteBatch.Draw(_background, new Vector2(tile.X, tile.Y), tile.ToRectangle(), Color.White);
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
