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

namespace Engine.Screens
{
    public class MainMenuScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        MouseState oldMouseState;
        List<string> menuItems;
        float centerPoint = 0f;
        BitmapFont Font;


        public MainMenuScreen(Game game) : base(game)
        {
            menuItems = new List<string>();
            centerPoint = GraphicsDevice.Viewport.Width / 2f;
            Font = Content.Load<BitmapFont>("Fonts/montserrat-32");
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("MenuBackground");
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
            _spriteBatch.DrawString(Font, "Test", new Vector2(centerPoint, 500), Color.White);
            _spriteBatch.End();
        }

        private void HandleInput()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                ScreenManager.LoadScreen(new MainMenuScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 2f));
            }

            oldMouseState = mouseState;
        }
    }
}
