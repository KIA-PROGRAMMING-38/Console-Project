using System.Media;


namespace SnakeGame
{
    public class SoundManager : LazySingleton<SoundManager>
    {
        public SoundManager()
        {

            _soundPlayers = new Dictionary<string, SoundPlayer>();


            AddSound("CollisionSound",        new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "CollisionSound.wav")));
            AddSound("TitleBackgroundMusic",  new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "배드애플.wav")));
            AddSound("EndingBackgroundMusic", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "우마우마.wav")));
            AddSound("DeadBackgroundMusic", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "DeadSceneSound.wav")));
            AddSound("Stage_1", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "dropkick.wav")));
            AddSound("Stage_2", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "Nostalgia.wav")));
            AddSound("Stage_3", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "쇄월.wav")));
            AddSound("Stage_4", new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", "MyDearest.wav")));
        }

        private Dictionary<string, SoundPlayer> _soundPlayers;

        /// <summary>
        /// 등록한 사운드들을 로드해줍니다.
        /// </summary>
        public void Load()
        {
            foreach(var sound in _soundPlayers.Values) 
            {
                sound.Load();
            }
        }

        /// <summary>
        /// 사운드를 추가해줍니다.
        /// </summary>
        /// <param name="name">사운드 이름</param>
        /// <param name="sound">사운드 객체</param>
        public void AddSound(string name, SoundPlayer sound)
        {
            _soundPlayers.Add(name, sound);
        }

        /// <summary>
        /// 이름이 일치하는 사운드플레이어를 Play해줍니다.
        /// </summary>
        /// <param name="name">플레이 할 사운드 이름</param>
        /// <param name="loop">반복여부</param>
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

        /// <summary>
        /// 이름이 일치하는 사운드플레이어를 Play해줍니다.
        /// </summary>
        /// <param name="name">플레이 할 사운드 이름</param>
        /// <param name="loop">반복여부</param>
        public void PlaySync(string name, bool loop = false)
        {
            if (!loop)
            {
                _soundPlayers[name].PlaySync();
            }
            else
            {
                _soundPlayers[name].PlayLooping();
            }
        }

        /// <summary>
        /// 이름이 일치하는 사운드플레이어를 Stop해줍니다.
        /// </summary>
        /// <param name="name"></param>
        public void Stop(string name)
        {
            _soundPlayers[name].Stop();
        }
        
        /// <summary>
        /// 모든 사운드플레이어 Stop
        /// </summary>
        public void StopAll()
        {
            foreach (var sound in _soundPlayers)
            {
                sound.Value.Stop();
            }
        }

        public void Release()
        {
            foreach(var sound in _soundPlayers.Values)
            {
                sound.Dispose();
            }
        }
    }
}
