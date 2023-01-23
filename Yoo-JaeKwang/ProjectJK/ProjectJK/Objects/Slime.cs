using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool CanMove;
        public Direction MoveDirection;

        
        public static void Render(Slime slime)
        {
            Game.Function.ObjRender(slime.PastX, slime.PastY, "S", ConsoleColor.White);
            Game.Function.ObjRender(slime.X, slime.Y, "S", ConsoleColor.DarkGreen);
        }
        public static void Update(Slime slime, Player player, Wall[] walls, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal)
        {
            Move(slime);
            CollisionWithWall(slime, walls);
            CollisionWithStageUpPortal(slime, stageUpPortal);
            CollisionWithStageDownPortal(slime, stageDownPortal);
        }
        private static void Move(Slime slime)
        {
            Random random = new Random();
            int _randomNum = random.Next(1, 100);
            if (slime.CanMove)
            {
                switch (_randomNum)
                {
                    case <= 5:
                        slime.PastX = slime.X;
                        slime.PastY = slime.Y;
                        --slime.X;
                        slime.MoveDirection = Direction.Left;
                        break;
                    case <= 10:
                        slime.PastX = slime.X;
                        slime.PastY = slime.Y;
                        ++slime.X;
                        slime.MoveDirection = Direction.Right;
                        break;
                    case <= 15:
                        slime.PastX = slime.X;
                        slime.PastY = slime.Y;
                        --slime.Y;
                        slime.MoveDirection = Direction.Up;
                        break;
                    case <= 20:
                        slime.PastX = slime.X;
                        slime.PastY = slime.Y;
                        ++slime.Y;
                        slime.MoveDirection = Direction.Down;
                        break;
                    case <= 99:
                        break;
                    default:
                        Game.Function.ExitWithError($"슬라임 이동 방향 데이터 오류{_randomNum}");
                        break;
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
                    Game.Function.ExitWithError($"슬라임 이동 방향 데이터 오류{slime.MoveDirection}");
                    break;
            }
        }
        private static void CollisionWithWall(Slime slime, Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                if (false == isCollision(slime, walls[i].X, walls[i].Y))
                {
                    continue;
                }

                Back(slime, walls[i].X, walls[i].Y);
                break;
            }
        }
        private static void CollisionWithStageUpPortal(Slime slime, StageUpPortal stageUpPortal)
        {
            if (isCollision(slime, stageUpPortal.X, stageUpPortal.Y))
            {
                Back(slime, stageUpPortal.X, stageUpPortal.Y);
            }
        }
        private static void CollisionWithStageDownPortal(Slime slime, StageDownPortal stageDownPortal)
        {
            if (isCollision(slime, stageDownPortal.X, stageDownPortal.Y))
            {
                Back(slime, stageDownPortal.X, stageDownPortal.Y);
            }
        }
    }
}
