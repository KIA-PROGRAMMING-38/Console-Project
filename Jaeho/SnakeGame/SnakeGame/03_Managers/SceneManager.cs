﻿using SnakeGame;
using System.Diagnostics;
using System.Media;

namespace SnakeGame
{
    public class SceneManager : LazySingleton<SceneManager>
    {
        public SceneManager()
        {
            Scenes = new Dictionary<string, Scene>();
            _changeFlag= false;
            _nextScene = string.Empty;
        }

        private bool    _changeFlag;
        private string  _nextScene;
        private Scene   _currentScene;

        public  bool    ChangeFlag { get { return _changeFlag; } }
        

        public readonly Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>();

        /// <summary>
        /// Scene 로딩
        /// </summary>
        public void Load()
        {
            // Scene
            AddScene(sceneName: "TitleScene",   nextSceneName: "Stage_1",       soundName: "Title_bgm",     new TitleScene());
            AddScene(sceneName: "EndingScene",  nextSceneName: "TitleScene",    soundName: "Ending_bgm",    new EndingScene());
            AddScene(sceneName: "DeadScene",    nextSceneName: "TitleScene",    soundName: "Dead_bgm",      new DeadScene());

            // Stage
            AddScene(sceneName: "Stage_1",      nextSceneName: "Stage_2",       soundName: "Stage_1_bgm",   new Stage());
            AddScene(sceneName: "Stage_2",      nextSceneName: "Stage_3",       soundName: "Stage_2_bgm",   new Stage());
            AddScene(sceneName: "Stage_3",      nextSceneName: "Stage_4",       soundName: "Stage_3_bgm",   new Stage());
            AddScene(sceneName: "Stage_4",      nextSceneName: "Stage_5",       soundName: "Stage_4_bgm",   new Stage());
            AddScene(sceneName: "Stage_5",      nextSceneName: "EndingScene",   soundName: "Stage_5_bgm",   new Stage(), windowWidth: 120, windowHeight: 40);
        }

        /// <summary>
        /// 맨 처음 Scene을 설정
        /// </summary>
        /// <param name="sceneName">처음 Scene의 이름</param>
        public void SetStartScene(string sceneName)
        {
            Console.ResetColor();
            Console.Clear();
            bool isSuccess = false;
            Scene Scene;
            isSuccess = Scenes.TryGetValue(sceneName, out Scene);
            Debug.Assert(isSuccess, $"Cant Find Scene / SceneName : {sceneName}");
            _currentScene = Scene;
            GameObjectManager.Instance.Start();
            _currentScene.Start();    
        }

        /// <summary>
        /// Scene 변경 플래그 On
        /// </summary>
        /// <param name="nextScene"></param>
        public void ChangeFlagOn(string nextScene)
        {
            _changeFlag = true;
            _nextScene = nextScene;
        }

        /// <summary>
        /// Scene을 변경해줍니다.
        /// </summary>
        /// <param name="sceneName"></param>
        public void ChangeScene(string sceneName)
        {
            bool isSuccess = false;

            if (_currentScene is not null)
            {
                _currentScene.ClearScene();
            }

            TimeManager.Instance.ResetTimeScale();
            Scene Scene;
            isSuccess = Scenes.TryGetValue(sceneName, out Scene);
            Debug.Assert(isSuccess, "Cant Find Scene ");

            Console.ResetColor();
            Console.Clear();
            _changeFlag = false;
            _currentScene = Scene;

            InputManager.Instance.ResetKey();

            _currentScene.Start();
        }

        /// <summary>
        /// Scene을 추가해줍니다.
        /// </summary>
        /// <param name="sceneName">추가하려는 Scene의 이름</param>
        /// <param name="nextSceneName">다음 Scene의 이름</param>
        /// <param name="scene">씬</param>

        public void AddScene(string sceneName, string nextSceneName, string soundName, Scene scene, int windowWidth = GameDataManager.DEFAULT_SCREEN_WIDTH, int windowHeight = GameDataManager.DEFAULT_SCREEN_HEIGHT)
        {
            bool isSucces = Scenes.TryAdd(sceneName, scene);
            Debug.Assert(isSucces, $"Already Contains Key SceneName: {sceneName}");
            scene.SetSceneName(sceneName);
            scene.SetSoundName(soundName);
            scene.SetNextSceneName(nextSceneName);
            scene.SetScreenSize(windowWidth, windowHeight);
            SoundManager.Instance.AddSound(soundName, new SoundPlayer(Path.Combine(GameDataManager.ResourcePath, "Sound", soundName + ".wav")));
        }

        public void Update()
        {
           _currentScene.Update();
        }

        public void Render()
        {
            _currentScene.Render();

            MapShaker.Instance.RenderShakedMap();

            if (_changeFlag)
            {
                ChangeScene(_nextScene);
            }

        }

        public void Release()
        {
            Scenes.Clear();
        }
    }
}
