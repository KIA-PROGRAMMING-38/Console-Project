using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Wooseok_Console_Project
{
    

    internal class Function
    {
        Game game = new()
        {
            minx = 1,
            miny = 1,
            maxx = 55,
            maxy = 18
        };

        
        /// <summary>
        /// 초기세팅을 해준다!
        /// </summary>
        public void Setting()
        {
            Console.Clear();
            Console.Title = "군대 시뮬레이션";
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }


        

        













        /// <summary>
        /// 텍스트 파일을 불러온다
        /// </summary>
        /// <param name="path"></param>
        /// <param name="show"></param>
        public static void RenderText(string path, string[] show)
        {
            for (int i = 0; i < show.Length; ++i)
            {
                Console.WriteLine(show[i]);
            }
        }



        /// <summary>
        /// n번째 퀴즈를 그려준다!
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quizpath"></param>
        /// <param name="quizshow"></param>
        public static void RenderQuiz(int number, string quizpath, string[] quizshow)
        {
            quizpath = Path.Combine("Assets", "Scenes", "Quizzes", $"quiz{number}.txt");
            quizshow = File.ReadAllLines(quizpath);
            for (int i = 0; i < quizshow.Length; ++i)
            {
                Console.WriteLine(quizshow[i]);
            }
        }



        /// <summary>
        /// 그려준다
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="yeah"></param>
        public static void Render(int x, int y, string yeah)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(yeah);
        }

        /// <summary>
        /// 선택된 캐릭터로 데이터를 복사해온다
        /// </summary>
        /// <param name="pick"></param>
        /// <param name="character"></param>
        public static void CopyData(Character pick, Character character)
        {
            pick.name = character.name;
            pick.hp = character.hp;
            pick.mp = character.mp;
            pick.passive = character.passive;
            pick.passiveeffect = character.passiveeffect;
            pick.atk = character.atk;
            pick.attackeffect = character.attackeffect;
            pick.skill1 = character.skill1;
            pick.skill1effect = character.skill1effect;
        }
        /// <summary>
        /// 설명을 그려준다
        /// </summary>
        /// <param name="chosen"></param>
        /// <param name="path"></param>
        /// <param name="show"></param>
        public static void RenderExplanation(Character chosen, string path, string[] show)
        {

            switch (chosen.name)
            {
                case "신병":
                    Console.ForegroundColor = ConsoleColor.White;
                    path = Path.Combine("Assets", "Scenes", "Characters", "sin.txt");
                    show = File.ReadAllLines(path);
                    for (int i = 0; i < show.Length; ++i)
                    {
                        Console.WriteLine(show[i]);
                    }
                    break;
                case "이병":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    path = Path.Combine("Assets", "Scenes", "Characters", "ee.txt");
                    show = File.ReadAllLines(path);
                    for (int i = 0; i < show.Length; ++i)
                    {
                        Console.WriteLine(show[i]);
                    }
                    break;
                case "일병":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    path = Path.Combine("Assets", "Scenes", "Characters", "il.txt");
                    show = File.ReadAllLines(path);
                    for (int i = 0; i < show.Length; ++i)
                    {
                        Console.WriteLine(show[i]);
                    }
                    break;
                case "상병":
                    Console.ForegroundColor = ConsoleColor.Green;
                    path = Path.Combine("Assets", "Scenes", "Characters", "sang.txt");
                    show = File.ReadAllLines(path);
                    for (int i = 0; i < show.Length; ++i)
                    {
                        Console.WriteLine(show[i]);
                    }
                    break;

            }
        }



    }
}
