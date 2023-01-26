namespace Sokoban
{
    enum Direction // 방향을 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down
    }
    class Sokoban
    {
        static void Main()
        {
            // a와 b중 최대값을 구하는 것.

            // 1교시
            // 초기 세팅 = 게임을 실행하기전 무조건 거칠수 밖에 없음. 초기세팅의 순서는 상관이 없음.
            Console.ResetColor();// 컬러를 초기화하는 것. = 콘솔게임은 명령 프롬프트 에서 실행됨.
            Console.CursorVisible = false; //커서를 숨겨야함.  Visible은 불리언 타입이기 때문에 false 와 true로 나뉨 숨기려면 false
            Console.Title = "미로탈출";   //타이틀을 설정
            Console.BackgroundColor = ConsoleColor.Black;      //ConsoleColor = enum 타입 배경색을 설정함.
            Console.ForegroundColor = ConsoleColor.White;         //글꼴을 설정함
            Console.Clear(); //출력된 내용을 지운다.   

            // 작성을 했으면 실행을 해봐야함. 다 하고 나서든 중간에 하든 실행을 해봐야 재빨리 버그가 난것을 캐치할 수 있음.

            // 프레임 워크 = 프로그램의 동작 순서를 정의한 것.


            // 플레이어 위치를 저장하기 위한 변수
            // 기준점에서 x는  오른쪽으로 갈수록 값이 증가, y 값은 아래로 갈수록 값이 증가
            int playerX = 1;
            int playerY = 1;
            //  이런 변수를 보면 두개의 네모 박스가 생각이 나야함. 박스안에 데이터 이름 주소가 생각이 나야함.

            // 박스를 밀고있는 플레이어가 어떤 박스를 밀고 있는지 알기 위해 새로운 객체를 만들어야함.
            int pushBox = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 박스 위치를 저장하기 위한 변수

            const int BOX_COUNT = 3; // 개수 설정

            int[] BoxPositionX = new int[BOX_COUNT] { 46 , 2 , 48 }; // 박스의 위치와 개수
            int[] BoxPositionY = new int[BOX_COUNT] { 3 , 24 , 24};

            // 골 위치를 저장하기 위한 변수
            const int GOAL_COUNT = BOX_COUNT;

            int[] GoalPositionX = new int[GOAL_COUNT] { 47 , 1 , 49};
            int[] GoalPositionY = new int[GOAL_COUNT] { 3 , 24 , 24};

            // 벽 위치를 저장하기 위한 변수
            const int WALL_COUNT = 533;

            int[] wallPositionX = new int[] { 2, 3, 4, 2, 2, 4, 4, 4, 3, 2, 1, 4, 2, 2, 3, 4, 1, 2, 3, 4, 1, 1, 1, 3, 4, 1, 2, 3, 1, 2, 3, 4, 3, 3, 3, 3, 1, 2, 3, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 9, 10, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 30, 30, 30, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 32, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 36, 36, 36, 36, 36, 36, 36, 36, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 38, 38, 38, 38, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 39, 40, 40, 40, 40, 40, 40, 40, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 41, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 43, 44, 44, 44, 44, 44, 44, 44, 44, 44, 44, 44, 44, 44, 44, 45, 45,45, 45, 45, 45, 45, 45, 45, 45, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 46, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, };
            int[] wallPositionY = new int[] { 2, 2, 2, 3, 4, 4, 5, 6, 6, 6, 6, 7, 8, 9, 9, 9, 11, 11, 11, 11, 12, 13, 14, 13, 13, 15, 15, 15, 17, 17, 17, 17, 18, 19, 20, 21, 23, 23, 23, 23, 23, 21, 20, 19, 17, 16, 15, 12, 5, 1, 2, 3, 3, 4, 5, 5, 5, 5, 7, 8, 9, 10, 14, 15, 17, 19, 23, 3, 4, 5, 6, 7, 10, 11, 13, 14, 17, 19, 21, 22, 23,5, 11, 13, 17, 19, 22, 3, 5, 7, 8, 9, 15, 17, 19, 20, 7, 11, 12, 13, 14, 15, 22, 23, 24, 3, 5, 7, 8, 9, 10, 11, 15, 16, 17, 18, 19, 20, 22, 3, 10, 13, 20, 22, 24, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 16, 17, 18, 24, 4, 11, 13, 14, 16, 19, 22, 23, 24, 1, 2, 4, 5, 6, 7, 9, 11, 13, 16, 19, 24, 2, 4, 9, 11, 12, 13, 15, 16, 19, 21, 22, 23, 24, 2, 4, 6, 7, 8, 9, 13, 15, 19, 2, 6, 11, 13, 15, 17, 19, 20, 21, 22, 23, 24, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 17, 24, 8, 11, 13, 15, 16, 19, 20, 21, 22, 24, 1, 3, 4, 5,11, 18, 19, 22, 24, 3, 6, 8, 9, 11, 12, 13, 14, 16, 18, 22, 23, 24, 2, 3, 5, 7, 14, 16, 18, 5, 7, 8, 9, 10, 11, 12, 16, 18, 20 ,21, 1, 2, 3, 7, 13, 14, 16, 18, 20, 21, 22, 23, 24, 3, 9, 10, 11, 16, 18, 3, 5, 6, 7, 8, 9, 12, 13, 14, 16, 18, 19, 20, 21, 22, 23, 3, 5, 6, 7, 8, 9, 12, 13, 14, 16, 18, 19, 20, 21, 22, 23, 3, 14, 3, 6, 8, 9, 10, 11, 12, 14, 15, 16, 17, 19, 20, 21, 22, 23, 24, 6, 12, 16, 1, 2, 3, 4, 6, 7, 8, 9, 10, 12, 13, 16, 18, 19, 20, 21, 22, 23, 24, 8, 10, 13, 14, 16, 2, 3, 4, 5, 6, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 2, 6, 8, 12, 2, 4, 10, 12, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 2, 4, 5, 6, 7, 8, 10, 12, 2, 5, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 2, 3, 5, 10, 3, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21, 22, 23, 24, 3, 4, 5, 6, 7, 17, 1, 7, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21, 22, 23, 1, 2, 3, 4, 5, 7, 16, 17, 19, 23, 1, 5, 7, 9, 10, 11, 13, 14, 16, 17, 19, 21, 23, 1, 2, 5, 7, 9, 10, 11, 14, 16, 17, 19, 21, 23, 1, 7, 9, 11, 12, 14, 16, 17, 19, 20, 21, 23, 1, 2, 4, 5, 7, 8, 9, 12, 14, 16, 17, 23, 1, 4, 5, 6, 14, 16, 17, 18, 19, 20, 21, 22, 23, 1, 3, 4, 6, 7, 8, 9, 10, 11, 14, 16, 11, 14, 16, 17, 18, 19, 20, 21, 22, 23 };

            // 가로
            const int O_COUNT = 50;

            // 세로
            const int V_COUNT = 25;

            // 박스가 골위에 올라와 있는지 저장
            const int BOXONGOAL_COUNT = 3;

            bool[] isBoxonGoal = new bool[BOXONGOAL_COUNT];

            // 게임루프 구성?y 
            while (true)
            {
                //-----------------------------------Rnder(데이터를 가지고 그려주는 단계)------------------------------------------------
                // 이전 프레임을 지우는 것. 루프한번이 한 화면을 동작하는 것을 보여주는것
                Console.Clear();

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("♣");

                // 박스를 그린다

                for (int boxi = 0; boxi < BOX_COUNT; ++boxi)
                {
                    Console.SetCursorPosition(BoxPositionX[boxi], BoxPositionY[boxi]);
                    Console.Write("B");
                }

                // 골을 그린다                

                for (int goali = 0; goali < GOAL_COUNT; ++goali)
                {
                    Console.SetCursorPosition(GoalPositionX[goali], GoalPositionY[goali]);
                    Console.Write(isBoxonGoal[goali] ? "★" : "♥");
                }

                // 벽을 그린다
                for (int walli = 0; walli < WALL_COUNT; ++walli)
                {
                    Console.SetCursorPosition(wallPositionX[walli], wallPositionY[walli]);
                    Console.Write("☎");                    
                }

                
                // 벽 테두리 그리기                                

                for (int oi = 0; oi < O_COUNT; ++oi)
                {
                    for (int O = 0; O < O_COUNT; ++O)
                    {
                        Console.SetCursorPosition(O, 0);    // 위
                        Console.Write("★");
                        Console.SetCursorPosition(O, V_COUNT);   // 아래
                        Console.Write("★");
                    }
                }

                for (int V = 0; V < V_COUNT; ++V)
                {
                    Console.SetCursorPosition(0, V); // 왼쪽
                    Console.Write("★");
                    Console.SetCursorPosition(O_COUNT, V); // 오른쪽
                    Console.Write("★");
                }
                

                //--------------------------------------- processInput(입력을 저장하는 부분)---------------------------------------------------

                ConsoleKey Key = Console.ReadKey().Key; // ConsoleKey() 타입

                //------------------------------------------update(게임 데이터를 업데이트)-----------------------------------------------------

                // 플레이어 이동처리
                // 박스 밀기는 플에이어 이동 후에 해야함.
                if (Key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(1, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (Key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(O_COUNT - 1, playerX + 1);
                    playerMoveDirection = Direction.Right;
                }

                if (Key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(1, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (Key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(V_COUNT - 1, playerY + 1);
                    playerMoveDirection = Direction.Down;
                }

                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가 ? 밀었을때라는건 플레이어가 이동했는데 플레이어의 위치와 박스위치가 겹친것.
                // 박스의 위치좌표와 플레이어 위치 좌표가 같아야함.

                // 플레이어와 박스 충돌 처리
                for (int boxi = 0; boxi < BOX_COUNT; ++boxi)
                {
                    if (playerX == BoxPositionX[boxi] && playerY == BoxPositionY[boxi])
                    {
                        // 맵밖으로 나가지 않게하기
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                BoxPositionX[boxi] = Math.Max(1, BoxPositionX[boxi] - 1); // 박스 옮기기
                                playerX = BoxPositionX[boxi] + 1; // 끝까지가도 플레이어가 박스에 안겹치게 하기
                                break;

                            case Direction.Right:
                                BoxPositionX[boxi] = Math.Min(O_COUNT - 1, BoxPositionX[boxi] + 1);
                                playerX = BoxPositionX[boxi] - 1;
                                break;

                            case Direction.Up:
                                BoxPositionY[boxi] = Math.Max(1, BoxPositionY[boxi] - 1);
                                playerY = BoxPositionY[boxi] + 1;
                                break;

                            case Direction.Down:
                                BoxPositionY[boxi] = Math.Min(V_COUNT - 1, BoxPositionY[boxi] + 1);
                                playerY = BoxPositionY[boxi] - 1;
                                break;
                        }
                        pushBox = boxi;
                    }
                }

                // 플레이어랑 벽의 충돌 처리
                for (int walli = 0; walli < WALL_COUNT; ++walli)
                {
                    if (playerX == wallPositionX[walli] && playerY == wallPositionY[walli])
                    {

                        switch (playerMoveDirection)
                        {

                            case Direction.Left:
                                playerX = wallPositionX[walli] + 1;
                                break;
                            case Direction.Right:
                                playerX = wallPositionX[walli] - 1;
                                break;
                            case Direction.Up:
                                playerY = wallPositionY[walli] + 1;
                                break;
                            case Direction.Down:
                                playerY = wallPositionY[walli] - 1;
                                break;
                        }
                    }
                }

                // 플레이어가 박스를 벽에 밀때 벽이랑 박스랑, 박스랑 플레이어랑 안겹치게 하는 작업.
                for (int boxi = 0; boxi < BOX_COUNT; ++boxi)
                {
                    for (int walli = 0; walli < WALL_COUNT; ++walli)
                    {
                        if (BoxPositionX[boxi] == wallPositionX[walli] && BoxPositionY[boxi] == wallPositionY[walli])
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:
                                    BoxPositionX[boxi] = wallPositionX[walli] + 1;
                                    playerX = BoxPositionX[boxi] + 1;
                                    break;
                                case Direction.Right:
                                    BoxPositionX[boxi] = wallPositionX[walli] - 1;
                                    playerX = BoxPositionX[boxi] - 1;
                                    break;
                                case Direction.Up:
                                    BoxPositionY[boxi] = wallPositionY[walli] + 1;
                                    playerY = BoxPositionY[boxi] + 1;
                                    break;
                                case Direction.Down:
                                    BoxPositionY[boxi] = wallPositionY[walli] - 1;
                                    playerY = BoxPositionY[boxi] - 1;
                                    break;
                            }
                        }
                    }
                }

                // 박스와 박스 충돌처리
                for (int boxi = 0; boxi < BOX_COUNT; ++boxi)
                {
                    if (pushBox == boxi)
                    {
                        continue;
                    }
                    if (BoxPositionX[boxi] == BoxPositionX[pushBox] && BoxPositionY[boxi] == BoxPositionY[pushBox])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                if (pushBox == boxi)  // 플레이어가 밀려는 박스의 객체 BoxX,BoxY = PushBox1.
                                {
                                    BoxPositionX[boxi] = BoxPositionX[pushBox] + 1; // 왼쪽으로 가려고 하기 떄문에 박스가 밀리지 않으려면 박스를 반대로 이동시켜야함.
                                    //밀리는 박스            //밀려는 박스
                                    playerX = BoxPositionX[boxi] + 1;
                                }
                                else
                                {
                                    BoxPositionX[boxi] = BoxPositionX[pushBox] + 1;
                                    playerX = BoxPositionX[boxi] + 1;
                                }
                                break;
                            case Direction.Right:
                                if (pushBox == boxi)
                                {
                                    BoxPositionX[boxi] = BoxPositionX[pushBox] - 1;
                                    playerX = BoxPositionX[boxi] - 1;
                                }
                                else
                                {
                                    BoxPositionX[boxi] = BoxPositionX[pushBox] - 1;
                                    playerX = BoxPositionX[boxi] - 1;
                                }
                                break;
                            case Direction.Up:
                                if (pushBox == boxi)
                                {
                                    BoxPositionY[boxi] = BoxPositionY[pushBox] + 1;
                                    playerY = BoxPositionY[boxi] + 1;
                                }
                                else
                                {
                                    BoxPositionY[boxi] = BoxPositionY[pushBox] + 1;
                                    playerY = BoxPositionY[boxi] + 1;
                                }
                                break;
                            case Direction.Down:
                                if (pushBox == boxi)
                                {
                                    BoxPositionY[boxi] = BoxPositionY[pushBox] - 1;
                                    playerY = BoxPositionY[boxi] - 1;
                                }
                                else
                                {
                                    BoxPositionY[boxi] = BoxPositionY[pushBox] - 1;
                                    playerY = BoxPositionY[boxi] - 1;
                                }
                                break;
                        }
                    }
                }

                // 박스가 골 위에 올라왔는지 확인

                int BoxOnGoalCount = 0; // 골안에 박스 개수.

                // 골 기호 바꾸기
                for (int boxi = 0; boxi < GOAL_COUNT; ++boxi)
                {
                    isBoxonGoal[boxi] = false;      // 박스가 골밖으로 나갔을때 골기호가 다시 바뀜. 

                    for (int goali = 0; goali < BOX_COUNT; ++goali)
                    {

                        if (BoxPositionX[boxi] == GoalPositionX[goali] && BoxPositionY[boxi] == GoalPositionY[goali])
                        {
                            ++BoxOnGoalCount;           //  박스와 골이 같은 위치에 있을때 골안에 박스 개수 하나씩 증가함
                            isBoxonGoal[boxi] = true;   // 박스와 골이 같은 위치에 있을때  박스 개수가 하나씩 증가하고 박스가 올라와 있다면 true. true면 

                        }
                    }
                }

                // 모든 골 지점에 박스가 올라와 있다면 ? //  꼴 ~~
                if (BoxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.Write("CLEAR");
                    break;
                }
            }           
        }               
    }
}
