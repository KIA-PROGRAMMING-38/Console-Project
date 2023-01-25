using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectJK.Objects
{
    public enum Select
    {
        Attack = 10,
        Escape = 11,
        Yes = 18,
        No = 19,
    }
    public class SelectCursor
    {
        public int X;
        public int Y;
        public int PastY;
        public static class Function
        {
            public static void Render(SelectCursor selectCursor, Player player)
            {
                if (false == player.CanMove)
                {
                    if (true == player.IsOnBattle)
                    {

                        Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                        Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
                        Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y, "Attack", ConsoleColor.Black);
                        Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y + 1, "Escape", ConsoleColor.Black);
                    }

                    else
                    {
                        Game.Function.ObjRender(selectCursor.X, selectCursor.PastY, ">", ConsoleColor.White);
                        Game.Function.ObjRender(selectCursor.X, selectCursor.Y, ">", ConsoleColor.Black);
                        Game.Function.ObjRender(Game.DialogCursor_X + 2, Game.DialogCursor_Y, "Yes", ConsoleColor.Black);
                        Game.Function.ObjRender(Game.DialogCursor_X + 2, Game.DialogCursor_Y + 1, "No", ConsoleColor.Black);
                    }
                }
                else
                {
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y, ">", ConsoleColor.White);
                    Game.Function.ObjRender(Game.DialogCursor_X, Game.DialogCursor_Y + 1, ">", ConsoleColor.White);
                    Game.Function.ObjRender(Game.BattleCursor_X, Game.BattleCursor_Y, ">", ConsoleColor.White);
                    Game.Function.ObjRender(Game.BattleCursor_X, Game.BattleCursor_Y + 1, ">", ConsoleColor.White);
                    Game.Function.ObjRender(Game.DialogCursor_X + 2, Game.DialogCursor_Y, "Yes", ConsoleColor.White);
                    Game.Function.ObjRender(Game.DialogCursor_X + 2, Game.DialogCursor_Y + 1, "No", ConsoleColor.White);
                    Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y, "Attack", ConsoleColor.White);
                    Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y + 1, "Escape", ConsoleColor.White);

                }
            }
            public static void Move(SelectCursor selectCursor, Player player)
            {
                if (false == player.CanMove)
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
            public static bool SelectAttack(SelectCursor selectCursor)
            {
                if (selectCursor.Y == (int)Select.Attack && (Input.IsKeyDown(ConsoleKey.Enter) || Input.IsKeyDown(ConsoleKey.Spacebar)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool SelectEscape(SelectCursor selectCursor)
            {
                if (selectCursor.Y == (int)Select.Escape && (Input.IsKeyDown(ConsoleKey.Enter) || Input.IsKeyDown(ConsoleKey.Spacebar)))
                {
                    return true;
                }
                else
                {
                    return false;
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
