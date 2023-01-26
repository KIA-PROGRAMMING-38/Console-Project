
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Linq.Expressions;
using System;

enum UserKey
{
    None,
    SpaceBar,
    Left,
    Right,
    Up,
    Down,
    G,
    R

}

namespace ConsoleSoloProject
{
    class Program
    {

        public static void Main()
        {
            // 콘솔 초기 세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "강화 디펜스";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();

            Scene.TitleScene();
            Game.gameWait();

        }
    }
}


