using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class Target
    {
        private static int[] _x = new int[5];
        private static int[] _preY = new int[5];
        private static int[] _y = new int[5];
        private static bool[] _isDead = new bool[5];
        private static int _maxHp = 9;
        private static int[] _hp = new int[5];
        private static int _index = 0;
        private static bool _isAvailableCreateNewTarget = true;
        private static int _countDeadTarget;

        private static Random random = new Random();

        public static int[] GetX()
        {
            return _x;
        }

        public static int[] GetY()
        {
            return _y;
        }

        public static void IsEnoughDeadNumber()
        {
            if (_countDeadTarget == 3)
            {
                SceneManager._currentScene = SceneKind.Ending;
            }
        }

        public static void Create()
        {
            if (_isAvailableCreateNewTarget)
            {
                _x[_index] = random.Next(SceneData.MIN_OF_INGAME_X, SceneData.MAX_OF_INGAME_X);
                _y[_index] = SceneData.MIN_OF_INGAME_Y + 1;
                _hp[_index] = _maxHp;
                _isDead[_index] = false;

                if (_index == 3)  // 한 화면에 동시에 존재할 수 있는 목표물의 개수
                {
                    _index = 0;
                }
                else
                {
                    ++_index;
                }
            }

            CheckAvailableCreateNewTarget();
        }

        public static void CheckAvailableCreateNewTarget()
        {
            if (_index == _x.Length - 1)
            {
                _isAvailableCreateNewTarget = false;
            }

            if (_y[0] == SceneData.MAX_OF_INGAME_Y)
            {
                _y[0] = 0;
                _isAvailableCreateNewTarget = true;
            }
        }

        public static void Fly()
        {
            for (int targetId = 0; targetId < _x.Length; ++targetId)
            {
                if (_y[targetId] == 0)
                {
                    continue;
                }

                _preY[targetId] = _y[targetId];
                _y[targetId] += 1;

                if (_y[targetId] == SceneData.MAX_OF_INGAME_Y || _isDead[targetId] == true)
                {
                    _y[targetId] = 0;
                }
            }
        }

        public static void Render()
        {
            for (int targetId = 0; targetId < _x.Length; ++targetId)
            {
                Console.SetCursorPosition(_x[targetId], _preY[targetId]);
                Console.Write(" ");

                if (_y[targetId] == 0)
                {
                    continue;
                }

                if (_y[targetId] != SceneData.MAX_OF_INGAME_Y && _isDead[targetId] != true)
                {
                    Console.SetCursorPosition(_x[targetId], _y[targetId]);
                    Console.Write(_hp[targetId]);
                }
            }

            Console.SetCursorPosition(SceneData.X_OF_DEADCOUNT, SceneData.Y_OF_DEADCOUNT);
            Console.WriteLine();
            Console.WriteLine($"죽인 타겟의 수: {_countDeadTarget}");
        }


        public static void Hurt(int index, bool isAvailable)
        {
            if (isAvailable)
            {
                _hp[index] -= 1;
            }

            if (_isDead[index] == false && _hp[index] <= 0)
            {
                IncreaseDeadCount();
                _y[index] = 0;
                _isDead[index] = true;
            }
        }

        public static void IncreaseDeadCount()
        {
            ++_countDeadTarget;
        }
    }
}
