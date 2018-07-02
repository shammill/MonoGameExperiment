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
using Engine.Graphics;

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
        float textHalfLenth;
        float oneThirdScreenSize = 500f;
        float newScale = 1f;
        float scaledWidth = 500f;


        public MainMenuScreen(Game game) : base(game)
        {
            menuItems = new List<string>();
            centerPoint = GraphicsDevice.Viewport.Width / 2f;
            oneThirdScreenSize = GraphicsDevice.Viewport.Width / 3f;
            Font = Content.Load<BitmapFont>("Fonts/montserrat-32");
            newScale = oneThirdScreenSize / Font.GetStringRectangle("NEW GAME").Width;
            textHalfLenth = (Font.GetStringRectangle("NEW GAME").Width * newScale) / 2f;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("Images/MenuBackground");
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
            _spriteBatch.DrawString(Font, "NEW GAME", new Vector2(centerPoint - textHalfLenth, 500), Color.White, 0f, Vector2.Zero, newScale, SpriteEffects.None, 0f);
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
