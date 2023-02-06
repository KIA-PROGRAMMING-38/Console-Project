
using System.Threading;

namespace ThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread myThread = new Thread(Func);
            myThread.Start();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main: " + (i + 1));
                Thread.Sleep(100);
            }
            Console.WriteLine("메인쓰레드 종료");
        }

        private static void Func()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Second: " + (i + 1));
                Thread.Sleep(100);
            }
        }
    }
}