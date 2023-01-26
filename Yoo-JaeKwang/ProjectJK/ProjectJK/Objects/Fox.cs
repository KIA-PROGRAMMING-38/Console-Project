using ProjectJK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class Fox
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

        public static void InitFoxRender(Fox[] fox)
        {
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 1, $" ATK: {fox[0].ATK:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 2, $" DEF: {fox[0].DEF:D3}", ConsoleColor.Black);
        }
        public static void RenderPast(Fox[] fox)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                Game.ObjRender(fox[i].PastX, fox[i].PastY, "F", ConsoleColor.White);
            }
        }
        public static void RenderNow(Fox[] fox)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                if (fox[i].Alive)
                {
                    Game.ObjRender(fox[i].X, fox[i].Y, "F", ConsoleColor.DarkGray);
                }
            }
        }
        public static void RenderBattle(Player player, Fox[] fox)
        {
            if (player.X == fox[player.MonsterIndex].X && player.Y == fox[player.MonsterIndex].Y && fox[player.MonsterIndex].Alive)
            {
                Game.ObjRender(player.X, player.Y, "B", ConsoleColor.Red);
                BattleGraphic.Fox();
                RenderHP(fox[player.MonsterIndex]);
            }
            else
            {
                BattleGraphic.Clear();
            }
        }
        public static void RenderHP(Fox fox)
        {
            Game.ObjRender(Game.BattleCursor_X + 12, Game.BattleCursor_Y + 1, $"{fox.CurrentHP:D3} / {fox.MaxHP:D3}", ConsoleColor.Black);
        }
        public static void Update(Fox[] fox, Player player, Wall[] walls, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal)
        {
            Move(fox, player);
            CollisionWithWall(fox, walls);
            CollisionWithStageUpPortal(fox, stageUpPortal);
            CollisionWithStageDownPortal(fox, stageDownPortal);
            CollisionWithFox(fox);
            Respawn(fox, player);
        }
        private static void Move(Fox[] fox, Player player)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                Random random = new Random();
                int _randomNum = random.Next(1, 1000);
                if (player.CanMove)
                {
                    switch (_randomNum)
                    {
                        case <= 10:
                            fox[i].PastX = fox[i].X;
                            fox[i].PastY = fox[i].Y;
                            --fox[i].X;
                            fox[i].MoveDirection = Direction.Left;
                            break;
                        case <= 20:
                            fox[i].PastX = fox[i].X;
                            fox[i].PastY = fox[i].Y;
                            ++fox[i].X;
                            fox[i].MoveDirection = Direction.Right;
                            break;
                        case <= 30:
                            fox[i].PastX = fox[i].X;
                            fox[i].PastY = fox[i].Y;
                            --fox[i].Y;
                            fox[i].MoveDirection = Direction.Up;
                            break;
                        case <= 40:
                            fox[i].PastX = fox[i].X;
                            fox[i].PastY = fox[i].Y;
                            ++fox[i].Y;
                            fox[i].MoveDirection = Direction.Down;
                            break;
                        case <= 999:
                            break;
                        default:
                            Game.ExitWithError($"여우 이동 방향 데이터 오류{_randomNum}");
                            break;
                    }
                }
            }
        }
        private static bool isCollision(Fox fox, int objX, int objY)
        {
            if (fox.X == objX && fox.Y == objY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void Back(Fox fox, int objX, int objY)
        {
            switch (fox.MoveDirection)
            {
                case Direction.Left:
                    fox.X = objX + 1;
                    break;
                case Direction.Right:
                    fox.X = objX - 1;
                    break;
                case Direction.Up:
                    fox.Y = objY + 1;
                    break;
                case Direction.Down:
                    fox.Y = objY - 1;
                    break;
                default:
                    Game.ExitWithError($"여우 이동 방향 데이터 오류{fox.MoveDirection}");
                    break;
            }
        }
        private static void CollisionWithWall(Fox[] fox, Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                for (int j = 0; j < fox.Length; ++j)
                {
                    if (false == isCollision(fox[j], walls[i].X, walls[i].Y))
                    {
                        continue;
                    }

                    Back(fox[j], walls[i].X, walls[i].Y);
                    break;
                }
            }
        }
        private static void CollisionWithStageUpPortal(Fox[] fox, StageUpPortal stageUpPortal)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                if (isCollision(fox[i], stageUpPortal.X, stageUpPortal.Y))
                {
                    Back(fox[i], stageUpPortal.X, stageUpPortal.Y);
                }
            }
        }
        private static void CollisionWithStageDownPortal(Fox[] fox, StageDownPortal stageDownPortal)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                if (isCollision(fox[i], stageDownPortal.X, stageDownPortal.Y))
                {
                    Back(fox[i], stageDownPortal.X, stageDownPortal.Y);
                }
            }
        }
        private static void CollisionWithFox(Fox[] fox)
        {
            for (int i = 0; i < fox.Length; ++i)
            {
                for (int j = 0; j < fox.Length; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (false == isCollision(fox[j], fox[i].X, fox[i].Y))
                    {
                        continue;
                    }

                    Back(fox[j], fox[i].X, fox[i].Y);
                }
            }
        }
        private static void Respawn(Fox[] fox, Player player)
        {
            if (player.CanMove)
            {
                for (int i = 0; i < fox.Length; ++i)
                {
                    if (false == fox[i].Alive)
                    {
                        Random random = new Random();
                        int _randomNum = random.Next(1, 1000);
                        switch (_randomNum)
                        {
                            case <= 8:
                                fox[i].Alive = true;
                                break;
                            case <= 999:
                                break;
                            default:
                                Game.ExitWithError($"여우 리스폰 데이터 오류{_randomNum}");
                                break;
                        }
                    }
                }
            }
        }
    }
}