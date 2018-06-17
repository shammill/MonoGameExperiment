using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Engine.Utility.Extensions;

namespace Engine.Managers
{
    /// <summary>
    /// Holds a SongCollection and is responsible for accessing the MediaPlayer to play songs
    /// </summary>
    public class MusicManager : GameComponent
    {
        SongCollection songs;

        public MusicManager(Game game) : base(game)
        {
            MediaPlayer.Volume = Settings.SystemSettings.Default.Audio_MusicVolume.PercentageToFloat();
        }

        public void Register(Song song)
        {
            songs.Add(song);
        }

        public void UpdateVolume(int percentage)
        {
            MediaPlayer.Volume = Settings.SystemSettings.Default.Audio_MusicVolume.PercentageToFloat();
        }

        public void UpdateVolume(float percentage)
        {
            MediaPlayer.Volume = percentage;
        }

    }
}
