namespace ConsoleGame
{
    internal class RenderManager
    {
        public static void RenderObject(int x, int y, char icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }

        public static void RenderTitle()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 10);
            Console.Write("  :::::::::::       :::    :::       ::::::::::        :::    :::       ::::::::       :::::::::       ::::::::       :::::::::: ");
            Console.SetCursorPosition(15, 11);
            Console.Write("     :+:           :+:    :+:       :+:               :+:    :+:      :+:    :+:      :+:    :+:     :+:    :+:      :+:         ");
            Console.SetCursorPosition(15, 12);
            Console.Write("    +:+           +:+    +:+       +:+               +:+    +:+      +:+    +:+      +:+    +:+     +:+             +:+          ");
            Console.SetCursorPosition(15, 13);
            Console.Write("   +#+           +#++:++#++       +#++:++#          +#++:++#++      +#+    +:+      +#++:++#:      +#++:++#++      +#++:++#      ");
            Console.SetCursorPosition(15, 14);
            Console.Write("  +#+           +#+    +#+       +#+               +#+    +#+      +#+    +#+      +#+    +#+            +#+      +#+            ");
            Console.SetCursorPosition(15, 15);
            Console.Write(" #+#           #+#    #+#       #+#               #+#    #+#      #+#    #+#      #+#    #+#     #+#    #+#      #+#             ");
            Console.SetCursorPosition(15, 16);
            Console.Write("###           ###    ###       ##########        ###    ###       ########       ###    ###      ########       ##########       ");
            Console.SetCursorPosition(65, 21);
            Console.Write("         Start");
            Console.SetCursorPosition(65, 22);
            Console.Write("         Exit");
        }

        public static void RenderStory()
        {
            Console.SetCursorPosition(1, 1);
            Console.Write("서기 2032년.. 세계는 짜이호 대마왕의 폭정아래 핍박받고 있");
            Console.SetCursorPosition(1, 2);
            Console.Write("다.. 그를 물리칠 방법은 오직 도시의 뒷골목 카드가게에서 ");
            Console.SetCursorPosition(1, 3);
            Console.Write("Mino 카드를 뽑는 것이다. 경마장에서 돈을 모아 카드가게에");
            Console.SetCursorPosition(1, 4);
            Console.Write("서 카드를 구매해 절망에 빠진 이 세계를 구해주세요..");

        }

        public static void RenderGameExit()
        {
            Console.Clear();
            Console.SetCursorPosition(44, 10);
            Console.Write("    ####   #####   #####     #   #   ####    #    #   ##         ");
            Console.SetCursorPosition(44, 11);
            Console.Write("   #       #       #          # #   #    #   #    #   ##         ");
            Console.SetCursorPosition(44, 12);
            Console.Write("   #####   ####    ####        #    #    #   #    #   ##         ");
            Console.SetCursorPosition(44, 13);
            Console.Write("       #   #       #           #    #    #   #    #              ");
            Console.SetCursorPosition(44, 14);
            Console.Write("   ####    #####   #####       #     ####     ####    ##         ");
        }

        public static void ShowBackDialog()
        {
            Console.SetCursorPosition(82, 13);
            Console.Write("┌─────────────────────────────┐");
            Console.SetCursorPosition(82, 14);
            Console.Write("│마을로 돌아가시겠습니까?(y/n)│");
            Console.SetCursorPosition(82, 15);
            Console.Write("│>>                           │");
            Console.SetCursorPosition(82, 16);
            Console.Write("└─────────────────────────────┘");

        }

        public static void ShowRank(Horse[] horses, int playerChoice)
        {
            bool[] isPrintedLine = new bool[11];
            Console.SetCursorPosition(1, 14);
            Console.Write("Rank");

            Horse[] newHorses = horses.OrderBy(x => x.Rank).ToArray();

            for (int idx = 0; idx < 10; ++idx)
            {
                if (newHorses[idx].Id == playerChoice)
                {
                    ConsoleColor prev = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(1, 15 + idx);
                    Console.Write($"{newHorses[idx].Rank, 3}등 {newHorses[idx].Id, 3}번마 {newHorses[idx].HorseSpeed * RandomManager.GetInctance.Next(10, 15), 2}km/h");
                    Console.ForegroundColor = prev;
                }
                else
                {
                    Console.SetCursorPosition(1, 15 + idx);
                    Console.Write($"{newHorses[idx].Rank, 3}등 {newHorses[idx].Id, 3}번마 {newHorses[idx].HorseSpeed * RandomManager.GetInctance.Next(10, 15), 2}km/h");
                }
            }
        }

        public static void ShowState(Player player)
        {
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y);
            Console.Write($"PlayerDirection : {player.Dir}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 1);
            Console.Write($"Player X : {player.X}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 2);
            Console.Write($"Player Y : {player.Y}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 3);
            Console.Write($"Player Money : {Player.Money}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 5);
            Console.Write("<Card Collection>");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 6);
            Console.Write($"Mino   : {GameManager.Cards[0]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 7);
            Console.Write($"Choi   : {GameManager.Cards[1]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 8);
            Console.Write($"YJK    : {GameManager.Cards[2]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 9);
            Console.Write($"Galaxy : {GameManager.Cards[3]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 10);
            Console.Write($"Eom    : {GameManager.Cards[4]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 11);
            Console.Write($"HSJ    : {GameManager.Cards[5]}");
            Console.SetCursorPosition(Constants.TOWN_MAX_X + 3, Constants.TOWN_MIN_Y + 12);
            Console.Write($"Doik   : {GameManager.Cards[6]}");


        }

        public static void ShowGachaResult(Card card)
        {
            Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y);
            Console.Write($"{card.Type}카드를 뽑으셨습니다!");
            Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y + 1);
            Console.Write("계속하시려면 엔터를 눌러주세요.");
            Console.ReadLine();
        }

        public static void ShowCardCollection(Card[] cards, int[] cardCounts)
        {
            Console.SetCursorPosition(56, 14);
            Console.Write("<Card Collection>");
            Console.SetCursorPosition(56, 15);
            Console.Write($"F1 : {cards[0].Type, -6}  : {cardCounts[0], 2}");
            Console.SetCursorPosition(56, 16);
            Console.Write($"F2 : {cards[1].Type, -5}   : {cardCounts[1], 2}");
            Console.SetCursorPosition(56, 17);
            Console.Write($"F3 : {cards[2].Type, -5}   : {cardCounts[2], 2}");
            Console.SetCursorPosition(56, 18);
            Console.Write($"F4 : {cards[3].Type, -5}  : {cardCounts[3], 2}");
            Console.SetCursorPosition(56, 19);
            Console.Write($"F5 : {cards[4].Type, -5}   : {cardCounts[4], 2}");
            Console.SetCursorPosition(56, 20);
            Console.Write($"F6 : {cards[5].Type, -5}   : {cardCounts[5], 2}");
            Console.SetCursorPosition(56, 21);
            Console.Write($"F7 : {cards[6].Type, -5}   : {cardCounts[6], 2}");
        }

        public static void ShowCardEffect(ConsoleKey key)
        {
            if (key == ConsoleKey.F1)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<Mino> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("이진수를 조작하는 이진세계를");
                Console.SetCursorPosition(70, 11);
                Console.Write("시전해 응원하는 말의 위치를");
                Console.SetCursorPosition(70, 12);
                Console.Write("전진시킨다.");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F2)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<Choi> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("응원하는 말에게 갈!!을 시전해");
                Console.SetCursorPosition(70, 11);
                Console.Write("말의 속도를 비약적으로 상승");
                Console.SetCursorPosition(70, 12);
                Console.Write("시킨다.");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F3)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<YJK> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("Solo교 교주의 정수가 담긴 카드.");
                Console.SetCursorPosition(70, 11);
                Console.Write("솔로 천국 커플 지옥을 외치며");
                Console.SetCursorPosition(70, 12);
                Console.Write("말에게 용기를 북돋아 준다.");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F4)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<Galaxy> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("응원하는 말에게 프레임을");
                Console.SetCursorPosition(70, 11);
                Console.Write("씌워 말의 속도를 약간 ");
                Console.SetCursorPosition(70, 12);
                Console.Write("올려준다.");
                Console.SetCursorPosition(70, 15);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F5)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<HSJ> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("부끄러운 소리를 외치며 응원");
                Console.SetCursorPosition(70, 11);
                Console.Write("하는 말을 제외한 다른말들을");
                Console.SetCursorPosition(70, 12);
                Console.Write("뒷걸음치게 한다.");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F6)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<Eom> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("엄..");
                Console.SetCursorPosition(70, 11);
                Console.Write("준..");
                Console.SetCursorPosition(70, 12);
                Console.Write("식..");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }
            if (key == ConsoleKey.F7)
            {
                Console.SetCursorPosition(70, 8);
                Console.Write("<Doik> 카드");
                Console.SetCursorPosition(70, 10);
                Console.Write("응원하는 말에게 군인 정신을");
                Console.SetCursorPosition(70, 11);
                Console.Write("주입해 속도를 아주약간 상승");
                Console.SetCursorPosition(70, 12);
                Console.Write("시킨다.");
                Console.SetCursorPosition(70, 14);
                Console.Write("Enter를 눌러 진행.");
                Console.ReadLine();
            }

        }

        public static void ShowGameManual()
        {
            Console.SetCursorPosition(70, 1);
            Console.Write("===========조작법============");
            Console.SetCursorPosition(70, 2);
            Console.Write("← → ↑ ↓ : 이동");
            Console.SetCursorPosition(70, 3);
            Console.Write("spacebar : NPC 말걸기");
            Console.SetCursorPosition(70, 4);
            Console.Write("Enter : 선택");
            Console.SetCursorPosition(70, 5);
            Console.Write("F1 ~ 7 : 카드효과 보기.");
            Console.SetCursorPosition(70, 6);
            Console.Write("=============================");
            Console.SetCursorPosition(70, 7);
            Console.Write("==========카드효과===========");
        }

    }

    
}
