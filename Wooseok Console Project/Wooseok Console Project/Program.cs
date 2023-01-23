
using System;
using System.Diagnostics;
using System.Media;
using System.Numerics;
using Wooseok_Console_Project;

class Program
{
    enum DIRECTION
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

        Character chosen_one = new();
        Character sin = new() // 신병 객체 생성
        {
            id = "신병",
            hp = 5,
            mp = 5,
            passive = "존댓말 하기 [병장 ATK 1 증가]",
            passive_effect = "군대 언어가 익숙치 않아 매 턴 50%의 확률로 존댓말을 하여 병장을 화나게 한다",
            atk = 3,
            attackeffect = "예?",
            skill = "관등성명 대기(MP 5) → [병장 MP 3~5 감소]",
            skill_effect = "관등성명을 댐으로써 병장의 흥미가 떨어진다 "
        };

        Character ee = new() // 이병 객체 생성
        {
            id = "이병",
            hp = 10,
            mp = 10,
            passive = "얼 타기 [병장 MP 1 증가]",
            passive_effect = "일머리가 없어 병장의 사기를 증가시킨다 ",
            atk = 3,
            attackeffect = "잘 못들었습니다?",
            skill = "마음의 편지 쓰기(MP 5) → [병장 HP 3 ~ 5 감소]",
            skill_effect = "마음의 편지를 씀으로써 병장의 휴가가 잘렸다"
        };

        Character il = new() // 일병 객체 생성
        {
            id = "일병",
            hp = 10,
            mp = 10,
            passive = "작업노예 [매턴 30% 확률로 받는 데미지 반감]",
            passive_effect = "하루종일 작업만 하다 맷집이 좋아졌다",
            atk = 3,
            attackeffect = "잘 못슴다?",
            skill = "담배 물리기 (MP 5) → [병장 2턴 Freeze]",
            skill_effect = "담배를 권함으로써 병장이 2턴동안 흡연을 한다"
        };

        Character sang = new() // 상병 객체 생성
        {
            id = "상병",
            hp = 15,
            mp = 10,
            passive = "개기기 [매턴 10% 확률로 본인 ATK 1 증가]",
            passive_effect = "짬 좀 찼다고 선임들 하나씩 먹으려 함",
            atk = 3,
            attackeffect = "자슴다?",
            skill = "무시하기 (MP 5) → [병장 MP 5~10 감소]",
            skill_effect = "집에 갈 양반이 왜 아직도 실세놀이를 하고 계십니까?"
        };


