
using System;
using System.Diagnostics;
using System.Media;
using System.Numerics;
using Wooseok_Console_Project;

class Program
{
    enum PLAYERDIRECTION
    {
        NONE,
        RIGHT,
        LEFT,
        UP,
        DOWN
    }

    enum LINE4DIRECTION
    {
        NONE,
        RIGHT,
        LEFT,
        UP,
        DOWN
    }


    static void Main()
    {

        // 초기세팅


        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Title = "군대 시뮬레이션";

        Player player = new();




        Gameinfo game = new();
        Random random = new();
        Quiz quiz = new();

        string character_path = ""; // 캐릭터 설명 텍스트 파일 경로를 담기 위한 객체 생성

        Character ChosenOne = new();
        Character sin = new() // 신병 객체 생성
        {
            id = "신병",
            hp = 5,
            mp = 5,
            passive = "존댓말 하기",
            passive_effect = "군대 언어가 익숙치 않아 매 턴 50%의 확률로 존댓말을 하여 병장을 화나게 한다 [병장 ATK 1 증가]",
            atk = 3,
            attackeffect = "예?",
            skill = "관등성명 대기(MP 5)",
            skill_effect = "관등성명을 댐으로써 병장의 흥미가 떨어진다 [병장 MP 3~5 감소] "
        };

        Character ee = new() // 이병 객체 생성
        {
            id = "이병",
            hp = 10,
            mp = 10,
            passive = "얼 타기",
            passive_effect = "일머리가 없어 병장의 사기를 증가시킨다 [병장 MP 1 증가] ",
            atk = 3,
            attackeffect = "잘 못들었습니다?",
            skill = "마음의 편지 쓰기(MP 5)",
            skill_effect = "마음의 편지를 씀으로써 병장의 휴가가 잘렸다 [병장 HP 3 ~ 5 감소]"
        };

        Character il = new() // 일병 객체 생성
        {
            id = "일병",
            hp = 10,
            mp = 10,
            passive = "작업노예",
            passive_effect = "하루종일 작업만 하다 맷집이 좋아졌다 [매턴 30% 확률로 받는 데미지 반감]",
            atk = 3,
            attackeffect = "잘 못슴다?",
            skill = "담배 물리기(MP 5)",
            skill_effect = "담배를 권함으로써 병장이 2턴동안 흡연을 한다 [병장 2턴 Freeze]"
        };

        Character sang = new() // 상병 객체 생성
        {
            id = "상병",
            hp = 15,
            mp = 10,
            passive = "개기기",
            passive_effect = "짬 좀 찼다고 선임들 하나씩 먹으려 함 [매턴 10% 확률로 본인 ATK 1 증가]",
            atk = 3,
            attackeffect = "자몽소다?",
            skill = "무시하기 (MP 5)",
            skill_effect = "\"집에 갈 양반이 왜 아직도 실세놀이를 하고 계십니까?\" [병장 MP 5~10 감소]"
        };


        Character Byeong = new() // 병장 객체 생성
        {
            id = "병장",
            hp = 20,
            savehp = 20,
            mp = 10,
            savemp = 10,
            passive = "현타 세게 맞기",
            passive_effect = "동기들을 먼저 보내고 혼자만 남아 현타를 맞음[매턴 20% 확률로 턴 중지]",
            atk = 5,
            attackeffect = "야 여동생이나 누나 있냐?",
            skill = "군가 부르게 하기 (MP 2)",
            skill_effect = "야 군가 한곡 기깔나게 뽑아봐[플레이어 HP 1 ~ 3 감소]",

            skill2 = "기강 잡기 (MP 3)",
            skill2_effect = "개빠졌네 진짜?? 군대가 니 집 안방이냐??[플레이어 HP 3 ~ 5 감소]",

            skill3 = "맞선임 호출하기 (MP 5)",
            skill3_effect = "야, 니 맞선임 누구냐??[플레이어 HP 5 ~ 10 감소]"
        };



        SoundPlayer music_wakeup = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\army_wakeup.wav");
        SoundPlayer music_song1 = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\강한친구대한육군.wav");
        SoundPlayer music_walk = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\군화소리.wav");
        SoundPlayer music_laugh = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\남자비웃음.wav");
        SoundPlayer music_victory = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\Victory.wav");
        SoundPlayer music_battle = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\Battle.wav");
        SoundPlayer music_lose = new SoundPlayer(@"C:\Users\user1234\Console_Project\Console-Project\Wooseok Console Project\Wooseok Console Project\bin\Debug\net6.0\Assets\Music\Lose.wav");

        #region 퀴즈부분

        string choose = ""; // 선택받은 캐릭터를 저장하기 위한 객체
        // 캐릭터 선택용 게임루프
        while (game.character_selection) // 엔터키를 누르면 캐릭터 선택용 게임루프가 종료된다
        {
            Console.Clear(); // 텍스트라 클리어해도 될듯


            // 기상나팔 소리.wav 을 musicplayer에 저장

            // render
            if (quiz.initial_on == true) // 만약 초기화면 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(0); // 초기화면을 그려준다
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }

            if (quiz.startmusic == true)
            {
                music_wakeup.PlayLooping();// 시작노래 틀기
            }


            if (quiz.stopmiddle == false) // 군가 반복을 멈추지않으면
            {
                quiz.middlemusic = false; // 군가를 다시 재생하지 않는다
            }


            if (quiz.middlemusic == true)
            {
                music_song1.PlayLooping(); // 강한친구 대한육군 군가 반복재생하기
                quiz.stopmiddle = false; // 군가 반복을 멈추지않는다
            }






            if (quiz.score_on == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(20, 3, $"현재 점수 = +{quiz.score}");
            }


            Console.ForegroundColor = ConsoleColor.Red;
            if (quiz.quiz1_on == true) // 만약 퀴즈1 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(1); // 퀴즈1을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }

            if (quiz.quiz2_on == true) // 만약 퀴즈2 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(2); // 퀴즈2을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }

            if (quiz.quiz3_on == true) // 만약 퀴즈3 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(3); // 퀴즈3을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }

            if (quiz.quiz4_on == true) // 만약 퀴즈4 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(4); // 퀴즈4을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz5_on == true) // 만약 퀴즈5 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(5); // 퀴즈5을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz6_on == true) // 만약 퀴즈6 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(6); // 퀴즈6을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz7_on == true) // 만약 퀴즈7 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(7); // 퀴즈7을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz8_on == true) // 만약 퀴즈8 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(8); // 퀴즈8을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz9_on == true) // 만약 퀴즈9 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(9); // 퀴즈9을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }


            if (quiz.quiz10_on == true) // 만약 퀴즈10 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(10); // 퀴즈10을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }
            }



