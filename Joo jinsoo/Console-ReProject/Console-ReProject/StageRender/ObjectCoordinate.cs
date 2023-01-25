using Console_ReProject.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_ReProject.Map
{
    class ObjectCoordinate
    {
        public const int INITIAL_HINT = 3;

        #region 플레이어 초기좌표 저장
        public static string[] LoadPlayer()
        {
            string stageFilePath = Path.Combine("Assets", "Coordinate", "PlayerCoordinate.txt");

            if (false == File.Exists(stageFilePath))
            {
                Console.WriteLine($"파일이 없습니다. PlayerCoordinate.txt");
            }

            return File.ReadAllLines(stageFilePath);
        }

        public static void ParsePlayer(string[] playerExist, out Player player)
        {

            string[] stageMetadata = playerExist[0].Split(" ");
            player = null;

            for (int y = 1; y < playerExist.Length; ++y)
            {
                for (int x = 0; x < playerExist[y].Length; ++x)
                {
                    switch (playerExist[y][x])
                    {
                        case ObjectSymbol.player:
                            player = new Player { X = x, Y = y - 1 };

                            break;

                        case ' ':

                            break;
                    }
                }
            }
        }
        #endregion

        // 시작 힌트 좌표 무작위 저장
        public static int[] InitialRandomHint()
        {
            Random random = new Random();
            const int INTERACTIVE_FIELDS = 12;
            int[] outputArray = new int[INITIAL_HINT];

            for (int hintID = 0; hintID < INITIAL_HINT;)
            {
                int randomNumber = random.Next(0, INTERACTIVE_FIELDS);
                bool checkNum = true;

                for (int alreadyInputCheck = 0; alreadyInputCheck < INITIAL_HINT; ++alreadyInputCheck)
                {
                    if (outputArray[alreadyInputCheck] == randomNumber)
                    {
                        checkNum = false;
                        break;
                    }
                }

                if (checkNum)
                {
                    outputArray[hintID] = randomNumber;
                    ++hintID;
                }
                
            }

            return outputArray;
        }

 
    }
}
