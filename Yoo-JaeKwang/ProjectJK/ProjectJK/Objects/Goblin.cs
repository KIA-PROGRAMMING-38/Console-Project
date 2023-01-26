using ProjectJK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class Goblin
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

        public static void InitGoblinRender(Goblin[] goblin)
        {
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 1, $" ATK: {goblin[0].ATK:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 2, $" DEF: {goblin[0].DEF:D3}", ConsoleColor.Black);
        }
        public static void RenderPast(Goblin[] goblin)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                Game.ObjRender(goblin[i].PastX, goblin[i].PastY, "G", ConsoleColor.White);
            }
        }
        public static void RenderNow(Goblin[] goblin)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                if (goblin[i].Alive)
                {
                    Game.ObjRender(goblin[i].X, goblin[i].Y, "G", ConsoleColor.DarkGreen);
                }
            }
        }
        public static void RenderBattle(Player player, Goblin[] goblin)
        {
            if (player.X == goblin[player.MonsterIndex].X && player.Y == goblin[player.MonsterIndex].Y && goblin[player.MonsterIndex].Alive)
            {
                Game.ObjRender(player.X, player.Y, "B", ConsoleColor.Red);
                BattleGraphic.Goblin();
                RenderHP(goblin[player.MonsterIndex]);
            }
            else
            {
                BattleGraphic.Clear();
            }
        }
        public static void RenderHP(Goblin goblin)
        {
            Game.ObjRender(Game.BattleCursor_X + 12, Game.BattleCursor_Y + 1, $"{goblin.CurrentHP:D3} / {goblin.MaxHP:D3}", ConsoleColor.Black);
        }
        public static void Update(Goblin[] goblin, Player player, Wall[] walls, StageUpPortal stageUpPortal, StageDownPortal stageDownPortal)
        {
            Move(goblin, player);
            CollisionWithWall(goblin, walls);
            CollisionWithStageUpPortal(goblin, stageUpPortal);
            CollisionWithStageDownPortal(goblin, stageDownPortal);
            CollisionWithGoblin(goblin);
            Respawn(goblin, player);
        }
        private static void Move(Goblin[] goblin, Player player)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                Random random = new Random();
                int _randomNum = random.Next(1, 1000);
                if (player.CanMove)
                {
                    switch (_randomNum)
                    {
                        case <= 10:
                            goblin[i].PastX = goblin[i].X;
                            goblin[i].PastY = goblin[i].Y;
                            --goblin[i].X;
                            goblin[i].MoveDirection = Direction.Left;
                            break;
                        case <= 20:
                            goblin[i].PastX = goblin[i].X;
                            goblin[i].PastY = goblin[i].Y;
                            ++goblin[i].X;
                            goblin[i].MoveDirection = Direction.Right;
                            break;
                        case <= 30:
                            goblin[i].PastX = goblin[i].X;
                            goblin[i].PastY = goblin[i].Y;
                            --goblin[i].Y;
                            goblin[i].MoveDirection = Direction.Up;
                            break;
                        case <= 40:
                            goblin[i].PastX = goblin[i].X;
                            goblin[i].PastY = goblin[i].Y;
                            ++goblin[i].Y;
                            goblin[i].MoveDirection = Direction.Down;
                            break;
                        case <= 999:
                            break;
                        default:
                            Game.ExitWithError($"고블린 이동 방향 데이터 오류{_randomNum}");
                            break;
                    }
                }
            }
        }
        private static bool isCollision(Goblin goblin, int objX, int objY)
        {
            if (goblin.X == objX && goblin.Y == objY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void Back(Goblin goblin, int objX, int objY)
        {
            switch (goblin.MoveDirection)
            {
                case Direction.Left:
                    goblin.X = objX + 1;
                    break;
                case Direction.Right:
                    goblin.X = objX - 1;
                    break;
                case Direction.Up:
                    goblin.Y = objY + 1;
                    break;
                case Direction.Down:
                    goblin.Y = objY - 1;
                    break;
                default:
                    Game.ExitWithError($"고블린 이동 방향 데이터 오류{goblin.MoveDirection}");
                    break;
            }
        }
        private static void CollisionWithWall(Goblin[] goblin, Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                for (int j = 0; j < goblin.Length; ++j)
                {
                    if (false == isCollision(goblin[j], walls[i].X, walls[i].Y))
                    {
                        continue;
                    }

                    Back(goblin[j], walls[i].X, walls[i].Y);
                    break;
                }
            }
        }
        private static void CollisionWithStageUpPortal(Goblin[] goblin, StageUpPortal stageUpPortal)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                if (isCollision(goblin[i], stageUpPortal.X, stageUpPortal.Y))
                {
                    Back(goblin[i], stageUpPortal.X, stageUpPortal.Y);
                }
            }
        }
        private static void CollisionWithStageDownPortal(Goblin[] goblin, StageDownPortal stageDownPortal)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                if (isCollision(goblin[i], stageDownPortal.X, stageDownPortal.Y))
                {
                    Back(goblin[i], stageDownPortal.X, stageDownPortal.Y);
                }
            }
        }
        private static void CollisionWithGoblin(Goblin[] goblin)
        {
            for (int i = 0; i < goblin.Length; ++i)
            {
                for (int j = 0; j < goblin.Length; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (false == isCollision(goblin[j], goblin[i].X, goblin[i].Y))
                    {
                        continue;
                    }

                    Back(goblin[j], goblin[i].X, goblin[i].Y);
                }
            }
        }
        private static void Respawn(Goblin[] goblin, Player player)
        {
            if (player.CanMove)
            {
                for (int i = 0; i < goblin.Length; ++i)
                {
                    if (false == goblin[i].Alive)
                    {
                        Random random = new Random();
                        int _randomNum = random.Next(1, 1000);
                        switch (_randomNum)
                        {
                            case <= 8:
                                goblin[i].Alive = true;
                                break;
                            case <= 999:
                                break;
                            default:
                                Game.ExitWithError($"고블린 리스폰 데이터 오류{_randomNum}");
                                break;
                        }
                    }
                }
            }
        }
    }
}