            if (quiz.choose_on == true) // 만약 채점화면 스위치가 on 이라면
            {
                string[] quizzes = Quiz.Loadquiz(11); // 채점화면을 그려준다
                for (int i = 0; i < quizzes.Length; ++i)
                {
                    Console.WriteLine(quizzes[i]);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(33, 3, $"+{quiz.score}");

                if (0 <= quiz.score && quiz.score <= 3)
                {
                    choose = sin.id;
                    ChosenOne.id = choose;
                    Function.Render(45, 18, ChosenOne.id);
                }
                else if (4 <= quiz.score && quiz.score <= 6)
                {
                    choose = ee.id;
                    ChosenOne.id = choose;
                    Function.Render(45, 18, ChosenOne.id);
                }
                else if (7 <= quiz.score && quiz.score <= 8)
                {
                    choose = il.id;
                    ChosenOne.id = choose;
                    Function.Render(45, 18, ChosenOne.id);
                }
                else if (9 <= quiz.score && quiz.score <= 10)
                {
                    choose = sang.id;
                    ChosenOne.id = choose;
                    Function.Render(45, 18, ChosenOne.id);
                }

            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (quiz.explanation_on)
            {
                switch (choose)
                {
                    case "신병":
                        character_path = Path.Combine("Assets", "Character", "sin.txt");
                        string[] sin_show = File.ReadAllLines(character_path);
                        for (int i = 0; i < sin_show.Length; ++i)
                        {
                            Console.WriteLine(sin_show[i]);
                        }
                        break;

                    case "이병":
                        character_path = Path.Combine("Assets", "Character", "ee.txt");
                        string[] ee_show = File.ReadAllLines(character_path);
                        for (int i = 0; i < ee_show.Length; ++i)
                        {
                            Console.WriteLine(ee_show[i]);
                        }
                        break;

                    case "일병":
                        character_path = Path.Combine("Assets", "Character", "il.txt");
                        string[] il_show = File.ReadAllLines(character_path);
                        for (int i = 0; i < il_show.Length; ++i)
                        {
                            Console.WriteLine(il_show[i]);
                        }
                        break;

                    case "상병":
                        character_path = Path.Combine("Assets", "Character", "sang.txt");
                        string[] sang_show = File.ReadAllLines(character_path);
                        for (int i = 0; i < sang_show.Length; ++i)
                        {
                            Console.WriteLine(sang_show[i]);
                        }
                        break;

                }
            }





            // input

            ConsoleKey key1 = Console.ReadKey(true).Key; // true를 넣어줌으로써 내가 입력한 키가 화면에서 보이지 않는다!
            // 구글링 최고!


            // update

            if (quiz.initial_on = true && key1 == ConsoleKey.S) // 초기화면에서 s를 누르면
            {
                key1 = ConsoleKey.NoName;
                quiz.startmusic = false; // 시작음악 끄기
                quiz.initial_on = false; // 초기화면 스위치를 끄고
                quiz.middlemusic = true; // 중간음악 스위치를 키고
                quiz.quiz1_on = true; // 퀴즈1 스위치를 켜준다
                quiz.score_on = true; // 스코어 스위치를 켜준다
            }


            if (quiz.quiz1_on == true) // 퀴즈1 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz1_on = false; // 퀴즈1을 끄고
                        quiz.quiz2_on = true; // 퀴즈2를 켜준다

                        break;
                    case ConsoleKey.B:
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz1_on = false; // 퀴즈1을 끄고
                        quiz.quiz2_on = true; // 퀴즈2를 켜준다
                        break;

                    case ConsoleKey.C:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz1_on = false; // 퀴즈1을 끄고
                        quiz.quiz2_on = true; // 퀴즈2를 켜준다
                        break;

                    case ConsoleKey.D:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz1_on = false; // 퀴즈1을 끄고
                        quiz.quiz2_on = true; // 퀴즈2를 켜준다
                        break;
                }
            }



