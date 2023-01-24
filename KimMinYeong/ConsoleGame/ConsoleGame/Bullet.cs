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
        private static string _icon = "o";
        private static int _bulletIndex;

        public static void Shooting()
        {
            while (true)
            {
                Shoot();
                Fly();
                Thread.Sleep(100);
            }
        }

        public static void Shoot()
        {
            _preY[_bulletIndex] = _y[_bulletIndex];

            _x[_bulletIndex] = Player._currentX;
            _y[_bulletIndex] = Player._y - 1;


            if(_bulletIndex == Player._y - 0)  // 0은 나중에 맵 꾸미게되면 총알이 갈 수 있는 맵의 가장 상단 y좌표
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
            for(int bulletId = 0; bulletId < _y.Length; ++bulletId)
            {
                if (_y[bulletId] == 0)
                {
                    continue;
                }

                _y[bulletId] -= 1;
            }
        }

        public static void Render()
        {
            for(int bulletId = 0; bulletId < _preY.Length; ++bulletId)
            {
                // 배열 선언할 때 초기화된 기본값이라는 건 shoot해서 변경된 위치가 아님 
                if (_y[bulletId] == 0)
                {
                    continue;
                }

                Console.SetCursorPosition(_x[bulletId], _preY[bulletId]);
                Console.Write(" ");
            }

            for(int bulletId = 0; bulletId < _x.Length; ++bulletId)
            {
                if (_y[bulletId] == 0)
                {
                    continue;
                }

                Console.SetCursorPosition(_x[bulletId], _y[bulletId]);
                Console.Write(_icon);
            }
        }
    }
}
