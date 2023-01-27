namespace SnakeGame
{
    public class Scene
    {
        public Scene(  )
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
        protected int _windowWidth = GameDataManager.DEFAULT_SCREEN_WIDTH;
        protected int _windowHeight = GameDataManager.DEFAULT_SCREEN_HEIGHT;

        public void SetSoundName(string soundName) => _soundName = soundName;
        public void SetSceneName(string sceneName) => _sceneName = sceneName;
        public void SetNextSceneName(string nextSceneName) => _nextSceneName = nextSceneName;

        public void SetScreenSize(int width, int height)
        {
            _windowWidth = width;
            _windowHeight = height;
        }

        public virtual void Start()
        {
            Console.SetWindowSize( _windowWidth, _windowHeight );
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

    }
}
