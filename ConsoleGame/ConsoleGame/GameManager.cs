using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{

    internal class GameManager
    {
        private static int[] _cards = { 0, 0, 0, 0, 0, 0, 0 };
        private static int _totalWeight = 1000;

        private static Card[] _cardsTable = new Card[(int)CardType.MAX]
        {
            new Card {Type = CardType.Mino,     Weight = 10},
            new Card {Type = CardType.Choi,     Weight = 90},
            new Card {Type = CardType.YJK,      Weight = 100},
            new Card {Type = CardType.Galaxy,   Weight = 100},
            new Card {Type = CardType.Eom,      Weight = 100},
            new Card {Type = CardType.HSJ,      Weight = 200},
            new Card {Type = CardType.Doik,     Weight = 400}
        };

        public static int TotalWeight
        {
            get
            {
                return _totalWeight;
            }
            set
            {
                _totalWeight = value;
            }
        }

        public static int[] Cards
        {
            get
            {
                return _cards;
            }
            set
            {
                _cards = value;
            }
        }

        public static Card[] CardsTable
        {
            get
            {
                return _cardsTable;
            }
            set
            {
                _cardsTable = value;
            }
        }

        public static void ExitWithError(string msg)
        {
            Console.Clear();
            Console.Write(msg);
            Environment.Exit(0);
        }

        public static void Initialize()
        {
            Console.ResetColor();
            Console.Title = "The Horse";
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static Card Gacha(Card[] cards)
        {
            Card card = null;
            int weight = 0;
            int selectedNumber = (int)(1 + RandomManager.GetInctance.NextDouble() * (GameManager.TotalWeight - 1) + 0.5);
            for (int i = 0; i < cards.Length; ++i)
            {
                weight += cards[i].Weight;
                if (selectedNumber <= weight)
                {
                    card = cards[i];
                    break;
                }
            }

            return card;
        }

        public static int ChoiceHorse()
        {
            while (true)
            {
                Console.SetCursorPosition(25, 14);
                Console.Write("몇번말에 거시겠습니까?");
                Console.SetCursorPosition(25, 15);
                Console.Write(">> ");
                Console.SetCursorPosition(28, 15);
                string playerChoice = "";
                playerChoice = Console.ReadLine();
                if (playerChoice == "1" || playerChoice == "2" || playerChoice == "3" || playerChoice == "4" || playerChoice == "5"
                    || playerChoice == "6" || playerChoice == "7" || playerChoice == "8" || playerChoice == "9" || playerChoice == "10")
                {
                    int pc = int.Parse(playerChoice);
                    return pc;
                }
                else
                {
                    Console.SetCursorPosition(28, 15);
                    Console.Write("                       ");
                }

            }
        }

        public static void UseCard(ConsoleKey key, Horse[] horses, int playerChoice)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    
                    if (GameManager.Cards[0] != 0)
                    {
                        horses[playerChoice - 1].X += 20;
                        GameManager.Cards[0]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("Mino 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("이.진.세.계 !!");
                        Console.SetCursorPosition(25, 22);
                        Console.Write("후후 그건 제 잔상입니다만..?");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F2:
                    if (GameManager.Cards[1] != 0)
                    {
                        horses[playerChoice - 1].X += 10;
                        GameManager.Cards[1]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("Choi 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("갈!! 놀고있을 시간이 있나?");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F3:
                    if (GameManager.Cards[2] != 0)
                    {
                        GameManager.Cards[2]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("YJK 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("커플 지옥. 솔로 천국.");
                        Console.SetCursorPosition(25, 22);
                        Console.Write("솔로는 지지 않는다!!");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F4:
                    if (GameManager.Cards[3] != 0)
                    {
                        horses[playerChoice - 1].X += 10;
                        GameManager.Cards[3]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("Galaxy 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("개잘뛰네~");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F5:
                    if (GameManager.Cards[4] != 0)
                    {
                        horses[playerChoice - 1].X += 1;
                        GameManager.Cards[4]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("Eom 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("엄..");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F6:
                    if (GameManager.Cards[5] != 0)
                    {
                        GameManager.Cards[5]--;
                        for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
                        {
                            if (horseId != playerChoice - 1)
                            {
                                horses[horseId].X -= 10;
                            }
                        }
                        Console.SetCursorPosition(25, 20);
                        Console.Write("HSJ 카드 발동 !!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("나츠짱 사랑해 !!");
                        Thread.Sleep(500);
                    }
                    break;
                case ConsoleKey.F7:
                    if (GameManager.Cards[6] != 0)
                    {
                        horses[playerChoice - 1].X += 1;
                        GameManager.Cards[6]--;
                        Console.SetCursorPosition(25, 20);
                        Console.Write("Doik 카드 발동!!");
                        Console.SetCursorPosition(25, 21);
                        Console.Write("강한친구 대한육군 !!");
                        Thread.Sleep(500);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void CheckRank(Horse[] horses, bool[] isHorseEnd)
        {
            for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
            {
                if (isHorseEnd[horseId] == true)
                {
                    continue;
                }
                horses[horseId].Rank = 1;
                for (int horseId2 = 0; horseId2 < Constants.HORSE_COUNT; ++horseId2)
                {
                    if (horses[horseId].X < horses[horseId2].X)
                    {
                        ++horses[horseId].Rank;
                    }
                }
            }
        }

        public static void AssignHorseSpeed(bool[] isHorseEnd, Horse[] horses)
        {
            for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
            {
                if (false == isHorseEnd[horseId])
                {
                    if (Constants.RACE_END_LINE - 3 <= horses[horseId].X && horses[horseId].X < Constants.RACE_END_LINE)
                    {
                        horses[horseId].HorseSpeed = 1;
                    }
                    else
                    {
                        horses[horseId].HorseSpeed = RandomManager.GetInctance.Next(1, 4);
                    }
                }
            }
        }

        public static void CalculateHorseDistance(bool[] isHorseEnd, Horse[] horses)
        {
            for (int horseId = 0; horseId < Constants.HORSE_COUNT; ++horseId)
            {
                if (true == isHorseEnd[horseId])
                {
                    continue;
                }
                horses[horseId].X += horses[horseId].HorseSpeed;
                if (horses[horseId].X >= Constants.RACE_END_LINE)
                {
                    horses[horseId].X = Constants.RACE_END_LINE;
                    isHorseEnd[horseId] = true;
                }
            }
        }
    }
}
