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

            Console.SetCursorPosition(GameDataManager.MAP_MIN_X, 1);
            StringBuilder sb = new StringBuilder();
            sb.Append("┌");
            for(int i = 0; i < GameDataManager.MAP_WIDTH-1; ++i)
            {
                sb.Append("─");
            }
            sb.Append("┐\n");
            Console.Write(sb.ToString());
            sb.Clear();

            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_WIDTH / 2) - (_sceneName.Length / 2), 2);
            Console.Write(_sceneName);

            Console.SetCursorPosition(GameDataManager.MAP_MIN_X, 3);
            sb.Append("└");
            for (int i = 0; i < GameDataManager.MAP_WIDTH - 1; ++i)
            {
                sb.Append("─");
            }
            sb.Append("┘\n");
            Console.Write(sb.ToString());
            sb.Clear();

            // todo 수정
            string ui = $"│   {GameDataManager.Instance.CurrentFeedCount:D2} / {GameDataManager.Instance.NeedClearFeedCount:D2}   │";
            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_WIDTH / 2) - ui.Length / 2, 4);
            Console.Write(ui);

            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_WIDTH / 2) - ui.Length / 2, 5);
            sb.Append("└");
            for (int i = 0; i < ui.Length-2; ++i)
            {
                sb.Append("─");
            }
            sb.Append("┘");
            Console.Write(sb.ToString());
            sb.Clear();

            
            string directionString = " 방 향 ";
            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_WIDTH / 2) - directionString.Length/2 - 1, GameDataManager.MAP_MAX_Y + 1);
            Console.Write(directionString);
            Console.SetCursorPosition(GameDataManager.MAP_MIN_X + (GameDataManager.MAP_WIDTH / 2), GameDataManager.MAP_MAX_Y + 3);
            switch (player.GetComponent<PlayerMovement>().MoveDirection)
            {
                case PlayerMovement.Direction.None:
                    break;
                case PlayerMovement.Direction.Left:
                    Console.Write("←");
                    break;
                case PlayerMovement.Direction.Right:
                    Console.Write("→");
                    break;
                case PlayerMovement.Direction.Up:
                    Console.Write("↑");
                    break;
                case PlayerMovement.Direction.Down:
                    Console.Write("↓");
                    break;
            }
            
        }
    }
}
