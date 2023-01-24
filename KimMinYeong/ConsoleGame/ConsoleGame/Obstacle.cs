using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public enum ObstacleType
    {
        Straight,
        Zigzag
    }

    public static class Obstacle
    {
        private static int[] _x = new int[5];
        private static int[] _preY = new int[5];
        private static int[] _y = new int[5];

        private static string _icon = "~~~";
        private static int _index = 0;
        private static bool _isAvailableCreateNewObst = true;

        private static Random random = new Random();

        public static void Create()
        {
            while (true)
            {
                if (_isAvailableCreateNewObst)
                {
                    _x[_index] = random.Next(SceneData.MIN_OF_INGAME_X, SceneData.MAX_OF_INGAME_X);
                    _y[_index] = SceneData.MIN_OF_INGAME_Y + 1;

                    if (_index == 3)  // 한 화면에 동시에 존재할 수 있는 장애물의 개수 (속도에따라 달라질듯)
                    {
                        _index = 0;
                    }
                    else
                    {
                        ++_index;
                    }
                }

                CheckAvailableCreateNewObst();
                Thread.Sleep(3000);
            }
        }

        public static void CheckAvailableCreateNewObst()
        {
            if(_index == _x.Length - 1)
            {
                _isAvailableCreateNewObst = false;
            }

            if (_y[0] == SceneData.MAX_OF_INGAME_Y)
            {
                _y[0] = 0;
                _isAvailableCreateNewObst = true;
            }
        }

        public static void Fly()
        {
            while (true)
            {
                for(int obstId = 0; obstId < _x.Length; ++obstId)
                {
                    if (_y[obstId] == 0)
                    {
                        continue;
                    }

                    _preY[obstId] = _y[obstId];
                    _y[obstId] += 1;

                    if (_y[obstId] == SceneData.MAX_OF_INGAME_Y)
                    {
                        _y[obstId] = 0;
                    }
                }

                Thread.Sleep(500);
            }
        }

        public static void Render()
        {
            for(int obstId = 0; obstId < _x.Length; ++obstId)
            {
                Console.SetCursorPosition(_x[obstId], _preY[obstId]);
                Console.Write("   ");
                
                if (_y[obstId] == 0)
                {
                    continue;
                }

                if (_y[obstId] != SceneData.MAX_OF_INGAME_Y)
                {
                    Console.SetCursorPosition(_x[obstId], _y[obstId]);
                    Console.Write(_icon);
                }
            }
        }
    }
}
