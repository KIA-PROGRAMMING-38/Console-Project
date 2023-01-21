using ConsoleGame;
using ConsoleGame.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class StageOne : Scene
    {
        public StageOne(string nextScene)
        {
            _nextSceneName = nextScene;
            player = null;
        }

        public GameDataManager.MapInfo mapInfo;
        public GameObject player;

        public override void Start()
        {
            // Load MapData
            mapInfo = GameDataManager.Instance.ReadMapFile("Stage_1");


            // Set Map Data
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


            FeedSpawner.Instance.StartSpawn(mapInfo.MapSpawnableTable, 2000);
            GameObjectManager.Instance.Start();
        }

        public override void Update()
        {
            if(GameDataManager.Instance.NeedClearFeedCount == GameDataManager.Instance.CurrentFeedCount)
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
            string ui = $"{GameDataManager.Instance.CurrentFeedCount :D2} / {GameDataManager.Instance.NeedClearFeedCount:D2}";
            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_MAX_X - GameDataManager.MAP_MIN_X)/2 - ui.Length/2, 5);
            Console.WriteLine(ui);
        }

 
    }
}
