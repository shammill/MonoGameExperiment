using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Engine.Screens.Transitions;

namespace Engine.Screens
{
    /// <summary>
    /// ScreenManager is responsible for holding a list of screens, updating and drawing them.
    /// Also sets visibility on screens.
    /// </summary>
    public class ScreenManager : SimpleDrawableGameComponent // DrawableGameComponent,
    {
        private Screen _activeScreen;
        private bool _isInitialized;
        private bool _isLoaded;
        private Transition _activeTransition;

        //private readonly List<Screen> _screens;

        //public ScreenManager(Game game, IEnumerable<Screen> screens) : this(game)
        //{
        //    foreach (var screen in screens)
        //        Register(screen);
        //}

        public ScreenManager()
        {
        }

        public void LoadScreen(Screen screen, Transition transition)
        {
            if (_activeTransition != null)
                return;

            _activeTransition = transition;
            _activeTransition.StateChanged += (sender, args) => LoadScreen(screen);
            _activeTransition.Completed += (sender, args) =>
            {
                _activeTransition.Dispose();
                _activeTransition = null;
            };
        }

        public void LoadScreen(Screen screen)
        {
            _activeScreen?.UnloadContent();
            _activeScreen?.Dispose();

            screen.ScreenManager = this;

            if (_isInitialized)
                screen.Initialize();

            if (_isLoaded)
                screen.LoadContent();

            _activeScreen = screen;
        }

        public override void Initialize()
        {
            base.Initialize();
            _activeScreen?.Initialize();
            _isInitialized = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _activeScreen?.LoadContent();
            _isLoaded = true;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _activeScreen?.UnloadContent();
            _isLoaded = false;
        }

        public override void Update(GameTime gameTime)
        {
            _activeScreen?.Update(gameTime);
            _activeTransition?.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _activeScreen?.Draw(gameTime);
            _activeTransition?.Draw(gameTime);
        }
    }
}