using System;
using System.Dynamic;
using System.Text;
using System.Media;

namespace Way_back_home
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down

    }

    class Way_back_home
    {
        static void Main()
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.SetWindowSize(105, 35); 
            Console.Title = "Way back home";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Clear();


            // 기호 상수 정의
            const int GOAL_COUNT = 9;
            const int WALL_COUNT = 187;
            const int BOX_COUNT = GOAL_COUNT;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 100;
            int playerY = 0;
            int playerprex = 101;
            int playerprey = 0;
            

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
            int pushedBoxId = 0; // 1이면 박스1, 2이면 박스2

            // 박스 위치를 저장하기 위한 변수
            int[] boxPositionsX = { 80, 8, 54, 84, 30, 48, 10, 40, 34 }; 
            int[] boxPositionsY = { 15, 6, 11, 2, 18, 3, 9, 15, 7 };

            // 벽 위치를 저장하기 위한 변수
            int[] wallPositionX = { 4, 12, 2, 4, 2, 0 ,14, 6, 8, 10, 12, 12, 12, 12, 12, 4, 4, 4, 4, 2, 2, 6, 8, 10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,16,16,16,18,20,22,24,26,28,30,32,34,36,38,38,38,38,40,42,44,46,48,50,52,52,52,52,52,52,54,56,58,60,62,64,66,68,68,68,68,68,68,70,72,74,76,76,76,76,76,76,76,76,76,74,72,70,70,72,74,76,78,80,82,84,86,88,90,90,86,88,90,92,94,96,98,100,86,86,86,86,86,86,86,86,86,86,86,86,84,82,80,78,76,74,72,70,68,66,64,62,60,58,58,58,58,58,58,58,58,58,58,58,56,54,52,50,48,46,46,46,46,46,46,46,46,44,42,40,38,36,34,32,30,28,26,24,22,20,18,16,16,16,16,16,16,16,16};
            int[] wallPositionY = { 3,  8, 5, 5, 3, 4,  8, 3, 3,  3,  3,  4,  5,  6,  7, 6, 7, 8,11, 9,10,11,11, 11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11, 8, 7, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 4, 2, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 9,10,11,12,12,12,12,12,11,10, 9, 8, 7, 6, 5, 4, 4, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 4, 4, 4, 4, 4, 4, 4,  4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,17,17,17,17,17,17,17,17,17,17,17,17,17,16,15,14,13,12,11,18,19,20,21,22,22,22,22,22,21,20,19,18,17,16,15,14,20,20,20,20,20,20,20,20,20,20,20,20,20,20,19,18,17,16,15,14,13,12};

            // 골 위치를 저장하기 위한 변수
            int[] goalPositionX = { 4, 44, 2, 20, 74, 64, 52, 38, 18 }; 
            int[] goalPositionY = { 10, 2, 4, 14, 2, 12, 19, 17, 8 };

            // 골에 몇번 박스가 들어가 있는지 저장하기 위한 변수
            bool[] isBoxOnGoal = new bool[BOX_COUNT];
            bool[] isBoxInGoal = new bool[BOX_COUNT];
            

            // 대사 하나씩 변경
            string[] dialogs = new string[GOAL_COUNT + 1]
           {     

                 " ",
                 "Mommy..? I Love you too~",
                 "Mommy. Mommy..? Mo..m..m..y..... Where are you..",
                 "Why? It's not my fault! No! Stop!! Don't hurt me..! No!!!",
                 "It's so dark and cold here.. I'm hungry.. Please let me out of here.. please...plea..se..",
                 "Mommy....?! Nooooooo!!! Don't!!!!!!! Kaaaaaaaaaaaaaaaa!!!! Don't you dare touch me!!     ",
                 "It's not my fault.. Please don't blame me.. Stop....please stop...                       ",
                 "Get away from me!!!! Your not my Mommy anymore! Your an evil                             ",
                 "Somebody Help me.. Please...                                                             ",
                 " "
                };


            // 가로 100 세로 30
            // 게임 루프 == 프레임(Freame)
            while (true)
            {
                Console.SetCursorPosition(30, 13);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("This stroy is about a Strong Cat named ...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(33, 20);
                Console.Write("▶ Press ENTER to see the manual");
                ConsoleKey Key = Console.ReadKey().Key;
                if (Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    break; 
                }
            }

            while (true)
            {

                
                Console.SetCursorPosition(42, 9);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("-- M A N U A L --");
                Thread.Sleep(000);

                Console.SetCursorPosition(32, 13);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Move the broken hearts to empty hearts.");
                Thread.Sleep(000);

                Console.SetCursorPosition(37, 15);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("To find the true happiness.");
                Thread.Sleep(000);

                Console.SetCursorPosition(39, 20);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("▶ Press ENTER to start");
                ConsoleKey Key = Console.ReadKey().Key;
                if (Key == ConsoleKey.Enter)
                {
                    Console.Clear(); // 깜빡이 방지 위해 만들어줌
                    
                    break;
                }

            }


            while (true)
            {
                // 이전 프레임을 지운다.
                //Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                // -------------------------------------------------------------Render-------------------------------------------------------------
                // 플레이어 출력하기
                //for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                //{
                //    if (playerprex == goalPositionX[goalId] && playerprey == goalPositionY[goalId])
                //    {
                //        // 아무것도 안해주고
                //    }
                //    else
                //    {
                //        Console.SetCursorPosition(playerprex, playerprey);
                //        Console.Write("  ");
                //        Console.SetCursorPosition(playerX, playerY);
                //        Console.Write("🐈");
                //    }
                //}


                // 박스 출력하기 
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write("💔");

                }

                // 골인 출력하기
              

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    if (playerprex == goalPositionX[goalId] && playerprey == goalPositionY[goalId])
                    {
                        // 아무것도 안해주고
                    }
                    else
                    {
                        
                        Console.SetCursorPosition(playerprex, playerprey);
                        Console.Write("  ");
                        Console.SetCursorPosition(playerX, playerY);
                        Console.Write("🐈");

                    }
                }


                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    int goalX = goalPositionX[goalId];
                    int goalY = goalPositionY[goalId];

                    Console.SetCursorPosition(goalX, goalY);

                    if (isBoxOnGoal[goalId] == true)
                    {
                        ConsoleColor temp1 = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("🖤");
                        Console.ForegroundColor = temp1;

                    }
                    else
                    {
                        Console.Write("🤍");
                    }

                }


                int goalCount = 0;
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    if (isBoxOnGoal[goalId] == true)
                    {
                        ++goalCount;
                        
                    }
                }


                Console.SetCursorPosition(7, 27);
                ConsoleColor temp2 = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(dialogs[goalCount]);
                Console.ForegroundColor = temp2;
                

                // 벽 출력하기
                for (int i = 0; i < WALL_COUNT; i++)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("🧱");
                }


                // ----------------------------------------------------------ProcessINput----------------------------------------------------------

                ConsoleKey key = Console.ReadKey().Key; // 저장을 해야 사용할 수 있다

                // -------------------------------------------------------------Update-------------------------------------------------------------

                playerprex = playerX;
                playerprey = playerY;
                
                // 플레이어 업데이트(이동 처리)
                if (key == ConsoleKey.LeftArrow) // ← 왼쪽으로 이동
                {
                    playerX = Math.Max(0, playerX - 2);
                    playerMoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow) // → 오른쪽으로 이동
                {
                    playerX = Math.Min(playerX + 2, 100);
                    playerMoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow) // ↑ 위로 이동
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow) // ↓ 밑으로 이동
                {
                    playerY = Math.Min(playerY + 1, 30);
                    playerMoveDirection = Direction.Down;
                }


                // 플레이어가 벽에 부딪혔을 때
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

                    if (playerX == wallX && playerY == wallY)
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left: // ←
                                playerX = wallX + 2;
                                break;
                            case Direction.Right: // →
                                playerX = wallX - 2;
                                break;
                            case Direction.Up: // ↑
                                playerY = wallY + 1;
                                break;
                            case Direction.Down: // ↓
                                playerY = wallY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. : {playerMoveDirection}");

                                return;
                        }
                    }

                }

                 
                // 박스 업데이트               
                //플레이어가 이동한 후
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    if (playerX == boxX && playerY == boxY) // 플레이어가 이동하고나니 박스가 있네?
                    {
                        if (isBoxInGoal[i] == true) // 골에 들어갔는데 박스가 고정된다
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left: // ←
                                    playerX = boxX + 2;
                                    break;
                                case Direction.Right: // →
                                    playerX = boxX - 2;
                                    break;
                                case Direction.Up: // ↑
                                    playerY = boxY + 1;
                                    break;
                                case Direction.Down: // ↓
                                    playerY = boxY - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. : {playerMoveDirection}");

                                    return;
                            }
                        }

                        else // 하지만 
                        {
                            // 박스를 움직여주면 됨
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:  // ← 왼쪽으로 이동 중
                                    boxX = Math.Max(0, boxX - 2);
                                    playerX = boxX + 2;
                                    break;
                                case Direction.Right:  // → 오른쪽으로 이동 중
                                    boxX = Math.Min(boxX + 2, 100);
                                    playerX = boxX - 2;
                                    break;
                                case Direction.Up:  // ↑ 위로 이동 중
                                    boxY = Math.Max(0, boxY - 1);
                                    playerY = boxY + 1;
                                    break;
                                case Direction.Down:  // ↓ 밑으로 이동 중
                                    boxY = Math.Min(boxY + 1, 30);
                                    playerY = boxY - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerMoveDirection}");

                                    return;
                            }
                            pushedBoxId = i;
                        }
                       
                    }
                    boxPositionsX[i] = boxX;
                    boxPositionsY[i] = boxY;
                }


                // 박스가 벽에 부딪혔을 때
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        int wallX = wallPositionX[wallId];
                        int wallY = wallPositionY[wallId];

                        if (boxX == wallX && boxY == wallY)
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:
                                    boxX = wallX + 2;
                                    playerX = boxX + 2;
                                    break;

                                case Direction.Right:
                                    boxX = wallX - 2;
                                    playerX = boxX - 2;
                                    break;

                                case Direction.Up:
                                    boxY = wallY + 1;
                                    playerY = boxY + 1;
                                    break;

                                case Direction.Down:
                                    boxY = wallY - 1;
                                    playerY = boxY - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerMoveDirection}");

                                    return; // 프로그램 종료

                            }

                            boxPositionsX[i] = boxX;
                            boxPositionsY[i] = boxY;
                            break; // 박스가 동시에 같은 벽에 충돌할 일은 없을 것이다

                        }
                    }
                }


                // 박스끼리 부딪혔을 때
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                {
                    if (pushedBoxId == collidedBoxId)
                    {
                        continue;
                    }
                    if (boxPositionsX[pushedBoxId] == boxPositionsX[collidedBoxId] && boxPositionsY[pushedBoxId] == boxPositionsY[collidedBoxId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxPositionsX[pushedBoxId] += 2;
                                playerX += 2;
                                break;

                            case Direction.Right:
                                boxPositionsX[pushedBoxId] -= 2;
                                playerX -= 2;
                                break;

                            case Direction.Up:
                                boxPositionsY[pushedBoxId] += 1;
                                playerY += 1;
                                break;

                            case Direction.Down:
                                boxPositionsY[pushedBoxId] -= 1;
                                playerY -= 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                                return;
                        }
                    }
                }

                // 골인 지점 만들기
                // 1) Box1번과 Goal1번이 만났을 때
                // 2) Box1번과 Goal2번이 만났을 때
                // 3) Box2번과 Goal1번이 만났을 때
                // 4. Box2번과 Goal2번이 만났을 때

                int boxOnGoalCount = 0;
                for (int goalId = 0; goalId < GOAL_COUNT; goalId++)
                {
                    isBoxOnGoal[goalId] = false;

                    for (int boxId = 0; boxId < BOX_COUNT; boxId++)
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (goalPositionX[goalId] == boxPositionsX[boxId] && goalPositionY[goalId] == boxPositionsY[boxId])
                        {

                            ++boxOnGoalCount;
                            isBoxOnGoal[goalId] = true;
                            isBoxInGoal[boxId] = true;
                            break; // goal하나에 박스 하나만 올라가 있기 때문에
                        }

                    }
                }

                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(1, 2); //1
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\"..ry\"");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 3);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 4); //2
                    Console.WriteLine("\"...ory~ ..k...p~~\"");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(1, 5);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 6); //3
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("....");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 7);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 8); //4
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\"HaHa~ ..o..y~\" ");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 9);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 10); //5
                    Console.WriteLine("\"It's morning~!! Wake up~~\"");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 11);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 12); //6
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(".......");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 13);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 14); //7
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Ah....");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 15);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 16); //8
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\"...y~~\"");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 17);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 18); //9
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Right..");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 19);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 20); //10
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\"..ory...?\"");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 21);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 22); //11
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(".....");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(6, 22); //11
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("I'm at home already.");
                    Thread.Sleep(2000);

                    Console.SetCursorPosition(1, 23);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    Console.SetCursorPosition(1, 24);
                    Console.WriteLine(" ");
                    Thread.Sleep(000);

                    // Toryvoice.PlayLooping();
                    SoundPlayer Toryvoice = new SoundPlayer(@"Music\송토리목소리.wav");
                    Toryvoice.PlayLooping();

                    Console.SetCursorPosition(1, 25); //12
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\"Meaw~\"");
                    Thread.Sleep(1000);
                    Toryvoice.Stop();

                    Console.SetCursorPosition(1, 26);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(1, 2);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);



                    Console.Clear();

                    while (true)
                    {
                        Console.SetCursorPosition(30, 13);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("This stroy is about a Strong Cat named ...");
                        Thread.Sleep(3000);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(47, 17);
                        Console.Write("Tory");
                        Thread.Sleep(1000);

                        Console.SetCursorPosition(47, 18);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Thread.Sleep(1000);

                        Console.SetCursorPosition(47, 19);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Thread.Sleep(1000);

                        Console.SetCursorPosition(47, 20);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Thread.Sleep(1000);

                        return;
                    }
                    
                }

            }

        }
    }

}


