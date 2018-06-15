using System;
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
            Application.Run(new SystemSettingForm());

            using (var game = new GameMain())
                game.Run();
        }
    }
}
