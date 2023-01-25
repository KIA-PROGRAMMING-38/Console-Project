using System;
using System.Media;
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
        #endregion

        #region 오브젝트 추가하기
        Player player;
        Box[] box;
        Wall[] wall;
        Goal[] goal;
        Line4[] line4;
        PX px;

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
            skill1effect = "관등성명을 댐으로써 병장의 흥미가 떨어짐(병장 MP 3~5 감소)"
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
            skill1effect = "마음의 편지를 씀으로써 병장의 휴가가 잘렸다(병장 HP 3 ~ 5 감소)"
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
            skill1effect = "담배를 권함으로써 병장이 3턴동안 흡연을 한다 [병장 3턴 Freeze]"
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
            skill1effect = "\"집에 갈 양반이 왜 아직도 실세놀이를 하고 계십니까?\" [병장 MP 5~10 감소]"
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
            saveatk = 5,
            attackeffect = "야 여동생이나 누나 있냐?",

            skill1 = "군가 부르게 하기 (MP 2)",
            skill1effect = "야 군가 한곡 기깔나게 뽑아봐[플레이어 HP 1 ~ 3 감소]",

            skill2 = "기강 잡기 (MP 3)",
            skill2effect = "개빠졌네 진짜?? 군대가 니 집 안방이냐??[플레이어 HP 3 ~ 5 감소]",

            skill3 = "맞선임 호출하기 (MP 5)",
            skill3effect = "야, 니 맞선임 누구냐??[플레이어 HP 5 ~ 10 감소]"
        };
        #endregion

        Random random = new Random(); // random 객체 생성
        Scene scene = new Scene(); // Scene 객체 생성
        Game game = new();
        PLAYERDIRECTION PlayerDirection = PLAYERDIRECTION.NONE; // 플레이어 방향 ENUM 초기화
        LINE4DIRECTION Line4Direction= LINE4DIRECTION.NONE; // 병장 방향 ENUM 초기화

        #region 시작화면

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

        #region 퀴즈부분
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
                }
                else if (key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.C)
                {
                    Console.Clear();
                    key = default;
                    quiz.RenderQ10 = false;
                    scene.QuizOn = false;
                    scene.MarkOn = true;
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

        #region 채점 부분

        string MarkPath = Path.Combine("Assets", "Scenes", "MarkScene.txt"); ;
        string[] MarkShow = File.ReadAllLines(MarkPath);
        while (scene.MarkOn) // 채점 scene
        {
            // Render
            Console.ForegroundColor = ConsoleColor.White;
            Function.RenderText(MarkPath, MarkShow); // 텍스트 파일 불러오기

            Console.ForegroundColor = ConsoleColor.Green;
            Function.Render(33, 3, $"+{quiz.Score}"); // 점수 그리기
            Function.Render(45, 18, ChosenOne.name); // 선택된 캐릭터 이름 그리기

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update

            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.MarkOn = false;
                scene.ExplanationOn = true;
            }
            else
            {
                // 암것도 안함
            }
        }
        #endregion

        #region 캐릭터 설명부분

        string ExplanationPath = "";
        string[] ExplanationShow = default;
        while (scene.ExplanationOn) // 캐릭터설명 scene
        {
            // render
            Function.RenderExplanation(ChosenOne, ExplanationPath, ExplanationShow);

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.ExplanationOn = false;
                scene.PresokobanOn = true;
                MusicSong1.Stop(); // 군가끄기
            }

        }
        #endregion

        #region 소코반 전 미션설명 부분

        string PrePath = Path.Combine("Assets", "Scenes", "PresokobanScene.txt"); ;
        string[] PreShow = File.ReadAllLines(PrePath);
        while (scene.PresokobanOn) // Presokoban scene
        {
            // render
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Function.RenderText(PrePath, PreShow);

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



        string ShoppingPath = Path.Combine("Assets", "Scenes", "ShoppingScene.txt");
        string[] ShoppingShow = File.ReadAllLines(ShoppingPath);
        while (scene.ShoppingOn) // 상점이용 scene
        {
            // render
            Function.RenderText(ShoppingPath, ShoppingShow);

            // input
            ConsoleKey key = Console.ReadKey(true).Key;

            // update
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                key = default;
                scene.ShoppingOn = false;
                scene.SokobanOn = true;

            }

        }


        string[] data1 = Game.LoadStage(1); // 스테이지 1 로드하기
        string[] data2 = Game.LoadStage(2); // 스테이지 2 로드하기
        string[] data3 = Game.LoadStage(3); // 스테이지 3 로드하기

        if (game.Stage1Clear == false)
        {
            Game.RenderStage(data1); // 스테이지 1 렌더하기
        }
        
        
        Game.ParseStage(data1, out player, out wall, out goal, out box, out line4, out px); // 스테이지 파싱하기


        int BoxOnGoalCount = 0;
        bool[] BoxOnGoal = new bool[box.Length]; // 골위에 올라간 박스 확인용
        int pushed = 0; // 민 박스 확인용
        MusicWalk.PlayLooping(); // 걷는 소리 켜기
        while (scene.SokobanOn) // 소코반 scene
        {
            // render
            
            Function.Render(player.x, player.y, "P"); // 플레이어를 render
            if (player.x == player.prex && player.y == player.prey)
            {
                // 암것도안함
            }
            else // 현좌표와 전좌표가 겹치지 않을때만 지워줌
            {
                Function.Render(player.prex, player.prey, " ");
            }

            

            for (int i = 0; i < wall.Length; ++i)
            {
                Function.Render(wall[i].x, wall[i].y, "W"); // 벽을 렌더
            }

            for (int i = 0; i < line4.Length; ++i)
            {
                Function.Render(line4[i].x, line4[i].y, "4"); // 병장을 렌더

                if (line4[i].x == line4[i].prex && line4[i].y == line4[i].prey)
                {
                    // 암것도안함
                }
                else // 현좌표와 전좌표가 겹치지 않을때만 지워줌
                {
                    Function.Render(line4[i].prex, line4[i].prey, " ");
                }
            }

            


            for (int i = 0; i < goal.Length; ++i)
            {
                Function.Render(goal[i].x, goal[i].y, "*"); // 골을 렌더
            }

            for (int i = 0; i < box.Length; ++i)
            {
                Function.Render(box[i].x, box[i].y, "B"); // box를 렌더
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

            // 병장 벽에 막히는 거 구현 아니 벽 왜 뚫음???? ㅅㅂ 어이가 없네
            Line4VsWall();

            // 병장 박스에 막히는 거 구현
            Line4VsBox();

            






            // 골에 박스 몇개들어갔는지 확인하는거 구현
            BoxOnGoalCount = CountBoxOnGoal(box, goal, ref BoxOnGoal);

            if (BoxOnGoalCount == goal.Length) // 만약 모든 박스가 골에 들어갔다면
            {
                Console.Clear();
                key = default;
                scene.SokobanOn = false; // 소코반 모드 꺼주고
                scene.EncounterOn = true; // 일단 실험용

            }

        }


        

        while (scene.EncounterOn) // 병장 조우 scene
        {
            Console.WriteLine("소코반 클리어");
        }

        while (scene.BattleOn) // 병장 배틀 scene
        {

        }

        while (scene.ResultOn) // 배틀 결과 scene 승리 or 패배
        {

        }

        while (scene.ClearOn) // 소코반 완료 scene
        {

        }

        while (scene.DisabledOn) // 의가사전역 scene
        {

        }






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
                        Line4Direction = LINE4DIRECTION.NONE;  // 노방향
                        break;
                    case 1: // 1 이면 오른쪽 방향
                        MoveRight(ref line4[i].x);
                        Line4Direction = LINE4DIRECTION.RIGHT; // 오른쪽 방향
                        break;
                    case 2: // 2 면 왼쪽 방향 
                        MoveLeft(ref line4[i].x);
                        Line4Direction = LINE4DIRECTION.LEFT;
                        break;
                    case 3: // 위쪽 방향
                        MoveUp(ref line4[i].y);
                        Line4Direction = LINE4DIRECTION.UP;
                        break;
                    case 4: // 아래쪽 방향
                        MoveDown(ref line4[i].y);
                        Line4Direction = LINE4DIRECTION.DOWN;
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
                        switch (Line4Direction)
                        {
                            case LINE4DIRECTION.RIGHT:
                                BlocksRightmove(out line4[k].x, ref wall[i].x);
                                break;
                            case LINE4DIRECTION.LEFT:
                                BlocksLeftmove(out line4[k].x, ref wall[i].x);
                                break;
                            case LINE4DIRECTION.UP:
                                BlocksUpmove(out line4[k].y, ref wall[i].y);
                                break;
                            case LINE4DIRECTION.DOWN:
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
                        switch (Line4Direction)
                        {
                            case LINE4DIRECTION.RIGHT:
                                BlocksRightmove(out line4[k].x, ref box[i].x);
                                break;
                            case LINE4DIRECTION.LEFT:
                                BlocksLeftmove(out line4[k].x, ref box[i].x);
                                break;
                            case LINE4DIRECTION.UP:
                                BlocksUpmove(out line4[k].y, ref box[i].y);
                                break;
                            case LINE4DIRECTION.DOWN:
                                BlocksDownmove(out line4[k].y, ref box[i].y);
                                break;

                        }
                    }
                }
            }

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