using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame._05_Components
{
    public class Menu
    {
        public enum MENU_TYPE
        {
            Title,
            Continue,
        }

        public Menu() { }
        MENU_TYPE currentType;
        private bool _flag;

        public bool IsUiOpened() => _flag;
        public void SetUiOpen() => _flag = true;

        public void Start()
        {
            currentType  = MENU_TYPE.Title;
            _flag = false;
        }

        public void Update()
        {
            if (_flag == false && InputManager.Instance.IsKeyDown(ConsoleKey.Escape))
            {
                _flag = true;
                InputManager.Instance.ResetKey();
            }
            if (_flag)
            {
                if (InputManager.Instance.IsKeyDown(ConsoleKey.UpArrow) && currentType == MENU_TYPE.Continue)
                {
                    currentType = MENU_TYPE.Title;
                }
                if (InputManager.Instance.IsKeyDown(ConsoleKey.DownArrow) && currentType == MENU_TYPE.Title)
                {
                    currentType = MENU_TYPE.Continue;
                }
                if (InputManager.Instance.IsKeyDown(ConsoleKey.Enter))
                {
                    switch(currentType)
                    {
                        case MENU_TYPE.Title:
                            SceneManager.Instance.ChangeFlagOn("TitleScene");
                            break;
                        case MENU_TYPE.Continue:
                            _flag = false;
                            break;
                    }
                }
            }
        }

        public void Render()
        {
            if (_flag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 8);
                Console.Write("┌─────────────────┐");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 7);
                Console.Write("│                 │");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 6);
                Console.Write("└─────────────────┘");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(10, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 7);
                Console.Write("메 뉴");
                switch (currentType)
                {
                    case MENU_TYPE.Title:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 5);
                        Console.Write("┌─────────────────┐");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 4);
                        Console.Write("       Title");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 3);
                        Console.Write("└─────────────────┘");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 2);
                        Console.Write("┌─────────────────┐");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 1);
                        Console.Write("      Continue   ");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2);
                        Console.Write("└─────────────────┘");
                        break;
                    case MENU_TYPE.Continue:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 5);
                        Console.Write("┌─────────────────┐");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 4);
                        Console.Write("       Title");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 3);
                        Console.Write("└─────────────────┘");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 2);
                        Console.Write("┌─────────────────┐");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 1);
                        Console.Write("      Continue   ");
                        Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2);
                        Console.Write("└─────────────────┘");
                        break;
                }
            }
            else
            {
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 8);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 7);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 6);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 5);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 4);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 3);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 2);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2 - 1);
                Console.Write("                       ");
                Console.SetCursorPosition(3, GameDataManager.MAP_MIN_Y + GameDataManager.MAP_HEIGHT / 2);
                Console.Write("                       ");
            }
            Console.ForegroundColor = GameDataManager.DEFAULT_FOREGROUND_COLOR;
        }

    }
}
