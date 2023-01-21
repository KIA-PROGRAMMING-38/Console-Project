using System.Diagnostics;

namespace ConsoleGame
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

        public void SetCurrentScene(string sceneName)
        {
            Console.ResetColor();
            Console.Clear();
            bool isSuccess = false;
            Scene Scene;
            isSuccess = _scenes.TryGetValue(sceneName, out Scene);
            Debug.Assert(isSuccess, $"Cant Find Scene / SceneName : {sceneName}");
            _currentScene = Scene;
            _currentScene.Start();
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

        public void AddScene(string sceneName, Scene scene)
        {
            bool isSuccess = false;
            isSuccess = _scenes.TryAdd(sceneName, scene);
            Debug.Assert(isSuccess, $"Cant Add Scene SceneName : {sceneName}");
        }

        public void Start()
        {
            //_currentScene.Start();
        }

        public void Update()
        {
           _currentScene.Update();
        }

        public void Render()
        {
            _currentScene.Render();
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
