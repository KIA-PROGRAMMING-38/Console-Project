using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Title
    {
        public void TitleSceen()
        {
            Console.Title = "블랙잭";
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            const int titleShapeX = 60;

            while (true)
            {
                Console.CursorVisible = false;
                for (int titleId = 0; titleId < titleShapeX; ++titleId)
                {
                    TitleRender(titleShapeX + titleId, 3, "#");
                    TitleRender(titleShapeX + titleId, 20, "#");
                    TitleRender(titleShapeX, 8, "______  _               _       ___               _    ");
                    TitleRender(titleShapeX, 9, "| ___ \\| |             | |     |_  |             | |   ");
                    TitleRender(titleShapeX, 10, "| |_/ /| |  __ _   ___ | | __    | |  __ _   ___ | | __");
                    TitleRender(titleShapeX, 11, "| ___ \\| | / _` | / __|| |/ /    | | / _` | / __|| |/ /");
                    TitleRender(titleShapeX, 12, "| |_/ /| || (_| || (__ |   < /\\__/ /| (_| || (__ |   < ");
                    TitleRender(titleShapeX, 13, "\\____/ |_| \\__,_| \\___||_|\\_\\\\____/  \\__,_| \\___||_|\\_\\");
                }

                TitleRender(titleShapeX, 30, "▶ 게임을 시작하려면 Yes를 입력 : ");
                string gameStart = Console.ReadLine();
                switch (gameStart)
                {
                    case "yes":
                        return;
                    default:
                        Console.Clear();
                        Console.Write("게임을 종료합니다.");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void TitleRender(int x, int y, string titleObject)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(titleObject);
        }
    }
}
