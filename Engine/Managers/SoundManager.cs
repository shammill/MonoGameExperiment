using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Engine.Managers
{
    public interface ISoundEffectManager
    {

    }

    public class SoundEffectManager : GameComponent, ISoundEffectManager
    {
        Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        List<SoundEffectInstance> SoundEffectInstances = new List<SoundEffectInstance>();

        public SoundEffectManager(Game game) : base(game)
        {

        }

        public SoundEffect Register(SoundEffect sound)
        {
            Sounds.Add(sound.Name, sound);
            return sound;
        }


        public SoundEffect GetSoundEffect(string soundName)
        {
            SoundEffect sound;
            Sounds.TryGetValue(soundName, out sound);

            return sound;
        }

        public SoundEffectInstance PlaySoundEffect(SoundEffect sound)
        {
            SoundEffectInstance soundEffectInsatance = sound.CreateInstance();
            soundEffectInsatance.Play();

            return soundEffectInsatance;
        }

        public SoundEffectInstance StopSoundInstance(SoundEffectInstance sound)
        {
            sound.Stop();
            sound.Dispose();

            return sound;
        }

        public void DisposeSoundInstances()
        {
            foreach(var soundEffectInsatance in SoundEffectInstances)
            {
                if (soundEffectInsatance.State == SoundState.Stopped);
                {
                    soundEffectInsatance.Dispose();
                }
            }
        }



    }
}
