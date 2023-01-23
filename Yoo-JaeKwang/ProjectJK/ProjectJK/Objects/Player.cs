using System;
using System.Collections.Generic;
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
        public static class Function
        {
            public static void Render(Player player)
            {
                Game.Function.ObjRender(player.PastX, player.PastY, "P", ConsoleColor.White);
                Game.Function.ObjRender(player.X, player.Y, "P", ConsoleColor.Black);
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
                        player.PastX = 0;
                        player.PastY = 0;
                        player.X = objX + 1;
                        break;
                    case Direction.Right:
                        player.PastX = 0;
                        player.PastY = 0;
                        player.X = objX - 1;
                        break;
                    case Direction.Up:
                        player.PastX = 0;
                        player.PastY = 0;
                        player.Y = objY + 1;
                        break;
                    case Direction.Down:
                        player.PastX = 0;
                        player.PastY = 0;
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
            public static void WithVillageChief(Player player, VillageChief villageChief)
            {
                if (isCollision(player, villageChief.X, villageChief.Y))
                {
                    Back(player, villageChief.X, villageChief.Y);
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
            public static void WithSlime(Player player, Slime slime, SelectCursor selectCursor)
            {
                if (isCollision(player, slime.X, slime.Y))
                {
                    player.CanMove = false;
                    slime.CanMove = false;
                    player.IsOnBattle = true;
                    selectCursor.On = true;
                }
            }
        }
        public static class Interaction
        {
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
            public static void WithStageUpPortal(Player player, StageUpPortal stageUpPortal, ref bool isSelectCursorOn)
            {
                if (IsFront(player, stageUpPortal.X, stageUpPortal.Y) && Input.IsKeyDown(ConsoleKey.E))
                {
                    isSelectCursorOn = true;
                    player.CanMove = false;
                }
                
            }
            public static void WithStageDownPortal(Player player, StageDownPortal stageDownPortal, ref bool isSelectCursorOn)
            {
                if (IsFront(player, stageDownPortal.X, stageDownPortal.Y) && Input.IsKeyDown(ConsoleKey.E))
                {
                    isSelectCursorOn = true;
                    player.CanMove = false;
                }

            }
            public static void WithVillageCheif(Player player, VillageChief villageChief, ref bool isSelectCursorOn)
            {
                if (IsFront(player, villageChief.X, villageChief.Y) && Input.IsKeyDown(ConsoleKey.E))
                {
                    isSelectCursorOn = true;
                    player.CanMove = false;
                }
            }
        }
    }
}
