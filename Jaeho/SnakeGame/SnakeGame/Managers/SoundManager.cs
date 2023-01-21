using System.Media;


namespace ConsoleGame
{
    public class SoundManager : Singleton<SoundManager>
    {
        public SoundManager()
        {

            _soundPlayers = new Dictionary<string, SoundPlayer>();


            string resourcePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //AddSound("TitleBackgroundMusic", new SoundPlayer(Path.Combine("Assets", "Sound", "MyDearest.wav")));
            //AddSound("TitleBackgroundMusic", new SoundPlayer(Path.Combine("Assets", "Sound", "Nostalgia.wav")));
            AddSound("TitleBackgroundMusic",  new SoundPlayer(Path.Combine(resourcePath,"Assets", "Sound", "쇄월.wav")));
            AddSound("EndingBackgroundMusic", new SoundPlayer(Path.Combine(resourcePath, "Assets", "Sound", "우마우마.wav")));
            
        }

        private SoundPlayer _currentSoundName;

        public void AddSound(string name, SoundPlayer sound)
        {
            _soundPlayers.Add(name, sound);
            sound.Load();
        }
        public void Play(string name, bool loop = false)
        {
            if (loop)
            {
                _soundPlayers[name].PlayLooping();
            }
            else
            {
                _soundPlayers[name].Play();
            }
        }

        public void EffectSoundPlay(string name)
        { 
            Task.Factory.StartNew(() => { _soundPlayers[name].Play(); });
        }

        public void Stop(string name)
        {
            _soundPlayers[name].Stop();
        }

        public void StopAll()
        {
            foreach (var sound in _soundPlayers)
            {
                sound.Value.Stop();
            }
        }
        private Dictionary<string, SoundPlayer> _soundPlayers;

        public void Release()
        {
            foreach(var sound in _soundPlayers.Values)
            {
                sound.Dispose();
            }
        }
    }
}
