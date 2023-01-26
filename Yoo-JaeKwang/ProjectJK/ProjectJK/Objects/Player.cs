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
        public static class Function
        {
            public static void RenderPast(Player player)
            {
                Game.Function.ObjRender(player.PastX, player.PastY, "☺", ConsoleColor.White);
            }
            public static void RenderNow(Player player)
            {
                Game.Function.ObjRender(player.X, player.Y, "☺", ConsoleColor.Black);
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
                if(player.CurrentEXP >= player.MaxEXP)
                {
                    player.CurrentEXP -= player.MaxEXP;
                    player.MaxEXP += 10;
                    if (player.MaxEXP > 999)
                    {
                        player.MaxEXP = 999;
                    }
                    player.Level += 1;
                    if (player.Level > 999)
                    {
                        player.Level = 999;
                    }
                    player.MaxHP += 10;
                    if (player.MaxHP > 999)
                    {
                        player.MaxHP = 999;
                    }
                    player.CurrentHP = player.MaxHP;
                    player.ATK += 1;
                    if (player.ATK > 999)
                    {
                        player.ATK = 999;
                    }
                    player.DEF += 1;
                    if (player.DEF > 999)
                    {
                        player.DEF = 999;
                    }
                }
            }
            public static bool Die(Player player)
            {
                if (player.CurrentHP <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                        Game.Function.ExitWithError($"플레이어 이동 방향 데이터 오류{player.MoveDirection}");
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
            public static void WithVillageNPC(Player player, VillageChief villageChief, VillageRecoveringMerchant villageRecoveringMerchant, VillageMaxHPMerchant villageMaxHPMerchant,
                                                                                            VillageATKMerchant villageATKMerchant, VillageDEFMerchant villageDEFMerchant)
            {
                WithVillageChief(player, villageChief);
                WithVillageRecoveringMerchant(player, villageRecoveringMerchant);
                WithVillageMaxHPMerchant(player, villageMaxHPMerchant);
                WithVillageATKMerchant(player, villageATKMerchant);
                WithVillageDEFMerchant(player, villageDEFMerchant);
            }
            private static void WithVillageChief(Player player, VillageChief villageChief)
            {
                if (isCollision(player, villageChief.X, villageChief.Y))
                {
                    Back(player, villageChief.X, villageChief.Y);
                }
            }
            private static void WithVillageRecoveringMerchant(Player player, VillageRecoveringMerchant villageRecoveringMerchant)
            {
                if (isCollision(player, villageRecoveringMerchant.X, villageRecoveringMerchant.Y))
                {
                    Back(player, villageRecoveringMerchant.X, villageRecoveringMerchant.Y);
                }
            }
            private static void WithVillageMaxHPMerchant(Player player, VillageMaxHPMerchant villageMaxHPMerchant)
            {
                if (isCollision(player, villageMaxHPMerchant.X, villageMaxHPMerchant.Y))
                {
                    Back(player, villageMaxHPMerchant.X, villageMaxHPMerchant.Y);
                }
            }
            private static void WithVillageATKMerchant(Player player, VillageATKMerchant villageATKMerchant)
            {
                if (isCollision(player, villageATKMerchant.X, villageATKMerchant.Y))
                {
                    Back(player, villageATKMerchant.X, villageATKMerchant.Y);
                }
            }
            private static void WithVillageDEFMerchant(Player player, VillageDEFMerchant villageDEFMerchant)
            {
                if (isCollision(player, villageDEFMerchant.X, villageDEFMerchant.Y))
                {
                    Back(player, villageDEFMerchant.X, villageDEFMerchant.Y);
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
            public static void WithSlime(Player player, Slime[] slime)
            {
                for (int i = 0; i < slime.Length; ++i)
                {
                    if (isCollision(player, slime[i].X, slime[i].Y) && slime[i].Alive)
                    {
                        player.CanMove = false;
                        player.IsOnBattle = true;
                        player.MonsterIndex = i;
                        break;
                    }
                }
            }
            public static void WithFox(Player player, Fox[] fox)
            {
                for (int i = 0; i < fox.Length; ++i)
                {
                    if (isCollision(player, fox[i].X, fox[i].Y) && fox[i].Alive)
                    {
                        player.CanMove = false;
                        player.IsOnBattle = true;
                        player.MonsterIndex = i;
                        break;
                    }
                }
            }
            public static void WithGoblin(Player player, Goblin[] goblin)
            {
                for (int i = 0; i < goblin.Length; ++i)
                {
                    if (isCollision(player, goblin[i].X, goblin[i].Y) && goblin[i].Alive)
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
            public static void RenderVillageNPC(Player player, VillageChief villageChief, VillageRecoveringMerchant villageRecoveringMerchant, VillageMaxHPMerchant villageMaxHPMerchant,
                                                                                            VillageATKMerchant villageATKMerchant, VillageDEFMerchant villageDEFMerchant)
            {
                RenderVillageChief(player, villageChief);
                RenderVillageRecoveringMerchant(player, villageRecoveringMerchant);
                RenderVillageMaxHPMerchant(player, villageMaxHPMerchant);
                RenderVillageATKMerchant(player, villageATKMerchant);
                RenderVillageDEFMerchant(player, villageDEFMerchant);

            }
            private static void RenderVillageChief(Player player, VillageChief villageChief)
            {
                if (false == player.CanMove && IsFront(player, villageChief.X, villageChief.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 촌장 ", ConsoleColor.Black);
                    if (false == player.BeginnerSupport)
                    {
                        Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"초보자 지원 물품을 가져가게나.", ConsoleColor.Black);
                    }
                    else if (player.GameClear)
                    {
                        Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"자네는 마을의 영웅일세 고맙네", ConsoleColor.Black);
                        Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $"더 넓은 세상으로 가겠는가?", ConsoleColor.Black);
                    }
                    else
                    {
                        Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"뭐", ConsoleColor.Black);
                    }
                }
            }
            private static void RenderVillageRecoveringMerchant(Player player, VillageRecoveringMerchant villageRecoveringMerchant)
            {
                if (false == player.CanMove && IsFront(player, villageRecoveringMerchant.X, villageRecoveringMerchant.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 회복상인 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 회복량: 100 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 10 ", ConsoleColor.Black);
                }
            }
            private static void RenderVillageMaxHPMerchant(Player player, VillageMaxHPMerchant villageMaxHPMerchant)
            {
                if (false == player.CanMove && IsFront(player, villageMaxHPMerchant.X, villageMaxHPMerchant.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 체력강화상인 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 10 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 50 ", ConsoleColor.Black);
                }
            }
            private static void RenderVillageATKMerchant(Player player, VillageATKMerchant villageATKMerchant)
            {
                if (false == player.CanMove && IsFront(player, villageATKMerchant.X, villageATKMerchant.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 공격강화상인 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 1 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 50 ", ConsoleColor.Black);
                }
            }
            private static void RenderVillageDEFMerchant(Player player, VillageDEFMerchant villageDEFMerchant)
            {
                if (false == player.CanMove && IsFront(player, villageDEFMerchant.X, villageDEFMerchant.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 방어강화상인 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $" 증가량: 1 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $" 비용: 50 ", ConsoleColor.Black);
                }
            }

            public static void RenderStageUpPortal(Player player, StageUpPortal stageUpPortal)
            {
                if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {

                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 포탈 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"이동하시겠습니까?", ConsoleColor.Black);
                }
            }
            public static void RenderStageDownPortal(Player player, StageDownPortal stageDownPortal)
            {
                if (false == player.CanMove && IsFront(player, stageDownPortal.X, stageDownPortal.Y))
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $" 포탈 ", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"이동하시겠습니까?", ConsoleColor.Black);
                }
            }
            public static void RenderRelease(Player player)
            {
                if (player.CanMove)
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 4, $"━━━━━━━━━━━━━━━━━━━━━━━", ConsoleColor.Black);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 3, $"                                         ", ConsoleColor.White);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y - 2, $"                                         ", ConsoleColor.White);
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
            public static void WithStageUpPortal(Player player, /*Game game, */StageUpPortal stageUpPortal)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    //if (false == player.CanMove && IsFront(player, stageUpPortal.X, stageUpPortal.Y))
                    //{
                    //    if (SelectCursor.Function.SelectYes(selectCursor))
                    //    {
                    //        player.CanMove = true;
                    //        game.isStage00Doing = false;
                    //        isStage01Doing = true;

                    //    }
                    //    if (SelectCursor.Function.SelectNo(selectCursor))
                    //    {
                    //        player.CanMove = true;
                    //    }
                    //}
                }
            }
            public static void WithStageDownPortal(Player player, StageDownPortal stageDownPortal)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y) && Input.IsKeyDown(ConsoleKey.E))
                {
                    player.CanMove = false;
                }

            }
            public static void WithVillageNPC(Player player, SelectCursor selectCursor, VillageChief villageChief, VillageRecoveringMerchant villageRecoveringMerchant, VillageMaxHPMerchant villageMaxHPMerchant,
                                                                                            VillageATKMerchant villageATKMerchant, VillageDEFMerchant villageDEFMerchant)
            {
                WithVillageChief(player, selectCursor, villageChief);
                WithVillageRecoveringMerchant(player, selectCursor, villageRecoveringMerchant);
                WithVillageMaxHPMerchant(player, selectCursor, villageMaxHPMerchant);
                WithVillageATKMerchant(player, selectCursor, villageATKMerchant);
                WithVillageDEFMerchant(player, selectCursor, villageDEFMerchant);
            }
            private static void WithVillageChief(Player player, SelectCursor selectCursor, VillageChief villageChief)
            {
                if (IsFront(player, villageChief.X, villageChief.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (false == player.BeginnerSupport)
                        {
                            if (SelectCursor.Function.SelectYes(selectCursor))
                            {
                                player.BeginnerSupport = true;
                                VillageChief.Function.GetBeginnerSupport(player);
                                player.CanMove = true;
                            }
                            if (SelectCursor.Function.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                        else if (player.GameClear)
                        {
                            if (SelectCursor.Function.SelectYes(selectCursor))
                            {
                                Game.Function.GameClear();
                            }
                            if (SelectCursor.Function.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                        else
                        {

                            if (SelectCursor.Function.SelectYes(selectCursor))
                            {
                                player.CanMove = true;
                            }
                            if (SelectCursor.Function.SelectNo(selectCursor))
                            {
                                player.CanMove = true;
                            }
                        }
                    }
                }
            }
            private static void WithVillageRecoveringMerchant(Player player, SelectCursor selectCursor, VillageRecoveringMerchant villageRecoveringMerchant)
            {
                if (IsFront(player, villageRecoveringMerchant.X, villageRecoveringMerchant.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            VillageRecoveringMerchant.Buy(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            private static void WithVillageMaxHPMerchant(Player player, SelectCursor selectCursor, VillageMaxHPMerchant villageMaxHPMerchant)
            {
                if (IsFront(player, villageMaxHPMerchant.X, villageMaxHPMerchant.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            VillageMaxHPMerchant.Buy(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            private static void WithVillageATKMerchant(Player player, SelectCursor selectCursor, VillageATKMerchant villageATKMerchant)
            {
                if (IsFront(player, villageATKMerchant.X, villageATKMerchant.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            VillageATKMerchant.Buy(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
            private static void WithVillageDEFMerchant(Player player, SelectCursor selectCursor, VillageDEFMerchant villageDEFMerchant)
            {
                if (IsFront(player, villageDEFMerchant.X, villageDEFMerchant.Y))
                {
                    if (Input.IsKeyDown(ConsoleKey.E))
                    {
                        player.CanMove = false;
                    }
                    if (false == player.CanMove)
                    {
                        if (SelectCursor.Function.SelectYes(selectCursor))
                        {
                            VillageDEFMerchant.Buy(player);
                            player.CanMove = true;
                        }
                        if (SelectCursor.Function.SelectNo(selectCursor))
                        {
                            player.CanMove = true;
                        }
                    }
                }
            }
        }
        public static class Battle
        {
            public static void WithSlime(Player player, SelectCursor selectCursor, Slime[] slime)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        slime[player.MonsterIndex].CurrentHP = slime[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.Function.SelectAttack(selectCursor))
                    {
                        if (player.ATK - slime[player.MonsterIndex].DEF > 0)
                        {
                            slime[player.MonsterIndex].CurrentHP -= player.ATK - slime[player.MonsterIndex].DEF;
                            if (slime[player.MonsterIndex].CurrentHP <= 0)
                            {
                                slime[player.MonsterIndex].Alive = false;
                                player.CurrentEXP += slime[player.MonsterIndex].EXP;
                                player.Money += slime[player.MonsterIndex].Money;

                                player.CanMove = true;
                                player.IsOnBattle = false;
                                selectCursor.X = Game.DialogCursor_X;
                                selectCursor.Y = Game.DialogCursor_Y;
                                selectCursor.PastY = Game.DialogCursor_Y;
                                Function.RenderNow(player);
                            }
                        }
                        if (slime[player.MonsterIndex].Alive && slime[player.MonsterIndex].ATK - player.DEF > 0)
                        {
                            player.CurrentHP -= slime[player.MonsterIndex].ATK - player.DEF;
                        }
                    }
                    if (SelectCursor.Function.SelectEscape(selectCursor))
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
            public static void WithFox(Player player, SelectCursor selectCursor, Fox[] fox)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        fox[player.MonsterIndex].CurrentHP = fox[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.Function.SelectAttack(selectCursor))
                    {
                        if (player.ATK - fox[player.MonsterIndex].DEF > 0)
                        {
                            fox[player.MonsterIndex].CurrentHP -= player.ATK - fox[player.MonsterIndex].DEF;
                            if (fox[player.MonsterIndex].CurrentHP <= 0)
                            {
                                fox[player.MonsterIndex].Alive = false;
                                player.CurrentEXP += fox[player.MonsterIndex].EXP;
                                player.Money += fox[player.MonsterIndex].Money;

                                player.CanMove = true;
                                player.IsOnBattle = false;
                                selectCursor.X = Game.DialogCursor_X;
                                selectCursor.Y = Game.DialogCursor_Y;
                                selectCursor.PastY = Game.DialogCursor_Y;
                                Function.RenderNow(player);
                            }
                        }
                        if (fox[player.MonsterIndex].Alive && fox[player.MonsterIndex].ATK - player.DEF > 0)
                        {
                            player.CurrentHP -= fox[player.MonsterIndex].ATK - player.DEF;
                        }
                    }
                    if (SelectCursor.Function.SelectEscape(selectCursor))
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
            public static void WithGoblin(Player player, SelectCursor selectCursor, Goblin[] goblin)
            {
                if (player.IsOnBattle)
                {
                    if (selectCursor.X == Game.DialogCursor_X)
                    {
                        selectCursor.X = Game.BattleCursor_X;
                        selectCursor.Y = Game.BattleCursor_Y;
                        selectCursor.PastY = Game.BattleCursor_Y;
                        goblin[player.MonsterIndex].CurrentHP = goblin[player.MonsterIndex].MaxHP;
                    }
                    if (SelectCursor.Function.SelectAttack(selectCursor))
                    {
                        if (player.ATK - goblin[player.MonsterIndex].DEF > 0)
                        {
                            goblin[player.MonsterIndex].CurrentHP -= player.ATK - goblin[player.MonsterIndex].DEF;
                            if (goblin[player.MonsterIndex].CurrentHP <= 0)
                            {
                                goblin[player.MonsterIndex].Alive = false;
                                player.CurrentEXP += goblin[player.MonsterIndex].EXP;
                                player.Money += goblin[player.MonsterIndex].Money;

                                player.CanMove = true;
                                player.IsOnBattle = false;
                                selectCursor.X = Game.DialogCursor_X;
                                selectCursor.Y = Game.DialogCursor_Y;
                                selectCursor.PastY = Game.DialogCursor_Y;
                                Function.RenderNow(player);
                            }
                        }
                        if (goblin[player.MonsterIndex].Alive && goblin[player.MonsterIndex].ATK - player.DEF > 0)
                        {
                            player.CurrentHP -= goblin[player.MonsterIndex].ATK - player.DEF;
                        }
                    }
                    if (SelectCursor.Function.SelectEscape(selectCursor))
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
            public static void WithKingSlime(Player player, SelectCursor selectCursor, KingSlime kingSlime)
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
                    if (SelectCursor.Function.SelectAttack(selectCursor))
                    {
                        if (player.ATK - kingSlime.DEF > 0)
                        {
                            kingSlime.CurrentHP -= player.ATK - kingSlime.DEF;
                            if (kingSlime.CurrentHP <= 0)
                            {
                                kingSlime.Alive = false;
                                player.CurrentEXP += kingSlime.EXP;
                                player.Money += kingSlime.Money;

                                player.CanMove = true;
                                player.IsOnBattle = false;
                                player.GameClear = true;
                                selectCursor.X = Game.DialogCursor_X;
                                selectCursor.Y = Game.DialogCursor_Y;
                                selectCursor.PastY = Game.DialogCursor_Y;
                                Function.RenderNow(player);
                            }
                        }
                        if (kingSlime.Alive && kingSlime.ATK - player.DEF > 0)
                        {
                            player.CurrentHP -= kingSlime.ATK - player.DEF;
                        }
                    }
                    if (SelectCursor.Function.SelectEscape(selectCursor))
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
