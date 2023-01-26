using ProjectJK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public class KingSlime
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

        public static void InitKingSlimeRender(KingSlime kingSlime)
        {
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y, "KingSlime", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 1, $" ATK: {kingSlime.ATK:D3}", ConsoleColor.Black);
            Game.ObjRender(Game.Status_X, Game.Money_STATUS_Y + 2, $" DEF: {kingSlime.DEF:D3}", ConsoleColor.Black);
        }
        public static void RenderPast(KingSlime kingslime)
        {

            Game.ObjRender(kingslime.PastX, kingslime.PastY, "∰", ConsoleColor.White);
        }
        public static void RenderNow(KingSlime kingslime)
        {

            if (kingslime.Alive)
            {
                Game.ObjRender(kingslime.X, kingslime.Y, "∰", ConsoleColor.DarkBlue);
            }
        }
        public static void RenderBattle(Player player, KingSlime kingslime)
        {
            if (player.X == kingslime.X && player.Y == kingslime.Y && kingslime.Alive)
            {
                Game.ObjRender(player.X, player.Y, "B", ConsoleColor.Red);
                BattleGraphic.KingSlime();
                RenderHP(kingslime);
            }
            else
            {
                BattleGraphic.Clear();
            }
        }
        public static void RenderHP(KingSlime kingslime)
        {
            Game.ObjRender(Game.BattleCursor_X + 11, Game.BattleCursor_Y + 1, $"{kingslime.CurrentHP:D3} / {kingslime.MaxHP:D3}", ConsoleColor.Black);
        }
        public static void Update(KingSlime kingslime, Player player, Wall[] walls, StageDownPortal stageDownPortal)
        {
            Move(kingslime, player);
            CollisionWithWall(kingslime, walls);
            CollisionWithStageDownPortal(kingslime, stageDownPortal);
        }
        private static void Move(KingSlime kingslime, Player player)
        {

            Random random = new Random();
            int _randomNum = random.Next(1, 1000);
            if (player.CanMove)
            {
                switch (_randomNum)
                {
                    case <= 10:
                        kingslime.PastX = kingslime.X;
                        kingslime.PastY = kingslime.Y;
                        --kingslime.X;
                        kingslime.MoveDirection = Direction.Left;
                        break;
                    case <= 20:
                        kingslime.PastX = kingslime.X;
                        kingslime.PastY = kingslime.Y;
                        ++kingslime.X;
                        kingslime.MoveDirection = Direction.Right;
                        break;
                    case <= 30:
                        kingslime.PastX = kingslime.X;
                        kingslime.PastY = kingslime.Y;
                        --kingslime.Y;
                        kingslime.MoveDirection = Direction.Up;
                        break;
                    case <= 40:
                        kingslime.PastX = kingslime.X;
                        kingslime.PastY = kingslime.Y;
                        ++kingslime.Y;
                        kingslime.MoveDirection = Direction.Down;
                        break;
                    case <= 999:
                        break;
                    default:
                        Game.ExitWithError($"킹슬라임 이동 방향 데이터 오류{_randomNum}");
                        break;
                }
            }
        }
        private static bool isCollision(KingSlime kingslime, int objX, int objY)
        {
            if (kingslime.X == objX && kingslime.Y == objY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void Back(KingSlime kingslime, int objX, int objY)
        {
            switch (kingslime.MoveDirection)
            {
                case Direction.Left:
                    kingslime.X = objX + 1;
                    break;
                case Direction.Right:
                    kingslime.X = objX - 1;
                    break;
                case Direction.Up:
                    kingslime.Y = objY + 1;
                    break;
                case Direction.Down:
                    kingslime.Y = objY - 1;
                    break;
                default:
                    Game.ExitWithError($"킹슬라임 이동 방향 데이터 오류{kingslime.MoveDirection}");
                    break;
            }
        }
        private static void CollisionWithWall(KingSlime kingslime, Wall[] walls)
        {
            for (int i = 0; i < walls.Length; ++i)
            {
                if (false == isCollision(kingslime, walls[i].X, walls[i].Y))
                {
                    continue;
                }

                Back(kingslime, walls[i].X, walls[i].Y);
                break;
            }
        }
        private static void CollisionWithStageDownPortal(KingSlime kingslime, StageDownPortal stageDownPortal)
        {

            {
                if (isCollision(kingslime, stageDownPortal.X, stageDownPortal.Y))
                {
                    Back(kingslime, stageDownPortal.X, stageDownPortal.Y);
                }
            }
        }
    }
}