using Console_ReProject.Object;
using Console_ReProject.StageRender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_ReProject
{
    enum Message 
    { 
        VictimPicture,
        Prologue,
        Clear
    }

    enum MapIcon
    {
        Default,
        Player,
        Wall,
        Bed,
        Toilet,
        Utility,
        Living01,
        Living02,
        Living03,
        FrontDoor
    }

    class Game
    {
        public const int CRIME_EVIDENCE = 9;
        public const int JOB_COUNTS = 18;
        public const int WEAPON_COUNTS = 10;
        public const int MOTIVE_COUNTS = 9;

        // 용의자 로드
        public static string[] LoadJob(int jobNumber)
        {
            string jobFilePath = Path.Combine("Assets", "MurderIncident", "Job", $"Job{jobNumber:D2}.txt");

            if (false == File.Exists(jobFilePath))
            {
                Console.WriteLine($"용의자 파일이 없습니다. 파일 번호{jobNumber}.txt");
            }

            return File.ReadAllLines(jobFilePath);
        }

        // 살해 도구 로드
        public static string[] LoadWeapon(int WeaponNumber)
        {
            string weaponFilePath = Path.Combine("Assets", "MurderIncident", "Weapon", $"Weapon{WeaponNumber:D2}.txt");

            if (false == File.Exists(weaponFilePath))
            {
                Console.WriteLine($"살해 도구 파일이 없습니다. 파일 번호{WeaponNumber}.txt");
            }

            return File.ReadAllLines(weaponFilePath);
        }

        // 살해 동기 로드
        public static string[] LoadMotive(int motiveNumber)
        {
            string motiveFilePath = Path.Combine("Assets", "MurderIncident", "Motive", $"Motive{motiveNumber:D2}.txt");

            if (false == File.Exists(motiveFilePath))
            {
                Console.WriteLine($"살해 동기 파일이 없습니다. 파일 번호{motiveNumber}.txt");
            }

            return File.ReadAllLines(motiveFilePath);
        }

        // 문서 파싱
        public static string OutputTextToFirstLine(string[] file)
        {
            string outputText = file[0];
            return outputText;
        }
        public static string OutputTextToSecondLine(string[] file)
        {
            string outputText = file[1];
            return outputText;
        }
        public static string OutputTextToThirdLine(string[] file)
        {
            string outputText = file[2];
            return outputText;
        }

        // 용의자 추리기
        public static int[] RandomPickJob(int pickCount)
        {
            Random random = new Random();
            int[] outputArray = new int[pickCount];

            for (int checkJob = 0; checkJob < CRIME_EVIDENCE;)
            {
                bool checkNum = true;
                int randomnumber = random.Next(1, JOB_COUNTS + 1);

                for (int alreadyInputCheck = 0; alreadyInputCheck < CRIME_EVIDENCE; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomnumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[checkJob] = randomnumber;
                    ++checkJob;
                }
            }

            return outputArray;
        }

        // 도구 추리기
        public static int[] RandomPickWeapon(int pickCount)
        {
            Random random = new Random();
            int[] outputArray = new int[pickCount];

            for (int checkWeapon = 0; checkWeapon < CRIME_EVIDENCE;)
            {
                bool checkNum = true;
                int randomnumber = random.Next(1, WEAPON_COUNTS + 1);

                for (int alreadyInputCheck = 0; alreadyInputCheck < CRIME_EVIDENCE; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomnumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[checkWeapon] = randomnumber;
                    ++checkWeapon;
                }
            }

            return outputArray;
        }

        // 동기 추리기
        public static int[] RandomPickMotive(int pickCount)
        {
            Random random = new Random();
            int[] outputArray = new int[pickCount];

            for (int checkMotive = 0; checkMotive < CRIME_EVIDENCE;)
            {
                bool checkNum = true;
                int randomnumber = random.Next(1, MOTIVE_COUNTS + 1);

                for (int alreadyInputCheck = 0; alreadyInputCheck < CRIME_EVIDENCE; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomnumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[checkMotive] = randomnumber;
                    ++checkMotive;
                }
            }

            return outputArray;
        }

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

        public static int[] MixedHintString(int inputNumber)
        {
            Random random = new Random();
            int[] outputArray = new int[inputNumber];
               
            for (int checkIndex = 0; checkIndex < inputNumber;)
            {
                bool checkNum = true;
                int randomNumber = random.Next(1, inputNumber + 1);

                for (int alreadyInputCheck = 0; alreadyInputCheck < inputNumber; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomNumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[checkIndex] = randomNumber;
                    ++checkIndex;
                }
            }

            return outputArray;

        }

        // 정답 확정
        public static int correctAnswer(int[] inputArray)
        {
            Random random = new Random();
            int randomnumber = random.Next(9);

            int returnNumber = inputArray[randomnumber];

            return returnNumber;
        }

        public static void ObjectRender(int x, int y, string icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }

        public static string[] LoadMessage(int messageNumber)
        {
            string stageFilePath = Path.Combine("Assets", "InGameMessage", $"Message{messageNumber:D2}.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"메시지 파일이 없습니다. 메시지 번호{messageNumber}.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static string[] LoadBadEndMessage()
        {
            string messageFilePath = Path.Combine("Assets", "InGameMessage", $"BadEndingMessage.txt");

            if (false == File.Exists(messageFilePath))
            {
                Console.WriteLine($"메시지 파일이 없습니다.");
            }

            return File.ReadAllLines(messageFilePath);
        }

        public static string[] LoadGoodEndMessage()
        {
            string messageFilePath = Path.Combine("Assets", "InGameMessage", $"GoodEndingMessage.txt");

            if (false == File.Exists(messageFilePath))
            {
                Console.WriteLine($"메시지 파일이 없습니다.");
            }

            return File.ReadAllLines(messageFilePath);
        }

        public static string[] LoadTrueEndMessage()
        {
            string messageFilePath = Path.Combine("Assets", "InGameMessage", $"TrueEndingMessage.txt");

            if (false == File.Exists(messageFilePath))
            {
                Console.WriteLine($"메시지 파일이 없습니다.");
            }

            return File.ReadAllLines(messageFilePath);
        }
    }
}
