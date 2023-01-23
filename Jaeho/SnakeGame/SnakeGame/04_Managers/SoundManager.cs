using System.Media;


namespace SnakeGame
{
    public class SoundManager : LazySingleton<SoundManager>
    {
        public SoundManager()
        {

            _soundPlayers = new Dictionary<string, SoundPlayer>();


            //AddSound("TitleBackgroundMusic", new SoundPlayer(Path.Combine("Assets", "Sound", "MyDearest.wav")));
            //AddSound("TitleBackgroundMusic", new SoundPlayer(Path.Combine("Assets", "Sound", "Nostalgia.wav")));
            AddSound("CollisionSound",        new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "CollisionSound.wav")));
            AddSound("TitleBackgroundMusic",  new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "쇄월.wav")));
            AddSound("EndingBackgroundMusic", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "우마우마.wav")));
            
        }

        public void Load()
        {
            foreach(var sound in _soundPlayers.Values) 
            {
                sound.Load();
            }
        }

        public void AddSound(string name, SoundPlayer sound)
        {
            _soundPlayers.Add(name, sound);
        }

        public void Play(string name, bool loop = false)
        {
            if (!loop)
            {
                _soundPlayers[name].Play();
            }
            else
            {
                _soundPlayers[name].PlayLooping();
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
