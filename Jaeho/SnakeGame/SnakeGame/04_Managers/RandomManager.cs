namespace SnakeGame
{
    public class RandomManager : Singleton<RandomManager>
    {
        public  RandomManager() { _random = new Random(); }

        /// <summary>
        /// 0 ~ Int.MaxValue - 1까지 임의의 숫자를 반환
        /// </summary>
        public int GetRandomInt() => _random.Next();

        /// <summary>
        /// 0 ~ maxValue이하의 임의의 수 반환
        /// </summary>
        /// <param name="maxValue">최대값</param>
        /// <returns></returns>
        public int GetRandomInt(int maxValue) => _random.Next(maxValue + 1);

        /// <summary>
        /// min 부터 max까지의 숫자 반환
        /// </summary>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <returns></returns>
        public int GetRandomRangeInt(int min, int max) => _random.Next(min, max + 1);

        /// <summary>
        /// 0.0 ~ 1.0 사이의 실수를 반환
        /// </summary>
        /// <returns></returns>
        public double GetRandomDouble() => _random.NextDouble();

        /// <summary>
        /// min ~ max사이의 실수를 반환
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double GetRandomRangeDouble(double min, double max) => min + _random.NextDouble() * (max - min);

        private Random _random;
    }
}
