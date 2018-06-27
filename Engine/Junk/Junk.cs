using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Junk
{
    public class Junk
    {
        Game _game = new Game();
        SpriteBatch _spriteBatch;
        private Texture2D _background;
        private Texture2D _shape;


        public Junk()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _background = _game.Content.Load<Texture2D>("01");
            _shape = _game.Content.Load<Texture2D>("shape");
        }

        public void RenderToShape()
        {
            //Create your RenderTarget to be the size and shape you want 
            RenderTarget2D newImage = new RenderTarget2D(_game.GraphicsDevice, 250, 250, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);

            //Tell the graphics device that instead of drawing to the screen 
            //you want it to draw onto this new RenderTarget you just created 
            _game.GraphicsDevice.SetRenderTarget(newImage);
            _game.GraphicsDevice.Clear(Color.Transparent);

            //Create your alpha effect to be passed into the SpriteBatch 
            //Note that you ideally want to create this as a static object that always exists 
            //if you plan on reusing it often 
            AlphaTestEffect alphaEffect = new AlphaTestEffect(_game.GraphicsDevice);
            alphaEffect.AlphaFunction = CompareFunction.Greater;
            alphaEffect.ReferenceAlpha = 0;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, _game.GraphicsDevice.PresentationParameters.BackBufferWidth, _game.GraphicsDevice.PresentationParameters.BackBufferHeight, 0, 0, 1);
            alphaEffect.Projection = projection;

            //Create your Stencil Effect 
            DepthStencilState stencilStateBefore = new DepthStencilState();
            stencilStateBefore.StencilEnable = true;
            stencilStateBefore.ReferenceStencil = 1;
            stencilStateBefore.StencilFunction = CompareFunction.Always;
            stencilStateBefore.StencilPass = StencilOperation.Replace;

            //Tell the Spritebatch you want to start drawing and pass in stencil and alpha 
            //state objects you just created so it knows exactly HOW you want it to behave 
            //when it draws. 
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, stencilStateBefore, null, alphaEffect);

            //Draw the shape you want to "cut out" of your original image. 
            _spriteBatch.Draw(_shape, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            _spriteBatch.End();

            //Next create the Stencil Mask for drawing your original image onto the 
            //rendertarget. 
            DepthStencilState stencilMaskAfter = new DepthStencilState();
            stencilMaskAfter.StencilEnable = true;
            stencilMaskAfter.StencilFunction = CompareFunction.Equal;
            stencilMaskAfter.ReferenceStencil = 1;
            stencilMaskAfter.StencilPass = StencilOperation.Keep;

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, stencilMaskAfter, null, alphaEffect);

            //Draw your original image, because of how you setup the SpriteBatch the  
            //image will only be drawn to where there are pixels on the rendertarget 
            //from where you drew the first image. In effect, cutting out your image 
            //into the shape you wanted. 
            _spriteBatch.Draw(_background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            _spriteBatch.End();

            //Set the graphics device back to drawing on the screen 
            _game.GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();
            _spriteBatch.Draw(newImage, new Vector2(500, 500));
            _spriteBatch.End();

        }
    }
}
