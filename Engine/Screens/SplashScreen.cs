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

namespace Engine.Screens
{
    public class SplashScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _background;

        public SplashScreen(Game game) : base(game)
        {
            
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("01");
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            if (keyboardState.WasKeyJustDown(Keys.Escape))
                Game.Exit();

            if (mouseState.LeftButton == ButtonState.Pressed || keyboardState.WasAnyKeyJustDown())
                ScreenManager.LoadScreen(new MainMenuScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 2f));
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.End();
        }
    }
}
