
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleSoloProject
{
    internal class Item
    {
        public static bool itemFriendOnOff = false;
        public static int friendX = 12;
        public static int friendY = 9;


        public static string friendIcon = "=";
        public static string friendMessage = "축하합니다.동료뽑기에 당첨되셨습니다.";


        public static bool itemPower = false;
        public static int itemPowerX = 42;
        public static int itemPowerY = 9;
        public static string itemPowerMessage = "축하합니다. 공격력증가 뽑기에 당첨되셨습니다.";

        public static int playerWeaponY = 3;
        public static int playerFire = 42;


        public static void FriendText()
        {
            if (itemFriendOnOff)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Text.Position(friendX, friendY, friendIcon);
                Console.ForegroundColor = ConsoleColor.Blue;
                Text.Position(Text.itemTextX, Text.itemTextY, "동료");
            }
            if (Item.itemPower)
            {
                Text.Position(Text.itemTextX, Text.itemTextY + 1, "공격력 증가");
            }
        }

        public static void randomItem(Random random)
        {
            if (Upgrade.Gold >= 5)
            {
                Upgrade.Gold -= 5;

                if (itemFriendOnOff == false || itemPower == false)
                {
                    if (random.Next(250) < 5)
                    {
                        itemFriendOnOff = true;

                        Thread.Sleep(1000 / 3);
                        Text.Position(Text.itemMessageX, Text.itemMessageY, friendMessage);
                    }
                    else if (random.Next(250) < 8)
                    {
                        itemPower = true;
                        Thread.Sleep(1000 / 3);
                        Text.Position(Text.itemMessageX, Text.itemMessageY, itemPowerMessage);
                    }
                    else
                    {
                        Text.Position(Text.itemMessageX+5, Text.itemMessageY, $"꽝 입니다!");

                    }
                }
            }

            else
            {
                Text.Position(Text.goldTextX, Text.goldTextY, $"아이템 뽑기에 사용할 {Upgrade.goldErrorText}");
            }
        }

    }


}