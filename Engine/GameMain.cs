using Engine.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Engine.Localization;
using Engine.Utility;
using Engine.Screens;
using Engine.Managers;
using Engine.Registrations;

namespace Engine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        SoundEffectManager soundEffectManager;
        MusicManager musicManger;
        GameManager gameManager;

        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowAltF4 = true;
            Window.Title = MainWindowLocal.WindowTitle;
            Window.AllowUserResizing = false;
            Window.IsBorderless = SystemSettings.Default.Video_IsBorderless;

            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = SystemSettings.Default.Video_IsFullscreen,
                PreferredBackBufferWidth = SystemSettings.Default.Video_Resolution.Width,
                PreferredBackBufferHeight = SystemSettings.Default.Video_Resolution.Height,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false, // Enables anti-aliasing.
                PreferredDepthStencilFormat = DepthFormat.None,
                SynchronizeWithVerticalRetrace = true,

                // This tells MonoGame to not switch the mode of the graphics-card directly, but to scale the window.
                HardwareModeSwitch = false,

                SupportedOrientations = DisplayOrientation.Default,
                GraphicsProfile = GraphicsProfile.HiDef, // need to support textures over 2048x2048? HiDef otherwise Reach. Reach DX9 vs HiDef DX10.
            };

            soundEffectManager = new SoundEffectManager(this);
            Components.Add(soundEffectManager);

            musicManger = new MusicManager(this);
            Components.Add(musicManger);

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            gameManager = new GameManager(this);
            Components.Add(gameManager);

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
            Register.Screens(screenManager, this.Services);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Register.LoadTextures(Content);
            Register.LoadSoundEffects(Content, soundEffectManager);
            Register.LoadMusic(Content, musicManger);
            //var _sprite = new Sprite(image01)
            //{
            //    Position = viewportAdapter.Center.ToVector2()
            //};
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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

            base.Update(gameTime);
            screenManager.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var matrix = Matrix.CreateScale(
                           (float)GraphicsDevice.Viewport.Width / 1920,
                           (float)GraphicsDevice.Viewport.Width / 1920,
                           1f);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            screenManager.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, matrix);

            //spriteBatch.DrawString(font, "test", new Vector2(100, 150), Color.Black);
            spriteBatch.Draw(image01, new Rectangle(0, 0, 1980, 1080), Color.White);
            spriteBatch.End();
        }
    }
}
