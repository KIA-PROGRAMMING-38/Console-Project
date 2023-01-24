using SnakeGame;
using System.Diagnostics;

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

        public  bool    _changeFlag;
        public  string  _nextScene;
        private Scene   _currentScene;

        public readonly Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>();

        /// <summary>
        /// Scene 로딩
        /// </summary>
        public void Load()
        {
            AddScene(sceneName: "TitleScene",  nextSceneName : "Stage_1",    soundName: "TitleBackgroundMusic", new TitleScene());
            AddScene(sceneName: "EndingScene", nextSceneName : "TitleScene", soundName: "EndingBackgroundMusic", new EndingScene());
            AddScene(sceneName: "DeadScene",   nextSceneName : "TitleScene", soundName: "DeadBackgroundMusic", new DeadScene());

            AddScene(sceneName: "Stage_1", nextSceneName: "Stage_2",    soundName: "Stage_1", new Stage());
            AddScene(sceneName: "Stage_2", nextSceneName: "Stage_3",    soundName: "Stage_2", new Stage());
            AddScene(sceneName: "Stage_3", nextSceneName: "Stage_4",    soundName: "Stage_3", new Stage());
            AddScene(sceneName: "Stage_4", nextSceneName: "TitleScene", soundName: "Stage_4", new Stage());

            SetStartScene("TitleScene");
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
            _currentScene.Start();
            GameObjectManager.Instance.Start();
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
        public void AddScene(string sceneName,string nextSceneName, string soundName, Scene scene)
        {

            bool isSucces = Scenes.TryAdd(sceneName, scene);
            Debug.Assert(isSucces, $"Already Contains Key SceneName: {sceneName}");
            scene.SetSceneName(sceneName);
            scene.SetSoundName(soundName);
            scene.SetNextSceneName(nextSceneName);
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
