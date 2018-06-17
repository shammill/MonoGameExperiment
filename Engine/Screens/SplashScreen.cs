using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Screens
{
    public class SplashScreen : Screen
    {
        private readonly IServiceProvider _serviceProvider;
        private SpriteBatch _spriteBatch;

        public SplashScreen(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
    }
}
