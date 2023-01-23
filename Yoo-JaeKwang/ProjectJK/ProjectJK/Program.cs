using ProjectJK.Objects;
using ProjectJK.UI;
using ProjectJK.UI.Dialog;

namespace ProjectJK
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SelectCursor selectCursor = new SelectCursor
            {
                X = 15,
                Y = 18,
                PastY = 0,
            };
            Dialog dialog = new Dialog
            {
                X = 2,
                Y = 15
            };
            Player player = new Player
            {
                X = 12,
                Y = 7,
                Level = 1,
                MaxEXP = 10,
                MaxHP = 10,
                CurrentHP = 10,
                ATK = 1,
                DEF = 1,
            };
            Slime slime = new Slime
            {
                X = 3,
                Y = 3,
                ATK = 10,
                DEF = 0,
                Alive = true,
                CanMove= true,

            };
            Wall[] walls = default;
            VillageChief villageChief = default;
            StageUpPortal stageUpPortal = default;
            StageDownPortal stageDownPortal = default;
            Dialog1[] dialog1 = default;
            Dialog2[] dialog2 = default;
            Dialog3[] dialog3 = default;
            Dialog4[] dialog4 = default;
            Dialog5[] dialog5 = default;
            Dialog6[] dialog6 = default;
            Dialog7 dialog7 = default;
            Dialog8 dialog8 = default; 
            Dialog9 dialog9 = default;
            Dialog10 dialog10 = default;

            bool isGameDoing = true;
            bool isTitleDoing = true;
            bool isStage00Doing = false;
            bool isStage01Doing = false;
            bool isStage02Doing = false;
            ConsoleKey key = default;


            Game.Function.Initializing();

            while (isGameDoing)
            {
                if (isTitleDoing)
                {
                    InitTitle();
                }
                while (isTitleDoing)
                {
                    RenderTitle();
                    Input.Process();
                    UpdateTitle();
                }

                if (isStage00Doing)
                {
                    InitStage00();
                }
                while (isStage00Doing)
                {
                    RenderStage00();
                    Input.Process();
                    UpdateStage00();
                }

                if (isStage01Doing)
                {
                    InitStage01();
                }
                while (isStage01Doing)
                {
                    RenderStage01();
                    Input.Process();
                    UpdateStage01();
                }

            }



            void InitTitle()
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"");
                Console.WriteLine($"  @@@@@@@      @@@@@@         @@@@@     ");
                Console.WriteLine($"  @@@@@@@@@    @@@@@@@@     @@@@@@@@    ");
                Console.WriteLine($"  @@     @@    @@    @@@   @@@     @@   ");
                Console.WriteLine($"  @@      @@   @@     @@   @@      @@   ");
                Console.WriteLine($"  @@      @@   @@     @@   @@           ");
                Console.WriteLine($"  @@     @@    @@    @@@  @@            ");
                Console.WriteLine($"  @@@@@@@@     @@@@@@@@   @@     @@@@@  ");
                Console.WriteLine($"  @@@@@@@      @@@@@@@    @@     @@@@@  ");
                Console.WriteLine($"  @@   @@@     @@         @@@       @@  ");
                Console.WriteLine($"  @@    @@     @@          @@       @@  ");
                Console.WriteLine($"  @@     @@    @@          @@@     @@@  ");
                Console.WriteLine($"  @@      @@   @@           @@@@@@@@@   ");
                Console.WriteLine($"  @@      @@@  @@             @@@@@     ");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"                 Start");
                Console.WriteLine($"                 Exit");
                Console.ForegroundColor = ConsoleColor.White;
                selectCursor.On = true;
            }
            void RenderTitle()
            {
                Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
            }
            void UpdateTitle()
            {
                SelectCursor.Function.Move(selectCursor, player);

                if (SelectCursor.Function.SelectYes(selectCursor))
                {
                    isTitleDoing = false;
                    selectCursor.On = false;
                    selectCursor.X = 2;
                    isStage00Doing = true;

                }
                if (SelectCursor.Function.SelectNo(selectCursor))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Press Any Key To Exit");
                    Environment.Exit(0);
                }
            }

            void InitStage00()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageKind.Stage00);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out dialog1, out dialog2, out dialog3, out dialog4, out dialog5, out dialog6, out dialog7, out dialog8, out dialog9, out dialog10);
                Wall.Function.Render(walls);
                VillageChief.Function.Render(villageChief);
                StageUpPortal.Function.Render(stageUpPortal);
                Dialog.Render(dialog1, dialog2, dialog3, dialog4, dialog5, dialog6, dialog7, dialog8, dialog9, dialog10);
                UIName.Render();
                player.CanMove = true;
                selectCursor.X = Game.DialogCursor_X;
                selectCursor.Y = Game.DialogCursor_Y;
            }
            void RenderStage00()
            {
                Player.Function.Render(player);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                if (selectCursor.On && Player.Interaction.IsFront(player, villageChief.X, villageChief.Y))
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $" 촌장 ", ConsoleColor.Black);
                    if (false == villageChief.BeginnerSupport)
                    {
                        Game.Function.ObjRender(dialog.X, dialog.Y, $"초보자 지원 물품을 가져가게나.", ConsoleColor.Black);
                    }
                    else
                    {
                        Game.Function.ObjRender(dialog.X, dialog.Y, $"뭐", ConsoleColor.Black);
                    }
                }
                else if (selectCursor.On && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $" 포탈 ", ConsoleColor.Black);
                    Game.Function.ObjRender(dialog.X, dialog.Y, $"이동하시겠습니까?", ConsoleColor.Black);
                }
                else
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $"━━━━━━━━━━━━━", ConsoleColor.Black);
                    Game.Function.ObjRender(dialog.X, dialog.Y, $"                                         ", ConsoleColor.White);
                    
                }
            }
            void UpdateStage00()
            {
                Player.Function.Move(player);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithVillageChief(player, villageChief);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Interaction.WithVillageCheif(player, villageChief, ref selectCursor.On);
                if (selectCursor.On && Player.Interaction.IsFront(player, villageChief.X, villageChief.Y))
                {
                    if (false == villageChief.BeginnerSupport)
                    {
                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            villageChief.BeginnerSupport = true;
                            VillageChief.Function.GetBeginnerSupport(player);
                            selectCursor.On = false;
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            selectCursor.On = false;
                            player.CanMove = true;
                        }
                    }
                    else
                    {

                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            selectCursor.On = false;
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            selectCursor.On = false;
                            player.CanMove = true;
                        }
                    }
                }
                Player.Interaction.WithStageUpPortal(player, stageUpPortal, ref selectCursor.On);
                if (selectCursor.On && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                        isStage00Doing = false;
                        isStage01Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                    }
                }
                SelectCursor.Function.Move(selectCursor, player);


            }

            void InitStage01()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageKind.Stage01);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out dialog1, out dialog2, out dialog3, out dialog4, out dialog5, out dialog6, out dialog7, out dialog8, out dialog9, out dialog10);
                Wall.Function.Render(walls);
                StageUpPortal.Function.Render(stageUpPortal);
                StageDownPortal.Function.Render(stageDownPortal);
                Dialog.Render(dialog1, dialog2, dialog3, dialog4, dialog5, dialog6, dialog7, dialog8, dialog9, dialog10);
                UIName.Render();
                selectCursor.X = Game.BattleCursor_X;
                selectCursor.Y = Game.BattleCursor_Y;
            }
            void RenderStage01()
            {
                Game.Function.ObjRender(player.PastX, player.PastY, "P", ConsoleColor.White);
                Game.Function.ObjRender(slime.PastX, slime.PastY, "S", ConsoleColor.White);
                Game.Function.ObjRender(player.X, player.Y, "P", ConsoleColor.Black);
                Game.Function.ObjRender(slime.X, slime.Y, "S", ConsoleColor.DarkGreen);
                if(player.X == slime.X && player.Y == slime.Y)
                {
                    Game.Function.ObjRender(player.X, player.Y, "B", ConsoleColor.Red);
                    BattleGraphic.Slime(slime);
                }
                else
                {
                    BattleGraphic.Clear();
                }
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                if (selectCursor.On && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $" 포탈 ", ConsoleColor.Black);
                    Game.Function.ObjRender(dialog.X, dialog.Y, $"이동하시겠습니까?", ConsoleColor.Black);
                }
                else if (selectCursor.On && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $" 포탈 ", ConsoleColor.Black);
                    Game.Function.ObjRender(dialog.X, dialog.Y, $"이동하시겠습니까?", ConsoleColor.Black);
                }
                else
                {
                    Game.Function.ObjRender(dialog.X, dialog.Y - 1, $"━━━━━━━━━━━━━", ConsoleColor.Black);
                    Game.Function.ObjRender(dialog.X, dialog.Y, $"                                         ", ConsoleColor.White);
                    
                }
            }
            void UpdateStage01()
            {
                Player.Function.Move(player);
                Player.Function.LevelUp(player);
                Player.Collision.WithSlime(player, slime, selectCursor);
                if (player.IsOnBattle)
                {

                }
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Collision.WithStageDownPortal(player, stageDownPortal);
                Player.Interaction.WithStageUpPortal(player, stageUpPortal, ref selectCursor.On);
                if (selectCursor.On && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                        isStage01Doing = false;
                        isStage02Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                    }
                }
                Player.Interaction.WithStageDownPortal(player, stageDownPortal, ref selectCursor.On);
                if (selectCursor.On && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                        isStage01Doing = false;
                        isStage00Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        selectCursor.On = false;
                        player.CanMove = true;
                    }
                }

                SelectCursor.Function.Move(selectCursor, player);

                Slime.Update(slime, player, walls, stageUpPortal, stageDownPortal);

                if (Player.Function.Die(player))
                {
                    player.CurrentEXP = 0;
                    player.CurrentHP = player.MaxHP;
                    player.X = 12;
                    player.Y = 7;
                    player.PastX = 0;
                    player.PastY = 0;
                    player.Money -= player.Level * 100;
                    isStage01Doing = false;
                    isStage00Doing = true;
                }
            
            }
        }
    }
}