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
        //protected BitmapFont Font { get; private set; }

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
            //Font = Content.Load<BitmapFont>("Fonts/montserrat-32");
        }


        private MouseState _previousState;

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var isPressed = mouseState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed;

            //foreach (var menuItem in MenuItems)
            //{
            //    var isHovered = menuItem.BoundingRectangle.Contains(new Point2(mouseState.X, mouseState.Y));

            //    menuItem.Color = isHovered ? Color.Yellow : Color.White;

            //    if (isHovered && isPressed)
            //    {
            //        menuItem.Action?.Invoke();
            //        break;
            //    }
            //}

            _previousState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            //foreach (var menuItem in MenuItems)
            //    menuItem.Draw(_spriteBatch);

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
