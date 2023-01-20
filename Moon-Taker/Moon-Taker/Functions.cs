using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows;

namespace Moon_Taker
{
    public class Functions
    {
        public static void InitialSettings()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.Title = "Moon taker";
            Render("   ___  ___ _____  _____  _   _ \r\n" +
                   "   |  \\/  ||  _  ||  _  || \\ | |\r\n" +
                   "   | .  . || | | || | | ||  \\| |\r\n" +
                   "   | |\\/| || | | || | | || . ` |\r\n" +
                   "   | |  | |\\ \\_/ /\\ \\_/ /| |\\  |\r\n" +
                   "   \\_|  |_/ \\___/  \\___/ \\_| \\_/\r"
                   , ConsoleColor.Yellow);

            Render("_____   ___   _   __ _____ ______ \r\n" +
                   "|_   _| / _ \\ | | / /|  ___|| ___ \\\r\n" +
                   "  | |  / /_\\ \\| |/ / | |__  | |_/ /\r\n" +
                   "  | |  |  _  ||    \\ |  __| |    / \r\n" +
                   "  | |  | | | || |\\  \\| |___ | |\\ \\ \r\n" +
                   "  \\_/  \\_| |_/\\_| \\_/\\____/ \\_| \\_|\r\n");
        }
        public static void Render(string someString, ConsoleColor myColor = ConsoleColor.White)
        {
            Console.ForegroundColor = myColor;
            Console.Write(someString);
        }

        public static void StartGame(out bool isGameStarted)
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key != ConsoleKey.E)
                {
                    Console.Write("\b \b");
                    continue;
                }
                isGameStarted = true;
                return;
            }
        }
    }
}
