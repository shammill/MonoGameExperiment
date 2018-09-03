using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine.Screens.Transitions;
using Microsoft.Xna.Framework.Input;
using Engine.Input;
using Engine.BitmapFonts;
using Engine.Graphics.Fonts;
using Engine.Graphics;

namespace Engine.Screens
{
    public class MainMenuScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        private List<BitmapFont> _fontList = new List<BitmapFont>();

        MouseState oldMouseState;
        List<string> menuItems;


        Point centerPoint = new Point();

        // Font Goodness
        FontPackage fontPackage = new FontPackage();
        float proposedTextHeight = 0.1f; // percentage of scereen height 0.1f = 10%;

        private Textbox newGame = new Textbox() { Location = new Vector2(), Value = "NEW GAME" };
        private Textbox loadGame = new Textbox() { Location = new Vector2(), Value = "LOAD GAME" };
        private Textbox options = new Textbox() { Location = new Vector2(), Value = "OPTIONS" };
        private Textbox exit = new Textbox() { Location = new Vector2(), Value = "EXIT" };


        public MainMenuScreen(Game game) : base(game)
        {
            menuItems = new List<string>();

            centerPoint.X = (int)(GraphicsDevice.Viewport.Width / 2f);
            centerPoint.Y = (int)(GraphicsDevice.Viewport.Height / 2f);           
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("Images/MenuBackground");

            //Load font range for scaling.
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-20"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-22"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-24"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-26"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-28"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-30"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-32"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-34"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-36"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-38"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-40"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-42"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-44"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-46"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-48"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-50"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-52"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-54"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-56"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-58"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-60"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-62"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-64"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-66"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-68"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-70"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-72"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-74"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-76"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-78"));

            fontPackage = FontHelper.GetFont(_fontList, proposedTextHeight, GraphicsDevice.Viewport.Bounds);

            RectangleF newGameRectangle = fontPackage.Font.GetStringRectangle(newGame.Value);
            float centerPointNewGame = GraphicsDevice.Viewport.Bounds.Width / 2f  - (newGameRectangle.Width / 2f);
            float verticalPointNewGame = GraphicsDevice.Viewport.Bounds.Height * 0.25f;
            newGame.Location = new Vector2(centerPointNewGame, verticalPointNewGame);

            RectangleF loadGameRectangle = fontPackage.Font.GetStringRectangle(loadGame.Value);
            float centerPointLoadGame = GraphicsDevice.Viewport.Bounds.Width / 2f - (loadGameRectangle.Width / 2f);
            float verticalPointLoadGame = verticalPointNewGame + (newGameRectangle.Height * 1.5f);
            loadGame.Location = new Vector2(centerPointLoadGame, verticalPointLoadGame);

            RectangleF optionsRectangle = fontPackage.Font.GetStringRectangle(options.Value);
            float centerPointOptions = GraphicsDevice.Viewport.Bounds.Width / 2f - (optionsRectangle.Width / 2f);
            float verticalPointOptions = verticalPointLoadGame + (newGameRectangle.Height * 1.5f);
            options.Location = new Vector2(centerPointOptions, verticalPointOptions);

            RectangleF exitRectangle = fontPackage.Font.GetStringRectangle(exit.Value);
            float centerPointExit = GraphicsDevice.Viewport.Bounds.Width / 2f - (exitRectangle.Width / 2f);
            float verticalPointExit = verticalPointOptions + (newGameRectangle.Height * 1.5f);
            exit.Location = new Vector2(centerPointExit, verticalPointExit);

        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //_spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.DrawString(fontPackage.Font, newGame.Value, newGame.Location, Color.White, 0f, Vector2.Zero, fontPackage.Scale, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(fontPackage.Font, loadGame.Value, loadGame.Location, Color.White, 0f, Vector2.Zero, fontPackage.Scale, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(fontPackage.Font, options.Value, options.Location, Color.White, 0f, Vector2.Zero, fontPackage.Scale, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(fontPackage.Font, exit.Value, exit.Location, Color.White, 0f, Vector2.Zero, fontPackage.Scale, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }

        private void HandleInput()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                ScreenManager.LoadScreen(new TileGameScreen(Game, new Entities.SwapperGameSettings()), new FadeTransition(GraphicsDevice, Color.Black, 2f));
            }

            oldMouseState = mouseState;
        }
    }
}
