using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJK.Objects
{
    public enum NPCKind
    {
        Chief,
        RecoveringMerchant,
        MaxHPMerchant,
        ATKMerchant,
        DEFMerchant
    }
    public class VillageNPC
    {
        public int X;
        public int Y;

        public static void Render(VillageNPC[] villageNPCs)
        {
            Game.ObjRender(villageNPCs[(int)NPCKind.Chief].X, villageNPCs[(int)NPCKind.Chief].Y, "☺", ConsoleColor.DarkYellow);
            Game.ObjRender(villageNPCs[(int)NPCKind.RecoveringMerchant].X, villageNPCs[(int)NPCKind.RecoveringMerchant].Y, "☺", ConsoleColor.DarkRed);
            Game.ObjRender(villageNPCs[(int)NPCKind.MaxHPMerchant].X, villageNPCs[(int)NPCKind.MaxHPMerchant].Y, "☺", ConsoleColor.Cyan);
            Game.ObjRender(villageNPCs[(int)NPCKind.ATKMerchant].X, villageNPCs[(int)NPCKind.ATKMerchant].Y, "☺", ConsoleColor.DarkGreen);
            Game.ObjRender(villageNPCs[(int)NPCKind.DEFMerchant].X, villageNPCs[(int)NPCKind.DEFMerchant].Y, "☺", ConsoleColor.DarkBlue);
        }
        public static void GetBeginnerSupport(Player player)
        {
            player.MaxHP += 90;
            player.CurrentHP += 90;
            player.ATK += 9;
            player.DEF += 9;
            player.Money += 100;
        }
        public static void BuyRecovering(Player player)
        {
            if (player.Money >= 10 && player.CurrentHP != player.MaxHP)
            {
                player.Money -= 10;
                player.CurrentHP += 100;
                if (player.CurrentHP > player.MaxHP)
                {
                    player.CurrentHP = player.MaxHP;
                }
            }
        }
        public static void BuyMaxHP(Player player)
        {
            if (player.Money >= 20)
            {
                player.Money -= 20;
                player.MaxHP += 10;
                if (player.MaxHP > 999)
                {
                    player.MaxHP = 999;
                }
            }
        }
        public static void BuyATK(Player player)
        {
            if (player.Money >= 50)
            {
                player.Money -= 50;
                player.ATK += 1;
                if (player.ATK > 999)
                {
                    player.ATK = 999;
                }
            }
        }
        public static void BuyDEF(Player player)
        {
            if (player.Money >= 50)
            {
                player.Money -= 50;
                player.DEF += 1;
                if (player.DEF > 999)
                {
                    player.DEF = 999;
                }
            }
        }
    }
}