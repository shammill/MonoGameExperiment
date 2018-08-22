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
        string newGameText = "NEW GAME";
        float newGameTextHalfLenth = 0f;
        string loadGameText = "LOAD GAME";
        string optionsText = "OPTIONS";
        string exitText = "EXIT";


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
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-28"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-30"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-32"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-34"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-36"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-38"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-40"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-42"));
            _fontList.Add(Content.Load<BitmapFont>("Fonts/arial-44"));

            fontPackage = FontHelper.GetFont(_fontList, proposedTextHeight, GraphicsDevice.Viewport.Bounds);

            RectangleF newGameRectangle = fontPackage.Font.GetStringRectangle(newGameText);
            newGameTextHalfLenth = newGameRectangle.Width / 2f;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.DrawString(fontPackage.Font, newGameText, new Vector2(centerPoint.X - newGameTextHalfLenth, centerPoint.Y), Color.White, 0f, Vector2.Zero, fontPackage.Scale, SpriteEffects.None, 0f);
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
