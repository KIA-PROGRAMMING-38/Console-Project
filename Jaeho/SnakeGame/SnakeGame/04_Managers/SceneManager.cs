using SnakeGame;
using System.Diagnostics;

namespace SnakeGame
{
    public class SceneManager : Singleton<SceneManager>
    {
        public SceneManager()
        {
            _scenes = new Dictionary<string, Scene>();
            _changeFlag= false;
            _nextScene = string.Empty;
        }

        public  bool    _changeFlag;
        public  string  _nextScene;
        private Scene   _currentScene;

        private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

        public void LoadScenes()
        {
            AddScene(sceneName: "TitleScene",  nextSceneName : "Stage_1", new TitleScene());
            AddScene(sceneName: "EndingScene", nextSceneName : "TitleScene", new EndingScene());
            AddScene(sceneName: "DeadScene",   nextSceneName : "TitleScene",new EndingScene());

            AddScene(sceneName: "Stage_1", nextSceneName: "Stage_2", new Stage());
            AddScene(sceneName: "Stage_2", nextSceneName: "Stage_3", new Stage());

            SetStartScene("TitleScene");
        }

        public void SetStartScene(string sceneName)
        {
            Console.ResetColor();
            Console.Clear();
            bool isSuccess = false;
            Scene Scene;
            isSuccess = _scenes.TryGetValue(sceneName, out Scene);
            Debug.Assert(isSuccess, $"Cant Find Scene / SceneName : {sceneName}");
            _currentScene = Scene;
            _currentScene.Start();
            GameObjectManager.Instance.Start();
        }

        public void ChangeFlagOn(string nextScene)
        {
            _changeFlag = true;
            _nextScene = nextScene;
        }

        public void ChangeScene(string sceneName)
        {
            bool isSuccess = false;

            if (_currentScene is not null)
            {
                _currentScene.ClearScene();
            }

            Scene Scene;
            isSuccess = _scenes.TryGetValue(sceneName, out Scene);
            Debug.Assert(isSuccess, "Cant Find Scene ");
            Console.ResetColor();
            Console.Clear();
            _changeFlag = false;
            _currentScene = Scene;
            _currentScene.Start();
        }

        /// <summary>
        /// Scene을 추가해줍니다.
        /// </summary>
        /// <param name="sceneName">추가하려는 Scene의 이름</param>
        /// <param name="nextSceneName">다음 Scene의 이름</param>
        /// <param name="scene">씬</param>
        public void AddScene(string sceneName,string nextSceneName, Scene scene)
        {

            bool isSucces = _scenes.TryAdd(sceneName, scene);
            Debug.Assert(isSucces, $"Already Contains Key SceneName: {sceneName}");
            scene.SetSceneName(sceneName);
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
            _scenes.Clear();
        }
    }
}
