using ProjectJK.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.UI
{
    public class UIState
    {
        public static void Render(Player player)
        {
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.Level_EXP_Battle_Y + 1, $" {player.Level:D3}", ConsoleColor.Black);
            Game.Function.ObjRender(Game.EXP_X, Game.Level_EXP_Battle_Y + 1, $" {player.CurrentEXP:D3}/{player.MaxEXP:D3}", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.HP_STATUS_Y + 1, $" {player.CurrentHP:D3}/{player.MaxHP:D3}", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Status_X, Game.HP_STATUS_Y + 1, $" ATK: {player.ATK:D3}", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Status_X, Game.HP_STATUS_Y + 2, $" DEF: {player.DEF:D3}", ConsoleColor.Black);
            Game.Function.ObjRender(Game.Level_HP_Money_X, Game.Money_STATUS_Y + 1, $" {player.Money:D8} G", ConsoleColor.Black);
        }
    }
}
