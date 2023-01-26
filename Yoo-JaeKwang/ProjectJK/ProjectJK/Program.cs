using ProjectJK.Objects;
using ProjectJK.UI;
using ProjectJK.UI.Dialog;

namespace ProjectJK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game
            {
                IsGameDoing = true,
                IsTitleDoing = true,
                IsStage00Doing = false,
                IsStage01Doing = false,
                IsStage02Doing = false,
                IsStage03Doing = false,
                IsStage04Doing = false,
            };
            SelectCursor selectCursor = new SelectCursor
            {
                X = 15,
                Y = 18,
            };
            FrameUI frameUI = new FrameUI
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
            KingSlime kingSlime = new KingSlime
            {
                X = 12,
                Y = 10,
                ATK = 100,
                DEF = 100,
                MaxHP = 500,
                CurrentHP = 500,
                Money = 10000,
                EXP = 10000,
                Alive = true,
            };

            Wall[] walls = default;
            VillageChief villageChief = default;
            VillageRecoveringMerchant villageRecoveringMerchant = default;
            VillageMaxHPMerchant villageMaxHPMerchant = default;
            VillageATKMerchant villageATKMerchant = default;
            VillageDEFMerchant villageDEFMerchant = default;
            StageUpPortal stageUpPortal = default;
            StageDownPortal stageDownPortal = default;
            UI.Dialog.FrameUI0[] frameUI0 = default;
            FrameUI1[] frameUI1 = default;
            FrameUI2[] frameUI2 = default;
            FrameUI3[] frameUI3 = default;
            FrameUI4[] frameUI4 = default;
            FrameUI5[] frameUI5 = default;
            FrameUI6 frameUI6 = default;
            FrameUI7 frameUI7 = default;
            FrameUI8 frameUI8 = default;
            FrameUI9 frameUI9 = default;
            Slime[] slime = default;
            Fox[] fox = default;
            Goblin[] goblin = default;



            
            ConsoleKey key = default;


            Game.Function.Initializing();

            while (game.IsGameDoing)
            {

                if (game.IsTitleDoing)
                {
                    InitTitle();
                }
                while (game.IsTitleDoing)
                {
                    Thread.Sleep(10);
                    RenderTitle();
                    Input.Process();
                    UpdateTitle();
                }

                if (game.IsStage00Doing)
                {
                    InitStage00();
                }
                while (game.IsStage00Doing)
                {
                    Thread.Sleep(10);
                    RenderStage00();
                    Input.Process();
                    UpdateStage00();
                }

                if (game.IsStage01Doing)
                {
                    InitStage01();
                }
                while (game.IsStage01Doing)
                {
                    Thread.Sleep(10);
                    RenderStage01();
                    Input.Process();
                    UpdateStage01();
                }

                if (game.IsStage02Doing)
                {
                    InitStage02();
                }
                while (game.IsStage02Doing)
                {
                    Thread.Sleep(10);
                    RenderStage02();
                    Input.Process();
                    UpdateStage02();
                }

                if (game.IsStage03Doing)
                {
                    InitStage03();
                }
                while (game.IsStage03Doing)
                {
                    Thread.Sleep(10);
                    RenderStage03();
                    Input.Process();
                    UpdateStage03();
                }

                if (game.IsStage04Doing)
                {
                    InitStage04();
                }
                while (game.IsStage04Doing)
                {
                    Thread.Sleep(10);
                    RenderStage04();
                    Input.Process();
                    UpdateStage04();
                }

            }



            void InitTitle()
            {
                Scene.Render();
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
                    game.IsTitleDoing = false;
                    selectCursor.X = 2;
                    game.IsStage00Doing = true;

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
                string[] lines = Stage.LoadStage(StageNum.Stage00);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out frameUI0, out frameUI1, out frameUI2, out frameUI3, out frameUI4, out frameUI5, out frameUI6, out frameUI7, out frameUI8, out frameUI9,
                                out slime, out fox, out goblin,
                                out villageRecoveringMerchant, out villageMaxHPMerchant, out villageATKMerchant, out villageDEFMerchant);
                Wall.Function.Render(walls);
                VillageChief.Function.Render(villageChief);
                VillageRecoveringMerchant.Render(villageRecoveringMerchant);
                VillageMaxHPMerchant.Render(villageMaxHPMerchant);
                VillageATKMerchant.Render(villageATKMerchant);
                VillageDEFMerchant.Render(villageDEFMerchant);
                StageUpPortal.Function.Render(stageUpPortal);
                FrameUI.Render(frameUI0, frameUI1, frameUI2, frameUI3, frameUI4, frameUI5, frameUI6, frameUI7, frameUI8, frameUI9);
                UIName.Render();
                player.CanMove = true;
                selectCursor.X = Game.DialogCursor_X;
                selectCursor.Y = Game.DialogCursor_Y;
            }
            void RenderStage00()
            {
                Player.Function.RenderPast(player);
                Player.Function.RenderNow(player);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                Player.Interaction.RenderVillageNPC(player, villageChief, villageRecoveringMerchant, villageMaxHPMerchant, villageATKMerchant, villageDEFMerchant);
                Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
                Player.Interaction.RenderRelease(player);
            }
            void UpdateStage00()
            {
                Player.Function.Move(player);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithVillageNPC(player, villageChief, villageRecoveringMerchant, villageMaxHPMerchant, villageATKMerchant, villageDEFMerchant);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Interaction.WithVillageNPC(player, selectCursor, villageChief, villageRecoveringMerchant, villageMaxHPMerchant, villageATKMerchant, villageDEFMerchant);


                Player.Interaction.WithStageUpPortal(player, stageUpPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage00Doing = false;
                        game.IsStage01Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }
                SelectCursor.Function.Move(selectCursor, player);


            }

            void InitStage01()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageNum.Stage01);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out frameUI0, out frameUI1, out frameUI2, out frameUI3, out frameUI4, out frameUI5, out frameUI6, out frameUI7, out frameUI8, out frameUI9,
                                out slime, out fox, out goblin,
                                out villageRecoveringMerchant, out villageMaxHPMerchant, out villageATKMerchant, out villageDEFMerchant);
                Wall.Function.Render(walls);
                StageUpPortal.Function.Render(stageUpPortal);
                StageDownPortal.Function.Render(stageDownPortal);
                FrameUI.Render(frameUI0, frameUI1, frameUI2, frameUI3, frameUI4, frameUI5, frameUI6, frameUI7, frameUI8, frameUI9);
                UIName.Render();
                Slime.InitSlimeRender(slime);
            }
            void RenderStage01()
            {
                Player.Function.RenderPast(player);
                Slime.RenderPast(slime);
                Player.Function.RenderNow(player);
                Slime.RenderNow(slime);
                Slime.RenderBattle(player, slime);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
                Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
                Player.Interaction.RenderRelease(player);
            }
            void UpdateStage01()
            {
                Player.Function.Move(player);
                Player.Collision.WithSlime(player, slime);
                Player.Battle.WithSlime(player, selectCursor, slime);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Collision.WithStageDownPortal(player, stageDownPortal);
                Player.Interaction.WithStageUpPortal(player, stageUpPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage01Doing = false;
                        game.IsStage02Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }
                Player.Interaction.WithStageDownPortal(player, stageDownPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage01Doing = false;
                        game.IsStage00Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }

                SelectCursor.Function.Move(selectCursor, player);

                Slime.Move(slime, player);
                Slime.Update(slime, walls, stageUpPortal, stageDownPortal);
                Slime.Respawn(slime, player);

                if (Player.Function.Die(player))
                {
                    player.CurrentEXP = 0;
                    player.CurrentHP = player.MaxHP;
                    player.X = 12;
                    player.Y = 7;
                    player.PastX = 0;
                    player.PastY = 0;
                    player.Money -= player.Level * 100;
                    if (player.Money < 0)
                    {
                        player.Money = 0;
                    }
                    player.IsOnBattle = false;
                    game.IsStage01Doing = false;
                    game.IsStage00Doing = true;
                }

            }

            void InitStage02()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageNum.Stage02);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out frameUI0, out frameUI1, out frameUI2, out frameUI3, out frameUI4, out frameUI5, out frameUI6, out frameUI7, out frameUI8, out frameUI9,
                                out slime, out fox, out goblin,
                                out villageRecoveringMerchant, out villageMaxHPMerchant, out villageATKMerchant, out villageDEFMerchant);
                Wall.Function.Render(walls);
                StageUpPortal.Function.Render(stageUpPortal);
                StageDownPortal.Function.Render(stageDownPortal);
                FrameUI.Render(frameUI0, frameUI1, frameUI2, frameUI3, frameUI4, frameUI5, frameUI6, frameUI7, frameUI8, frameUI9);
                UIName.Render();
                Fox.InitFoxRender(fox);
            }
            void RenderStage02()
            {
                Player.Function.RenderPast(player);
                Fox.RenderPast(fox);
                Player.Function.RenderNow(player);
                Fox.RenderNow(fox);
                Fox.RenderBattle(player, fox);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
                Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
                Player.Interaction.RenderRelease(player);
            }
            void UpdateStage02()
            {
                Player.Function.Move(player);
                Player.Collision.WithFox(player, fox);
                Player.Battle.WithFox(player, selectCursor, fox);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Collision.WithStageDownPortal(player, stageDownPortal);
                Player.Interaction.WithStageUpPortal(player, stageUpPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage02Doing = false;
                        game.IsStage03Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }
                Player.Interaction.WithStageDownPortal(player, stageDownPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage02Doing = false;
                        game.IsStage01Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }

                SelectCursor.Function.Move(selectCursor, player);

                Fox.Move(fox, player);
                Fox.Update(fox, walls, stageUpPortal, stageDownPortal);
                Fox.Respawn(fox, player);

                if (Player.Function.Die(player))
                {
                    player.CurrentEXP = 0;
                    player.CurrentHP = player.MaxHP;
                    player.X = 12;
                    player.Y = 7;
                    player.PastX = 0;
                    player.PastY = 0;
                    player.Money -= player.Level * 100;
                    if (player.Money < 0)
                    {
                        player.Money = 0;
                    }
                    player.IsOnBattle = false;
                    game.IsStage02Doing = false;
                    game.IsStage00Doing = true;
                }

            }

            void InitStage03()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageNum.Stage03);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out frameUI0, out frameUI1, out frameUI2, out frameUI3, out frameUI4, out frameUI5, out frameUI6, out frameUI7, out frameUI8, out frameUI9,
                                out slime, out fox, out goblin,
                                out villageRecoveringMerchant, out villageMaxHPMerchant, out villageATKMerchant, out villageDEFMerchant);
                Wall.Function.Render(walls);
                StageUpPortal.Function.Render(stageUpPortal);
                StageDownPortal.Function.Render(stageDownPortal);
                FrameUI.Render(frameUI0, frameUI1, frameUI2, frameUI3, frameUI4, frameUI5, frameUI6, frameUI7, frameUI8, frameUI9);
                UIName.Render();
                Goblin.InitGoblinRender(goblin);
            }
            void RenderStage03()
            {
                Player.Function.RenderPast(player);
                Goblin.RenderPast(goblin);
                Player.Function.RenderNow(player);
                Goblin.RenderNow(goblin);
                Goblin.RenderBattle(player, goblin);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                Player.Interaction.RenderStageUpPortal(player, stageUpPortal);
                Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
                Player.Interaction.RenderRelease(player);
            }
            void UpdateStage03()
            {
                Player.Function.Move(player);
                Player.Collision.WithGoblin(player, goblin);
                Player.Battle.WithGoblin(player, selectCursor, goblin);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithStageUpPortal(player, stageUpPortal);
                Player.Collision.WithStageDownPortal(player, stageDownPortal);
                Player.Interaction.WithStageUpPortal(player, stageUpPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage03Doing = false;
                        game.IsStage04Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }
                Player.Interaction.WithStageDownPortal(player, stageDownPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage03Doing = false;
                        game.IsStage02Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }

                SelectCursor.Function.Move(selectCursor, player);

                Goblin.Move(goblin, player);
                Goblin.Update(goblin, walls, stageUpPortal, stageDownPortal);
                Goblin.Respawn(goblin, player);

                if (Player.Function.Die(player))
                {
                    player.CurrentEXP = 0;
                    player.CurrentHP = player.MaxHP;
                    player.X = 12;
                    player.Y = 7;
                    player.PastX = 0;
                    player.PastY = 0;
                    player.Money -= player.Level * 100;
                    if (player.Money < 0)
                    {
                        player.Money = 0;
                    }
                    player.IsOnBattle = false;
                    game.IsStage03Doing = false;
                    game.IsStage00Doing = true;
                }

            }

            void InitStage04()
            {
                Console.Clear();
                string[] lines = Stage.LoadStage(StageNum.Stage04);
                Stage.ParseStage(lines, out walls, out villageChief, out stageUpPortal, out stageDownPortal,
                                out frameUI0, out frameUI1, out frameUI2, out frameUI3, out frameUI4, out frameUI5, out frameUI6, out frameUI7, out frameUI8, out frameUI9,
                                out slime, out fox, out goblin,
                                out villageRecoveringMerchant, out villageMaxHPMerchant, out villageATKMerchant, out villageDEFMerchant);
                Wall.Function.Render(walls);
                StageDownPortal.Function.Render(stageDownPortal);
                FrameUI.Render(frameUI0, frameUI1, frameUI2, frameUI3, frameUI4, frameUI5, frameUI6, frameUI7, frameUI8, frameUI9);
                UIName.Render();
                KingSlime.InitKingSlimeRender(kingSlime);
            }
            void RenderStage04()
            {
                Player.Function.RenderPast(player);
                KingSlime.RenderPast(kingSlime);
                Player.Function.RenderNow(player);
                KingSlime.RenderNow(kingSlime);
                KingSlime.RenderBattle(player, kingSlime);
                UIState.Render(player);
                SelectCursor.Function.Render(selectCursor, player);
                Player.Interaction.RenderStageDownPortal(player, stageDownPortal);
                Player.Interaction.RenderRelease(player);
            }
            void UpdateStage04()
            {
                Player.Function.Move(player);
                Player.Collision.WithKingSlime(player, kingSlime);
                Player.Battle.WithKingSlime(player, selectCursor, kingSlime);
                Player.Function.LevelUp(player);
                Player.Collision.WithWall(player, walls);
                Player.Collision.WithStageDownPortal(player, stageDownPortal);
                Player.Interaction.WithStageDownPortal(player, stageDownPortal);
                if (false == player.CanMove && Player.Interaction.IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (SelectCursor.Function.SelectYes(selectCursor))
                    {
                        player.CanMove = true;
                        game.IsStage04Doing = false;
                        game.IsStage03Doing = true;

                    }
                    if (SelectCursor.Function.SelectNo(selectCursor))
                    {
                        player.CanMove = true;
                    }
                }

                SelectCursor.Function.Move(selectCursor, player);

                KingSlime.Move(kingSlime, player);
                KingSlime.Update(kingSlime, walls, stageDownPortal);

                if (Player.Function.Die(player))
                {
                    player.CurrentEXP = 0;
                    player.CurrentHP = player.MaxHP;
                    player.X = 12;
                    player.Y = 7;
                    player.PastX = 0;
                    player.PastY = 0;
                    player.Money -= player.Level * 100;
                    if(player.Money < 0)
                    {
                        player.Money = 0;
                    }
                    player.IsOnBattle = false;
                    game.IsStage04Doing = false;
                    game.IsStage00Doing = true;
                }

            }
        }
    }
}