            if (quiz.quiz2_on == true) // 퀴즈2 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz2_on = false; // 퀴즈2을 끄고
                        quiz.quiz3_on = true; // 퀴즈3를 켜준다

                        break;
                    case ConsoleKey.B:
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz2_on = false; // 퀴즈2을 끄고
                        quiz.quiz3_on = true; // 퀴즈3를 켜준다
                        break;

                    case ConsoleKey.C:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz2_on = false; // 퀴즈2을 끄고
                        quiz.quiz3_on = true; // 퀴즈3를 켜준다
                        break;

                    case ConsoleKey.D:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz2_on = false; // 퀴즈2을 끄고
                        quiz.quiz3_on = true; // 퀴즈3를 켜준다
                        break;
                }
            }

            if (quiz.quiz3_on == true) // 퀴즈3 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz3_on = false; // 퀴즈3을 끄고
                        quiz.quiz4_on = true; // 퀴즈4를 켜준다

                        break;
                    case ConsoleKey.B:
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz3_on = false; // 퀴즈3을 끄고
                        quiz.quiz4_on = true; // 퀴즈4를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz3_on = false; // 퀴즈3을 끄고
                        quiz.quiz4_on = true; // 퀴즈4를 켜준다
                        break;

                    case ConsoleKey.D:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz3_on = false; // 퀴즈3을 끄고
                        quiz.quiz4_on = true; // 퀴즈4를 켜준다
                        break;
                }
            }

            if (quiz.quiz4_on == true) // 퀴즈4 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz4_on = false; // 퀴즈4을 끄고
                        quiz.quiz5_on = true; // 퀴즈5를 켜준다

                        break;
                    case ConsoleKey.B:
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz4_on = false; // 퀴즈4을 끄고
                        quiz.quiz5_on = true; // 퀴즈5를 켜준다
                        break;

                    case ConsoleKey.C:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz4_on = false; // 퀴즈4을 끄고
                        quiz.quiz5_on = true; // 퀴즈5를 켜준다
                        break;

                    case ConsoleKey.D:
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz4_on = false; // 퀴즈4을 끄고
                        quiz.quiz5_on = true; // 퀴즈5를 켜준다
                        break;
                }
            }


            if (quiz.quiz5_on == true) // 퀴즈5 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz5_on = false; // 퀴즈5을 끄고
                        quiz.quiz6_on = true; // 퀴즈6를 켜준다

                        break;
                    case ConsoleKey.B:

                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz5_on = false; // 퀴즈5을 끄고
                        quiz.quiz6_on = true; // 퀴즈6를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz5_on = false; // 퀴즈5을 끄고
                        quiz.quiz6_on = true; // 퀴즈6를 켜준다
                        break;

                    case ConsoleKey.D:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz5_on = false; // 퀴즈5을 끄고
                        quiz.quiz6_on = true; // 퀴즈6를 켜준다
                        break;
                }
            }


            if (quiz.quiz6_on == true) // 퀴즈6 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz6_on = false; // 퀴즈6을 끄고
                        quiz.quiz7_on = true; // 퀴즈7를 켜준다

                        break;
                    case ConsoleKey.B:

                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz6_on = false; // 퀴즈6을 끄고
                        quiz.quiz7_on = true; // 퀴즈7를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz6_on = false; // 퀴즈6을 끄고
                        quiz.quiz7_on = true; // 퀴즈7를 켜준다
                        break;

                    case ConsoleKey.D:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz6_on = false; // 퀴즈6을 끄고
                        quiz.quiz7_on = true; // 퀴즈7를 켜준다
                        break;
                }
            }


            if (quiz.quiz7_on == true) // 퀴즈7 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz7_on = false; // 퀴즈7을 끄고
                        quiz.quiz8_on = true; // 퀴즈8를 켜준다

                        break;
                    case ConsoleKey.B:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz7_on = false; // 퀴즈7을 끄고
                        quiz.quiz8_on = true; // 퀴즈8를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz7_on = false; // 퀴즈7을 끄고
                        quiz.quiz8_on = true; // 퀴즈8를 켜준다
                        break;

                    case ConsoleKey.D:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz7_on = false; // 퀴즈7을 끄고
                        quiz.quiz8_on = true; // 퀴즈8를 켜준다
                        break;
                }
            }


            if (quiz.quiz8_on == true) // 퀴즈8 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz8_on = false; // 퀴즈8을 끄고
                        quiz.quiz9_on = true; // 퀴즈9를 켜준다

                        break;
                    case ConsoleKey.B:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz8_on = false; // 퀴즈8을 끄고
                        quiz.quiz9_on = true; // 퀴즈9를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz8_on = false; // 퀴즈8을 끄고
                        quiz.quiz9_on = true; // 퀴즈9를 켜준다
                        break;

                    case ConsoleKey.D:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz8_on = false; // 퀴즈8을 끄고
                        quiz.quiz9_on = true; // 퀴즈9를 켜준다
                        break;
                }
            }


            if (quiz.quiz9_on == true) // 퀴즈9 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz9_on = false; // 퀴즈9을 끄고
                        quiz.quiz10_on = true; // 퀴즈10를 켜준다

                        break;
                    case ConsoleKey.B:

                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz9_on = false; // 퀴즈9을 끄고
                        quiz.quiz10_on = true; // 퀴즈10를 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz9_on = false; // 퀴즈9을 끄고
                        quiz.quiz10_on = true; // 퀴즈10를 켜준다
                        break;

                    case ConsoleKey.D:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz9_on = false; // 퀴즈9을 끄고
                        quiz.quiz10_on = true; // 퀴즈10를 켜준다
                        break;
                }
            }


            if (quiz.quiz10_on == true) // 퀴즈10 스위치가 on 일때
            {
                switch (key1)
                {
                    case ConsoleKey.A:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz10_on = false; // 퀴즈10을 끄고
                        quiz.score_on = false; // 스코어 스위치를 꺼준다
                        quiz.choose_on = true; // 채점현황과 선택된 캐릭터를 보여주는 화면을 켜준다

                        break;
                    case ConsoleKey.B:

                        key1 = ConsoleKey.NoName;// 입력키를 초기화 시켜주고
                        quiz.quiz10_on = false; // 퀴즈10을 끄고
                        quiz.score_on = false; // 스코어 스위치를 꺼준다
                        quiz.choose_on = true; // 채점현황과 선택된 캐릭터를 보여주는 화면을 켜준다
                        break;

                    case ConsoleKey.C:

                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz10_on = false; // 퀴즈10을 끄고
                        quiz.score_on = false; // 스코어 스위치를 꺼준다
                        quiz.choose_on = true; // 채점현황과 선택된 캐릭터를 보여주는 화면을 켜준다
                        break;

                    case ConsoleKey.D:
                        ++quiz.score;
                        key1 = ConsoleKey.NoName; // 입력키를 초기화 시켜주고
                        quiz.quiz10_on = false; // 퀴즈10을 끄고
                        quiz.score_on = false; // 스코어 스위치를 꺼준다
                        quiz.choose_on = true; // 채점현황과 선택된 캐릭터를 보여주는 화면을 켜준다
                        break;
                }
            }


            if (quiz.choose_on == true && key1 == ConsoleKey.Enter) // enter키를 누르면 캐릭터 설명창으로 넘어간다
            {
                key1 = ConsoleKey.NoName; // 키 초기화 하기
                quiz.choose_on = false; // 채점창을 끈다
                quiz.explanation_on = true; // 캐릭터 설명창을 킨다
            }

            if (quiz.explanation_on == true && key1 == ConsoleKey.Enter) // enter키를 누르면 설명창을 끄고 테트리스모드로 넘어간다
            {
                quiz.middlemusic = false; // 중간음악을 꺼준다
                game.character_selection = false; // 캐릭터 선정 게임루프를 종료한다
                game.sokobanmode = true;
                music_song1.Stop(); // 노래를 끈다
                Console.Clear(); // 다음화면으로 넘어가기 위한 클리어
            }
        }

        #endregion


        switch (ChosenOne.id) // id의 선택에 따른 골라진 플레이어로 데이터 옮기기
        {
            case "신병":
                ChosenOne.hp = sin.hp;
                ChosenOne.mp = sin.mp;
                ChosenOne.passive = sin.passive;
                ChosenOne.passive_effect = sin.passive_effect;
                ChosenOne.atk = sin.atk;
                ChosenOne.attackeffect = sin.attackeffect;
                ChosenOne.skill = sin.skill;
                ChosenOne.skill_effect = sin.skill_effect;
                break;

            case "이병":
                ChosenOne.hp = ee.hp;
                ChosenOne.mp = ee.mp;
                ChosenOne.passive = ee.passive;
                ChosenOne.passive_effect = ee.passive_effect;
                ChosenOne.atk = ee.atk;
                ChosenOne.attackeffect = ee.attackeffect;
                ChosenOne.skill = ee.skill;
                ChosenOne.skill_effect = ee.skill_effect;
                break;

            case "일병":
                ChosenOne.hp = il.hp;
                ChosenOne.mp = il.mp;
                ChosenOne.passive = il.passive;
                ChosenOne.passive_effect = il.passive_effect;
                ChosenOne.atk = il.atk;
                ChosenOne.attackeffect = il.attackeffect;
                ChosenOne.skill = il.skill;
                ChosenOne.skill_effect = il.skill_effect;
                break;

            case "상병":
                ChosenOne.hp = sang.hp;
                ChosenOne.mp = sang.mp;
                ChosenOne.passive = sang.passive;
                ChosenOne.passive_effect = sang.passive_effect;
                ChosenOne.atk = sang.atk;
                ChosenOne.attackeffect = sang.attackeffect;
                ChosenOne.skill = sang.skill;
                ChosenOne.skill_effect = sang.skill_effect;
                break;
        }

        Box[] box =
        {
            new Box{x = 20, y = 3, symbol = "B"},
            new Box{x = 20, y = 6, symbol = "B"},
            new Box{x = 20, y = 7, symbol = "B"}
        };

        Wall[] wall =
        {
            new Wall{ x= 13, y = 2, symbol = "W"},
            new Wall{ x = 13, y = 5, symbol = "W"},
            new Wall{ x = 13, y = 6, symbol = "W"}
        };


        Goal[] goal = new Goal[game.max_y + 1]; // goal을 20개를 만든다
        for (int i = 0; i < goal.Length; ++i)
        {
            goal[i] = new Goal { x = game.max_x, y = i, symbol = "*" };
        }

        PX PX = new() { x = 3, y = 3, symbol = "S" };


        Player[] line4 =
        {
            new Player{ x = 18, y = 8, symbol = "4"},
            new Player{ x = 18, y = 5, symbol = "4"}
        };









        LOOP_SHOPPING:

        bool rendershop = true;
        while (game.shoppingmode)
        {
            // render
            string shoppingpath = Path.Combine("Assets", "Battle scene", "Shopping.txt");
            string[] shoppingshow = File.ReadAllLines(shoppingpath);


            if (rendershop == true)
            {
                for (int i = 0; i < shoppingshow.Length; ++i)
                {
                    Console.WriteLine(shoppingshow[i]);
                }
                rendershop = false;
            }


            // input

            ConsoleKey key4 = Console.ReadKey(true).Key;
            // update

            if (key4 == ConsoleKey.Enter)
            {
                Console.Clear();
                game.shoppingmode = false;
                game.sokobanmode = true;
            }


        }












        int pushed = 0;

            LOOP_EXIT:
        music_walk.PlayLooping();

        PLAYERDIRECTION PlayerDirection = PLAYERDIRECTION.NONE;
        LINE4DIRECTION Line4Direction = LINE4DIRECTION.NONE;

        // 테트리스 게임루프
        while (game.sokobanmode)
        {
            // render

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < line4.Length; ++i)// 병장을 그려준다
            {
                Function.Render(line4[i].x, line4[i].y, line4[i].symbol);
            }


            Function.Render(player.x, player.y, player.symbol); // 플레이어를 그려준다

            for (int i = 0; i < box.Length; ++i) // 박스를 그려준다
            {
                Function.Render(box[i].x, box[i].y, box[i].symbol);
            }


            for (int i = 0; i < wall.Length; ++i) // 벽을 그려준다
            {
                Function.Render(wall[i].x, wall[i].y, wall[i].symbol);
            }

            Function.Render(PX.x, PX.y, PX.symbol); // PX를 그려준다

            for (int i = 1; i < game.max_y + 1; ++i) // goal 을 그려준다
            {
                Function.Render(goal[i].x, goal[i].y, goal[i].symbol);
            }


            if (player.x == player.prex && player.y == player.prey)
            {
                // 플레이어의 현재좌표와 이전좌표가 겹친다면 그리지 않는다
            }
            else
            {
                Function.Render(player.prex, player.prey, " ");
            }


            for (int i = 0; i < line4.Length; ++i)
            {
                if (line4[i].x == line4[i].prex && line4[i].y == line4[i].prey)
                {
                    // 병장의 현재좌표와 이전좌표가 겹친다면 그리지 않는다
                }
                else
                {
                    Function.Render(line4[i].prex, line4[i].prey, " ");
                }
            }



            #region Draw Boundaries
            for (int i = 0; i < game.max_x + 2; ++i)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Function.Render(i, game.min_y - 1, "#");
                Function.Render(i, game.max_y + 1, "#");
            }
            for (int i = 0; i < game.max_y + 2; ++i)
            {
                Function.Render(game.min_x - 1, i, "#");
                Function.Render(game.max_x + 1, i, "#");
            }
            #endregion





            // input

            ConsoleKey key2 = Console.ReadKey(true).Key;


            // update


            player.prex = player.x;
            player.prey = player.y;

            if (key2 == ConsoleKey.RightArrow) // 플레이어 이동 구현
            {
                player.x = Math.Clamp(player.x + 1, game.min_x, game.max_x);
                PlayerDirection = PLAYERDIRECTION.RIGHT;
            }
            if (key2 == ConsoleKey.LeftArrow)
            {
                player.x = Math.Clamp(player.x - 1, game.min_x, game.max_x);
                PlayerDirection = PLAYERDIRECTION.LEFT;
            }
            if (key2 == ConsoleKey.UpArrow)
            {
                player.y = Math.Clamp(player.y - 1, game.min_y, game.max_y);
                PlayerDirection = PLAYERDIRECTION.UP;
            }
            if (key2 == ConsoleKey.DownArrow)
            {
                player.y = Math.Clamp(player.y + 1, game.min_y, game.max_y);
                PlayerDirection = PLAYERDIRECTION.DOWN;
            }




            PX.close = false; // 보통은 false이니 위에 걍 정의 (초기화용)

            // 플레이어가 PX의 근처에 있는지를 정의하는 코드
            if (player.x == PX.x - 1 && player.y == PX.y) // 플레이어가 px의 왼쪽에 있다면
            {
                PX.close = true; // px근처가 true가 된다
            }
            if (player.x == PX.x + 1 && player.y == PX.y) // 플레이어가 px의 오른쪽에 있다면
            {
                PX.close = true; // px근처가 true가 된다
            }
            if (player.x == PX.x && player.y == PX.y - 1) // 플레이어가 px의 위쪽에 있다면
            {
                PX.close = true; // px근처가 true가 된다
            }
            if (player.x == PX.x && player.y == PX.y + 1) // 플레이어가 px의 아래쪽에 있다면
            {
                PX.close = true; // px근처가 true가 된다
            }

            if (PX.close && key2 == ConsoleKey.Spacebar) // shopping mode로 들어가기
            {
                Console.Clear(); // 다음화면으로 넘어가기 위한 클리어
                game.sokobanmode = false;
                game.shoppingmode = true;
                goto LOOP_SHOPPING;
            }


            // 상점에 막히는 플레이어 구현
            if (player.x == PX.x && player.y == PX.y)
            {
                switch (PlayerDirection)
                {
                    case PLAYERDIRECTION.RIGHT:
                        player.x = PX.x - 1;
                        break;
                    case PLAYERDIRECTION.LEFT:
                        player.x = PX.x + 1;
                        break;
                    case PLAYERDIRECTION.UP:
                        player.y = PX.y + 1;
                        break;
                    case PLAYERDIRECTION.DOWN:
                        player.y = PX.y - 1;
                        break;

                }
            }




            for (int i = 0; i < box.Length; ++i) // 박스 밀기
            {
                if (player.x == box[i].x && player.y == box[i].y)
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            box[i].x = Math.Clamp(box[i].x + 1, game.min_x, game.max_x);
                            player.x = box[i].x - 1;
                            break;
                        case PLAYERDIRECTION.LEFT:
                            box[i].x = Math.Clamp(box[i].x - 1, game.min_x, game.max_x);
                            player.x = box[i].x + 1;
                            break;
                        case PLAYERDIRECTION.UP:
                            box[i].y = Math.Clamp(box[i].y - 1, game.min_y, game.max_y);
                            player.y = box[i].y + 1;
                            break;
                        case PLAYERDIRECTION.DOWN:
                            box[i].y = Math.Clamp(box[i].y + 1, game.min_y, game.max_y);
                            player.y = box[i].y - 1;
                            break;

                    }

                    pushed = i; // 민 박스 표기

                }

            }


            for (int collided = 0; collided < box.Length; ++collided) // 박스끼리 충돌
            {

                if (collided == pushed) // 민 박스와 밀린박스가 같다면 걍 스킵
                {
                    continue;
                }

                if (box[collided].x == box[pushed].x && box[collided].y == box[pushed].y)
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            box[pushed].x = box[collided].x - 1;
                            player.x = box[pushed].x - 1;
                            break;
                        case PLAYERDIRECTION.LEFT:
                            box[pushed].x = box[collided].x + 1;
                            player.x = box[pushed].x + 1;
                            break;
                        case PLAYERDIRECTION.UP:
                            box[pushed].y = box[collided].y + 1;
                            player.y = box[pushed].y + 1;
                            break;
                        case PLAYERDIRECTION.DOWN:
                            box[pushed].y = box[collided].y - 1;
                            player.y = box[pushed].y - 1;
                            break;

                    }
                }
            }


            for (int i = 0; i < box.Length; ++i) // 박스랑 벽이랑 충돌
            {
                for (int k = 0; k < wall.Length; ++k)
                {

                    if (box[i].x == wall[k].x && box[i].y == wall[k].y)
                    {
                        switch (PlayerDirection)
                        {
                            case PLAYERDIRECTION.RIGHT:
                                box[i].x = wall[k].x - 1;
                                player.x = box[i].x - 1;
                                break;
                            case PLAYERDIRECTION.LEFT:
                                box[i].x = wall[k].x + 1;
                                player.x = box[i].x + 1;
                                break;
                            case PLAYERDIRECTION.UP:
                                box[i].y = wall[k].y + 1;
                                player.y = box[i].y + 1;
                                break;
                            case PLAYERDIRECTION.DOWN:
                                box[i].y = wall[k].y - 1;
                                player.y = box[i].y - 1;
                                break;
                        }
                    }





                }


            }













            for (int i = 0; i < wall.Length; ++i) // 플레이어가 벽에 막히는 것 구현
            {
                if (player.x == wall[i].x && player.y == wall[i].y)
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            player.x = wall[i].x - 1;
                            break;
                        case PLAYERDIRECTION.LEFT:
                            player.x = wall[i].x + 1;
                            break;
                        case PLAYERDIRECTION.UP:
                            player.y = wall[i].y + 1;
                            break;
                        case PLAYERDIRECTION.DOWN:
                            player.y = wall[i].y - 1;
                            break;

                    }
                }
            }










            for (int i = 0; i < line4.Length; ++i)
            {
                line4[i].prex = line4[i].x;
                line4[i].prey = line4[i].y;
            }


            //for (int i = 0; i < line4.Length; ++i)
            //{
            //    int randommove = random.Next(1, 6); // 1부터 5까지의 랜덤한 숫자를 저장

            //    switch (randommove)
            //    {
            //        case 1: // 1 나오면 병장이 오른쪽으로 이동
            //            line4[i].x = Math.Clamp(line4[i].x + 1, game.min_x, game.max_x);
            //            Line4Direction = LINE4DIRECTION.RIGHT;
            //            break;
            //        case 2: // 2 나오면 병장이 왼쪽으로 이동
            //            line4[i].x = Math.Clamp(line4[i].x - 1, game.min_x, game.max_x);
            //            Line4Direction = LINE4DIRECTION.LEFT;
            //            break;
            //        case 3: // 3 나오면 병장이 위로 이동
            //            line4[i].y = Math.Clamp(line4[i].y - 1, game.min_y, game.max_y);
            //            Line4Direction = LINE4DIRECTION.UP;
            //            break;
            //        case 4: // 4 나오면 병장이 아래로 이동
            //            line4[i].y = Math.Clamp(line4[i].y + 1, game.min_y, game.max_y);
            //            Line4Direction = LINE4DIRECTION.DOWN;
            //            break;
            //        case 5: // 5 나오면 병장 가만히 stop
            //            break;

            //    }
            //}


            // 병장과 벽의 충돌
            for (int i = 0; i < line4.Length ;++i)
            {
                for (int k = 0; k < wall.Length ;++k)
                {
                    if (line4[i].x == wall[k].x && line4[i].y == wall[k].y)
                    {
                        switch (Line4Direction)
                        {
                            case LINE4DIRECTION.RIGHT:
                                line4[i].x = wall[k].x - 1;
                                break;
                            case LINE4DIRECTION.LEFT:
                                line4[i].x = wall[k].x + 1;
                                break;
                            case LINE4DIRECTION.UP:
                                line4[i].y = wall[k].y + 1;
                                break;
                            case LINE4DIRECTION.DOWN:
                                line4[i].y = wall[k].y - 1;
                                break;
                        }
                    }
                }
            }

            // 병장과 박스의 충돌
            for (int i = 0; i < line4.Length; ++i)
            {
                for (int k = 0; k < box.Length; ++k)
                {
                    if (line4[i].x == box[k].x && line4[i].y == box[k].y)
                    {
                        switch (Line4Direction)
                        {
                            case LINE4DIRECTION.RIGHT:
                                line4[i].x = box[k].x - 1;
                                break;
                            case LINE4DIRECTION.LEFT:
                                line4[i].x = box[k].x + 1;
                                break;
                            case LINE4DIRECTION.UP:
                                line4[i].y = box[k].y + 1;
                                break;
                            case LINE4DIRECTION.DOWN:
                                line4[i].y = box[k].y - 1;
                                break;
                        }
                    }
                }
            }




            for (int i = 0; i < line4.Length; ++i)
            {
                if (player.x == line4[i].x && player.y == line4[i].y) // 플레이어와 병장이 만난다면 테트리스모드를 끈다
                {
                    Console.Clear(); // 화면을 지워주고
                    game.sokobanmode = false; // 소코반모드 꺼주고
                    game.collision = true; // 병장과의 만남모드 켜주기
                    music_walk.Stop(); // 노래꺼주기
                    break;
                }
            }



        }




        music_laugh.PlayLooping();
        string prebattle_path = ""; // prebattle 씬의 주소를 저장하는 객체
        string[] prebattle; // prebattle의 씬을 저장할 배열
        bool render_prebattle = true;

        while (game.collision) // 플레이어와 병장이 부딪혔을 때 Scene을 위한 루프
        {

            //render 

            prebattle_path = Path.Combine("Assets", "Battle scene", "Prebattle.txt");
            prebattle = File.ReadAllLines(prebattle_path);

            if (render_prebattle == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < prebattle.Length; ++i) // prebattle scene을 그려준다
                {
                    Console.WriteLine(prebattle[i]);
                }
            }


            render_prebattle = false;

            // 인풋

            ConsoleKey key3 = Console.ReadKey(true).Key;

            // update

            if (key3 == ConsoleKey.Enter)
            {
                Console.Clear(); // 다음 루프로 넘어가기 위한 클리어
                game.collision = false; // 병장과의 만남 씬 꺼주고
                game.battlemode = true; // 배틀모드 켜주기
                break;
            }

        }









        string ByeongWhat = "[병장은 무엇을 할까?]";
        string ByeongPassive = $"Passive({Byeong.passive}) → {Byeong.passive_effect}"; ;
        string ByeongAttack = $"Attack({Byeong.attackeffect}) → [플레이어 HP {Byeong.atk} 감소]";
        string ByeongSkill1 = $"Skill1({Byeong.skill}) → {Byeong.skill_effect}";
        string ByeongSkill2 = $"Skill2({Byeong.skill2}) → {Byeong.skill2_effect}";
        string ByeongSkill3 = $"Skill3({Byeong.skill3}) → {Byeong.skill3_effect}";


        string PlayerWhat = $"[{ChosenOne.id}은 무엇을 할까?]";
        string PlayerPassive = $"Passive({ChosenOne.passive}) → {ChosenOne.passive_effect}";
        string PlayerAttack = $"A: Attack({ChosenOne.attackeffect}) → [병장 HP {ChosenOne.atk} 감소]";
        string PlayerSkill = $"S: Skill({ChosenOne.skill}) → {ChosenOne.skill_effect}";

        bool byeong_lose = false;
        Battle battle = new();
        battle.battle_on = true;
        music_battle.PlayLooping();

        while (game.battlemode) // 병장과의 배틀모드
        {
            Console.Clear(); // 여기엔 콘솔 클리어 하는게 더 좋을듯 어차피 텍스트니까



            // Render


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (battle.battle_on == true) // 배틀 스위치가 on이면
            {
                string battlepath = Path.Combine("Assets", "Battle scene", "Battle.txt");
                string[] battle_scene = File.ReadAllLines(battlepath);
                for (int i = 0; i < battle_scene.Length; ++i) // 배틀씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(battle_scene[i]);
                }


            }


            if (battle.victory_on == true) // 승리 스위치가 on이면
            {
                string victorypath = Path.Combine("Assets", "Battle scene", "Victory.txt");
                string[] victory_scene = File.ReadAllLines(victorypath);
                for (int i = 0; i < victory_scene.Length; ++i) // 승리씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(victory_scene[i]);
                }
            }

            if (battle.lose_on == true) // 패배 스위치가 on이면
            {
                string losepath = Path.Combine("Assets", "Battle scene", "Lose.txt");
                string[] lose_scene = File.ReadAllLines(losepath);
                for (int i = 0; i < lose_scene.Length; ++i) // 패배씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(lose_scene[i]);
                }
            }



            if (battle.battle_on == true) // 배틀스위치가 on이라면
            {
                // 입력키에 따른 병장과 플레이어의 행동설명
                Console.ForegroundColor = ConsoleColor.Red; // 병장은 무엇을 할까? 
                Function.Render(55, 18, ByeongWhat);

                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(55, 20, ByeongPassive); // 병장의 패시브를 그려준다

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Function.Render(55, 22, ByeongAttack); // 병장의 공격을 그려준다

                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(55, 24, ByeongSkill1); // 병장의 스킬을 그려준다
                Function.Render(55, 26, ByeongSkill2); // 병장의 스킬을 그려준다
                Function.Render(55, 28, ByeongSkill3); // 병장의 스킬을 그려준다

                Console.ForegroundColor = ConsoleColor.White; // 신병은 무엇을 할까?
                Function.Render(5, 34, PlayerWhat);

                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(5, 36, PlayerPassive); // 패시브를 그려준다

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Function.Render(5, 38, PlayerAttack); // Attack을 그려준다

                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(5, 40, PlayerSkill); // Skill을 그려준다

            }




            Console.ForegroundColor = ConsoleColor.DarkRed;
            Function.Render(53, 3, "[병장]");

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 5, $"HP({Byeong.hp})");
            for (int i = 0; i < Byeong.hp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Function.Render(63 + (i * 2), 5, "■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 6, $"MP({Byeong.mp})");
            for (int i = 0; i < Byeong.mp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(63 + (i * 2), 6, "■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(55, 7, $"ATK = {Byeong.atk}");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Function.Render(53, 10, "[플레이어]");

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 12, $"HP({ChosenOne.hp})");
            for (int i = 0; i < ChosenOne.hp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Function.Render(63 + (i * 2), 12, "■");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 13, $"MP({ChosenOne.mp})");
            for (int i = 0; i < ChosenOne.mp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(63 + (i * 2), 13, "■");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(55, 14, $"ATK = {ChosenOne.atk}");


            // input

            ConsoleKey key4 = Console.ReadKey(true).Key;

            // update


            if (battle.battle_on == true) // 플레이어 턴 구현하기
            {
                switch (ChosenOne.id)
                {
                    case "신병": // 만약 선택된 캐릭터의 id가 신병인데
                        switch (key4) // 입력한 키가 
                        {
                            case ConsoleKey.A: // A면
                                Byeong.hp -= 3; // 병장 HP -3 해버리기
                                break;
                            case ConsoleKey.S: // S면
                                Byeong.mp -= random.Next(3,6); // 병장 MP를 3~5 사이의 난수만큼 빼버리기
                                break;
                        }
                        break;
                    case "이병":
                        switch (key4)
                        {
                            case ConsoleKey.A:
                                Byeong.hp -= 3;
                                break;
                            case ConsoleKey.S:
                                Byeong.hp -= random.Next(3,6);
                                break;
                        }
                        break;
                    case "일병":
                        switch (key4)
                        {
                            case ConsoleKey.A:
                                Byeong.hp -= 3;
                                break;
                            case ConsoleKey.S:
                                // 병장 2턴동안 가만히 있게하는 옵션 구현해야함
                                break;
                        }
                        break;
                    case "상병":
                        switch (key4)
                        {
                            case ConsoleKey.A:
                                Byeong.hp -= 3;
                                break;
                            case ConsoleKey.S:
                                Byeong.mp -= random.Next(5,11);
                                break;
                           
                        }
                        break;
                }
            }

            if (battle.battle_on) // 병장 턴 구현하기
            {

            }











































            if (key4 == ConsoleKey.Enter)
            {
                game.sokobanmode = true;
                Console.Clear();
                goto LOOP_EXIT;

            }






            if (ChosenOne.hp <= 0) // 플레이어 hp가 0이하가 되는경우
            {
                key4 = ConsoleKey.NoName; // 키 초기화하기
                music_battle.Stop(); // 배틀모드 소리끄고
                music_lose.PlayLooping(); // 패배모드 소리 키기
                battle.battle_on = false; // 배틀모드를 끄고
                battle.lose_on = true; // 패배모드를 킨다
            }

            if (Byeong.hp <= 0) // 병장 hp가 0이하가 되는경우
            {
                key4 = ConsoleKey.NoName; // 키 초기화하기
                Byeong.hp = Byeong.savehp; // 병장 피 원래대로 돌려놓기
                byeong_lose = true;
                music_battle.Stop(); // 배틀모드 소리 끄고
                music_victory.PlayLooping(); // 승리모드 소리 키기
                game.collision = true; // 충돌씬 다시 true로 바꾸고
                battle.battle_on = false; // 배틀모드를 끄고
                battle.victory_on = true; // 승리모드를 킨다
            }
            
            
            
            if (battle.victory_on == true && key4 == ConsoleKey.Enter)
            {
                game.battlemode = false; // 배틀모드를 꺼주고
                game.sokobanmode = true; // 소코반 모드를 켜준다
                break;
            }

            if (battle.lose_on == true && key4 == ConsoleKey.Enter)
            {
                game.battlemode = false; // 배틀모드를 꺼주고
                game.sokobanmode = true; // 소코반 모드를 켜준다
                break;
            }







        }





    }

}
