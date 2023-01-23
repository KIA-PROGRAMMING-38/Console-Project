using ProjectJK.Objects;
using ProjectJK.UI.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    public enum StageKind
    {
        Stage00,
        Stage01
    }
    public static class Stage
    {

        private static StageKind _currentStage = StageKind.Stage00;
        public static StageKind GetCurrentStage()
        {
            return _currentStage;
        }
        private static bool _isStageChange = false;
        public static bool IsStageChange()
        {
            return _isStageChange;
        }
        private static StageKind _nextStage;
        public static void SetNextStage(StageKind nextStage)
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
        public static string[] LoadStage(StageKind stageKind)
        {
            string stageFilePath = Path.Combine("..\\..\\..\\Assets", "Stage", $"Stage{(int)stageKind:D2}.txt");
            if (false == File.Exists(stageFilePath))
            {
                Game.Function.ExitWithError($"스테이지 파일 로드 오류{stageFilePath}");
            }
            return File.ReadAllLines(stageFilePath);
        }
        public static void ParseStage(string[] stage, out Wall[] walls, out VillageChief villageChief, out StageUpPortal stageUpPortal, out StageDownPortal stageDownPortal,
                                        out Dialog1[] dialog1, out Dialog2[] dialog2, out Dialog3[] dialog3, out Dialog4[] dialog4, out Dialog5[] dialog5, out Dialog6[] dialog6, out Dialog7 dialog7, out Dialog8 dialog8, out Dialog9 dialog9, out Dialog10 dialog10)
        {
            string[] stageMetaData = stage[0].Split(" ");
            walls = new Wall[int.Parse(stageMetaData[0])];
            int wallIndex = 0;
            villageChief = default;
            stageUpPortal = default;
            stageDownPortal = default;
            dialog1 = new Dialog1[int.Parse(stageMetaData[1])];
            int dialog1Index = 0;
            dialog2 = new Dialog2[int.Parse(stageMetaData[2])];
            int dialog2Index = 0;
            dialog3 = new Dialog3[int.Parse(stageMetaData[3])];
            int dialog3Index = 0;
            dialog4 = new Dialog4[int.Parse(stageMetaData[4])];
            int dialog4Index = 0;
            dialog5 = new Dialog5[int.Parse(stageMetaData[5])];
            int dialog5Index = 0;
            dialog6 = new Dialog6[int.Parse(stageMetaData[6])];
            int dialog6Index = 0;
            dialog7 = default;
            dialog8 = default;
            dialog9 = default;
            dialog10 = default;

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
                            dialog1[dialog1Index] = new Dialog1 { X = x, Y = y };
                            ++dialog1Index;
                            break;
                        case '━':
                            dialog2[dialog2Index] = new Dialog2 { X = x, Y = y };
                            ++dialog2Index;
                            break;
                        case '┏':
                            dialog3[dialog3Index] = new Dialog3 { X = x, Y = y };
                            ++dialog3Index;
                            break;
                        case '┓':
                            dialog4[dialog4Index] = new Dialog4 { X = x, Y = y };
                            ++dialog4Index;
                            break;
                        case '┗':
                            dialog5[dialog5Index] = new Dialog5 { X = x, Y = y };
                            ++dialog5Index;
                            break;
                        case '┛':
                            dialog6[dialog6Index] = new Dialog6 { X = x, Y = y };
                            ++dialog6Index;
                            break;
                        case '┳':
                            dialog7 = new Dialog7 { X = x, Y = y };
                            break;
                        case '┣':
                            dialog8 = new Dialog8 { X = x, Y = y };
                            break;
                        case '┫':
                            dialog9 = new Dialog9 { X = x, Y = y };
                            break;
                        case '┻':
                            dialog10 = new Dialog10 { X = x, Y = y };
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
                case StageKind.Stage00:
                    RenderStage00();

                    break;

                case StageKind.Stage01:
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
                case StageKind.Stage00:
                    UpdateStage00();
                    break;
                case StageKind.Stage01:
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
