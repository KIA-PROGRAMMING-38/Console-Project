namespace SnakeGame
{
    public class Scene
    {
        public Scene()
        {
        }
        public void ClearScene()
        {
            GameObjectManager.Instance.Clear();
        }

        protected string _sceneName;
        protected string _nextSceneName;
        public void SetSceneName(string sceneName)
        {
            _sceneName = sceneName;
        }
        public void SetNextSceneName(string nextSceneName)
        {
            _nextSceneName = nextSceneName;
        }
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
