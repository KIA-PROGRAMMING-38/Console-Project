using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class StageTwo : Scene
    {
        public StageTwo(string nextScene) 
        {
            _nextSceneName = nextScene;
            mapInfo = GameDataManager.Instance.ReadMapFile("Stage_2");
            GameDataManager.Instance.SetMapInfo(mapInfo);
        }

        public GameDataManager.MapInfo mapInfo;
        public GameObject player;

        public override void Start() 
        {
            player = new Player();
            player.Position = mapInfo.PlayerPosition;
            List<GameObject> walls = new List<GameObject>();
            for (int i = 0; i < mapInfo.WallPosisions.Length; ++i)
            {
                Wall wall = new Wall();
                wall.Position = mapInfo.WallPosisions[i];
                walls.Add(wall);
            }
            GameObjectManager.Instance.Start();
        }

        public override void Update() 
        {
            GameObjectManager.Instance.Update();
        }

        public override void Render() 
        {
            RenderManager.Instance.Render();
        }
    }
}
