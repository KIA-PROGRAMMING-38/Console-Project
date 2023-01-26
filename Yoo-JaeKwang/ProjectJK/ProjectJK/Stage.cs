using ProjectJK.Objects;
using ProjectJK.UI.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void ChangeStage()
        {
            if (_isStageChange)
            {
                _isStageChange = false;

                _currentStage = _nextStage;

                _nextStage = default;

                InitStage();
            }
        }

        private static void InitStage()
        {

        }
        public static string[] LoadStage(StageNum stageNum)
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "Stage", $"Stage{(int)stageNum:D2}.txt");
            if (false == File.Exists(stageFilePath))
            {
                Game.Function.ExitWithError($"스테이지 파일 로드 오류{stageFilePath}");
            }
            return File.ReadAllLines(stageFilePath);
        }
        public static void ParseStage(string[] stage, out Wall[] walls, out VillageChief villageChief, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal,
                                        out UI.Dialog.FrameUI0[] dialog1, out FrameUI1[] dialog2, out FrameUI2[] dialog3, out FrameUI3[] dialog4, out FrameUI4[] dialog5, out FrameUI5[] dialog6, out FrameUI6 dialog7, out FrameUI7 dialog8, out FrameUI8 dialog9, out FrameUI9 dialog10,
                                        out Slime[] slime, out Fox[] fox, out Goblin[] goblin,
                                        out VillageRecoveringMerchant villageRecoveringMerchant, out VillageMaxHPMerchant villageMaxHPMerchant, out VillageATKMerchant villageATKMerchant, out VillageDEFMerchant villageDEFMerchant)
        {
            string[] stageMetaData = stage[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            villageChief = default;
            stageUpPortal = default;
            stageDownPortal = default;
            dialog1 = new UI.Dialog.FrameUI0[int.Parse(stageMetaData[1])];
            int dialog1Index = 0;
            dialog2 = new FrameUI1[int.Parse(stageMetaData[2])];
            int dialog2Index = 0;
            dialog3 = new FrameUI2[int.Parse(stageMetaData[3])];
            int dialog3Index = 0;
            dialog4 = new FrameUI3[int.Parse(stageMetaData[4])];
            int dialog4Index = 0;
            dialog5 = new FrameUI4[int.Parse(stageMetaData[5])];
            int dialog5Index = 0;
            dialog6 = new FrameUI5[int.Parse(stageMetaData[6])];
            int dialog6Index = 0;
            dialog7 = default;
            dialog8 = default;
            dialog9 = default;
            dialog10 = default;
            slime = new Slime[int.Parse(stageMetaData[7])];
            int slimeIndex = 0;
            fox = new Fox[int.Parse(stageMetaData[7])];
            int foxIndex = 0;
            goblin = new Goblin[int.Parse(stageMetaData[7])];
            int goblinIndex = 0;
            villageRecoveringMerchant = default;
            villageMaxHPMerchant = default;
            villageATKMerchant = default;
            villageDEFMerchant = default;



            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ' ':

                            break;
                        case '#':
                            walls[wallIndex] = new Wall { X = x, Y = y };
                            ++wallIndex;
                            break;
                        case 'C':
                            villageChief = new VillageChief { X = x, Y = y };
                            break;
                        case '↑':
                            stageUpPortal = new StageUpPortal { X = x, Y = y };
                            break;
                        case '↓':
                            stageDownPortal = new StageDownPortal { X = x, Y = y };
                            break;
                        case '┃':
                            dialog1[dialog1Index] = new UI.Dialog.FrameUI0 { X = x, Y = y };
                            ++dialog1Index;
                            break;
                        case '━':
                            dialog2[dialog2Index] = new FrameUI1 { X = x, Y = y };
                            ++dialog2Index;
                            break;
                        case '┏':
                            dialog3[dialog3Index] = new FrameUI2 { X = x, Y = y };
                            ++dialog3Index;
                            break;
                        case '┓':
                            dialog4[dialog4Index] = new FrameUI3 { X = x, Y = y };
                            ++dialog4Index;
                            break;
                        case '┗':
                            dialog5[dialog5Index] = new FrameUI4 { X = x, Y = y };
                            ++dialog5Index;
                            break;
                        case '┛':
                            dialog6[dialog6Index] = new FrameUI5 { X = x, Y = y };
                            ++dialog6Index;
                            break;
                        case '┳':
                            dialog7 = new FrameUI6 { X = x, Y = y };
                            break;
                        case '┣':
                            dialog8 = new FrameUI7 { X = x, Y = y };
                            break;
                        case '┫':
                            dialog9 = new FrameUI8 { X = x, Y = y };
                            break;
                        case '┻':
                            dialog10 = new FrameUI9 { X = x, Y = y };
                            break;
                        case 'S':
                            slime[slimeIndex] = new Slime { X = x, Y = y, MaxHP = 10, ATK = 0, DEF = 0, Money = 10, EXP = 1, Alive = true };
                            ++slimeIndex;
                            break;
                        case 'F':
                            fox[foxIndex] = new Fox { X = x, Y = y, MaxHP = 50, ATK = 15, DEF = 5, Money = 50, EXP = 5, Alive = true };
                            ++foxIndex;
                            break;
                        case 'G':
                            goblin[goblinIndex] = new Goblin { X = x, Y = y, MaxHP = 200, ATK = 40, DEF = 30, Money = 200, EXP = 20, Alive = true };
                            ++goblinIndex;
                            break;
                        case '1':
                            villageRecoveringMerchant = new VillageRecoveringMerchant { X = x, Y = y };
                            break;
                        case '2':
                            villageMaxHPMerchant = new VillageMaxHPMerchant { X = x, Y = y };
                            break;
                        case '3':
                            villageATKMerchant = new VillageATKMerchant { X = x, Y = y };
                            break;
                        case '4':
                            villageDEFMerchant = new VillageDEFMerchant { X = x, Y = y };
                            break;
                        default:
                            Game.Function.ExitWithError($"스테이지 파일 파싱 오류{stage[y]}");
                            break;

                    }
                }
            }

        }

        public static void Render()
        {
            switch (_currentStage)
            {
                case StageNum.Stage00:
                    RenderStage00();

                    break;

                case StageNum.Stage01:
                    RenderStage01();

                    break;
            }
        }
        private static void RenderStage00()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Stage00");
        }
        private static void RenderStage01()
        { 
        
        }


        public static void Update()
        {
            switch (_currentStage)
            {
                case StageNum.Stage00:
                    UpdateStage00();
                    break;
                case StageNum.Stage01:
                    UpdateStage01();
                    break;
            }
        }
        private static void UpdateStage00()
        {

        }
        private static void UpdateStage01()
        {

        }

    }
}
