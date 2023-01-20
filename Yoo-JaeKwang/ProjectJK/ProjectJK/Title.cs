using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK
{
    internal class Title
    {
        public static class Function
        {
            public static void TitleRender()
            {
                Game.Function.Initializing();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣶⣶⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⡀⠀⠀⢰⣶⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⣶⡆");
                Console.WriteLine("⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⠿⠿⠿⠿⠿⠿⠿⠿⠇⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⣶⣶⣶⣶⣶⣶⣾⣿⣿⣷⣶⣶⣶⣶⣶⣦");
                Console.WriteLine("⠀⠀⢸⣿⣿⠉⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⠉⠉⠉⠉⠙⣿⣿⡏⠉⠉⠉⠉⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣤⣤⣤⣤⣤⣤⣤⡀⠀⠀⠀⢸⣿⣿⣀⣀⣀⠀⠀⠀⠀⠀⠛⠛⠛⠛⠛⢻⣿⣿⣿⣿⡟⠛⠛⠛⠛⠛");
                Console.WriteLine("⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⣴⣶⣶⣶⣶⣶⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣇⠀⠀⠀⢰⣶⣶⣶⣾⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⡿⠿⠿⠿⠿⠿⠿⠇⠀⠀⠀⢸⣿⣿⣿⣿⣿⡇⠀⠀⢠⣤⣤⣤⣴⣶⣿⣿⡿⠛⠛⢿⣿⣿⣶⣦⣤⣤⣤");
                Console.WriteLine("⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣄⠀⠀⠘⠛⠛⠛⢻⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣤⣤⣤⣤⣤⣤⣤⣤⣤⠀⠀⢸⣿⣿⠁⠀⠀⠀⠀⠀⠀⠹⠿⠿⠛⠛⠉⠁⠀⠀⠀⠀⠀⠉⠛⠛⠿⠿⠏");
                Console.WriteLine("⠀⠀⢸⣿⣿⣤⣤⣤⣤⣤⣤⣤⣤⡄⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⠙⢿⣿⣷⣄⡀⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠿⠿⠿⠿⠿⠿⠿⠿⠿⠿⠟⠀⠀⠸⠿⠿⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
                Console.WriteLine("⠀⠀⢸⣿⣿⣿⡿⠿⠿⠿⠿⠿⠿⠃⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠠⣴⣿⣿⡿⠃⠀⠀⠀⠙⠻⣿⡿⠃⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⠀⠀⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⣻⣿⣿⣟⠛⠛⠛⠛⠛⠛⠛⠛");
                Console.WriteLine("⠀⠀⠀⠀⠀⢀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⠈⠛⠁⠀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠿⠿⠿⠿⠿⠿⠿⠿⠿⠿⢿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿");
                Console.WriteLine("⠀⠀⠀⠀⠀⢸⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠻⠿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠘⠿⠿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣿⣿⣿");
                Console.WriteLine("⠀⠀⠀⠀⠀⢸⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡿⠿⠿⠿⠿⠿⠿⠿⠿⠿⠿⠿");
                Console.WriteLine("⠀⠀⠀⠀⠀⢸⣿⣿⣷⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⡆⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣷⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⡆");
                Console.WriteLine("⠀⠀⠀⠀⠀⠈⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠃");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("                                           Press E To Start");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}