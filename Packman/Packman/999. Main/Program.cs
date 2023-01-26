using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Packman
{
    internal class Program
    {
        static void Main()
        {
            if ( true == Game.Instance.Initialize() )
            {
                Game.Instance.Run();
            
                Game.Instance.Release();
            }
        }
    }
}