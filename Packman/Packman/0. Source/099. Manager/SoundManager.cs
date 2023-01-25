using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class SoundManager : SingletonBase<SoundManager>
    {
        private Dictionary<string, SoundPlayer> _sounds = new Dictionary<string, SoundPlayer>();

        public bool AddSound( string soundID, string filePath )
        {
            if ( null != FindSound( soundID ) )
            {
                return false;
            }

            SoundPlayer sound = new SoundPlayer(filePath);
            sound.Load();

            _sounds.Add( soundID, sound );

            return true;
        }

        public void Play(string soundID, bool _isLooping = false )
        {
            SoundPlayer sound = FindSound(soundID);
            if(null != sound)
            {
                if( _isLooping )
                {
                    sound.PlayLooping();
                }
                else
                {
                    sound.Play();
                }
            }
        }

        public void Stop(string soundID)
        {
            SoundPlayer sound = FindSound(soundID);
            if ( null != sound )
            {
                sound.Stop();
            }
        }

        public SoundPlayer FindSound(string soundID)
        {
            SoundPlayer findSoundPlayer = null;

            _sounds.TryGetValue( soundID, out findSoundPlayer );

            return findSoundPlayer;
        }
    }
}
