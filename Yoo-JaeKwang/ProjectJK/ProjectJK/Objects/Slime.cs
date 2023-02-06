using ProjectJK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class Slime
    {
        public int X;
        public int Y;
        public int PastX;
        public int PastY;
        public int ATK;
        public int DEF;
        public int Money;
        public int EXP;
        public int MaxHP;
        public int CurrentHP;
        public bool Alive;
        public Direction MoveDirection;


        public static void InitSlimeRender(Slime[] slime)
        {
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 1, $" ATK: {slime[0].ATK:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 2, $" DEF: {slime[0].DEF:D3}", ConsoleColor.Black);
        }
        public static void RenderPast(Slime[] slime)
        {
            for (int i = 0; i < slime.Length; ++i)
            {
                Game.ObjRender(slime[i].PastX, slime[i].PastY, "S", ConsoleColor.White);
            }
        }
        public static void RenderNow(Slime[] slime)
        {
            for (int i = 0; i < slime.Length; ++i)
            {

                if (slime[i].Alive)
                {
                    Game.ObjRender(slime[i].X, slime[i].Y, "S", ConsoleColor.Blue);
                }
            }
        }
        public static void RenderBattle(Player player, Slime[] slime)
        {
            if (player.X == slime[player.MonsterIndex].X && player.Y == slime[player.MonsterIndex].Y && slime[player.MonsterIndex].Alive)
            {
                Game.ObjRender(player.X, player.Y, "B", ConsoleColor.Red);
                BattleGraphic.Slime();
                RenderHP(slime[player.MonsterIndex]);
            }
            else
            {
                BattleGraphic.Clear();
            }
        }
        public static void RenderHP(Slime slime)
        {
            Game.ObjRender(Game.BattleCursor_X + 12, Game.BattleCursor_Y + 1, $"{slime.CurrentHP:D3} / {slime.MaxHP:D3}", ConsoleColor.Black);
        }
        public static void Update(Slime[] slime, Player player, Wall[] walls, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal)
        {
            Move(slime, player);
            CollisionWithWall(slime, walls);
            CollisionWithStageUpPortal(slime, stageUpPortal);
            CollisionWithStageDownPortal(slime, stageDownPortal);
            CollisionWithSlime(slime);
            Respawn(slime, player);
        }
        private static void Move(Slime[] slime, Player player)
        {
            for (int i = 0; i < slime.Length; ++i)
            {
                Random random = new Random();
                int _randomNum = random.Next(1, 1000);
                if (player.CanMove)
                {
                    switch (_randomNum)
                    {
                        case <= 10:
                            slime[i].PastX = slime[i].X;
                            slime[i].PastY = slime[i].Y;
                            --slime[i].X;
                            slime[i].MoveDirection = Direction.Left;
                            break;
                        case <= 20:
                            slime[i].PastX = slime[i].X;
                            slime[i].PastY = slime[i].Y;
                            ++slime[i].X;
                            slime[i].MoveDirection = Direction.Right;
                            break;
                        case <= 30:
                            slime[i].PastX = slime[i].X;
                            slime[i].PastY = slime[i].Y;
                            --slime[i].Y;
                            slime[i].MoveDirection = Direction.Up;
                            break;
                        case <= 40:
                            slime[i].PastX = slime[i].X;
                            slime[i].PastY = slime[i].Y;
                            ++slime[i].Y;
                            slime[i].MoveDirection = Direction.Down;
                            break;
                        case <= 999:
                            break;
                        default:
                            Game.ExitWithError($"슬라임 이동 방향 데이터 오류{_randomNum}");
                            break;
                    }
                }
            }
        }
        private static bool isCollision(Slime slime, int objX, int objY)
        {
            if (slime.X == objX && slime.Y == objY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void Back(Slime slime, int objX, int objY)
        {
            switch (slime.MoveDirection)
            {
                case Direction.Left:
                    slime.X = objX + 1;
                    break;
                case Direction.Right:
                    slime.X = objX - 1;
                    break;
                case Direction.Up:
                    slime.Y = objY + 1;
                    break;
                case Direction.Down:
                    slime.Y = objY - 1;
                    break;
                default:
                    Game.ExitWithError($"슬라임 이동 방향 데이터 오류{slime.MoveDirection}");
                    break;
            }
        }
        private static void CollisionWithWall(Slime[] slime, Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                for (int j = 0; j < slime.Length; ++j)
                {
                    if (false == isCollision(slime[j], walls[i].X, walls[i].Y))
                    {
                        continue;
                    }

                    Back(slime[j], walls[i].X, walls[i].Y);
                    break;
                }
            }
        }
        private static void CollisionWithStageUpPortal(Slime[] slime, StageUpPortal stageUpPortal)
        {
            for (int i = 0; i < slime.Length; ++i)
            {
                if (isCollision(slime[i], stageUpPortal.X, stageUpPortal.Y))
                {
                    Back(slime[i], stageUpPortal.X, stageUpPortal.Y);
                }
            }
        }
        private static void CollisionWithStageDownPortal(Slime[] slime, StageDownPortal stageDownPortal)
        {
            for (int i = 0; i < slime.Length; ++i)
            {
                if (isCollision(slime[i], stageDownPortal.X, stageDownPortal.Y))
                {
                    Back(slime[i], stageDownPortal.X, stageDownPortal.Y);
                }
            }
        }
        private static void CollisionWithSlime(Slime[] slime)
        {
            for (int i = 0; i < slime.Length; ++i)
            {
                for (int j = 0; j < slime.Length; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (false == isCollision(slime[j], slime[i].X, slime[i].Y))
                    {
                        continue;
                    }

                    Back(slime[j], slime[i].X, slime[i].Y);
                }
            }
        }
        private static void Respawn(Slime[] slime, Player player)
        {
            if (player.CanMove)
            {
                for (int i = 0; i < slime.Length; ++i)
                {
                    if (false == slime[i].Alive)
                    {
                        Random random = new Random();
                        int _randomNum = random.Next(1, 1000);
                        switch (_randomNum)
                        {
                            case <= 8:
                                slime[i].Alive = true;
                                break;
                            case <= 999:
                                break;
                            default:
                                Game.ExitWithError($"슬라임 리스폰 데이터 오류{_randomNum}");
                                break;
                        }
                    }
                }
            }
        }
    }
}