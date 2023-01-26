using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class Bullet
    {
        private static int[] _x = new int[Player._y - 0];  // 0은 나중에 맵 꾸미게되면 총알이 갈 수 있는 맵의 가장 상단 y좌표
        private static int[] _preY = new int[Player._y - 0];
        private static int[] _y = new int[Player._y - 0];
        private static bool[] _isAvailable = Enumerable.Repeat<bool>(false, Player._y).ToArray<bool>();
        private static string _icon = "*";
        private static int _bulletIndex;

        public static void Shooting()
        {
            while (true)
            {
                Shoot();
                Fly();
                Thread.Sleep(200);
            }
        }

        public static void Shoot()
        {
            _x[_bulletIndex] = Player._currentX;
            _y[_bulletIndex] = Player._y - 1;
            _isAvailable[_bulletIndex] = true;

            if (_bulletIndex == Player._y - 1)  // 0은 나중에 맵 꾸미게되면 총알이 갈 수 있는 맵의 가장 상단 y좌표
            {
                _bulletIndex = 0;
            }
            else
            {
                ++_bulletIndex;
            }
        }

        public static void Fly()
        {
            for (int bulletId = 0; bulletId < _y.Length; ++bulletId)
            {
                _preY[bulletId] = _y[bulletId];

                if (_y[bulletId] == 0 || _isAvailable[bulletId] == false)
                {
                    continue;
                }

                _y[bulletId] -= 1;
            }
        }

        public static void Render()
        {
            for (int bulletId = 0; bulletId < _preY.Length; ++bulletId)
            {
                Console.SetCursorPosition(_x[bulletId], _preY[bulletId]);
                Console.Write(" ");
            }

            for (int bulletId = 0; bulletId < _x.Length; ++bulletId)
            {
                if (_y[bulletId] == 0 || _isAvailable[bulletId] == false)
                {
                    continue;
                }

                Console.SetCursorPosition(_x[bulletId], _y[bulletId]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(_icon);
            }
        }

        public static void IsCollisionWithSomething()
        {
            int[] targetX = Target.GetX();
            int[] targetY = Target.GetY();
            int[] obstX = Obstacle.GetX();
            int[] obstY = Obstacle.GetY();

            for (int bulletId = 0; bulletId < _y.Length; ++bulletId)
            {
                if (_y[bulletId] == 0)
                {
                    _isAvailable[bulletId] = false;
                    continue;
                }

                for (int obstId = 0; obstId < obstX.Length; ++obstId)
                {
                    if (_x[bulletId] == obstX[obstId] && _y[bulletId] == obstY[obstId])
                    {
                        _preY[bulletId] = _y[bulletId];
                        _isAvailable[bulletId] = false;
                        break;
                    }

                    if (_x[bulletId] == obstX[obstId] + 1 && _y[bulletId] == obstY[obstId])
                    {
                        _preY[bulletId] = _y[bulletId];
                        _isAvailable[bulletId] = false;
                        break;
                    }

                    if (_x[bulletId] == obstX[obstId] + 2 && _y[bulletId] == obstY[obstId])
                    {
                        _preY[bulletId] = _y[bulletId];
                        _isAvailable[bulletId] = false;
                        break;
                    }
                }

                for (int targetId = 0; targetId < targetX.Length; ++targetId)
                {
                    if (_x[bulletId] == targetX[targetId] && _y[bulletId] == targetY[targetId])
                    {
                        _preY[bulletId] = _y[bulletId];
                        Target.Hurt(targetId, _isAvailable[bulletId]);
                        _isAvailable[bulletId] = false;
                        continue;
                    }
                }
            }
        }
    }
}
