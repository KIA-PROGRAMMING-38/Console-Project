using Console_Project_Refactoring.Assets.StageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project_Refactoring
{
    class GameClear
    {


        public static int opportunityCount = 0;

        public static bool[] answerOpportunity = new bool[4];

        public static InteractionObject[] RandomExistHint(int[] answerList)
        {
            const int InteractionObjectLength = 13;
            Random random = new Random();
            InteractionObject[] outputArray = new InteractionObject[InteractionObjectLength];

            for (int checkNum = 0; checkNum < answerList.Length;)
            {
                bool checkBool = true;
                int randomnumber = random.Next(1, InteractionObjectLength);

                for (int alreadyInputCheck = 0; alreadyInputCheck < outputArray.Length; ++alreadyInputCheck)
                {
                    if ((int)outputArray[alreadyInputCheck] == randomnumber)
                    {
                        checkBool = false;
                        break;
                    }
                }

                if (checkBool)
                {
                    outputArray[randomnumber - 1] = (InteractionObject)randomnumber;
                    ++checkNum;
                }
            }

            return outputArray;
        }



        public static void InputAnswer(Player player, Frontdoor[] frontDoor,
            string answerMurderer, string answerWeapon, string answerMotive,
            int answerCount, ConsoleKey inputkey) 
        {
            MapIcon checkData = GameSystem.mapMetaData[player.X, player.Y];


            switch (checkData)
            {
                case MapIcon.Front:
                    Console.Clear();
                player.X = player.pastX;
                player.Y = player.pastY;

                string[] readResultMessage = AnswerReadLine();
                for (int i = 0; i < readResultMessage.Length; ++i)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(readResultMessage[i]);
                }

                string readAnswer1 = Console.ReadLine();
                string readAnswer2 = Console.ReadLine();
                string readAnswer3 = Console.ReadLine();

                if (readAnswer1 == answerMurderer)
                {
                    ++answerCount;
                }
                if (readAnswer2 == answerWeapon)
                {
                    ++answerCount;
                }
                if (readAnswer3 == answerMotive)
                {
                    ++answerCount;
                }
                if (answerCount == 3)
                {
                    if (answerOpportunity[0] == false)
                    {
                        Console.Clear();
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        string[] endString = EndingCredit(0);
                        for (int i = 0; i < endString.Length; ++i)
                        {
                            Console.WriteLine(endString[i]);
                        }

                        inputkey = Console.ReadKey().Key;
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.Clear();
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string[] endString = EndingCredit(1);
                        for (int i = 0; i < endString.Length; ++i)
                        {
                            Console.WriteLine(endString[i]);
                        }

                        inputkey = Console.ReadKey().Key;
                        Environment.Exit(1);
                    }
                }
                else
                {
                    answerOpportunity[opportunityCount] = true;
                    ++opportunityCount;

                    if (opportunityCount == 4)
                    {
                        Console.Clear();
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        string[] endString = EndingCredit(2);
                        for (int i = 0; i < endString.Length; ++i)
                        {
                            Console.WriteLine(endString[i]);
                        }

                        inputkey = Console.ReadKey().Key;
                        Environment.Exit(2);
                    }
                    else
                        Console.WriteLine("잘못된 추리입니다");
                        
                    inputkey = Console.ReadKey().Key;
                }

                break;
            }

     
        }

        public static string[] AnswerReadLine()
        {
            string motiveFilePath = Path.Combine("..\\..\\..\\Assets", "MessageData", $"AnswerReadLine.txt");

            if (false == File.Exists(motiveFilePath))
            {
                Console.WriteLine($"파일이 없습니다. AnswerReadLine.txt");
            }

            return File.ReadAllLines(motiveFilePath);
        }

        public static string[] EndingCredit(int endingNumber)
        {
            string motiveFilePath = Path.Combine("..\\..\\..\\Assets", "MessageData", $"EndingCredit{endingNumber:D2}.txt");

            if (false == File.Exists(motiveFilePath))
            {
                Console.WriteLine($"파일이 없습니다. AnswerReadLine.txt");
            }

            return File.ReadAllLines(motiveFilePath);
        }

    }
}