        Character byeong = new() // 병장 객체 생성
        {
            id = "병장",
            hp = 20,
            savehp = 20,
            mp = 10,
            savemp = 10,
            passive = "현타 세게 맞기 [매턴 20% 확률로 턴 중지]",
            passive_effect = "동기들을 먼저 보내고 혼자만 남아 현타를 맞음",
            atk = 5,
            attackeffect = "야 여동생이나 누나 있냐?",
            skill = "군가 부르게 하기 (MP 2) → [DMG 1 ~ 3]",
            skill_effect = "야 군가 한곡 기깔나게 뽑아봐",

            skill2 = "기강 잡기 (MP 3) → [DMG 3 ~ 5]",
            skill2_effect = "개빠졌네 진짜?? 군대가 니 집 안방이냐??",

            skill3 = "맞선임 호출하기 (MP 5) → [DMG 5 ~ 10]",
            skill3_effect = "야, 니 맞선임 누구냐??"
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
                    chosen_one.id = choose;
                    Function.Render(45, 18, chosen_one.id);
                }
                else if (4 <= quiz.score && quiz.score <= 6)
                {
                    choose = ee.id;
                    chosen_one.id = choose;
                    Function.Render(45, 18, chosen_one.id);
                }
                else if (7 <= quiz.score && quiz.score <= 8)
                {
                    choose = il.id;
                    chosen_one.id = choose;
                    Function.Render(45, 18, chosen_one.id);
                }
                else if (9 <= quiz.score && quiz.score <= 10)
                {
                    choose = sang.id;
                    chosen_one.id = choose;
                    Function.Render(45, 18, chosen_one.id);
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
                music_song1.Stop(); // 노래를 끈다
            }
        }
        Console.Clear(); // 다음화면으로 넘어가기 위한 클리어
        #endregion


        switch (chosen_one.id) // id의 선택에 따른 골라진 플레이어로 데이터 옮기기
        {
            case "신병":
                chosen_one.hp = sin.hp;
                chosen_one.mp = sin.mp;
                chosen_one.passive = sin.passive;
                chosen_one.passive_effect = sin.passive_effect;
                chosen_one.atk = sin.atk;
                chosen_one.attackeffect = sin.attackeffect;
                chosen_one.skill = sin.skill;
                break;

            case "이병":
                chosen_one.hp = ee.hp;
                chosen_one.mp = ee.mp;
                chosen_one.passive = ee.passive;
                chosen_one.passive_effect = ee.passive_effect;
                chosen_one.atk = ee.atk;
                chosen_one.attackeffect = ee.attackeffect;
                chosen_one.skill = ee.skill;
                break;

            case "일병":
                chosen_one.hp = il.hp;
                chosen_one.mp = il.mp;
                chosen_one.passive = il.passive;
                chosen_one.passive_effect = il.passive_effect;
                chosen_one.atk = il.atk;
                chosen_one.attackeffect = il.attackeffect;
                chosen_one.skill = il.skill;
                break;

            case "상병":
                chosen_one.hp = sang.hp;
                chosen_one.mp = sang.mp;
                chosen_one.passive = sang.passive;
                chosen_one.passive_effect = sang.passive_effect;
                chosen_one.atk = sang.atk;
                chosen_one.attackeffect = sang.attackeffect;
                chosen_one.skill = sang.skill;
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


        Player[] line4 =
        {
            new Player{ x = 18, y = 8, symbol = "4"},
            new Player{ x = 18, y = 5, symbol = "4"}
        };

        int pushed = 0;

    LOOP_EXIT:
        music_walk.PlayLooping();

        DIRECTION PlayerDirection = DIRECTION.NONE;

        // 테트리스 게임루프
        while (game.sokobanmode)
        {
            // render

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < line4.Length ;++i)// 병장을 그려준다
            {
                Function.Render(line4[i].x, line4[i].y, line4[i].symbol); 
            }
            

            Function.Render(player.x, player.y, player.symbol); // 플레이어를 그려준다

            for (int i = 0; i < box.Length ;++i) // 박스를 그려준다
            {
                Function.Render(box[i].x, box[i].y, box[i].symbol);
            }


            for (int i = 0; i < wall.Length ;++i)
            {
                Function.Render(wall[i].x, wall[i].y, wall[i].symbol);
            }



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


            for (int i = 0; i < line4.Length ;++i)
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

            if (key2 == ConsoleKey.RightArrow)
            {
                player.x = Math.Clamp(player.x + 1, game.min_x, game.max_x);
                PlayerDirection = DIRECTION.RIGHT;
            }
            if (key2 == ConsoleKey.LeftArrow)
            {
                player.x = Math.Clamp(player.x - 1, game.min_x, game.max_x);
                PlayerDirection = DIRECTION.LEFT;
            }
            if (key2 == ConsoleKey.UpArrow)
            {
                player.y = Math.Clamp(player.y - 1, game.min_y, game.max_y);
                PlayerDirection = DIRECTION.UP;
            }
            if (key2 == ConsoleKey.DownArrow)
            {
                player.y = Math.Clamp(player.y + 1, game.min_y, game.max_y);
                PlayerDirection = DIRECTION.DOWN;
            }



            for (int i = 0; i < box.Length ;++i) // 박스 밀기
            {
                if (player.x == box[i].x && player.y == box[i].y)
                {
                    switch (PlayerDirection)
                    {
                        case DIRECTION.RIGHT:
                            box[i].x = Math.Clamp(box[i].x +1, game.min_x, game.max_x);
                            player.x = box[i].x - 1;
                            break;
                        case DIRECTION.LEFT:
                            box[i].x = Math.Clamp(box[i].x - 1, game.min_x, game.max_x);
                            player.x = box[i].x + 1;
                            break;
                        case DIRECTION.UP:
                            box[i].y = Math.Clamp(box[i].y -1, game.min_y, game.max_y);
                            player.y = box[i].y + 1;
                            break;
                        case DIRECTION.DOWN:
                            box[i].y = Math.Clamp(box[i].y + 1, game.min_y, game.max_y);
                            player.y = box[i].y - 1;
                            break;

                    }

                    pushed = i; // 민 박스 표기

                }

            }


            for (int collided= 0; collided < box.Length ;++collided) // 박스끼리 충돌
            {

                if (collided == pushed) // 민 박스와 밀린박스가 같다면 걍 스킵
                {
                    continue;
                }

                if (box[collided].x == box[pushed].x && box[collided].y == box[pushed].y)
                {
                    switch (PlayerDirection)
                    {
                        case DIRECTION.RIGHT:
                            box[pushed].x = box[collided].x - 1;
                            player.x = box[pushed].x - 1;
                            break;
                        case DIRECTION.LEFT:
                            box[pushed].x = box[collided].x + 1;
                            player.x = box[pushed].x + 1;
                            break;
                        case DIRECTION.UP:
                            box[pushed].y = box[collided].y + 1;
                            player.y = box[pushed].y + 1;
                            break;
                        case DIRECTION.DOWN:
                            box[pushed].y = box[collided].y - 1;
                            player.y = box[pushed].y - 1;
                            break;

                    }
                }
            }


            for (int i = 0; i < box.Length ;++i) // 박스랑 벽이랑 충돌
            {
                for (int  k = 0; k < wall.Length ; ++k)
                {

                    if (box[i].x == wall[k].x && box[i].y == wall[k].y)
                    {
                        switch (PlayerDirection)
                        {
                            case DIRECTION.RIGHT:
                                box[i].x = wall[k].x - 1;
                                player.x = box[i].x - 1;
                                break;
                            case DIRECTION.LEFT:
                                box[i].x = wall[k].x + 1;
                                player.x = box[i].x + 1;
                                break;
                            case DIRECTION.UP:
                                box[i].y = wall[k].y + 1;
                                player.y = box[i].y + 1;
                                break;
                            case DIRECTION.DOWN:
                                box[i].y = wall[k].y - 1;
                                player.y = box[i].y - 1;
                                break;
                        }
                    }





                }


            }













            for (int i = 0; i < wall.Length ;++i) // 플레이어가 벽에 막히는 것 구현
            {
                if (player.x == wall[i].x && player.y == wall[i].y)
                {
                    switch (PlayerDirection)
                    {
                        case DIRECTION.RIGHT:
                            player.x = wall[i].x - 1;
                            break;
                        case DIRECTION.LEFT:
                            player.x = wall[i].x + 1;
                            break;
                        case DIRECTION.UP:
                            player.y = wall[i].y + 1;
                            break;
                        case DIRECTION.DOWN:
                            player.y = wall[i].y - 1;
                            break;

                    }
                }
            }








            

            for (int i = 0; i < line4.Length ;++i)
            {
                line4[i].prex = line4[i].x;
                line4[i].prey = line4[i].y;
            }


            //for (int i = 0; i < line4.Length ; ++i)
            //{
            //    int randommove = random.Next(1, 6); // 1부터 5까지의 랜덤한 숫자를 저장

            //    switch (randommove)
            //    {
            //        case 1: // 1 나오면 병장이 오른쪽으로 이동
            //            line4[i].x = Math.Clamp(line4[i].x + 1, game.min_x, game.max_x);
            //            break;
            //        case 2: // 2 나오면 병장이 왼쪽으로 이동
            //            line4[i].x = Math.Clamp(line4[i].x - 1, game.min_x, game.max_x);
            //            break;
            //        case 3: // 3 나오면 병장이 위로 이동
            //            line4[i].y = Math.Clamp(line4[i].y - 1, game.min_y, game.max_y);
            //            break;
            //        case 4: // 4 나오면 병장이 아래로 이동
            //            line4[i].y = Math.Clamp(line4[i].y + 1, game.min_y, game.max_y);
            //            break;
            //        case 5: // 5 나오면 병장 가만히 stop
            //            break;

            //    }
            //}
            








            for (int i = 0; i < line4.Length ;++i)
            {
                if (player.x == line4[i].x && player.y == line4[i].y) // 플레이어와 병장이 만난다면 테트리스모드를 끈다
                {
                    game.sokobanmode = false;
                    music_walk.Stop();
                    break;
                }
            }
            


        }

        Console.Clear(); // 다음 루프로 넘어가기 위한 클리어


        music_laugh.PlayLooping();
        string prebattle_path = ""; // prebattle 씬의 주소를 저장하는 객체
        string[] prebattle; // prebattle의 씬을 저장할 배열
        bool render_prebattle = true;

        while (game.collision) // 플레이어와 병장이 부딪혔을 때 Scene을 위한 루프
        {

            //render 

            prebattle_path = Path.Combine("Assets", "Battle_scene", "Prebattle.txt");
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
                game.collision = false;
                break;
            }

        }
        Console.Clear(); // 다음 루프로 넘어가기 위한 클리어








        string byeong_line = "병장 테스트용";
        string player_basic = $"{chosen_one.id}은 무엇을 할까?";
        string player_line = $"플레이어 테스트용";

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
                string battlepath = Path.Combine("Assets", "Battle_scene", "Battle.txt");
                string[] battle_scene = File.ReadAllLines(battlepath);
                for (int i = 0; i < battle_scene.Length; ++i) // 배틀씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(battle_scene[i]);
                }


            }


            if (battle.victory_on == true) // 승리 스위치가 on이면
            {
                string victorypath = Path.Combine("Assets", "Battle_scene", "Victory.txt");
                string[] victory_scene = File.ReadAllLines(victorypath);
                for (int i = 0; i < victory_scene.Length; ++i) // 승리씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(victory_scene[i]);
                }
            }

            if (battle.lose_on == true) // 패배 스위치가 on이면
            {
                string losepath = Path.Combine("Assets", "Battle_scene", "Lose.txt");
                string[] lose_scene = File.ReadAllLines(losepath);
                for (int i = 0; i < lose_scene.Length; ++i) // 패배씬 텍스트파일을 불러와 읽는다
                {
                    Console.WriteLine(lose_scene[i]);
                }
            }



            if (battle.battle_on == true) // 배틀스위치가 on이라면
            {
                // 입력키에 따른 병장과 플레이어의 행동설명
                Console.ForegroundColor = ConsoleColor.Red;
                Function.Render(55, 20, byeong_line);

                Console.ForegroundColor = ConsoleColor.White;
                Function.Render(5, 32, player_basic);

                Function.Render(5, 34, player_line);

            }




            Console.ForegroundColor = ConsoleColor.DarkRed;
            Function.Render(53, 3, "[병장]");

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 5, $"HP({byeong.hp})");
            for (int i = 0; i < byeong.hp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Function.Render(63 + (i * 2), 5, "■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 6, $"MP({byeong.mp})");
            for (int i = 0; i < byeong.mp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(63 + (i * 2), 6, "■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(55, 7, $"ATK = {byeong.atk}");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Function.Render(53, 10, "[플레이어]");

            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 12, $"HP({chosen_one.hp})");
            for (int i = 0; i < chosen_one.hp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Function.Render(63 + (i * 2), 12, "■");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(56, 13, $"MP({chosen_one.mp})");
            for (int i = 0; i < chosen_one.mp; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Function.Render(63 + (i * 2), 13, "■");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(55, 14, $"ATK = {chosen_one.atk}");


            // input

            ConsoleKey key4 = Console.ReadKey(true).Key;

            // update



            if (byeong_lose == false && key4 == ConsoleKey.A)
            {
                byeong.hp--;
            }
            if (key4 == ConsoleKey.D)
            {
                chosen_one.hp--;
            }













































            if (key4 == ConsoleKey.Enter)
            {
                game.sokobanmode = true;
                Console.Clear();
                goto LOOP_EXIT;

            }






            if (chosen_one.hp <= 0) // 플레이어 hp가 0이하가 되는경우
            {
                key4 = ConsoleKey.NoName; // 키 초기화하기
                music_battle.Stop(); // 배틀모드 소리끄고
                music_lose.PlayLooping(); // 패배모드 소리 키기
                battle.battle_on = false; // 배틀모드를 끄고
                battle.lose_on = true; // 패배모드를 킨다
            }

            if (byeong.hp <= 0) // 병장 hp가 0이하가 되는경우
            {
                key4 = ConsoleKey.NoName; // 키 초기화하기
                byeong.hp = byeong.savehp; // 병장 피 원래대로 돌려놓기
                byeong_lose = true;
                music_battle.Stop(); // 배틀모드 소리 끄고
                music_victory.PlayLooping(); // 승리모드 소리 키기
                game.collision = true; // 충돌씬 다시 true로 바꾸고
                battle.battle_on = false; // 배틀모드를 끄고
                battle.victory_on = true; // 승리모드를 킨다
            }


            if (battle.lose_on == true && key4 == ConsoleKey.Enter)
            {
                game.sokobanmode = false;
                break;
            }







        }





    }

}