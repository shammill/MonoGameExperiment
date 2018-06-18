using Engine.Managers;
using Engine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
            //screenManger.Register(new SplashScreen(serviceProvider));
            //screenManger.Register(new MainMenuScreen(serviceProvider));
            //screenManger.Register(new GameScreen(serviceProvider));
        }

        public static void LoadTextures(ContentManager Content)
        {
            var image01 = Content.Load<Texture2D>("01");
        }

        public static void LoadSoundEffects(ContentManager Content, SoundEffectManager soundEffectManager)
        {
            //soundEffectManager.Register(Content.Load<SoundEffect>("01"));

        }

        public static void LoadMusic(ContentManager Content, MusicManager musicManager)
        {
            //musicManager.Register(Content.Load<Song>("01"));
        }
    }
}
