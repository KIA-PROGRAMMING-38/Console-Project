using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Stage : Scene
    {
        public GameDataManager.MapInfo mapInfo;
        public GameObject player;

        public override void Start()
        {
            // Load MapData
            mapInfo = GameDataManager.Instance.GetMapData(_sceneName);

            player = new Player();
            player.Position = mapInfo.PlayerPosition;

            List<GameObject> walls = new List<GameObject>();

            for (int i = 0; i < mapInfo.WallPosisions.Length; ++i)
            {
                Wall wall = new Wall();
                wall.Position = mapInfo.WallPosisions[i];
                walls.Add(wall);
            }

            FeedSpawner.Instance.StartSpawn(mapInfo.MapSpawnableTable, 2000);
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

            //todo 수정
            string ui = $"{GameDataManager.Instance.CurrentFeedCount:D2} / {GameDataManager.Instance.NeedClearFeedCount:D2}";
            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_MAX_X - GameDataManager.MAP_MIN_X) / 2 - ui.Length / 2, 5);
            Console.WriteLine(ui);
        }
    }
}
