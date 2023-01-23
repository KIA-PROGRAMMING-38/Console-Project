using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public enum Select
    {
        Yes = 18,
        No = 19,
    }
    public class SelectCursor
    {
        public int X;
        public int Y;
        public int PastY;
        public bool On;
        public static class Function
        {
            public static void Render(SelectCursor selectCursor, Player player)
            {
                if (selectCursor.On && true == player.IsOnBattle)
                {
                    Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                    Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
                }
                else if (selectCursor.On && false == player.IsOnBattle)
                {
                    Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                    Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
                    Game.Function.ObjRender(selectCursor.X + 2, Game.DialogCursor_Y, "Yes", ConsoleColor.Black);
                    Game.Function.ObjRender(selectCursor.X + 2, Game.DialogCursor_Y + 1, "No", ConsoleColor.Black);
                }
                else
                {
                    Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                    Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.White);
                    Game.Function.ObjRender(selectCursor.X + 2, Game.DialogCursor_Y, "Yes", ConsoleColor.White);
                    Game.Function.ObjRender(selectCursor.X + 2, Game.DialogCursor_Y + 1, "No", ConsoleColor.White);
                }
            }
            public static void Move(SelectCursor selectCursor, Player player)
            {
                if (selectCursor.On)
                {
                    if (player.IsOnBattle)
                    {
                        if (selectCursor.Y == Game.BattleCursor_Y && Input.IsKeyDown(ConsoleKey.DownArrow))
                        {
                            selectCursor.PastY = selectCursor.Y;
                            ++selectCursor.Y;
                        }
                        if (selectCursor.Y == Game.BattleCursor_Y + 1 && Input.IsKeyDown(ConsoleKey.UpArrow))
                        {
                            selectCursor.PastY = selectCursor.Y;
                            --selectCursor.Y;
                        }
                    }
                    else
                    {

                        if (selectCursor.Y == Game.DialogCursor_Y && Input.IsKeyDown(ConsoleKey.DownArrow))
                        {
                            selectCursor.PastY = selectCursor.Y;
                            ++selectCursor.Y;
                        }
                        if (selectCursor.Y == Game.DialogCursor_Y + 1 && Input.IsKeyDown(ConsoleKey.UpArrow))
                        {
                            selectCursor.PastY = selectCursor.Y;
                            --selectCursor.Y;
                        }
                    }
                }
            }
            public static bool SelectYes(SelectCursor selectCursor)
            {
                if (selectCursor.Y == (int)Select.Yes && (Input.IsKeyDown(ConsoleKey.Enter) || Input.IsKeyDown(ConsoleKey.Spacebar)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool SelectNo(SelectCursor selectCursor)
            {
                if (selectCursor.Y == (int)Select.No && (Input.IsKeyDown(ConsoleKey.Enter) || Input.IsKeyDown(ConsoleKey.Spacebar)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
