namespace ConsoleGame
{
    public class StageTwo : Scene
    {
        public StageTwo(string nextScene) 
        {
            _nextSceneName = nextScene;
            player = null;
        }

        public GameDataManager.MapInfo mapInfo;
        public GameObject player;

        public override void Start() 
        {
            mapInfo = GameDataManager.Instance.ReadMapFile("Stage_2");
            GameDataManager.Instance.SetData(mapInfo);

            player = new Player();
            player.Position = mapInfo.PlayerPosition;
            List<GameObject> walls = new List<GameObject>();
            for (int i = 0; i < mapInfo.WallPosisions.Length; ++i)
            {
                Wall wall = new Wall();
                wall.Position = mapInfo.WallPosisions[i];
                walls.Add(wall);
            }

            FeedSpawner.Instance.StartSpawn(mapInfo.MapSpawnableTable, 3000);
            GameObjectManager.Instance.Start();
        }

        public override void Update() 
        {
            if (GameDataManager.Instance.NeedClearFeedCount == GameDataManager.Instance.CurrentFeedCount)
            {
                SceneManager.Instance.ChangeFlagOn(_nextSceneName);
            }
            GameObjectManager.Instance.Update();
            FeedSpawner.Instance.Update();
        }

        public override void Render() 
        {
            RenderManager.Instance.Render();
        }
    }
}
