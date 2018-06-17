using Engine.Utility.ScreenModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Winforms Launcher for configuring system settings
            Application.Run(new SystemSettingForm());

            // When SysSettings closes, run game main
            using (var game = new GameMain())
                game.Run();
        }
    }
}
