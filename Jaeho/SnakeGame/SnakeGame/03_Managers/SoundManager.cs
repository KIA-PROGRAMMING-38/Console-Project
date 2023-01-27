using System.Diagnostics;
using System.Media;


namespace SnakeGame
{
    public class SoundManager : LazySingleton<SoundManager>
    {
        public SoundManager()
        {
            _soundPlayers = new Dictionary<string, SoundPlayer>();
        }

        private Dictionary<string, SoundPlayer> _soundPlayers;

        /// <summary>
        /// 등록한 사운드들을 로드해줍니다.
        /// </summary>
        public void Load()
        {
            foreach(SoundPlayer sound in _soundPlayers.Values) 
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
            bool isSuccess = _soundPlayers.TryAdd(name, sound);
            Debug.Assert(isSuccess, $"Cant add sound => Sound Name : {name}");
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
            foreach (SoundPlayer sound in _soundPlayers.Values)
            {
                sound.Stop();
            }
        }

        public void Release()
        {
            foreach(SoundPlayer sound in _soundPlayers.Values)
            {
                sound.Dispose();
            }
        }
    }
}
