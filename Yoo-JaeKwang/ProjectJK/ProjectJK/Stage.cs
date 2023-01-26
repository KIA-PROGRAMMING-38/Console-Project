using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    public enum StageNum
    {
        Stage00,
        Stage01,
        Stage02,
        Stage03,
        Stage04,
    }
    public static class Stage
    {
        private static StageNum _currentStage = StageNum.Stage00;
        public static StageNum GetCurrentStage()
        {
            return _currentStage;
        }
        private static bool _isStageChange = false;
        public static bool IsStageChange()
        {
            return _isStageChange;
        }
        private static StageNum _nextStage;
        public static void SetNextStage(StageNum nextStage)
        {
            _nextStage = nextStage;
            _isStageChange = true;
        }
        public static void ChangeStage(ref Wall[] walls, ref VillageNPC[] villageNPCs, ref StageUpPortal stageUpPortal, ref StageDownPortal stageDownPortal,
                                    ref Slime[] slimes, ref Fox[] foxes, ref Goblin[] goblins, KingSlime kingSlime)
        {
            if (_isStageChange)
            {
                _isStageChange = false;

                _currentStage = _nextStage;

                _nextStage = default;

                InitStage(ref walls, ref villageNPCs, ref stageUpPortal, ref stageDownPortal, ref slimes, ref foxes, ref goblins, kingSlime);
            }
        }
        public static void InitStage(ref Wall[] walls, ref VillageNPC[] villageNPCs, ref StageUpPortal stageUpPortal, ref StageDownPortal stageDownPortal,
                                    ref Slime[] slimes, ref Fox[] foxes, ref Goblin[] goblins, KingSlime kingSlime)
        {
            switch (_currentStage)
            {
                case StageNum.Stage00:
                    InitStage00(out walls, out villageNPCs, out stageUpPortal);
                    break;
                case StageNum.Stage01:
                    InitStage01(out walls, out stageUpPortal, out stageDownPortal, out slimes);
                    break;
                case StageNum.Stage02:
                    InitStage02(out walls, out stageUpPortal, out stageDownPortal, out foxes);
                    break;
                case StageNum.Stage03:
                    InitStage03(out walls, out stageUpPortal, out stageDownPortal, out goblins);
                    break;
                case StageNum.Stage04:
                    InitStage04(out walls, out stageDownPortal, kingSlime);
                    break;

            }
        }
        private static string[] _lines = null;
        public static void InitStage00(out Wall[] walls, out VillageNPC[] villageNPCs, out StageUpPortal stageUpPortal)
        {
            Console.Clear();
            _lines = LoadUIFrame(StageNum.Stage00);
            for (int i = 0; i < _lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(_lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            _lines = LoadStage(StageNum.Stage00);
            ParseStage00(_lines, out walls, out villageNPCs, out stageUpPortal);
            Wall.Render(walls);
            VillageNPC.Render(villageNPCs);
            StageUpPortal.Render(stageUpPortal);
        }
        public static void InitStage01(out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Slime[] slimes)
        {
            Console.Clear();
            _lines = LoadUIFrame(StageNum.Stage01);
            for (int i = 0; i < _lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(_lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            _lines = LoadStage(StageNum.Stage01);
            ParseStage01(_lines, out walls, out stageUpPortal, out stageDownPortal, out slimes);
            Wall.Render(walls);
            StageUpPortal.Render(stageUpPortal);
            StageDownPortal.Render(stageDownPortal);
            Slime.InitSlimeRender(slimes);
        }
        public static void InitStage02(out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Fox[] foxes)
        {
            Console.Clear();
            _lines = LoadUIFrame(StageNum.Stage02);
            for (int i = 0; i < _lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(_lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            _lines = LoadStage(StageNum.Stage02);
            ParseStage02(_lines, out walls, out stageUpPortal, out stageDownPortal, out foxes);
            Wall.Render(walls);
            StageUpPortal.Render(stageUpPortal);
            StageDownPortal.Render(stageDownPortal);
            Fox.InitFoxRender(foxes);
        }
        public static void InitStage03(out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Goblin[] goblins)
        {
            Console.Clear();
            _lines = LoadUIFrame(StageNum.Stage03);
            for (int i = 0; i < _lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(_lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            _lines = LoadStage(StageNum.Stage03);
            ParseStage03(_lines, out walls, out stageUpPortal, out stageDownPortal, out goblins);
            Wall.Render(walls);
            StageUpPortal.Render(stageUpPortal);
            StageDownPortal.Render(stageDownPortal);
            Goblin.InitGoblinRender(goblins);
        }
        public static void InitStage04(out Wall[] walls, out StageDownPortal stageDownPortal, KingSlime kingSlime)
        {
            Console.Clear();
            _lines = LoadUIFrame(StageNum.Stage04);
            for (int i = 0; i < _lines.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(_lines[i]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            _lines = LoadStage(StageNum.Stage04);
            ParseStage04(_lines, out walls, out stageDownPortal);
            Wall.Render(walls);
            StageDownPortal.Render(stageDownPortal);
            KingSlime.InitKingSlimeRender(kingSlime);
        }
        private static string[] LoadStage(StageNum stageNum)
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "Stage", $"Stage{(int)stageNum:D2}.txt");
            if (false == File.Exists(stageFilePath))
            {
                Game.ExitWithError($"스테이지 파일 로드 오류{stageFilePath}");
            }
            return File.ReadAllLines(stageFilePath);
        }
        private static string[] LoadUIFrame(StageNum stageNum)
        {
            string UIFilePath = Path.Combine("..\\..\\..\\Assets", "Stage", $"UIFrame{(int)stageNum:D2}.txt");
            if (false == File.Exists(UIFilePath))
            {
                Game.ExitWithError($"UI 파일 로드 오류{UIFilePath}");
            }
            return File.ReadAllLines(UIFilePath);
        }
        private static void ParseStage00(string[] lines, out Wall[] walls, out VillageNPC[] villageNPCs, out StageUpPortal stageUpPortal)
        {
            string[] stageMetaData = lines[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            villageNPCs = new VillageNPC[5];
            stageUpPortal = default;
            for (int y = 1; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case ' ':
                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case '0':
                            villageNPCs[0] = new VillageNPC { X = x, Y = y };
                            break;
                        case '1':
                            villageNPCs[1] = new VillageNPC { X = x, Y = y };
                            break;
                        case '2':
                            villageNPCs[2] = new VillageNPC { X = x, Y = y };
                            break;
                        case '3':
                            villageNPCs[3] = new VillageNPC { X = x, Y = y };
                            break;
                        case '4':
                            villageNPCs[4] = new VillageNPC { X = x, Y = y };
                            break;
                        case '↑':
                            stageUpPortal = new StageUpPortal { X = x, Y = y };
                            break;
                        default:
                            Game.ExitWithError($"스테이지 파일 파싱 오류{lines[y]}");
                            break;
                    }
                }
            }
        }
        private static void ParseStage01(string[] lines, out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Slime[] slimes)
        {
            string[] stageMetaData = lines[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            slimes = new Slime[int.Parse(stageMetaData[1])];
            int slimeIndex = 0;
            stageUpPortal = default;
            stageDownPortal = default;
            for (int y = 1; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case ' ':
                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case '↑':
                            stageUpPortal = new StageUpPortal { X = x, Y = y };
                            break;
                        case '↓':
                            stageDownPortal = new StageDownPortal { X = x, Y = y };
                            break;
                        case 'S':
                            slimes[slimeIndex] = new Slime { X = x, Y = y, MaxHP = 10, ATK = 0, DEF = 0, Money = 10, EXP = 1, Alive = true };
                            ++slimeIndex;
                            break;
                        default:
                            Game.ExitWithError($"스테이지 파일 파싱 오류{lines[y]}");
                            break;
                    }
                }
            }
        }
        private static void ParseStage02(string[] lines, out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Fox[] foxes)
        {
            string[] stageMetaData = lines[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            foxes = new Fox[int.Parse(stageMetaData[1])];
            int foxIndex = 0;
            stageUpPortal = default;
            stageDownPortal = default;
            for (int y = 1; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case ' ':
                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case '↑':
                            stageUpPortal = new StageUpPortal { X = x, Y = y };
                            break;
                        case '↓':
                            stageDownPortal = new StageDownPortal { X = x, Y = y };
                            break;
                        case 'F':
                            foxes[foxIndex] = new Fox { X = x, Y = y, MaxHP = 50, ATK = 15, DEF = 5, Money = 50, EXP = 5, Alive = true };
                            ++foxIndex;
                            break;
                        default:
                            Game.ExitWithError($"스테이지 파일 파싱 오류{lines[y]}");
                            break;
                    }
                }
            }
        }
        private static void ParseStage03(string[] lines, out Wall[] walls, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal, out Goblin[] goblins)
        {
            string[] stageMetaData = lines[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            goblins = new Goblin[int.Parse(stageMetaData[1])];
            int goblinIndex = 0;
            stageUpPortal = default;
            stageDownPortal = default;
            for (int y = 1; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case ' ':
                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case '↑':
                            stageUpPortal = new StageUpPortal { X = x, Y = y };
                            break;
                        case '↓':
                            stageDownPortal = new StageDownPortal { X = x, Y = y };
                            break;
                        case 'G':
                            goblins[goblinIndex] = new Goblin { X = x, Y = y, MaxHP = 200, ATK = 40, DEF = 30, Money = 200, EXP = 20, Alive = true };
                            ++goblinIndex;
                            break;
                        default:
                            Game.ExitWithError($"스테이지 파일 파싱 오류{lines[y]}");
                            break;
                    }
                }
            }
        }
        private static void ParseStage04(string[] lines, out Wall[] walls, out StageDownPortal stageDownPortal)
        {
            string[] stageMetaData = lines[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            stageDownPortal = default;
            for (int y = 1; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case ' ':
                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case '↓':
                            stageDownPortal = new StageDownPortal { X = x, Y = y };
                            break;
                        default:
                            Game.ExitWithError($"스테이지 파일 파싱 오류{lines[y]}");
                            break;
                    }
                }
            }
        }
        public static void Render(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                    Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (_currentStage)
            {
                case StageNum.Stage00:
                    RenderStage00(player, villageNPCs, stageUpPortal, selectCursor);
                    break;
                case StageNum.Stage01:
                    RenderStage01(player, slimes, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage02:
                    RenderStage02(player, foxes, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage03:
                    RenderStage03(player, goblins, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage04:
                    RenderStage04(player, kingSlime, stageDownPortal, selectCursor);
                    break;

            }
        }
        public static void RenderStage00(Player player, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, SelectCursor selectCursor)
        {
            Player.RenderPast(player);

            Player.RenderNow(player);

            Player.RenderState(player);

            SelectCursor.Render(selectCursor, player);

            Player.Interaction.RenderVillageNPC(player, villageNPCs);
            Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
            Player.Interaction.RenderRelease(player);
        }
        public static void RenderStage01(Player player, Slime[] slimes, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.RenderPast(player);
            Slime.RenderPast(slimes);

            Player.RenderNow(player);
            Slime.RenderNow(slimes);

            Player.RenderState(player);

            SelectCursor.Render(selectCursor, player);

            Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
            Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
            Player.Interaction.RenderRelease(player);

            Slime.RenderBattle(player, slimes);
        }
        public static void RenderStage02(Player player, Fox[] foxes, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.RenderPast(player);
            Fox.RenderPast(foxes);

            Player.RenderNow(player);
            Fox.RenderNow(foxes);

            Player.RenderState(player);

            SelectCursor.Render(selectCursor, player);

            Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
            Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
            Player.Interaction.RenderRelease(player);

            Fox.RenderBattle(player, foxes);
        }
        public static void RenderStage03(Player player, Goblin[] goblins, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.RenderPast(player);
            Goblin.RenderPast(goblins);

            Player.RenderNow(player);
            Goblin.RenderNow(goblins);

            Player.RenderState(player);

            SelectCursor.Render(selectCursor, player);

            Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
            Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
            Player.Interaction.RenderRelease(player);

            Goblin.RenderBattle(player, goblins);
        }
        public static void RenderStage04(Player player, KingSlime kingSlime, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.RenderPast(player);
            KingSlime.RenderPast(kingSlime);

            Player.RenderNow(player);
            KingSlime.RenderNow(kingSlime);

            Player.RenderState(player);

            SelectCursor.Render(selectCursor, player);

            Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
            Player.Interaction.RenderRelease(player);

            KingSlime.RenderBattle(player, kingSlime);
        }


        public static void Update(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal,
                                    Slime[] slimes, Fox[] foxes, Goblin[] goblins, KingSlime kingSlime, SelectCursor selectCursor)
        {
            switch (_currentStage)
            {
                case StageNum.Stage00:
                    UpdateStage00(player, walls, villageNPCs, stageUpPortal, selectCursor);
                    break;
                case StageNum.Stage01:
                    UpdateStage01(player, walls, slimes, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage02:
                    UpdateStage02(player, walls, foxes, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage03:
                    UpdateStage03(player, walls, goblins, stageUpPortal, stageDownPortal, selectCursor);
                    break;
                case StageNum.Stage04:
                    UpdateStage04(player, walls, kingSlime, stageDownPortal, selectCursor);
                    break;
            }
        }
        public static void UpdateStage00(Player player, Wall[] walls, VillageNPC[] villageNPCs, StageUpPortal stageUpPortal, SelectCursor selectCursor)
        {
            Player.Move(player);

            Player.Collision.WithWall(player, walls);
            Player.Collision.WithVillageNPC(player, villageNPCs);
            Player.Collision.WithStageUpPortal(player, stageUpPortal);

            Player.Interaction.WithVillageNPC(player, selectCursor, villageNPCs);
            Player.Interaction.WithStageUpPortal00(player, stageUpPortal, selectCursor);

            SelectCursor.Move(selectCursor, player);

            Player.LimitState(player);
        }
        public static void UpdateStage01(Player player, Wall[] walls, Slime[] slimes, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.Move(player);

            Player.Collision.WithWall(player, walls);
            Player.Collision.WithSlime(player, slimes);
            Player.Collision.WithStageUpPortal(player, stageUpPortal);
            Player.Collision.WithStageDownPortal(player, stageDownPortal);

            Player.Interaction.WithStageUpPortal01(player, stageUpPortal, selectCursor);
            Player.Interaction.WithStageDownPortal01(player, stageDownPortal, selectCursor);

            Player.Battle.WithSlime(player, slimes, selectCursor);

            SelectCursor.Move(selectCursor, player);

            Slime.Update(slimes, player, walls, stageUpPortal, stageDownPortal);

            Player.LevelUp(player);
            Player.LimitState(player);
            Player.Die(player, selectCursor);
        }
        public static void UpdateStage02(Player player, Wall[] walls, Fox[] foxes, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.Move(player);

            Player.Collision.WithWall(player, walls);
            Player.Collision.WithFox(player, foxes);
            Player.Collision.WithStageUpPortal(player, stageUpPortal);
            Player.Collision.WithStageDownPortal(player, stageDownPortal);

            Player.Interaction.WithStageUpPortal02(player, stageUpPortal, selectCursor);
            Player.Interaction.WithStageDownPortal02(player, stageDownPortal, selectCursor);

            Player.Battle.WithFox(player, foxes, selectCursor);

            SelectCursor.Move(selectCursor, player);

            Fox.Update(foxes, player, walls, stageUpPortal, stageDownPortal);

            Player.LevelUp(player);
            Player.LimitState(player);
            Player.Die(player, selectCursor);
        }
        public static void UpdateStage03(Player player, Wall[] walls, Goblin[] goblins, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.Move(player);

            Player.Collision.WithWall(player, walls);
            Player.Collision.WithGoblin(player, goblins);
            Player.Collision.WithStageUpPortal(player, stageUpPortal);
            Player.Collision.WithStageDownPortal(player, stageDownPortal);

            Player.Interaction.WithStageUpPortal03(player, stageUpPortal, selectCursor);
            Player.Interaction.WithStageDownPortal03(player, stageDownPortal, selectCursor);

            Player.Battle.WithGoblin(player, goblins, selectCursor);

            SelectCursor.Move(selectCursor, player);

            Goblin.Update(goblins, player, walls, stageUpPortal, stageDownPortal);

            Player.LevelUp(player);
            Player.LimitState(player);
            Player.Die(player, selectCursor);
        }
        public static void UpdateStage04(Player player, Wall[] walls, KingSlime kingSlime, StageDownPortal stageDownPortal, SelectCursor selectCursor)
        {
            Player.Move(player);

            Player.Collision.WithWall(player, walls);
            Player.Collision.WithKingSlime(player, kingSlime);
            Player.Collision.WithStageDownPortal(player, stageDownPortal);

            Player.Interaction.WithStageDownPortal04(player, stageDownPortal, selectCursor);

            Player.Battle.WithKingSlime(player, kingSlime, selectCursor);

            SelectCursor.Move(selectCursor, player);

            KingSlime.Update(kingSlime, player, walls, stageDownPortal);

            Player.LevelUp(player);
            Player.LimitState(player);
            Player.Die(player, selectCursor);
        }

    }
}