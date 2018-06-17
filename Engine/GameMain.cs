﻿using Engine.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Localization;
using Engine.Utility;
using Engine.Screens;

namespace Engine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowAltF4 = true;
            Window.Title = MainWindowLocal.WindowTitle;
            Window.AllowUserResizing = false;
            Window.IsBorderless = SystemSettings.Default.Video_IsBorderless;
            //Window.Position = SystemSettings.Default.Video_Location.ToXnaPoint();

            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = SystemSettings.Default.Video_IsFullscreen,
                PreferredBackBufferWidth = SystemSettings.Default.Video_Resolution.Width,
                PreferredBackBufferHeight = SystemSettings.Default.Video_Resolution.Height,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false,
                PreferredDepthStencilFormat = DepthFormat.None,
                SynchronizeWithVerticalRetrace = true,

                // This tells MonoGame to not switch the mode of the graphics-card directly, but to scale the window.
                HardwareModeSwitch = false,

                SupportedOrientations = DisplayOrientation.Default,
                GraphicsProfile = GraphicsProfile.HiDef, // need to support textures over 2048x2048? HiDef otherwise Reach. Reach DX9 vs HiDef DX10.
            };

            ScreenGameComponent screenGameComponent = new ScreenGameComponent(this);
            screenGameComponent.Register(new MyScreen());
            Components.Add(screenGameComponent);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
