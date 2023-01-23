namespace SnakeGame
{
    public class Scene
    {
        public Scene()
        {
        }

        public void ClearScene()
        {
            Console.Clear();
            GameObjectManager.Instance.Clear();
        }

        protected string _soundName;
        protected string _sceneName;
        protected string _nextSceneName;

        public void SetSoundName(string soundName) => _soundName = soundName;
        public void SetSceneName(string sceneName) => _sceneName = sceneName;
        public void SetNextSceneName(string nextSceneName) => _nextSceneName = nextSceneName;

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

    }
}
