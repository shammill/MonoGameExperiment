using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers
{
    public class GameManager : GameComponent
    {

        public enum GameStateEnum
        {
            SplashScreen = 0,
            MainMenuScreen = 1,
            SettingsScreen = 2,
            GameScreen = 4,

            PauseMenu = 128
        }

        public GameStateEnum GameState { get; set; }
        public bool IsPaused { get; set; }
        public bool IsLoading { get; set; }

        public GameManager(Game game) : base(game)
        {
            IsLoading = true;
        }

        public void ChangeGameState(GameStateEnum gameState)
        {
            GameState = gameState;



        }

    }
}
