using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class RandomManager : SingletonBase<RandomManager>
    {
        private Random random;

        public RandomManager()
        {
            random = new Random();
        }

        /// <summary>
        /// 0 ~ max 까지의 범위 중 랜덤한 숫자를 반환합니다..
        /// </summary>
        /// <param name="max"> 최대값 </param>
        /// <returns></returns>
        public int GetRandomNumber( int max = int.MaxValue )
        {
            return random.Next( -1, max ) + 1;
        }

        /// <summary>
        /// min ~ max까지의 범위 중 랜덤한 숫자를 반환합니다..
        /// </summary>
        /// <param name="min"> 랜덤한 숫자의 최소값 </param>
        /// <param name="max"> 랜덤한 숫자의 최대값 </param>
        /// <returns> min ~ max 사이의 숫자 </returns>
        public int GetRandomNumber( int min, int max )
        {
            return random.Next( min, max + 1 );
        }

        public double GetRandomDouble( double max = 1.0f)
        {
            return random.Next() * max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetRandomDouble( double min, double max )
        {
            Debug.Assert( max >= min );

            double dist = max - min;

            return random.Next() * dist + min;
        }
    }
}
