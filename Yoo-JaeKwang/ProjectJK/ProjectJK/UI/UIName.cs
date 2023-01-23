using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI
{
    public class UIName
    {
        public static void Render()
        {
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.Level_EXP_Battle_Y, "Level", ConsoleColor.Black);
            Game.Function.ObjRender(Game.EXP_X, Game.Level_EXP_Battle_Y, "EXP", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y, "Battle", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y, "Attack", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 2, Game.BattleCursor_Y + 1, "Escape", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y, "HP", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Status_X, Game.HP_STATUS_Y, "Status", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.Money_Y, "Money", ConsoleColor.Black);
        }
    }
}
