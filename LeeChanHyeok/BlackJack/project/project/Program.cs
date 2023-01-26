using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace project
{
    class Program
    {
        static void Main()
        {
            Title title = new Title();
            title.TitleSceen();
            Console.Clear();

            //함수 불러오기
            Input input = new Input();
            Game game = new Game();
            
            while (true)
            {
                //------------------------------------랜더-------------------------------------
                game.mapRender.MapRender();
                //------------------------------------ProcessInput ----------------------------
                input.Process();
                //------------------------------------update-----------------------------------
                switch (Input.key)
                {
                    case ConsoleKey.UpArrow:
                        game.mapRender.PreY = game.mapRender.playerY;
                        game.mapRender.playerY = Math.Max(game.mapRender.MIN_Y, game.mapRender.playerY - 3);
                        break;
                    case ConsoleKey.DownArrow:
                        game.mapRender.PreY = game.mapRender.playerY;
                        game.mapRender.playerY = Math.Min(game.mapRender.playerY + 3, game.mapRender.MAX_Y);
                        break;
                    case ConsoleKey.Enter:
                        game.NextGameExecution();
                        break;
                }
                game.FristGameExecution();
                game.GameEnd();
                
            }
        }
    }
}