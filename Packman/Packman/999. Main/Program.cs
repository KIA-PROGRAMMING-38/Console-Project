using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Packman
{
    internal class Program
    {
        static void Main()
        {
            Game game = Game.Instance;

			if ( true == game.Initialize() )
            {
				game.Run();
				game.Release();
            }
        }
    }
}