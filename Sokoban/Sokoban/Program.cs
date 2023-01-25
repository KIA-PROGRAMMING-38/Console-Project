using System;

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
            Console.Title = "홍성재의 파이어펀치";   //타이틀을 설정
            Console.BackgroundColor = ConsoleColor.DarkGreen;      //ConsoleColor = enum 타입 배경색을 설정함.
            Console.ForegroundColor = ConsoleColor.Yellow;         //글꼴을 설정함
            Console.Clear(); //출력된 내용을 지운다.   

            // 작성을 했으면 실행을 해봐야함. 다 하고 나서든 중간에 하든 실행을 해봐야 재빨리 버그가 난것을 캐치할 수 있음.

            // 프레임 워크 = 프로그램의 동작 순서를 정의한 것.


            // 플레이어 위치를 저장하기 위한 변수
            // 기준점에서 x는  오른쪽으로 갈수록 값이 증가, y 값은 아래로 갈수록 값이 증가
            int playerX = 5;
            int playerY = 3;
            //  이런 변수를 보면 두개의 네모 박스가 생각이 나야함. 박스안에 데이터 이름 주소가 생각이 나야함.

            // 박스를 밀고있는 플레이어가 어떤 박스를 밀고 있는지 알기 위해 새로운 객체를 만들어야함.
            int pushBox = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 박스 위치를 저장하기 위한 변수

            const int BOX_COUNT = 3; // 개수 설정

            int[] BoxPositionX = new int[BOX_COUNT] { 10, 4, 9 }; // 박스의 위치와 개수
            int[] BoxPositionY = new int[BOX_COUNT] { 10, 5, 8 };

            // 골 위치를 저장하기 위한 변수
            const int GOAL_COUNT = BOX_COUNT;

            int[] GoalPositionX = new int[GOAL_COUNT] { 13, 7, 10 };
            int[] GoalPositionY = new int[GOAL_COUNT] { 13, 7, 3 };

            // 벽 위치를 저장하기 위한 변수
            const int WALL_COUNT = 2;

            int[] wallPositionX = new int[] { 12, 2 };
            int[] wallPositionY = new int[] { 12, 2 };

            //위
            const int O_COUNT = 30;
            int[] OPositionX = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            int[] OPositionY = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //아래
            const int B_COUNT = 30;
            int[] BPositionX = new int[] {0 , 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            int[] BPositionY = new int[] {20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };

            //왼쪽
            const int V_COUNT = 20;
            int[] VPositionY = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11 ,12, 13, 14, 15, 16, 17, 18, 19};
            int[] VPositionX = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //오른쪽
            const int Q_COUNT = 20;
            int[] QPositionY = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19};
            int[] QPositionX = new int[] { 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29 };

            // 박스가 골위에 올라와 있는지 저장
            const int BOXONGOAL_COUNT = 3;
            
            bool[] isBoxonGoal = new bool [BOXONGOAL_COUNT];          

            // 게임루프 구성
            while (true)
            {
                //-----------------------------------Rnder(데이터를 가지고 그려주는 단계)------------------------------------------------
                // 이전 프레임을 지우는 것. 루프한번이 한 화면을 동작하는 것을 보여주는것
                Console.Clear();

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

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
                    Console.Write(isBoxonGoal[goali] ? "★" : "G");
                }

                // 벽을 그린다
                for (int walli = 0; walli < WALL_COUNT; ++walli)
                {
                    Console.SetCursorPosition(wallPositionX[walli], wallPositionY[walli]);
                    Console.Write("W");
                }
                    // 벽 테두리 그리기
                
                // 위
                for (int oi = 0; oi < O_COUNT; ++oi)
                {
                    Console.SetCursorPosition(OPositionX[oi], OPositionY[oi]);
                    Console.Write("H");
                }

                // 아래
                for (int bi = 0; bi < B_COUNT; ++bi)
                {
                    Console.SetCursorPosition(BPositionX[bi], BPositionY[bi]);
                    Console.Write("H");
                }

                //왼쪽
                for (int vi = 0; vi < V_COUNT; ++vi)
                {
                    Console.SetCursorPosition(VPositionX[vi], VPositionY[vi]);
                    Console.WriteLine("H");
                }

                //오른쪽
                for (int qi = 0; qi < Q_COUNT; ++qi)
                {
                    Console.SetCursorPosition(QPositionX[qi], QPositionY[qi]);
                    Console.WriteLine("H");
                }
                //--------------------------------------- processInput(입력을 저장하는 부분)---------------------------------------------------

                ConsoleKey Key = Console.ReadKey().Key; // ConsoleKey() 타입

                //------------------------------------------update(게임 데이터를 업데이트)------------------------------------------------


                // 플레이어 이동처리
                // 박스 밀기는 플에이어 이동 후에 해야함.
                if (Key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(0, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (Key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(30, playerX + 1);
                    playerMoveDirection = Direction.Right;
                }

                if (Key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (Key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(20, playerY + 1);
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
                                BoxPositionX[boxi] = Math.Max(0, BoxPositionX[boxi] - 1); // 박스 옮기기
                                playerX = BoxPositionX[boxi] + 1; // 끝까지가도 플레이어가 박스에 안겹치게 하기
                                break;

                            case Direction.Right:
                                BoxPositionX[boxi] = Math.Min(30, BoxPositionX[boxi] + 1);
                                playerX = BoxPositionX[boxi] - 1;
                                break;

                            case Direction.Up:
                                BoxPositionY[boxi] = Math.Max(0, BoxPositionY[boxi] - 1);
                                playerY = BoxPositionY[boxi] + 1;
                                break;

                            case Direction.Down:
                                BoxPositionY[boxi] = Math.Min(20, BoxPositionY[boxi] + 1);
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
                for (int boxi = 0; boxi < BOX_COUNT; ++boxi)
                {                    
                    isBoxonGoal[boxi] = false;      // 박스가 골밖으로 나갔을때 골기호가 다시 바뀜. 

                    for (int goali = 0; goali < GOAL_COUNT; ++goali)
                    {

                        if (BoxPositionX[boxi] == GoalPositionX[goali] && BoxPositionY[boxi] == GoalPositionY[goali]) 
                        {
                            ++BoxOnGoalCount;         
                            isBoxonGoal[boxi] = true;
                        }
                        
                    }
                }
                
                // 모든 골 지점에 박스가 올라와 있다면 ? //  꼴 ~~
                if (BoxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.Write("꼴~~");
                    break;
                }
            }   // 현재 박스와 골의 좌표가 지정되어 있어서 박스를 골에 넣었을때 지정된 골의 기호만 바뀜. ex . 1번박스를 1번골에 넣으면 2번골의 기호가 바뀜.
        }       // 박스 테두리 만들어야함.
    }
}