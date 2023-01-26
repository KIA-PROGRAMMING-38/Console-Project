using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoloProject
{
    internal class Upgrade
    {
        public static int X;
        public static int Y;
        public static int Gold = 0;
        public static int Damage = 5;
        public static int Hp = 5;

        public static string Icon = "<";
        public static string Icon2 = " > ";
        public static string damage15 = "€";
        public static string damage30 = "㏄";
        public static string goldErrorText = "골드가 부족합니다.";
        public static string damageUpgradeText = "업그레이드 성공.";


        public void goldUpgrade()
        {
            if (Gold >= 10)
            {
                Gold -= 10;
                Damage++;

                Text.Position(Text.damageTextX, Text.damageTextY, damageUpgradeText);
                // 아이템 획득시 공격력 추가증가
                if(Item.itemPower)
                {
                    Damage += 2;
                }

            }
            else
            {
                Text.Position(Text.goldTextX, Text.goldTextY, goldErrorText);
            }


        }
    }
}

