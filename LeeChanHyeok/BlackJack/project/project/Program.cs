using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace project
{
    enum Direction
    {
        none,
        up,
        down,
        enter
    }
    class Program
    {
        static void Main()
        {
            //Title titleSceen = new Title();
            //titleSceen.TitleMain();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;

            //카드
            const int CARD_COUNT = 13;
            string[] pattern = { "♠", "◆", "♥", "♣" };
            string[] number = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            //플레이어 이동
            Direction player = Direction.none;


            //함수 불러오기
            Map mapRender = new Map();
            Random random = new Random();
            Input input = new Input();



            //카드 정보 저장
            Card cardUser = new Card();
            Card cardDealer = new Card();

            //카드, 숫자 좌표
            DealerFrontCardPatternCoordinate[] dealerFrontCardPatternCoordinate = new DealerFrontCardPatternCoordinate[]
            {
                new DealerFrontCardPatternCoordinate {X = 51, Y = 9},
                new DealerFrontCardPatternCoordinate {X = 66, Y = 9},
                new DealerFrontCardPatternCoordinate {X = 81, Y = 9}
            };

            DealerEndCardPatternCoordinate[] dealerEndCardPatternCoordinate = new DealerEndCardPatternCoordinate[]
            {
                new DealerEndCardPatternCoordinate {X = 60, Y = 15},
                new DealerEndCardPatternCoordinate {X = 75, Y = 15},
                new DealerEndCardPatternCoordinate {X = 90, Y = 15}
            };

            DealerNumberCoordinate[] dealerNumberCoordinate = new DealerNumberCoordinate[]
            {
                new DealerNumberCoordinate {X = 56, Y = 12},
                new DealerNumberCoordinate {X = 71, Y = 12},
                new DealerNumberCoordinate {X = 86, Y = 12}
            };

            UserFrontCardPatternCoordinate[] userFrontCardPatternCoordinate = new UserFrontCardPatternCoordinate[]
            {
                new UserFrontCardPatternCoordinate {X = 51, Y = 24},
                new UserFrontCardPatternCoordinate {X = 66, Y = 24},
                new UserFrontCardPatternCoordinate {X = 81, Y = 24}
            };

            UserEndCardPatternCoordinate[] userEndCardPatternCoordinate = new UserEndCardPatternCoordinate[]
            {
                new UserEndCardPatternCoordinate {X = 60, Y = 30},
                new UserEndCardPatternCoordinate {X = 75, Y = 30},
                new UserEndCardPatternCoordinate {X = 90, Y = 30}
            };

            UserNumberCoordinate[] userNumberCoordinate = new UserNumberCoordinate[]
            {
                new UserNumberCoordinate {X = 56, Y = 27},
                new UserNumberCoordinate {X = 71, Y = 27},
                new UserNumberCoordinate {X = 86, Y = 27}
            };

            // 레이스, 체크 정보 저장
            int[] bet = new int[1];

            //판돈
            int backGroundMoney = 200000;

            // 덱 초기화
            int user = 0;
            int dealer = 0;

            // 덱 정보 저장
            int saveDealer = 0;
            int saveUser = 0;

            // 랜덤 적용
            int patternIndex = 0;
            string patterns = "";

            while (true)
            {
                //------------------------------------랜더-------------------------------------

                mapRender.MapRender();

                //------------------------------------ProcessInput ----------------------------
                input.Process();

                //------------------------------------update-----------------------------------

                // 메뉴 오브젝트
                switch (Input.key)
                {
                    case ConsoleKey.UpArrow:
                        mapRender.PreY = mapRender.playerY;
                        mapRender.playerY = Math.Max(mapRender.MIN_Y, mapRender.playerY - 3);
                        break;
                    case ConsoleKey.DownArrow:
                        mapRender.PreY = mapRender.playerY;
                        mapRender.playerY = Math.Min(mapRender.playerY + 3, mapRender.MAX_Y);
                        break;
                    case ConsoleKey.Enter:
                        EnterRun();
                        break;

                }

                if(cardDealer.cardIndex == 0)
                {
                    //게임 진행
                    mapRender.money[0] -= backGroundMoney;
                    // 딜러
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    dealer = random.Next(1, 13);
                    GameCoordinate(dealerNumberCoordinate[cardDealer.cardIndex].X, dealerNumberCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(number[dealer]);

                    cardDealer.value[cardDealer.cardIndex] = number[dealer];
                    saveDealer += dealer;
                    ++cardDealer.cardIndex;

                    //유저
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(userFrontCardPatternCoordinate[cardUser.cardIndex].X, userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(userEndCardPatternCoordinate[cardUser.cardIndex].X, userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    user = random.Next(1, 13);
                    GameCoordinate(userNumberCoordinate[cardUser.cardIndex].X, userNumberCoordinate[cardUser.cardIndex].Y);
                    Console.Write(number[user]);

                    cardUser.value[cardUser.cardIndex] = number[user];
                    ++cardUser.cardIndex;
                    saveUser += user;
                }
                // 게임이 끝난 후
                if(cardDealer.cardIndex == 2 && cardUser.cardIndex== 2)
                {
                    if (saveDealer < saveUser && saveUser < 22)
                    {
                        if (1 <= bet[0])
                        {
                            mapRender.money[0] = ((backGroundMoney * 2) * 2) * bet[0];
                        }

                        saveDealer = 0;
                        saveUser = 0;
                    }
                }
            }
            
            void GameCoordinate(int x, int y)
            {
                Console.SetCursorPosition(x, y);
            }

            void EnterRun()
            {

                // 레이스 선택 시
                if (mapRender.playerX + 3 == mapRender.menu[0].X && mapRender.playerY == mapRender.menu[0].Y)
                {
                    mapRender.money[0] = mapRender.money[0] - (backGroundMoney * 2);
                    // 딜러 덱
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    dealer = random.Next(1, 13);
                    GameCoordinate(dealerNumberCoordinate[cardDealer.cardIndex].X, dealerNumberCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(number[dealer]);

                    cardDealer.value[cardDealer.cardIndex] = number[dealer];
                    ++cardDealer.cardIndex;
                    saveDealer += dealer;

                    // 유저 덱
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(userFrontCardPatternCoordinate[cardUser.cardIndex].X, userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(userEndCardPatternCoordinate[cardUser.cardIndex].X, userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    user = random.Next(1, 13);
                    GameCoordinate(userNumberCoordinate[cardUser.cardIndex].X, userNumberCoordinate[cardUser.cardIndex].Y);
                    Console.Write(number[user]);

                    cardUser.value[cardUser.cardIndex] = number[dealer];
                    ++cardUser.cardIndex;
                    saveUser += user;
                    ++bet[0];

                    if (21 < saveUser)
                    {
                        Console.Write("버스트!");
                        Thread.Sleep(5000);
                        return;
                    }

                }


                // 체크 선택시
                if (mapRender.playerX + 3 == mapRender.menu[1].X && mapRender.playerY == mapRender.menu[1].Y)
                {

                    mapRender.money[0] = mapRender.money[0];

                    // 딜러 덱
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    dealer = random.Next(1, 13);
                    GameCoordinate(dealerNumberCoordinate[cardDealer.cardIndex].X, dealerNumberCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(number[dealer]);
                    cardDealer.value[cardDealer.cardIndex] = number[dealer];
                    ++cardDealer.cardIndex;
                    saveDealer += int.Parse(number[dealer]);

                    // 유저 덱
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(userFrontCardPatternCoordinate[cardUser.cardIndex].X, userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(userEndCardPatternCoordinate[cardUser.cardIndex].X, userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    user = random.Next(1, 13);
                    GameCoordinate(userNumberCoordinate[cardUser.cardIndex].X, userNumberCoordinate[cardUser.cardIndex].Y);
                    Console.Write(number[user]);
                    cardUser.value[cardUser.cardIndex] = number[user];
                    ++cardUser.cardIndex;
                    saveUser += user;

                    if (21 < saveUser)
                    {
                        Console.Write("버스트!");
                        Thread.Sleep(5000);
                        return;
                    }

                }

                // 넘겨 선택 시
                if (mapRender.playerX + 3 == mapRender.menu[2].X && mapRender.playerY == mapRender.menu[2].Y)
                {
                    // 딜러 덱
                    patternIndex = random.Next(pattern.Length);
                    patterns = pattern[patternIndex];
                    GameCoordinate(dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(patterns);
                    dealer = random.Next(1, 13);
                    GameCoordinate(dealerNumberCoordinate[cardDealer.cardIndex].X, dealerNumberCoordinate[cardDealer.cardIndex].Y);
                    Console.Write(number[dealer]);
                    cardDealer.value[cardDealer.cardIndex] = number[dealer];
                    ++cardDealer.cardIndex;
                    saveDealer += dealer;

                    // 유저 덱
                    Console.Write(number[0]);
                    saveDealer += 0;
                    saveUser += 0;
                }


                // 포기 선택 시
                if (mapRender.playerX + 3 == mapRender.menu[3].X && mapRender.playerY == mapRender.menu[3].Y)
                {
                    saveDealer = 0;
                    saveUser = 0;
                    return;
                }


            }
        }
            
        
    }
}