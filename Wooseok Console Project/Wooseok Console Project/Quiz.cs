using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wooseok_Console_Project
{
    internal class Quiz
    {


        public int quizcount;
        public bool initial_on;

        public bool score_on;
        public bool quiz1_on;
        public bool quiz2_on;
        public bool quiz3_on;
        public bool quiz4_on;
        public bool quiz5_on;
        public bool quiz6_on;
        public bool quiz7_on;
        public bool quiz8_on;
        public bool quiz9_on;
        public bool quiz10_on;

        public bool choose_on;
        public bool explanation_on;


        public int score;

        public bool startmusic;
        public bool middlemusic;
        public bool stopmiddle;


        public Quiz()
        {
            quizcount = 10;
            initial_on = true;
            startmusic = true;
            middlemusic = false;
            stopmiddle = true;

        }

        public static string[] Loadquiz(int number)
        {

            string quizpath = Path.Combine("Assets", "Quizzes", $"quiz{number}.txt");

            if (File.Exists(quizpath) == false) // 파일명이나 번호가 잘못됬다면 알려주는 화면 띄우기
            {
                Console.Clear();
                Console.WriteLine("퀴즈파일을 찾을 수 없습니다");
            }

            return File.ReadAllLines(quizpath);


        }



    }
}
