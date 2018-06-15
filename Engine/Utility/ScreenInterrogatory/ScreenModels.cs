using Engine.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility.ScreenModels
{
    public class Monitor
    {
        public Monitor(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public List<Size> Resolutions { get; set; }
        public Size CurrentResolution { get; set; }
        public string ComboName
        {
            get { return Id.ToString() + ". " + FriendlyName; }
        }
    }

    public abstract class WindowOption
    {
        public string Name { get; set; }
        public bool IsFullscreen { get; set; }
        public bool IsBorderless { get; set; }
    }

    public class Fullscreen : WindowOption
    {
        public Fullscreen()
        {
            Name = Local.Fullscreen;
            IsFullscreen = true;
            IsBorderless = true;
        }
    }

    public class Windowed : WindowOption
    {
        public Windowed()
        {
            Name = Local.Windowed;
            IsFullscreen = false;
            IsBorderless = false;
        }
    }

    public class BorderlessWindow : WindowOption
    {
        public BorderlessWindow()
        {
            Name = Local.BorderlessWindow;
            IsFullscreen = false;
            IsBorderless = true;
        }
    }



}
