using System.Drawing;
using System.Media;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleGame
{
    enum Scene
    {
        None,
        Title,
        Story,
        Town,
        RaceTrack,
        Shop,
        Exit
    }

    internal class SceneManager
    {
        public static Scene _sceneType;
        public static Scene _prevSceneType;

        public static string[] LoadScene(string scene)
        {
            // 1. 경로를 구성한다.
            string sceneFilePath = Path.Combine("Assets", "MapTile", $"{scene}.txt");

            // 2. 파일이 존재하는지 확인한다.
            if (false == File.Exists(sceneFilePath))
            {
                GameManager.ExitWithError($"Scene파일이 없습니다. Scene파일 이름 : {scene}");
            }

            // 3. 파일의 내용을 불러온다.
            return File.ReadAllLines(sceneFilePath);
        }

        public static void ParseTownScene(string[] scene, out Player player, out Wall[] walls, out Npc[] npcs, out Dialog[] dialogs)
        {
            string[] sceneMetaData = scene[scene.Length - 1].Split(" ");
            player = null;
            walls = new Wall[int.Parse(sceneMetaData[0])];
            int wallCounts = 0;
            dialogs = new Dialog[int.Parse(sceneMetaData[1])];
            int dialogCounts = 0;
            npcs = new Npc[int.Parse(sceneMetaData[2])];
            int npcCounts = 0;

            for (int y = 0; y < scene.Length - 1; ++y)
            {
                for (int x = 0; x < scene[y].Length; ++x)
                {
                    switch (scene[y][x])
                    {
                        case ObjectSymbol.Player:
                            player = new Player { X = x, Y = y };
                            break;
                        case ObjectSymbol.Wall:
                            walls[wallCounts] = new Wall { X = x, Y = y, Shape = '▒' };
                            wallCounts++;
                            break;
                        case ObjectSymbol.RaceNpc:
                            npcs[npcCounts] = new Npc { X = x, Y = y, Type = NpcType.RaceNpc };
                            npcCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┌' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┐' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '└' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┘' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_H:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '━' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_V:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┃' };
                            dialogCounts++;
                            break;
                        case ' ':
                            break;
                        default:
                            GameManager.ExitWithError($"오브젝트 심볼이 잘못되었습니다. {scene[y][x]}");
                            return;
                    }
                }
            }
        }

        public static void ParseRaceScene(string[] scene, out Wall[] walls, out Dialog[] dialogs, out Horse[] horses)
        {
            string[] sceneMetaData = scene[scene.Length - 1].Split(" ");
            walls = new Wall[int.Parse(sceneMetaData[0])];
            int wallCounts = 0;
            dialogs = new Dialog[int.Parse(sceneMetaData[1])];
            int dialogCounts = 0;
            horses = new Horse[int.Parse(sceneMetaData[2])];
            int horseCounts = 0;

            for (int y = 0; y < scene.Length - 1; ++y)
            {
                for (int x = 0; x < scene[y].Length; ++x)
                {
                    switch (scene[y][x])
                    {
                        case ObjectSymbol.Wall:
                            walls[wallCounts] = new Wall { X = x, Y = y, Shape = '▒' };
                            wallCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┌' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┐' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '└' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┘' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_H:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '─' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_V:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┃' };
                            dialogCounts++;
                            break;
                        case 'A':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'A', Id = 1 };
                            horseCounts++;
                            break;
                        case 'B':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'B', Id = 2 };
                            horseCounts++;
                            break;
                        case 'C':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'C', Id = 3 };
                            horseCounts++;
                            break;
                        case 'D':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'D', Id = 4 };
                            horseCounts++;
                            break;
                        case 'E':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'E', Id = 5 };
                            horseCounts++;
                            break;
                        case 'F':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'F', Id = 6 };
                            horseCounts++;
                            break;
                        case 'G':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'G', Id = 7 };
                            horseCounts++;
                            break;
                        case 'H':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'H', Id = 8 };
                            horseCounts++;
                            break;
                        case 'I':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'I', Id = 9 };
                            horseCounts++;
                            break;
                        case 'J':
                            horses[horseCounts] = new Horse { X = x, Y = y, Shape = 'J', Id = 10 };
                            horseCounts++;
                            break;
                        case ' ':
                            break;
                        default:
                            GameManager.ExitWithError($"오브젝트 심볼이 잘못되었습니다. {scene[y][x]}");
                            return;
                    }
                }
            }
        }

        public static void ParseShopScene(string[] scene, out Player player, out Wall[] walls, out Npc[] npcs, out Dialog[] dialogs)
        {
            string[] sceneMetaData = scene[scene.Length - 1].Split(" ");
            player = null;
            walls = new Wall[int.Parse(sceneMetaData[0])];
            int wallCounts = 0;
            dialogs = new Dialog[int.Parse(sceneMetaData[1])];
            int dialogCounts = 0;
            npcs = new Npc[int.Parse(sceneMetaData[2])];
            int npcCounts = 0;

            for (int y = 0; y < scene.Length - 1; ++y)
            {
                for (int x = 0; x < scene[y].Length; ++x)
                {
                    switch (scene[y][x])
                    {
                        case ObjectSymbol.Player:
                            player = new Player { X = x, Y = y };
                            break;
                        case ObjectSymbol.Wall:
                            walls[wallCounts] = new Wall { X = x, Y = y, Shape = '▒' };
                            wallCounts++;
                            break;
                        case ObjectSymbol.ShopNpc:
                            npcs[npcCounts] = new Npc { X = x, Y = y, Type = NpcType.ShopNpc };
                            npcCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┌' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┐' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '└' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┘' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_H:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '━' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_V:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┃' };
                            dialogCounts++;
                            break;
                        case ' ':
                            break;
                        default:
                            GameManager.ExitWithError($"오브젝트 심볼이 잘못되었습니다. {scene[y][x]}");
                            return;
                    }
                }
            }
        }

        public static void ParseStoryScene(string[] scene, out Dialog[] dialogs)
        {
            string sceneMetaData = scene[scene.Length - 1];
            dialogs = new Dialog[int.Parse(sceneMetaData)];
            int dialogCounts = 0;
            for (int y = 0; y < scene.Length - 1; ++y)
            {
                for (int x = 0; x < scene[y].Length; ++x)
                {
                    switch (scene[y][x])
                    {
                        case ObjectSymbol.DIALOG_LT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┌' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RT:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┐' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_LD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '└' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_RD:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┘' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_H:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '━' };
                            dialogCounts++;
                            break;
                        case ObjectSymbol.DIALOG_V:
                            dialogs[dialogCounts] = new Dialog { X = x, Y = y, Shape = '┃' };
                            dialogCounts++;
                            break;
                        case ' ':
                            break;
                        default:
                            GameManager.ExitWithError($"오브젝트 심볼이 잘못되었습니다. {scene[y][x]}");
                            return;
                    }
                }
            }

        }

        public static void Title()
        {
            Cursor cursor = new Cursor();
            cursor.X = 80;
            cursor.Y = 21;
            SoundPlayer soundPlayer = new SoundPlayer(@"Assets\Sound\Title.wav");
            soundPlayer.Load();
            soundPlayer.PlayLooping();

            while (true)
            {
                // ========= Render ==========
                Console.Clear();
                RenderManager.RenderTitle();

                RenderManager.RenderObject(cursor.X, cursor.Y, '◀');

                // ========= Input ==========
                ConsoleKey key = Console.ReadKey().Key;

                // ========= Update =========

                if (key == ConsoleKey.UpArrow)
                {
                    cursor.Y = Math.Clamp(cursor.Y - 1, 21, 22);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    cursor.Y = Math.Clamp(cursor.Y + 1, 21, 22);
                }

                if (key == ConsoleKey.Enter)
                {
                    if (cursor.Y == 21)
                    {
                        SceneManager._sceneType = Scene.Story;
                        break;
                    }
                    else
                    {
                        SceneManager._sceneType = Scene.Exit;
                        break;
                    }
                }

            }
        }

        public static void Story()
        {
            Dialog[] dialogs = new Dialog[Constants.STORY_DIALOG_COUNT];
            string[] scene = LoadScene("Story");
            ParseStoryScene(scene, out dialogs);
            while (true)
            {
                // ========== Render ============
                Console.Clear();

                for (int dialogId = 0; dialogId < Constants.STORY_DIALOG_COUNT; ++dialogId)
                {
                    RenderManager.RenderObject(dialogs[dialogId].X, dialogs[dialogId].Y, dialogs[dialogId].Shape);
                }

                RenderManager.RenderStory();


                // ========== Input ==============
                ConsoleKey key = Console.ReadKey().Key;

                // ========== Update =============


                if (key == ConsoleKey.Enter)
                {
                    SceneManager._sceneType = Scene.Town;
                    break;
                }
            }

        }

        public static void Exit()
        {
            Console.Clear();
            RenderManager.RenderGameExit();
            Environment.Exit(0);
        }

        public static void Town()
        {

            Cursor cursor = new Cursor();
            cursor.X = Constants.TOWN_DIALOG_MIN_X + 8;
            cursor.Y = Constants.TOWN_DIALOG_MIN_Y + 1;
            Player player = new Player();
            Wall[] walls = new Wall[Constants.TOWN_WALL_COUNT];
            Npc[] npcs = new Npc[Constants.TOWN_NPC_COUNT];
            Dialog[] dialogs = new Dialog[Constants.TOWN_DIALOG_COUNT];
            bool isPlayerChoice = false;
            string[] scene = LoadScene("Town");
            ParseTownScene(scene, out player, out walls, out npcs, out dialogs);
            SoundPlayer soundPlayer = new SoundPlayer(@"Assets\Sound\Town.wav");
            soundPlayer.Load();
            soundPlayer.PlayLooping();


            while (true)
            {
                // ========= Render =============
                Console.Clear();

                // 플레이어 상태 체크
                RenderManager.ShowState(player);

                Console.SetCursorPosition(32, 9);
                Console.Write("경마장");

                Console.SetCursorPosition(14, 1);
                Console.Write("상점");

                // 플레이어 출력.
                if (SceneManager._prevSceneType == Scene.Shop)
                {
                    player.X = 19;
                    player.Y = 1;
                    RenderManager.RenderObject(player.X, player.Y, ObjectSymbol.Player);

                }
                else if (SceneManager._prevSceneType == Scene.RaceTrack)
                {
                    player.X = 34;
                    player.Y = 10;
                    RenderManager.RenderObject(player.X, player.Y, ObjectSymbol.Player);
                }
                else
                {
                    RenderManager.RenderObject(player.X, player.Y, ObjectSymbol.Player);

                }

                SceneManager._prevSceneType = Scene.None;

                // 벽 출력.
                for (int wallId = 0; wallId < Constants.TOWN_WALL_COUNT; ++wallId)
                {
                    RenderManager.RenderObject(walls[wallId].X, walls[wallId].Y, walls[wallId].Shape);
                }

                // 대화창 출력
                for (int dialogIdx = 0; dialogIdx < Constants.TOWN_DIALOG_COUNT; ++dialogIdx)
                {
                    RenderManager.RenderObject(dialogs[dialogIdx].X, dialogs[dialogIdx].Y, dialogs[dialogIdx].Shape);
                }

                // npc 출력.
                for (int npcId = 0; npcId < Constants.TOWN_NPC_COUNT; ++npcId)
                {
                    if (npcs[npcId].Type == NpcType.RaceNpc)
                    {
                        RenderManager.RenderObject(npcs[npcId].X, npcs[npcId].Y, ObjectSymbol.RaceNpc);
                    }
                    if (npcs[npcId].Type == NpcType.ShopNpc)
                    {
                        RenderManager.RenderObject(npcs[npcId].X, npcs[npcId].Y, ObjectSymbol.ShopNpc);
                    }
                }
                
                // 커서 출력.
                if (player.PlayerState == PlayerState.Talk)
                {
                    RenderManager.RenderObject(cursor.X, cursor.Y, '◀');
                }

                RenderManager.ShowGameManual();


                // ========= Input ==============
                ConsoleKey key = Console.ReadKey().Key;
                

                // ========= Update =============
                if (player.PlayerState != PlayerState.Talk)
                {
                    player.MovePlayer(key);
                }
                
                CollisionManager.OnCollisionWithWallInTown(player, walls);
                CollisionManager.OnCollisionWithNpcInTown(player, npcs);
                player.TalkToNpc(key, npcs, isPlayerChoice, cursor);
                RenderManager.ShowCardEffect(key);

                // 플레이어가 상점입구 좌표로 이동하면 상점으로 이동.
                if ((player.X == 18 || player.X == 19) && player.Y == 0)
                {
                    SceneManager._sceneType = Scene.Shop;
                    SceneManager._prevSceneType = Scene.Town;
                    break;
                }

                if (SceneManager._sceneType != Scene.Town)
                {
                    break;
                }

                //Thread.Sleep(50);
            }
        }

        public static void RaceScene()
        {
            Wall[] walls = new Wall[Constants.RACE_WALL_COUNT];
            Dialog[] dialogs = new Dialog[Constants.RACE_DIALOG_COUNT];
            Horse[] horses = new Horse[Constants.HORSE_COUNT];
            int[] horsesSpeed = new int[Constants.HORSE_COUNT];
            string[] scene = LoadScene("RaceTrack");
            ParseRaceScene(scene, out walls, out dialogs, out horses);
            int[] price = { 0, 1000, 900, 800, 100, 100, 100, 50, 30, 10, 0 };
            int playerChoice = 0;
            bool isChoice = false;
            bool isRaceEnd = false;
            bool isRaceStart = false;
            bool[] isHorseEnd = new bool[Constants.HORSE_COUNT];

            SoundPlayer soundPlayer = new SoundPlayer(@"Assets\Sound\Race.wav");
            soundPlayer.Load();
            soundPlayer.PlayLooping();

            while (true)
            {

                // ===========================
                // ========= Render ==========
                // ===========================
                Console.Clear();

                // 벽 출력.
                for (int wallId = 0; wallId < Constants.RACE_WALL_COUNT; ++wallId)
                {
                    RenderManager.RenderObject(walls[wallId].X, walls[wallId].Y, walls[wallId].Shape);
                }

                // Dialog 출력.
                for (int dialogIdx = 0; dialogIdx < Constants.RACE_DIALOG_COUNT; ++dialogIdx)
                {
                    RenderManager.RenderObject(dialogs[dialogIdx].X, dialogs[dialogIdx].Y, dialogs[dialogIdx].Shape);
                }

                // 말 출력.
                for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
                {
                    if (horses[horseId].Id == playerChoice)
                    {
                        ConsoleColor prev = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        RenderManager.RenderObject(horses[horseId].X, horses[horseId].Y, horses[horseId].Shape);
                        Console.ForegroundColor = prev;
                    }
                    else
                    {
                        RenderManager.RenderObject(horses[horseId].X, horses[horseId].Y, horses[horseId].Shape);
                    }
                }

                RenderManager.ShowCardCollection(GameManager.CardsTable, GameManager.Cards);

                // 말 순위표 출력.
                if (isRaceStart)
                {
                    RenderManager.ShowRank(horses, playerChoice);
                }

                // ===========================
                // ========= Input ===========
                // ===========================

                ConsoleKey key = ConsoleKey.NoName;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;
                }


                if (false == isChoice)
                {
                    while (true)
                    {
                        playerChoice = GameManager.ChoiceHorse();
                        if (1 <= playerChoice && playerChoice <= 10)
                        {
                            break;
                        }
                    }
                    isChoice = true;
                    isRaceStart = true;
                }

                if (isRaceEnd)
                {
                    Console.SetCursorPosition(25, 14);
                    Console.Write($"당신은 {price[horses[playerChoice - 1].Rank]}원을 받았습니다.");
                    RenderManager.ShowBackDialog();
                    Console.SetCursorPosition(86, 15);
                    string input = Console.ReadLine();
                    if (input == "y" || input == "yes")
                    {
                        Player.Money += price[horses[playerChoice - 1].Rank];
                        SceneManager._sceneType = Scene.Town;
                        SceneManager._prevSceneType = Scene.RaceTrack;
                        break;
                    }
                }

                // ===========================
                // ========= Update ==========
                // ===========================

                for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
                {
                    horses[horseId].HorseSpeed = 0;
                }

                // 말 속력할당.
                GameManager.AssignHorseSpeed(isHorseEnd, horses);

                // 말 속력에 따른 X값 업데이트.
                GameManager.CalculateHorseDistance(isHorseEnd, horses);

                // 순위계산.
                if (false == isRaceEnd)
                {
                    GameManager.CheckRank(horses, isHorseEnd);
                }

                // 카드사용시 효과발동하는 부분.
                if (horses[playerChoice - 1].X < 120)
                {
                    GameManager.UseCard(key, horses, playerChoice);
                }


                // 모든말이 결승점에 도달하면 Race종료.
                for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
                {
                    if (isHorseEnd[horseId] == true)
                    {
                        isRaceEnd = true;
                    }
                    else
                    {
                        isRaceEnd = false;
                        break;
                    }
                }

                // Race가 끝났는지 판단.
                if (isRaceEnd)
                {
                    Console.SetCursorPosition(25, 14);
                    Console.Write($"당신은 {price[horses[playerChoice - 1].Rank]}원을 받았습니다.");
                }

                // Scene타입이 다르다면 빠져나가기.
                if (SceneManager._sceneType != Scene.RaceTrack)
                {
                    break;
                }

                Thread.Sleep(200);
            }


        }

        public static void ShopScene()
        {
            Cursor cursor = new Cursor();
            Player player = new Player();
            Npc[] npcs = new Npc[Constants.SHOP_NPC_COUNT];
            Wall[] walls = new Wall[Constants.SHOP_WALL_COUNT];
            Dialog[] dialogs = new Dialog[Constants.SHOP_DIALOG_COUNT];
            bool isPlayerChoice = false;

            string[] scene = LoadScene("Shop");
            ParseShopScene(scene, out player, out walls, out npcs, out dialogs);

            while (true)
            {
                // ========= Render =============
                Console.Clear();

                // 플레이어 상태 체크
                RenderManager.ShowState(player);


                // 플레이어 출력.
                RenderManager.RenderObject(player.X, player.Y, ObjectSymbol.Player);

                // 벽 출력.
                for (int wallId = 0; wallId < Constants.SHOP_WALL_COUNT; ++wallId)
                {
                    RenderManager.RenderObject(walls[wallId].X, walls[wallId].Y, walls[wallId].Shape);
                }

                // 대화창 출력
                for (int dialogIdx = 0; dialogIdx < Constants.SHOP_DIALOG_COUNT; ++dialogIdx)
                {
                    RenderManager.RenderObject(dialogs[dialogIdx].X, dialogs[dialogIdx].Y, dialogs[dialogIdx].Shape);
                }

                // npc 출력.
                for (int npcId = 0; npcId < Constants.SHOP_NPC_COUNT; ++npcId)
                {
                    if (npcs[npcId].Type == NpcType.RaceNpc)
                    {
                        RenderManager.RenderObject(npcs[npcId].X, npcs[npcId].Y, ObjectSymbol.RaceNpc);
                    }
                    if (npcs[npcId].Type == NpcType.ShopNpc)
                    {
                        RenderManager.RenderObject(npcs[npcId].X, npcs[npcId].Y, ObjectSymbol.ShopNpc);
                    }
                }


                // ========= Input ==============
                ConsoleKey key = Console.ReadKey().Key;

                // ========= Update =============

                player.MovePlayer(key);
                CollisionManager.OnCollisionWithWallInShop(player, walls);
                CollisionManager.OnCollisionWithNpcInShop(player, npcs);
                player.TalkToNpc(key, npcs, isPlayerChoice, cursor);
                RenderManager.ShowCardEffect(key);

                if ((player.X == 13 || player.X == 14) && player.Y == 14)
                {
                    SceneManager._sceneType = Scene.Town;
                    SceneManager._prevSceneType = Scene.Shop;
                    break;
                }

                if (SceneManager._sceneType != Scene.Shop)
                {
                    break;
                }
            }


        }

    }
}
