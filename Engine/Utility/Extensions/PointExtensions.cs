using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Engine.Utility
{
    public static class PointExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Microsoft.Xna.Framework.Point ToXnaPoint(this System.Drawing.Point point)
        {
            return new Microsoft.Xna.Framework.Point(point.X, point.Y);
        }

        public static Microsoft.Xna.Framework.Point GetCenter()
        {
            var selectedMonitor = Screen.AllScreens.Where(x => x.DeviceName == Settings.SystemSettings.Default.Video_Monitor).FirstOrDefault();

            if (selectedMonitor == null)
            {
                selectedMonitor = Screen.PrimaryScreen;
            }

            var X = selectedMonitor.Bounds.Size.Width / 2;
            var Y = selectedMonitor.Bounds.Size.Height / 2;

            return new Microsoft.Xna.Framework.Point(X, Y);
        }
    }
}
