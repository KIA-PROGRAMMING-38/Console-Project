using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
    public class Player
    {
        public int X;
        public int Y;
        public int PastX;
        public int PastY;
        public Direction MoveDirection;
        public bool CanMove;
        public bool IsOnBattle;
        public int ATK;
        public int DEF;
        public int CurrentHP;
        public int MaxHP;
        public int Level;
        public int CurrentEXP;
        public int MaxEXP;
        public int Money;
        public bool GameClear;
        public int MonsterIndex;
        public bool BeginnerSupport;
        public static void RenderPast(Player player)
        {
            Game.ObjRender(player.PastX, player.PastY, "☺", ConsoleColor.White);
        }
        public static void RenderNow(Player player)
        {
            Game.ObjRender(player.X, player.Y, "☺", ConsoleColor.Black);
        }
        public static void RenderState(Player player)
        {
            Game.ObjRender(Game.Level_HP_Money_X, Game.Level_EXP_Battle_Y + 1, $" {player.Level:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.EXP_X, Game.Level_EXP_Battle_Y + 1, $" {player.CurrentEXP:D3}/{player.MaxEXP:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.HP_STATUS_Y + 1, $" ATK: {player.ATK:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.HP_STATUS_Y + 2, $" DEF: {player.DEF:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Level_HP_Money_X, Game.Money_STATUS_Y + 1, $" {player.Money:D8} G", ConsoleColor.Black);
        }
        public static void Move(Player player)
        {
            if (player.CanMove)
            {
                if (Input.IsKeyDown(ConsoleKey.LeftArrow))
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    --player.X;
                    player.MoveDirection = Direction.Left;
                }
                if (Input.IsKeyDown(ConsoleKey.RightArrow))
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    ++player.X;
                    player.MoveDirection = Direction.Right;
                }
                if (Input.IsKeyDown(ConsoleKey.UpArrow))
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    --player.Y;
                    player.MoveDirection = Direction.Up;
                }
                if (Input.IsKeyDown(ConsoleKey.DownArrow))
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    ++player.Y;
                    player.MoveDirection = Direction.Down;
                }
            }
        }

        public static void LevelUp(Player player)
        {
            if (player.CurrentEXP >= player.MaxEXP)
            {
                player.CurrentEXP -= player.MaxEXP;
                player.MaxEXP += 10;
                player.Level += 1;
                player.MaxHP += 10;
                player.CurrentHP = player.MaxHP;
                player.ATK += 1;
                player.DEF += 1;
            }
        }
        public static void LimitState(Player player)
        {
            if (player.CurrentEXP > 999)
            {
                player.CurrentEXP = 999;
            }
            if (player.MaxEXP > 999)
            {
                player.MaxEXP = 999;
            }
            if (player.Level > 999)
            {
                player.Level = 999;
            }
            if (player.CurrentHP > 999)
            {
                player.CurrentHP = 999;
            }
            if (player.MaxHP > 999)
            {
                player.MaxHP = 999;
            }
            if (player.ATK > 999)
            {
                player.ATK = 999;
            }
            if (player.DEF > 999)
            {
                player.DEF = 999;
            }
            if (player.Money > 99999999)
            {
                player.Money = 99999999;
            }
        }
        public static void Die(Player player, SelectCursor selectCursor)
        {
            if (player.CurrentHP <= 0)
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
                player.CanMove = true;
                selectCursor.X = Game.DialogCursor_X;
                selectCursor.Y = Game.DialogCursor_Y;
                selectCursor.PastY = Game.DialogCursor_Y;
                Stage.SetNextStage(StageNum.Stage00);
            }
        }
        public static class Collision
        {
            private static bool isCollision(Player player, int objX, int objY)
            {
                if (player.X == objX && player.Y == objY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static void Back(Player player, int objX, int objY)
            {
                switch (player.MoveDirection)
                {
                    case Direction.Left:
                        player.X = objX + 1;
                        break;
                    case Direction.Right:
                        player.X = objX - 1;
                        break;
                    case Direction.Up:
                        player.Y = objY + 1;
                        break;
                    case Direction.Down:
                        player.Y = objY - 1;
                        break;
                    default:
                        Game.ExitWithError($"플레이어 이동 방향 데이터 오류{player.MoveDirection}");
                        break;
                }
            }
            public static void WithWall(Player player, Wall[] walls)
            {
                for (int i = 0; i < walls.Length; ++i)
                {
                    if (false == isCollision(player, walls[i].X, walls[i].Y))
                    {
                        continue;
                    }

                    Back(player, walls[i].X, walls[i].Y);
                    break;
                }
            }
            public static void WithVillageNPC(Player player, VillageNPC[] villageNPCs)
            {
                if (isCollision(player, villageNPCs[0].X, villageNPCs[0].Y))
                {
                    Back(player, villageNPCs[0].X, villageNPCs[0].Y);
                }
                if (isCollision(player, villageNPCs[1].X, villageNPCs[1].Y))
                {
                    Back(player, villageNPCs[1].X, villageNPCs[1].Y);
                }
                if (isCollision(player, villageNPCs[2].X, villageNPCs[2].Y))
                {
                    Back(player, villageNPCs[2].X, villageNPCs[2].Y);
                }
                if (isCollision(player, villageNPCs[3].X, villageNPCs[3].Y))
                {
                    Back(player, villageNPCs[3].X, villageNPCs[3].Y);
                }
                if (isCollision(player, villageNPCs[4].X, villageNPCs[4].Y))
                {
                    Back(player, villageNPCs[4].X, villageNPCs[4].Y);
                }
            }
            public static void WithStageUpPortal(Player player, StageUpPortal stageUpPortal)
            {
                if (isCollision(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    Back(player, stageUpPortal.X, stageUpPortal.Y);
                }
            }
            public static void WithStageDownPortal(Player player, StageDownPortal stageDownPortal)
            {
                if (isCollision(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    Back(player, stageDownPortal.X, stageDownPortal.Y);
                }
            }
            public static void WithSlime(Player player, Slime[] slimes)
            {
                for (int i = 0; i < slimes.Length; ++i)
                {
                    if (isCollision(player, slimes[i].X, slimes[i].Y) && slimes[i].Alive)
                    {
                        player.CanMove = false;
                        player.IsOnBattle = true;
                        player.MonsterIndex = i;
                        break;
                    }
                }
            }
            public static void WithFox(Player player, Fox[] foxes)
            {
                for (int i = 0; i < foxes.Length; ++i)
                {
                    if (isCollision(player, foxes[i].X, foxes[i].Y) && foxes[i].Alive)
                    {
                        player.CanMove = false;
                        player.IsOnBattle = true;
                        player.MonsterIndex = i;
                        break;
                    }
                }
            }
            public static void WithGoblin(Player player, Goblin[] goblins)
            {
                for (int i = 0; i < goblins.Length; ++i)
                {
                    if (isCollision(player, goblins[i].X, goblins[i].Y) && goblins[i].Alive)
                    {
                        player.CanMove = false;
                        player.IsOnBattle = true;
                        player.MonsterIndex = i;
                        break;
                    }
                }
            }
            public static void WithKingSlime(Player player, KingSlime kingSlime)
            {
                if (isCollision(player, kingSlime.X, kingSlime.Y) && kingSlime.Alive)
                {
                    player.CanMove = false;
                    player.IsOnBattle = true;
                }
            }
        }
        public static class Interaction
        {
            public static void RenderVillageNPC(Player player, VillageNPC[] villageNPCs)
            {
                if (false == player.CanMove)
                {
                    if (IsFront(player, villageNPCs[0].X, villageNPCs[0].Y))
                    {
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 촌장 ", ConsoleColor.Black);
                        if (false == player.BeginnerSupport)
                        {
                            Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"지원 물품을 가져가게나", ConsoleColor.Black);
                        }
                        else if (player.GameClear)
                        {
                            Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"자네는 마을의 영웅일세 고맙네", ConsoleColor.Black);
                            Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $"더 넓은 세상으로 가겠는가?", ConsoleColor.Black);
                        }
                        else
                        {
                            Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"킹 슬라임을 처치하고 돌아와 주게", ConsoleColor.Black);
                        }
                    }
                    if (IsFront(player, villageNPCs[1].X, villageNPCs[1].Y))
                    {
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 회복상인 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 회복량: 100 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 10 ", ConsoleColor.Black);
                    }
                    if (IsFront(player, villageNPCs[2].X, villageNPCs[2].Y))
                    {
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 체력강화상인 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 10 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 20 ", ConsoleColor.Black);
                    }
                    if (IsFront(player, villageNPCs[3].X, villageNPCs[3].Y))
                    {
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 공격강화상인 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 1 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 50 ", ConsoleColor.Black);
                    }
                    if (IsFront(player, villageNPCs[4].X, villageNPCs[4].Y))
                    {
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 방어강화상인 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 1 ", ConsoleColor.Black);
                        Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 50 ", ConsoleColor.Black);
                    }
                }
            }

            public static void RenderStageUpPortal(Player player, StageUpPortal stageUpPortal)
            {
                if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {

                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 포탈 ", ConsoleColor.Black);
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"이동하시겠습니까?", ConsoleColor.Black);
                }
            }
            public static void RenderStageDownPortal(Player player, StageDownPortal stageDownPortal)
            {
                if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 포탈 ", ConsoleColor.Black);
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"이동하시겠습니까?", ConsoleColor.Black);
                }
            }
            public static void RenderRelease(Player player)
            {
                if (player.CanMove)
                {
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $"━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Black);
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"                                         ", ConsoleColor.White);
                    Game.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $"                                         ", ConsoleColor.White);
                }
            }
            public static bool IsFront(Player player, int objX, int objY)
            {
                if (player.X + 1 == objX && player.Y == objY)
                {
                    return true;
                }
                else if (player.X - 1 == objX && player.Y == objY)
                {
                    return true;
                }
                else if (player.X == objX && player.Y + 1 == objY)
                {
                    return true;
                }
                else if (player.X == objX && player.Y - 1 == objY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static void WithStageUpPortal00(Player player, StageUpPortal stageUpPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage01);
                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            public static void WithStageUpPortal01(Player player, StageUpPortal stageUpPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage02);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            public static void WithStageUpPortal02(Player player, StageUpPortal stageUpPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage03);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            public static void WithStageUpPortal03(Player player, StageUpPortal stageUpPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage04);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            public static void WithStageDownPortal01(Player player, StageDownPortal stageDownPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage00);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }

            }
            public static void WithStageDownPortal02(Player player, StageDownPortal stageDownPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage01);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }

            }
            public static void WithStageDownPortal03(Player player, StageDownPortal stageDownPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage02);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }

            }
            public static void WithStageDownPortal04(Player player, StageDownPortal stageDownPortal, SelectCursor selectCursor)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            player.CanMove = true;
                            Stage.SetNextStage(StageNum.Stage03);

                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }

            }
            public static void WithVillageNPC(Player player, SelectCursor selectCursor, VillageNPC[] villageNPCs)
            {
                if (IsFront(player, villageNPCs[0].X, villageNPCs[0].Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (false == player.BeginnerSupport)
                        {
                            if (SelectCursor.SelectYes(selectCursor))
                            {
                                player.BeginnerSupport = true;
                                VillageNPC.GetBeginnerSupport(player);
                                player.CanMove = true;
                            }
                            if (SelectCursor.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                        else if (player.GameClear)
                        {
                            if (SelectCursor.SelectYes(selectCursor))
                            {
                                Game.GameClear();
                            }
                            if (SelectCursor.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                        else
                        {

                            if (SelectCursor.SelectYes(selectCursor))
                            {
                                player.CanMove = true;
                            }
                            if (SelectCursor.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                    }
                }
                if (IsFront(player, villageNPCs[1].X, villageNPCs[1].Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            VillageNPC.BuyRecovering(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
                if (IsFront(player, villageNPCs[2].X, villageNPCs[2].Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            VillageNPC.BuyMaxHP(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
                if (IsFront(player, villageNPCs[3].X, villageNPCs[3].Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            VillageNPC.BuyATK(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
                if (IsFront(player, villageNPCs[4].X, villageNPCs[4].Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.SelectYes(selectCursor))
                        {
                            VillageNPC.BuyDEF(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
        }
        public static class Battle
        {
            public static void WithSlime(Player player, Slime[] slimes, SelectCursor selectCursor)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        slimes[player.MonsterIndex].CurrentHP = slimes[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.SelectAttack(selectCursor))
                    {
                        while (player.CurrentHP >= 0 && slimes[player.MonsterIndex].Alive)
                        {
                            Thread.Sleep(20);
                            if (player.ATK == slimes[player.MonsterIndex].DEF &&
                                player.DEF == slimes[player.MonsterIndex].ATK)
                            {
                                break;
                            }
                            if (player.ATK - slimes[player.MonsterIndex].DEF > 0)
                            {
                                slimes[player.MonsterIndex].CurrentHP -= player.ATK - slimes[player.MonsterIndex].DEF;
                                if (slimes[player.MonsterIndex].CurrentHP <= 0)
                                {
                                    slimes[player.MonsterIndex].CurrentHP = 0;
                                    slimes[player.MonsterIndex].Alive = false;
                                    player.CurrentEXP += slimes[player.MonsterIndex].EXP;
                                    player.Money += slimes[player.MonsterIndex].Money;

                                    player.CanMove = true;
                                    player.IsOnBattle = false;
                                    selectCursor.X = Game.DialogCursor_X;
                                    selectCursor.Y = Game.DialogCursor_Y;
                                    selectCursor.PastY = Game.DialogCursor_Y;
                                    RenderNow(player);
                                }
                                Slime.RenderHP(slimes[player.MonsterIndex]);
                            }
                            if (slimes[player.MonsterIndex].Alive && slimes[player.MonsterIndex].ATK - player.DEF > 0)
                            {
                                player.CurrentHP -= slimes[player.MonsterIndex].ATK - player.DEF;
                                Game.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
                            }
                        }
                    }
                    if (SelectCursor.SelectEscape(selectCursor))
                    {
                        player.CanMove = true;
                        player.IsOnBattle = false;
                        player.X = player.PastX;
                        player.Y = player.PastY;
                        selectCursor.X = Game.DialogCursor_X;
                        selectCursor.Y = Game.DialogCursor_Y;
                        selectCursor.PastY = Game.DialogCursor_Y;
                    }
                }
            }
            public static void WithFox(Player player, Fox[] foxes, SelectCursor selectCursor)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        foxes[player.MonsterIndex].CurrentHP = foxes[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.SelectAttack(selectCursor))
                    {
                        while (player.CurrentHP >= 0 && foxes[player.MonsterIndex].Alive)
                        {
                            Thread.Sleep(20);
                            if (player.ATK == foxes[player.MonsterIndex].DEF &&
                                player.DEF == foxes[player.MonsterIndex].ATK)
                            {
                                break;
                            }
                            if (player.ATK - foxes[player.MonsterIndex].DEF > 0)
                            {
                                foxes[player.MonsterIndex].CurrentHP -= player.ATK - foxes[player.MonsterIndex].DEF;
                                if (foxes[player.MonsterIndex].CurrentHP <= 0)
                                {
                                    foxes[player.MonsterIndex].CurrentHP = 0;
                                    foxes[player.MonsterIndex].Alive = false;
                                    player.CurrentEXP += foxes[player.MonsterIndex].EXP;
                                    player.Money += foxes[player.MonsterIndex].Money;

                                    player.CanMove = true;
                                    player.IsOnBattle = false;
                                    selectCursor.X = Game.DialogCursor_X;
                                    selectCursor.Y = Game.DialogCursor_Y;
                                    selectCursor.PastY = Game.DialogCursor_Y;
                                    RenderNow(player);
                                }
                                Fox.RenderHP(foxes[player.MonsterIndex]);
                            }
                            if (foxes[player.MonsterIndex].Alive && foxes[player.MonsterIndex].ATK - player.DEF > 0)
                            {
                                player.CurrentHP -= foxes[player.MonsterIndex].ATK - player.DEF;
                                Game.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
                            }
                        }
                    }
                    if (SelectCursor.SelectEscape(selectCursor))
                    {
                        player.CanMove = true;
                        player.IsOnBattle = false;
                        player.X = player.PastX;
                        player.Y = player.PastY;
                        selectCursor.X = Game.DialogCursor_X;
                        selectCursor.Y = Game.DialogCursor_Y;
                        selectCursor.PastY = Game.DialogCursor_Y;
                    }
                }
            }
            public static void WithGoblin(Player player, Goblin[] goblins, SelectCursor selectCursor)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        goblins[player.MonsterIndex].CurrentHP = goblins[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.SelectAttack(selectCursor))
                    {
                        while (player.CurrentHP >= 0 && goblins[player.MonsterIndex].Alive)
                        {
                            Thread.Sleep(20);
                            if (player.ATK == goblins[player.MonsterIndex].DEF &&
                                player.DEF == goblins[player.MonsterIndex].ATK)
                            {
                                break;
                            }
                            if (player.ATK - goblins[player.MonsterIndex].DEF > 0)
                            {
                                goblins[player.MonsterIndex].CurrentHP -= player.ATK - goblins[player.MonsterIndex].DEF;
                                if (goblins[player.MonsterIndex].CurrentHP <= 0)
                                {
                                    goblins[player.MonsterIndex].CurrentHP = 0;
                                    goblins[player.MonsterIndex].Alive = false;
                                    player.CurrentEXP += goblins[player.MonsterIndex].EXP;
                                    player.Money += goblins[player.MonsterIndex].Money;

                                    player.CanMove = true;
                                    player.IsOnBattle = false;
                                    selectCursor.X = Game.DialogCursor_X;
                                    selectCursor.Y = Game.DialogCursor_Y;
                                    selectCursor.PastY = Game.DialogCursor_Y;
                                    RenderNow(player);
                                }
                                Goblin.RenderHP(goblins[player.MonsterIndex]);
                            }
                            if (goblins[player.MonsterIndex].Alive && goblins[player.MonsterIndex].ATK - player.DEF > 0)
                            {
                                player.CurrentHP -= goblins[player.MonsterIndex].ATK - player.DEF;
                                Game.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
                            }
                        }
                    }
                    if (SelectCursor.SelectEscape(selectCursor))
                    {
                        player.CanMove = true;
                        player.IsOnBattle = false;
                        player.X = player.PastX;
                        player.Y = player.PastY;
                        selectCursor.X = Game.DialogCursor_X;
                        selectCursor.Y = Game.DialogCursor_Y;
                        selectCursor.PastY = Game.DialogCursor_Y;
                    }
                }
            }
            public static void WithKingSlime(Player player, KingSlime kingSlime, SelectCursor selectCursor)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        kingSlime.CurrentHP = kingSlime.MaxHP;
                    }
                    if (SelectCursor.SelectAttack(selectCursor))
                    {
                        while (player.CurrentHP >= 0 && kingSlime.Alive)
                        {
                            Thread.Sleep(20);
                            if (player.ATK == kingSlime.DEF &&
                                player.DEF == kingSlime.ATK)
                            {
                                break;
                            }
                            if (player.ATK - kingSlime.DEF > 0)
                            {
                                kingSlime.CurrentHP -= player.ATK - kingSlime.DEF;
                                if (kingSlime.CurrentHP <= 0)
                                {
                                    kingSlime.CurrentHP = 0;
                                    kingSlime.Alive = false;
                                    player.CurrentEXP += kingSlime.EXP;
                                    player.Money += kingSlime.Money;

                                    player.CanMove = true;
                                    player.IsOnBattle = false;
                                    player.GameClear = true;
                                    selectCursor.X = Game.DialogCursor_X;
                                    selectCursor.Y = Game.DialogCursor_Y;
                                    selectCursor.PastY = Game.DialogCursor_Y;
                                    RenderNow(player);
                                }
                                KingSlime.RenderHP(kingSlime);
                            }
                            if (kingSlime.Alive && kingSlime.ATK - player.DEF > 0)
                            {
                                player.CurrentHP -= kingSlime.ATK - player.DEF;
                                Game.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
                            }
                        }
                    }
                    if (SelectCursor.SelectEscape(selectCursor))
                    {
                        player.CanMove = true;
                        player.IsOnBattle = false;
                        player.X = player.PastX;
                        player.Y = player.PastY;
                        selectCursor.X = Game.DialogCursor_X;
                        selectCursor.Y = Game.DialogCursor_Y;
                        selectCursor.PastY = Game.DialogCursor_Y;
                    }
                }
            }
        }
    }
}