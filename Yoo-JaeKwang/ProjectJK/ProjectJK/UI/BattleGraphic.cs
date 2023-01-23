using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI
{
    public class BattleGraphic
    {
        public static void Clear()
        {
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 1, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 2, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 3, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 4, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 5, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 6, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 7, $"                         ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 10, Game.BattleCursor_Y, $"              ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 10, Game.BattleCursor_Y + 1, $"              ", ConsoleColor.Black);
        }
        public static void Slime(Slime slime)
        {
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 1, $"               ■■■       ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 2, $"        ■■■■■■■   ■      ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 3, $"      ■            ■     ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 4, $"     ■              ■    ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 5, $"     ■   ■   ■      ■    ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 6, $"      ■            ■     ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Battle_X, Game.Level_EXP_Battle_Y + 7, $"       ■■■■■■■■■■■       ", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 11, Game.BattleCursor_Y, $"SlimeHP", ConsoleColor.Black);
            Game.Function.ObjRender(Game.BattleCursor_X + 11, Game.BattleCursor_Y + 1, $"{slime.CurrentHP:D3} / {slime.MaxHP:D3}", ConsoleColor.Black);
        }
    }
}
