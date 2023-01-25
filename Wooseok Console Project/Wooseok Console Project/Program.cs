using System;
using System.Media;
using System.Security.Cryptography.X509Certificates;
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
        Function function = new();
        function.Setting(); // 초기세팅

        #region 음악 재생목록
        // 음악 따놓기
        SoundPlayer MusicStart = new SoundPlayer(@"Assets\Music\wakeup.wav");
        SoundPlayer MusicSong1 = new SoundPlayer(@"Assets\Music\강한친구대한육군.wav");
        SoundPlayer MusicWalk = new SoundPlayer(@"Assets\Music\군화소리.wav");
        SoundPlayer MusicLaugh = new SoundPlayer(@"Assets\Music\남자비웃음.wav");
        SoundPlayer MusicVictory = new SoundPlayer(@"Assets\Music\Victory.wav");
        SoundPlayer MusicBattle = new SoundPlayer(@"Assets\Music\Battle.wav");
        SoundPlayer MusicLose = new SoundPlayer(@"Assets\Music\Lose.wav");
        SoundPlayer MusicClap = new SoundPlayer(@"Assets\Music\박수소리.wav");
        SoundPlayer MusicRead = new SoundPlayer(@"Assets\Music\대기음악.wav");
        SoundPlayer MusicAnxiety = new SoundPlayer(@"Assets\Music\불안한소리.wav");
        SoundPlayer MusicFinish = new SoundPlayer(@"Assets\Music\국기경례.wav");
        #endregion

        #region 오브젝트 추가하기
        Player player = null;
        Box[] box = null;
        Wall[] wall = null;
        Goal[] goal = null;
        Line4[] line4 = null;
        Random random = new Random(); // random 객체 생성
        Scene scene = new Scene(); // Scene 객체 생성
        Game game = new();
        PLAYERDIRECTION PlayerDirection = PLAYERDIRECTION.NONE; // 플레이어 방향 ENUM 초기화
        LINE4DIRECTION line4direction = LINE4DIRECTION.NONE; // 병장 방향 ENUM 초기화
        #endregion

        #region 캐릭터 생성하기
        Character ChosenOne = new(); // 선택한 캐릭터

        Character sin = new()
        {
            name = "신병",
            hp = 5,
            mp = 5,
            passive = "존댓말하기",
            passiveeffect = "군대언어가 익숙치 않아 병장의 공격력을 올림(+3)",
            atk = 3,
            attackeffect = "네? 라고 대답함",
            skill1 = "관등성명대기(MP 5)",
            skill1effect = "관등성명을 댐으로써 병장의 흥미가 떨어짐(병장 MP 3~5 감소)",
            dmg = 0
        };

        Character ee = new()
        {
            name = "이병",
            hp = 10,
            mp = 10,
            passive = "얼 타기",
            passiveeffect = "일머리가 없어 병장의 MP를 증가시킴(+5) ",
            atk = 3,
            attackeffect = "잘 못들었습니다?",
            skill1 = "마음의 편지 쓰기(MP 5)",
            skill1effect = "마음의 편지를 씀으로써 병장의 휴가가 잘렸다(병장 HP 3 ~ 5 감소)",
            dmg = 0
        };

        Character il = new() // 일병 객체 생성
        {
            name = "일병",
            hp = 10,
            mp = 10,
            passive = "작업노예",
            passiveeffect = "하루종일 작업만 하다 맷집이 좋아졌다 [매턴 30% 확률로 받는 데미지 반감]",
            atk = 3,
            attackeffect = "잘 못슴다?",
            skill1 = "담배 물리기(MP 5)",
            skill1effect = "담배를 권함으로써 병장이 3턴동안 흡연을 한다 [병장 3턴 Freeze]" ,
            dmg = 0
        };

        Character sang = new() // 상병 객체 생성
        {
            name = "상병",
            hp = 15,
            mp = 10,
            passive = "개기기",
            passiveeffect = "짬 좀 찼다고 선임들 하나씩 먹으려 함 [매턴 20% 확률로 본인 ATK 1 증가]",
            atk = 3,
            attackeffect = "자몽소다?",
            skill1 = "무시하기 (MP 5)",
            skill1effect = "\"집에 갈 양반이 왜 아직도 실세놀이를 하고 계십니까?\" [병장 MP 5~10 감소]" ,
            dmg = 0
        };

        Character Byeong = new() // 병장 객체 생성
        {
            name = "병장",
            hp = 20,
            savehp = 20,
            mp = 10,
            savemp = 10,

            passive = "현타 세게 맞기",
            passiveeffect = "동기들을 먼저 보내고 혼자만 남아 현타를 맞음[매턴 20% 확률로 턴 중지]",

            atk = 5,
            attackeffect = "야 여동생이나 누나 있냐?",

            skill1 = "군가 부르게 하기 (MP 2)",
            skill1effect = "야 군가 한곡 기깔나게 뽑아봐[플레이어 HP 1 ~ 3 감소]",

            skill2 = "기강 잡기 (MP 3)",
            skill2effect = "개빠졌네 진짜?? 군대가 니 집 안방이냐??[플레이어 HP 3 ~ 5 감소]",

            skill3 = "맞선임 호출하기 (MP 5)",
            skill3effect = "야, 니 맞선임 누구냐??[플레이어 HP 5 ~ 10 감소]",
            dmg = 0
        };
        #endregion

        #region 아이템테이블 만들기

        Item[] itemlist =
        {
            new Item { id = 1, name = "꽈배기", weight = 40},
            new Item { id = 2, name = "군장", weight = 30},
            new Item { id = 3, name = "가스조절기", weight = 20},
            new Item { id = 4, name = "포상휴가증", weight = 10},
        };
        #endregion

        #region 시작 Scene

        string StartPath = Path.Combine("Assets", "Scenes", "StartScene.txt");
        string[] StartShow = File.ReadAllLines(StartPath);
        bool renderStart = true;
        MusicStart.PlayLooping(); // 기상나팔 틀기
        while (scene.StartOn) // 게임 시작 scene
        {
            // render
            if (renderStart == true)
            {
                for (int i = 0; i < StartShow.Length; ++i)
                {
                    Console.WriteLine(StartShow[i]);
                }
                renderStart = false;
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter) // 엔터를 누르면
            {
                Console.Clear();
                scene.StartOn = false; // 시작화면 끄기
                scene.QuizOn = true; // 퀴즈모드 켜기
                MusicStart.Stop(); // 기상나팔 끄기
            }
        }
        #endregion

        #region 퀴즈 Scene
        Quiz quiz = new();
        string QuizPath = ""; // 경로 default
        string[] QuizShow = default; // 실제파일 default
        MusicSong1.PlayLooping(); // 군가 틀기
        while (scene.QuizOn) // 퀴즈 scene
        {
            // render
            Console.Clear();

            // 퀴즈 렌더링 부분
            #region 퀴즈 렌더링 부분

            Console.ForegroundColor = ConsoleColor.Blue;
            Function.Render(30, 5, $"현재 점수 = +{quiz.Score}");

            Console.ForegroundColor = ConsoleColor.Red;
            if (quiz.RenderQ1)
            {
                Function.RenderQuiz(1, QuizPath, QuizShow);
            }
            if (quiz.RenderQ2)
            {
                Function.RenderQuiz(2, QuizPath, QuizShow);
            }
            if (quiz.RenderQ3)
            {
                Function.RenderQuiz(3, QuizPath, QuizShow);
            }
            if (quiz.RenderQ4)
            {
                Function.RenderQuiz(4, QuizPath, QuizShow);
            }
            if (quiz.RenderQ5)
            {
                Function.RenderQuiz(5, QuizPath, QuizShow);
            }
            if (quiz.RenderQ6)
            {
                Function.RenderQuiz(6, QuizPath, QuizShow);
            }
            if (quiz.RenderQ7)
            {
                Function.RenderQuiz(7, QuizPath, QuizShow);
            }
            if (quiz.RenderQ8)
            {
                Function.RenderQuiz(8, QuizPath, QuizShow);
            }
            if (quiz.RenderQ9)
            {
                Function.RenderQuiz(9, QuizPath, QuizShow);
            }
            if (quiz.RenderQ10)
            {
                Function.RenderQuiz(10, QuizPath, QuizShow);
            }
            #endregion

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            #region 답 처리 부분
            if (quiz.RenderQ1)
            {
                if (key == ConsoleKey.C)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ1 = false;
                    quiz.RenderQ2 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ1 = false;
                    quiz.RenderQ2 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ2)
            {
                if (key == ConsoleKey.C)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ2 = false;
                    quiz.RenderQ3 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ2 = false;
                    quiz.RenderQ3 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ3)
            {
                if (key == ConsoleKey.A)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ3 = false;
                    quiz.RenderQ4 = true;
                }
                else if (key == ConsoleKey.C || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ3 = false;
                    quiz.RenderQ4 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ4)
            {
                if (key == ConsoleKey.C)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ4 = false;
                    quiz.RenderQ5 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ4 = false;
                    quiz.RenderQ5 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ5)
            {
                if (key == ConsoleKey.A)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ5 = false;
                    quiz.RenderQ6 = true;
                }
                else if (key == ConsoleKey.C || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ5 = false;
                    quiz.RenderQ6 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ6)
            {
                if (key == ConsoleKey.D)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ6 = false;
                    quiz.RenderQ7 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.C)
                {
                    key = default;
                    quiz.RenderQ6 = false;
                    quiz.RenderQ7 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ7)
            {
                if (key == ConsoleKey.B)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ7 = false;
                    quiz.RenderQ8 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.C || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ7 = false;
                    quiz.RenderQ8 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ8)
            {
                if (key == ConsoleKey.B)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ8 = false;
                    quiz.RenderQ9 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.C || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ8 = false;
                    quiz.RenderQ9 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ9)
            {
                if (key == ConsoleKey.C)
                {
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ9 = false;
                    quiz.RenderQ10 = true;
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.D)
                {
                    key = default;
                    quiz.RenderQ9 = false;
                    quiz.RenderQ10 = true;
                }
                else
                {
                    // 암것도하지 않음
                }
            }

            if (quiz.RenderQ10)
            {
                if (key == ConsoleKey.D)
                {
                    Console.Clear();
                    key = default;
                    ++quiz.Score;
                    quiz.RenderQ10 = false;
                    scene.QuizOn = false;
                    scene.MarkOn = true;
                    MusicSong1.Stop(); // 군가끄기
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.C)
                {
                    Console.Clear();
                    key = default;
                    quiz.RenderQ10 = false;
                    scene.QuizOn = false;
                    scene.MarkOn = true;
                    MusicSong1.Stop(); // 군가끄기
                }
                else
                {
                    // 암것도하지 않음
                }
            }
            #endregion

            #region 데이터 카피하는 부분
            if (0 <= quiz.Score && quiz.Score <= 3)
            {
                Function.CopyData(ChosenOne, sin);
            }
            if (4 <= quiz.Score && quiz.Score <= 6)
            {
                Function.CopyData(ChosenOne, ee);
            }
            if (7 <= quiz.Score && quiz.Score <= 8)
            {
                Function.CopyData(ChosenOne, il);
            }
            if (9 <= quiz.Score && quiz.Score <= 10)
            {
                Function.CopyData(ChosenOne, sang);
            }
            #endregion

        }
        #endregion

        #region 채점 Scene
        MusicClap.PlayLooping(); // 박수소리 틀기
        bool RenderMark = true;
        string MarkPath = Path.Combine("Assets", "Scenes", "MarkScene.txt"); ;
        string[] MarkShow = File.ReadAllLines(MarkPath);
        while (scene.MarkOn) // 채점 scene
        {
            // Render
            if (RenderMark == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Function.RenderText(MarkPath, MarkShow); // 텍스트 파일 불러오기

                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(33, 3, $"+{quiz.Score}"); // 점수 그리기
                Function.Render(45, 18, ChosenOne.name); // 선택된 캐릭터 이름 그리기
                RenderMark = false;
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.MarkOn = false;
                scene.ExplanationOn = true;
                MusicClap.Stop(); // 박수소리 끄기
            }
        }
        #endregion

        #region 캐릭터설명 Scene
        MusicRead.PlayLooping(); // 대기음악 키기
        bool RenderExplanation = true;
        string ExplanationPath = "";
        string[] ExplanationShow = default;
        while (scene.ExplanationOn) // 캐릭터설명 scene
        {
            // render
            if (RenderExplanation == true)
            {
                Function.RenderExplanation(ChosenOne, ExplanationPath, ExplanationShow);
                RenderExplanation = false;
            }
            

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.ExplanationOn = false;
                scene.PresokobanOn = true;
                
            }

        }
        #endregion

        #region 소코반설명 Scene

        bool RenderPre = true;
        string PrePath = Path.Combine("Assets", "Scenes", "PresokobanScene.txt"); ;
        string[] PreShow = File.ReadAllLines(PrePath);
        while (scene.PresokobanOn) // Presokoban scene
        {
            // render
            if (RenderPre == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Function.RenderText(PrePath, PreShow);
                RenderPre = false;
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.PresokobanOn = false;
                scene.ShoppingOn = true;
                
                
            }
        }

        #endregion

        #region 쇼핑 Scene
        bool RenderShopping = true;
        Item outcome = null;
        int money = 500;
        string ShoppingPath = Path.Combine("Assets", "Scenes", "ShoppingScene.txt");
        string[] ShoppingShow = File.ReadAllLines(ShoppingPath);
        while (scene.ShoppingOn) // 상점이용 scene
        {
            // render
            if (RenderShopping == true)
            {
                Function.RenderText(ShoppingPath, ShoppingShow);
                RenderShopping = false;
            }

            Console.ForegroundColor = ConsoleColor.Magenta; // 소지금 그려주기
            Function.Render(43,30,$"{money}");

            if (outcome != null) // 뽑은 아이템 그려주기
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Function.Render(97, 6, "               ");
                Function.Render(97, 6, outcome.name);
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (0 < money && key == ConsoleKey.Spacebar) // 스페이스바 누르면 갓챠
            {
                int randomnumber = random.Next(1,101); // 1부터 100까지 중 난수 하나를 뽑음
                outcome = Gacha(randomnumber);
                money -= 100;
            }

            if (key == ConsoleKey.Enter) // 엔터 누르면 소코반 씬으로
            {
                Console.Clear();
                key = default;
                scene.ShoppingOn = false;
                scene.SokobanOn = true;
                MusicRead.Stop(); // 대기음악 끄기
            }

        }
        #endregion

        #region 아이템효과 적용하기
        if (outcome != null)// 소지한 아이템에 따른 플레이어 status 향상
        {
            switch (outcome.name)
            {
                case "꽈배기":
                    ChosenOne.hp += 2;
                    break;
                case "군장":
                    ChosenOne.hp += 5;
                    break;
                case "가스조절기":
                    ChosenOne.atk += 3;
                    break;
                case "포상휴가증":
                    ChosenOne.hp += 10;
                    ChosenOne.atk += 5;
                    break;
            }
        }
        #endregion

        LOOP_SOKOBAN:

        #region 소코반 Scene
        
        string[] data1 = Game.LoadStage(1); // 스테이지 1 로드하기
        string[] data2 = Game.LoadStage(2); // 스테이지 2 로드하기
        string[] data3 = Game.LoadStage(3); // 스테이지 3 로드하기

        LOOP_CHANGE:
        Console.ForegroundColor = ConsoleColor.Gray;
        if (game.Stage1Ongoing == true)
        {
            Game.RenderStage(data1); // 스테이지 1 렌더하기
            Game.ParseStage(data1, out player, out wall, out goal, out box, out line4); // 스테이지 파싱하기
        }
        if (game.Stage2Ongoing == true)
        {
            Game.RenderStage(data2); // 스테이지 2 렌더하기
            Game.ParseStage(data2, out player, out wall, out goal, out box, out line4); // 스테이지 파싱하기
        }
        if (game.Stage3Ongoing == true)
        {
            Game.RenderStage(data3); // 스테이지 3 렌더하기
            Game.ParseStage(data3, out player, out wall, out goal, out box, out line4); // 스테이지 파싱하기
        }
        
        int BoxOnGoalCount = 0;
        bool[] BoxOnGoal = new bool[box.Length]; // 골위에 올라간 박스 확인용
        int pushed = 0; // 민 박스 확인용
        MusicWalk.PlayLooping(); // 걷는 소리 켜기
        while (scene.SokobanOn) // 소코반 scene
        {
            // render
            Console.ForegroundColor = ConsoleColor.White;

            RenderPlayerHP();
            RenderPlayerMP();
            RenderSide();
            
            Console.ForegroundColor = ConsoleColor.Blue; // 골을 렌더
            for (int i = 0; i < goal.Length; ++i)
            {
                Function.Render(goal[i].x, goal[i].y, "*"); 
            }

            Console.ForegroundColor = ConsoleColor.White;// 플레이어 render
            Function.Render(player.x, player.y, "P");
            RenderPrePlayer(); // 플레이어 전 좌표 render
            
            Console.ForegroundColor = ConsoleColor.Yellow;// 벽을 렌더
            for (int i = 0; i < wall.Length; ++i)
            {
                Function.Render(wall[i].x, wall[i].y, "W"); 
            }

            RenderLine4(); // 병장 렌더
            
            
            for (int i = 0; i < box.Length; ++i)// box를 렌더
            {
                if (BoxOnGoal[i] == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Function.Render(box[i].x, box[i].y, "B");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Function.Render(box[i].x, box[i].y, "X");
                }
                
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update

            player.prex = player.x;
            player.prey = player.y;


            // 플레이어 이동 구현
            PlayerMove(key);

            // 플레이어 벽에 막히는 거 구현
            PlayerVsWall();

            // 플레이어 박스 미는거 구현
            PushBox();

            // 박스 벽에 막히는거 구현
            BoxVsWall();

            // 박스끼리 막히는 거 구현
            BoxVsBox();

            // 병장 무작위 움직이는거 구현
            Line4Move();

            // 병장 벽에 막히는 거 구현 
            Line4VsWall();

            // 병장 박스에 막히는 거 구현
            Line4VsBox();

            // 플레이어가 병장이랑 조우하면 배틀모드돌입
            PlayerMeetLine4();

            // 골에 박스 몇개들어갔는지 확인하는거 구현
            BoxOnGoalCount = CountBoxOnGoal(box, goal, ref BoxOnGoal);

            if (game.Stage1Ongoing == true && BoxOnGoalCount == goal.Length) // 1번 스테이지에 모든 박스가 골에 들어갔다면
            {
                Console.Clear();
                key = default;
                game.Stage1Ongoing = false;
                game.Stage2Ongoing = true; // 2번 stage로 간다
                goto LOOP_CHANGE;
            }

            if (game.Stage2Ongoing == true && BoxOnGoalCount == goal.Length) // 2번 스테이지에 모든 박스가 골에 들어갔다면
            {
                Console.Clear();
                key = default;
                game.Stage2Ongoing = false;
                game.Stage3Ongoing = true; // 3번 stage로 간다
                goto LOOP_CHANGE;
            }

            if (game.Stage3Ongoing == true && BoxOnGoalCount == goal.Length) // 3번 스테이지에 모든 박스가 골에 들어갔다면
            {
                Console.Clear();
                key = default;
                game.Stage3Ongoing = false;
                scene.SokobanOn = false;
                scene.ClearOn = true; // 군의관에게로 간다
                MusicWalk.Stop(); // 걷는 소리 끄기
            }

        }
        #endregion

        #region 병장 조우 Scene
        MusicLaugh.PlayLooping(); // 웃는 소리 켜기
        bool RenderEncounter = true;
        string EncounterPath = Path.Combine("Assets", "Scenes", "EncounterScene.txt");
        string[] EncounterShow = File.ReadAllLines(EncounterPath);
        while (scene.EncounterOn) // 병장 조우 scene
        {
            // render
            if (RenderEncounter == true)
            {
                Function.RenderText(EncounterPath, EncounterShow);
                RenderEncounter = false;
            }
            
            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter) // 엔터 누르면
            {
                scene.EncounterOn = false; // 조우 scene 끄고
                scene.BattleOn = true; // 배틀 모드 돌입
                MusicLaugh.Stop(); // 웃는소리 끄기
            }
        }
        #endregion

        #region 배틀 Scene
        MusicBattle.PlayLooping(); // 배틀소리 켜기
        bool Victory = false; // 승리 패배에 따라 변환할 것
        string BattlePath = Path.Combine("Assets", "Scenes", "BattleScene.txt");
        string[] BattleShow = File.ReadAllLines(BattlePath);
        bool ByeongTurn = true; // 병장 턴 활성기
        int ByeongPending = 0; // 병장 턴 조절기
        int PassiveNumber = 0; // 패시브 관련 정수
        
        #region 텍스트 출력용 정리
        string ByeongWhat = "[병장은 무엇을 할까?]";
        string ByeongPassive = $"Passive({Byeong.passive}) → {Byeong.passiveeffect}"; ;
        string ByeongAttack = $"Attack({Byeong.attackeffect}) → [플레이어 HP {Byeong.atk} 감소]";
        string ByeongSkill1 = $"Skill1({Byeong.skill1}) → {Byeong.skill1effect}";
        string ByeongSkill2 = $"Skill2({Byeong.skill2}) → {Byeong.skill2effect}";
        string ByeongSkill3 = $"Skill3({Byeong.skill3}) → {Byeong.skill3effect}";


        string PlayerWhat = $"[{ChosenOne.name}은 무엇을 할까?]";
        string PlayerPassive = $"Passive({ChosenOne.passive}) → {ChosenOne.passiveeffect}";
        string PlayerAttack = $"A: Attack({ChosenOne.attackeffect}) → [병장 HP {ChosenOne.atk} 감소]";
        string PlayerSkill = $"S: Skill({ChosenOne.skill1}) → {ChosenOne.skill1effect}";
        #endregion

        Console.ForegroundColor = ConsoleColor.Blue;
        while (scene.BattleOn) // 병장 배틀 scene
        {
            // render
            Console.Clear(); // 여긴 클리어해줘야함
            Function.RenderText(BattlePath, BattleShow);
            RenderText(); // 텍스트를 그려준다

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            ByeongPending = Math.Max(ByeongPending - 1, 0); // 병장 턴 delay 0으로 계속 만들기

            // 병장 턴
            if (0 == ByeongPending)
            {
                if (ByeongTurn == true)
                {
                    int Byeongplay = random.Next(1, 5); // 1부터 4중 하나를 뽑음

                    switch (Byeongplay)
                    {
                        case 1: // 1번이 나오면 그냥 공격
                            Byeong.dmg = Byeong.atk;
                            ChosenOne.hp -= Byeong.dmg;
                            break;
                        case 2: // 2번이 나오면 군가 부르게 하기
                            if (2 <= Byeong.mp)
                            {
                                Byeong.dmg = random.Next(1, 4);
                                ChosenOne.hp -= Byeong.dmg;
                            }
                            break;
                        case 3: // 3번이 나오면 기강 잡기
                            if (3 <= Byeong.mp)
                            {
                                Byeong.dmg = random.Next(3, 6);
                                ChosenOne.hp -= Byeong.dmg;
                            }
                            break;
                        case 4: // 4번이 나오면 맞선임 호출하기
                            if (5 <= Byeong.mp)
                            {
                                Byeong.dmg = random.Next(5, 11);
                                ChosenOne.hp -= Byeong.dmg;
                            }
                            ; break;
                    }
                }
            }



            // 플레이어 턴

            switch (ChosenOne.passive)
            {
                case "존댓말 하기":
                    Byeong.atk = Byeong.atk + 5; // 병장 atk +5

                    break;
                case "얼 타기":
                    Byeong.atk = Byeong.atk + 3; // 병장 atk +3

                    break;
                case "작업노예": // 25% 확률로 데미지 반감
                    PassiveNumber = random.Next(1, 5); // 1~4 까지의 난수를 뽑는다
                    if (PassiveNumber == 1)
                    {
                        Byeong.dmg = Byeong.dmg / 2; // 병장 atk 반감시키기
                    }
                    break;
                case "개기기": // 20%의 확률로 자기 공격력 +1
                    PassiveNumber = random.Next(1, 6); // 1~5 까지의 난수를 뽑는다
                    if (PassiveNumber == 1)
                    {
                        ChosenOne.atk += 1;
                    }
                    break;
            }

            switch (ChosenOne.name)
            {
                case "신병": // 만약 선택된 캐릭터의 id가 신병인데
                    switch (key) // 입력한 키가 
                    {
                        case ConsoleKey.A: // A면
                            ChosenOne.dmg = ChosenOne.atk;
                            Byeong.hp -= ChosenOne.dmg; // 병장 HP를 atk 만큼 감소시키기
                            break;
                        case ConsoleKey.S: // S면
                            if (5 <= ChosenOne.mp)
                            {
                                Byeong.mp -= random.Next(3, 6); // 병장 MP를 3~5 사이의 난수만큼 빼버리기
                                ChosenOne.mp -= 5;
                            }
                            break;
                    }
                    break;
                case "이병":
                    switch (key)
                    {
                        case ConsoleKey.A:
                            ChosenOne.dmg = ChosenOne.atk;
                            Byeong.hp -= ChosenOne.dmg; // 병장 HP atk 만큼 감소시키기
                            break;
                        case ConsoleKey.S:
                            if (5 <= ChosenOne.mp)
                            {
                                Byeong.hp -= random.Next(5, 9); // 병장 hp 5~8만큼 빼버리기
                                ChosenOne.mp -= 5;
                            }
                            break;
                    }
                    break;
                case "일병":
                    switch (key)
                    {
                        case ConsoleKey.A:
                            ChosenOne.dmg = ChosenOne.atk;
                            Byeong.hp -= ChosenOne.dmg; // 병장 HP atk 만큼 감소시키기
                            break;
                        case ConsoleKey.S:
                            if (5 <= ChosenOne.mp)
                            {
                                ByeongPending = 3; // 병장 턴 pending 3으로 만들기
                                ChosenOne.mp -= 5;
                            }
                            break;
                    }
                    break;
                case "상병":
                    switch (key)
                    {
                        case ConsoleKey.A:
                            ChosenOne.dmg = ChosenOne.atk;
                            Byeong.hp -= ChosenOne.dmg; // 병장 HP atk 만큼 감소시키기
                            break;
                        case ConsoleKey.S:
                            if (5 <= ChosenOne.mp)
                            {
                                Byeong.mp -= random.Next(5, 11); // 병장 mp 5~10만큼 빼버리기
                                ChosenOne.mp -= 5;
                            }
                            break;
                    }
                    break;
            }


            



            if (ChosenOne.hp <= 0)
            {
                Console.Clear();
                key = default;
                Victory = false;
                scene.BattleOn = false;
                scene.ResultOn = true;
                ResetByeong(); // 병장을 원상태로 돌려놓는다
            }
            else if (Byeong.hp <= 0)
            {
                Console.Clear();
                key = default;
                Victory = true;
                scene.BattleOn = false;
                scene.ResultOn = true;
                ResetByeong(); // 병장을 원상태로 돌려놓는다
            }

        }
        #endregion

        #region 결과 Scene
        string VictoryPath = Path.Combine("Assets", "Scenes", "VictoryScene.txt");
        string[] VictoryShow = File.ReadAllLines(VictoryPath);
        string LosePath = Path.Combine("Assets", "Scenes", "LoseScene.txt");
        string[] LoseShow = File.ReadAllLines(LosePath);

        Console.ForegroundColor = ConsoleColor.Blue;
        while (scene.ResultOn) // 배틀 결과 scene 승리 or 패배
        {
            // render
            if (Victory == true)
            {
                MusicVictory.PlayLooping();
                Function.RenderText(VictoryPath, VictoryShow);
            }
            else
            {
                MusicLose.PlayLooping();
                Function.RenderText(LosePath, LoseShow);
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (Victory == true && key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.ResultOn = false;
                scene.SokobanOn = true;
                goto LOOP_SOKOBAN;
            }
            else if (Victory == false && key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.ResultOn = false;
            }

        }
        #endregion

        #region 소코반 클리어 부분
        MusicAnxiety.PlayLooping(); // 불안한 음악 켜기
        bool RenderClear = true;
        string ClearPath = Path.Combine("Assets", "Scenes", "ClearScene.txt");
        string[] ClearShow = File.ReadAllLines(ClearPath);
        while (scene.ClearOn) // 소코반 완료 scene
        {
            // render
            if (RenderClear == true)
            {
                Function.RenderText(ClearPath, ClearShow);
                RenderClear = false;
            }
            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                MusicAnxiety.Stop(); // 불안한 음악 끄기
                Console.Clear();
                key = default;
                scene.ClearOn = false;
                scene.DisabledOn = true;
            }
        }
        #endregion

        #region 의가사전역 scene
        MusicFinish.PlayLooping(); // 끝 곡 켜기
        bool RenderDisable = true;
        string DisablePath = Path.Combine("Assets", "Scenes", "DisabledScene.txt");
        string[] DisableShow = File.ReadAllLines(DisablePath);
        Console.ForegroundColor = ConsoleColor.White;
        while (scene.DisabledOn) // 의가사전역 scene
        {
            // render
            if (RenderDisable == true)
            {
                Function.RenderText(DisablePath, DisableShow);
            }

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                MusicFinish.Stop(); // 끝 곡 끄기
                Console.Clear();
                scene.DisabledOn = false ;
                break;
            }

        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 함수모음집(static 써도 안되는것들 ㅠ 어케 쓰는지 모르게쒀)

        // 플레이어 이동함수
        void PlayerMove(ConsoleKey key)
        {
            if (key == ConsoleKey.RightArrow)
            {
                PlayerMoveRight();
            }
            if (key == ConsoleKey.LeftArrow)
            {
                PlayerMoveLeft();
            }
            if (key == ConsoleKey.UpArrow)
            {
                PlayerMoveUp();
            }
            if (key == ConsoleKey.DownArrow)
            {
                PlayerMoveDown();
            }
        }

        


        // 플레이어 벽에 막히는 함수
        void PlayerVsWall()
        {
            for (int i = 0; i < wall.Length; ++i)
            {
                if (Collision(player.x, player.y, wall[i].x, wall[i].y))
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            BlocksRightmove(out player.x, ref wall[i].x);
                            break;
                        case PLAYERDIRECTION.LEFT:
                            BlocksLeftmove(out player.x, ref wall[i].x);
                            break;
                        case PLAYERDIRECTION.UP:
                            BlocksUpmove(out player.y, ref wall[i].y);
                            break;
                        case PLAYERDIRECTION.DOWN:
                            BlocksDownmove(out player.y, ref wall[i].y);
                            break;

                    }
                }
            }
        }


        // 박스 미는 함수
        void PushBox()
        {
            for (int i = 0; i < box.Length; ++i)
            {
                if (Collision(player.x, player.y, box[i].x, box[i].y))
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            MoveRight(ref box[i].x);
                            BlocksRightmove(out player.x, ref box[i].x);
                            break;
                        case PLAYERDIRECTION.LEFT:
                            MoveLeft(ref box[i].x);
                            BlocksLeftmove(out player.x, ref box[i].x);
                            break;
                        case PLAYERDIRECTION.UP:
                            MoveUp(ref box[i].y);
                            BlocksUpmove(out player.y, ref box[i].y);
                            break;
                        case PLAYERDIRECTION.DOWN:
                            MoveDown(ref box[i].y);
                            BlocksDownmove(out player.y, ref box[i].y);
                            break;
                    }
                    pushed = i;
                }
            }
        }


        // 박스가 벽에 막히는 함수
        void BoxVsWall()
        {
            for (int i = 0; i < box.Length; ++i)
            {
                for (int k = 0; k < wall.Length; ++k)
                {
                    if (Collision(box[i].x, box[i].y, wall[k].x, wall[k].y))
                    {
                        switch (PlayerDirection)
                        {
                            case PLAYERDIRECTION.RIGHT:
                                BlocksRightmove(out box[i].x, ref wall[k].x);
                                BlocksRightmove(out player.x, ref box[i].x);
                                break;
                            case PLAYERDIRECTION.LEFT:
                                BlocksLeftmove(out box[i].x, ref wall[k].x);
                                BlocksLeftmove(out player.x, ref box[i].x);
                                break;
                            case PLAYERDIRECTION.UP:
                                BlocksUpmove(out box[i].y, ref wall[k].y);
                                BlocksUpmove(out player.y, ref box[i].y);
                                break;
                            case PLAYERDIRECTION.DOWN:
                                BlocksDownmove(out box[i].y, ref wall[k].y);
                                BlocksDownmove(out player.y, ref box[i].y);
                                break;
                        }
                    }
                }
            }
        }


        // 박스끼리 부딪히면 막히는 함수
        void BoxVsBox()
        {
            for (int crashed = 0; crashed < box.Length; ++crashed)
            {
                if (crashed == pushed)
                {
                    continue;
                }

                if (Collision(box[pushed].x, box[pushed].y, box[crashed].x, box[crashed].y))
                {
                    switch (PlayerDirection)
                    {
                        case PLAYERDIRECTION.RIGHT:
                            BlocksRightmove(out box[pushed].x, ref box[crashed].x);
                            BlocksRightmove(out player.x, ref box[pushed].x);
                            break;
                        case PLAYERDIRECTION.LEFT:
                            BlocksLeftmove(out box[pushed].x, ref box[crashed].x);
                            BlocksLeftmove(out player.x, ref box[pushed].x);
                            break;
                        case PLAYERDIRECTION.UP:
                            BlocksUpmove(out box[pushed].y, ref box[crashed].y);
                            BlocksUpmove(out player.y, ref box[pushed].y);
                            break;
                        case PLAYERDIRECTION.DOWN:
                            BlocksDownmove(out box[pushed].y, ref box[crashed].y);
                            BlocksDownmove(out player.y, ref box[pushed].y);
                            break;
                    }
                }
            }
        }

        

        // 골위 박스갯수 새주는 함수
        int CountBoxOnGoal(Box[] box, Goal[] goal, ref bool[] boxongoal)
        {

            int outcome = 0;
            for (int i = 0; i < box.Length; ++i)
            {
                boxongoal[i] = false;
                for (int k = 0; k < goal.Length; ++k)
                {
                    if (Collision(box[i].x, box[i].y, goal[k].x, goal[k].y))
                    {
                        ++outcome;
                        boxongoal[i] = true;
                        break;
                    }
                }
            }

            return outcome;
        }

        // 병장 무작위 움직임 함수
        void Line4Move()
        {
            for (int i = 0; i < line4.Length; ++i)
            {
                line4[i].prex = line4[i].x; // 이전좌표 저장
                line4[i].prey = line4[i].y;

                int ranmove = random.Next(0, 5); // 0~4 까지 난수를 방향으로 전환

                switch (ranmove)
                {
                    case 0: // 나띵
                        line4[i].Line4Direction = (int)LINE4DIRECTION.NONE;  // 노방향 // 0
                        break;
                    case 1: // 1 이면 오른쪽 방향
                        MoveRight(ref line4[i].x);
                        line4[i].Line4Direction = (int)LINE4DIRECTION.RIGHT; // 오른쪽 방향(1)
                        break;
                    case 2: // 2 면 왼쪽 방향 
                        MoveLeft(ref line4[i].x);
                        line4[i].Line4Direction = (int)LINE4DIRECTION.LEFT; // 왼쪽 방향(2)
                        //MoveLeft(ref line4[i].x);
                        //Line4Direction = LINE4DIRECTION.LEFT;
                        break;
                    case 3: // 위쪽 방향
                        MoveUp(ref line4[i].y);
                        line4[i].Line4Direction = (int)LINE4DIRECTION.UP; // 위쪽 방향(3)
                        //MoveUp(ref line4[i].y);
                        //Line4Direction = LINE4DIRECTION.UP;
                        break;
                    case 4: // 아래쪽 방향
                        MoveDown(ref line4[i].y);
                        line4[i].Line4Direction = (int)LINE4DIRECTION.DOWN; // 아래쪽 방향(4)
                        //MoveDown(ref line4[i].y);
                        //Line4Direction = LINE4DIRECTION.DOWN;
                        break;
                }
            }
        }


        // 병장이 벽에 막히는 함수
        void Line4VsWall()
        {
            for (int k = 0; k < line4.Length; ++k)
            {
                for (int i = 0; i < wall.Length; ++i)
                {
                    if (Collision(line4[k].x, line4[k].y, wall[i].x, wall[i].y))
                    {
                        switch (line4[k].Line4Direction)
                        {
                            case (int)LINE4DIRECTION.RIGHT:
                                BlocksRightmove(out line4[k].x, ref wall[i].x);
                                break;
                            case (int)LINE4DIRECTION.LEFT:
                                BlocksLeftmove(out line4[k].x, ref wall[i].x);
                                break;
                            case (int)LINE4DIRECTION.UP:
                                BlocksUpmove(out line4[k].y, ref wall[i].y);
                                break;
                            case (int)LINE4DIRECTION.DOWN:
                                BlocksDownmove(out line4[k].y, ref wall[i].y);
                                break;

                        }
                    }
                }
            }

        }


        // 병장이 박스에 막히는 함수
        void Line4VsBox()
        {
            for (int k = 0; k < line4.Length; ++k)
            {
                for (int i = 0; i < box.Length; ++i)
                {
                    if (Collision(line4[k].x, line4[k].y, box[i].x, box[i].y))
                    {
                        switch (line4[k].Line4Direction)
                        {
                            case (int)LINE4DIRECTION.RIGHT:
                                BlocksRightmove(out line4[k].x, ref box[i].x);
                                break;
                            case (int)LINE4DIRECTION.LEFT:
                                BlocksLeftmove(out line4[k].x, ref box[i].x);
                                break;
                            case (int)LINE4DIRECTION.UP:
                                BlocksUpmove(out line4[k].y, ref box[i].y);
                                break;
                            case (int)LINE4DIRECTION.DOWN:
                                BlocksDownmove(out line4[k].y, ref box[i].y);
                                break;

                        }
                    }
                }
            }
        }


        // 플레이어가 병장이랑 만나면 배틀모드에 돌입하는 함수
        void PlayerMeetLine4()
        {
            ConsoleKey key = default;
            for (int i = 0; i < line4.Length; ++i)
            {
                if (player.x == line4[i].x && player.y == line4[i].y)
                {
                    Console.Clear();
                    key = default;
                    scene.SokobanOn = false;
                    scene.EncounterOn = true;
                    MusicWalk.Stop(); // 걷는소리 끄기
                    break;
                }
            }
        }

        // 가챠 함수
        Item Gacha(int randomnumber)
        {
            Item output = null;
            int addedweight = 0;

            for (int i = 0; i < itemlist.Length; ++i)
            {
                addedweight += itemlist[i].weight;
                if (randomnumber <= addedweight)
                {
                    output = itemlist[i];
                    break;
                }
            }
            return output;
        }

        // HP를 그려주는 함수
        void RenderPlayerHP()
        {
            Function.Render(63, 3, $"HP({ChosenOne.hp}) ");
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < ChosenOne.hp; ++i)
            {
                Function.Render(70 + (i * 2), 3, "■");
            }
        }

        // MP를 그려주는 함수
        void RenderPlayerMP()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Function.Render(63, 5, $"MP({ChosenOne.mp}) ");
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < ChosenOne.mp; ++i)
            {
                Function.Render(70 + (i * 2), 5, "■");
            }
        }

        void RenderPrePlayer()
        {
            if (player.x == player.prex && player.y == player.prey)
            {
                // 암것도안함
            }
            else if (player.x != player.prex || player.y != player.prey) // 현좌표와 전좌표가 겹치지 않을때만 지워줌
            {
                Function.Render(player.prex, player.prey, " ");
            }
        }

        // 병장을 렌더
        void RenderLine4()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < line4.Length; ++i)
            {
                Function.Render(line4[i].x, line4[i].y, "4");

                if (line4[i].x == line4[i].prex && line4[i].y == line4[i].prey)
                {
                    // 암것도안함
                }
                else // 현좌표와 전좌표가 겹치지 않을때만 지워줌
                {
                    Function.Render(line4[i].prex, line4[i].prey, " ");
                }
            }
        }

        // 텍스트를 그리는 함수
        void RenderText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Function.Render(55, 18, ByeongWhat);// 병장은 무엇을 할까?

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
        }


        // 옆에 오브젝트 기호 그리기
        void RenderSide()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Function.Render(65, 9, "B = 박스");
            Console.ForegroundColor = ConsoleColor.Red;
            Function.Render(65, 11, "4 = 병장");
            Console.ForegroundColor = ConsoleColor.Blue;
            Function.Render(65, 13, "* = 골");
        }


        // 병장을 원상태로 돌려놓는 함수
        void ResetByeong()
        {
            Byeong.hp = 20;
            Byeong.mp = 10;
            Byeong.atk = 5;
        }

        // 부딪힐때 true or false 함수
        bool Collision(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // 이동제한 함수
        void BlocksRightmove(out int Former, ref int Latter) => Former = Latter - 1;
        void BlocksLeftmove(out int Former, ref int Latter) => Former = Latter + 1;
        void BlocksUpmove(out int Former, ref int Latter) => Former = Latter + 1;
        void BlocksDownmove(out int Former, ref int Latter) => Former = Latter - 1;


        // 이동함수
        void MoveRight(ref int x) => x = Math.Clamp(x + 1, game.minx, game.maxx);
        void MoveLeft(ref int x) => x = Math.Clamp(x - 1, game.minx, game.maxx);
        void MoveUp(ref int y) => y = Math.Clamp(y - 1, game.miny, game.maxy);
        void MoveDown(ref int y) => y = Math.Clamp(y + 1, game.miny, game.maxy);



        // 플레이어 방향키에 따른 이동함수
        void PlayerMoveRight()
        {
            player.x = Math.Clamp(player.x + 1, game.minx, game.maxx);
            PlayerDirection = PLAYERDIRECTION.RIGHT;
        }

        void PlayerMoveLeft()
        {
            player.x = Math.Clamp(player.x - 1, game.minx, game.maxx);
            PlayerDirection = PLAYERDIRECTION.LEFT;
        }

        void PlayerMoveUp()
        {
            player.y = Math.Clamp(player.y - 1, game.miny, game.maxy);
            PlayerDirection = PLAYERDIRECTION.UP;
        }

        void PlayerMoveDown()
        {
            player.y = Math.Clamp(player.y + 1, game.miny, game.maxy);
            PlayerDirection = PLAYERDIRECTION.DOWN;
        }

        #endregion
    }

    

}