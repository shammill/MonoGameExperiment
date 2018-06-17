using Engine.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Registrations
{
    public static class Register
    {
        public static void Screens(ScreenManager screenManger, IServiceProvider serviceProvider)
        {
            screenManger.Register(new SplashScreen(serviceProvider));
            screenManger.Register(new MainMenuScreen(serviceProvider));
            screenManger.Register(new GameScreen(serviceProvider));
        }
    }
}
