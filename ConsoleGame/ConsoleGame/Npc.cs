using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    enum NpcType
    {
        None,
        RaceNpc,
        ShopNpc
    }

    internal class Npc
    {
        private int _x;
        private int _y;
        private NpcType _type;

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public NpcType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public void SayToPlayer(NpcType npcType, bool isPlayerChoice, Cursor cursor, ref PlayerState playerState)
        {
            switch(npcType)
            {
                case NpcType.RaceNpc:
                    cursor.X = Constants.TOWN_DIALOG_MIN_X + 11;
                    cursor.Y = Constants.TOWN_DIALOG_MIN_Y + 1;
                    cursor.PrevX = Constants.TOWN_DIALOG_MIN_X + 11;
                    while (true)
                    {
                        if (playerState != PlayerState.Talk)
                        {
                            break;
                        }
                        Console.SetCursorPosition(Constants.TOWN_DIALOG_MIN_X, Constants.TOWN_DIALOG_MIN_Y);
                        Console.Write("안녕하신가!! 경마장으로 갈텐가?");
                        Console.SetCursorPosition(Constants.TOWN_DIALOG_MIN_X, Constants.TOWN_DIALOG_MIN_Y + 1);
                        Console.Write("[1] 간다.");
                        Console.SetCursorPosition(Constants.TOWN_DIALOG_MIN_X, Constants.TOWN_DIALOG_MIN_Y + 2);
                        Console.Write("[2] 안간다.");

                        RenderManager.RenderObject(cursor.PrevX, cursor.PrevY, ' ');
                        RenderManager.RenderObject(cursor.X, cursor.Y, '◀');
                        ConsoleKey key = Console.ReadKey().Key;

                        if (key == ConsoleKey.UpArrow)
                        {
                            cursor.PrevY = cursor.Y;
                            cursor.Y = Math.Clamp(cursor.Y - 1, Constants.TOWN_DIALOG_MIN_Y + 1, Constants.TOWN_DIALOG_MIN_Y + 2);
                        }
                        if (key == ConsoleKey.DownArrow)
                        {
                            cursor.PrevY = cursor.Y;
                            cursor.Y = Math.Clamp(cursor.Y + 1, Constants.TOWN_DIALOG_MIN_Y + 1, Constants.TOWN_DIALOG_MIN_Y + 2);
                        }

                        if (key == ConsoleKey.Enter)
                        {
                            if (cursor.Y == Constants.TOWN_DIALOG_MIN_Y + 1)
                            {
                                SceneManager._sceneType = Scene.RaceTrack;
                                playerState = PlayerState.Idle;
                                break;
                            }
                            else
                            {
                                playerState = PlayerState.Idle;
                                break;
                            }
                        }
                    }
                    break;
                case NpcType.ShopNpc:
                    cursor.X = Constants.SHOP_DIALOG_MIN_X + 11;
                    cursor.Y = Constants.SHOP_DIALOG_MIN_Y + 1;
                    cursor.PrevX = Constants.SHOP_DIALOG_MIN_X + 11;
                    while (true)
                    {
                        if (playerState != PlayerState.Talk)
                        {
                            break;
                        }
                        Console.SetCursorPosition(Constants.SHOP_DIALOG_MIN_X, Constants.SHOP_DIALOG_MIN_Y);
                        Console.Write("안녕하신가!! 카드를 뽑아보시겠는가?");
                        Console.SetCursorPosition(Constants.SHOP_DIALOG_MIN_X, Constants.SHOP_DIALOG_MIN_Y + 1);
                        Console.Write("[1] 뽑는다.");
                        Console.SetCursorPosition(Constants.SHOP_DIALOG_MIN_X, Constants.SHOP_DIALOG_MIN_Y + 2);
                        Console.Write("[2] 다음에.");

                        RenderManager.RenderObject(cursor.PrevX, cursor.PrevY, ' ');
                        RenderManager.RenderObject(cursor.X, cursor.Y, '◀');
                        ConsoleKey key = Console.ReadKey().Key;

                        if (key == ConsoleKey.UpArrow)
                        {
                            cursor.PrevY = cursor.Y;
                            cursor.Y = Math.Clamp(cursor.Y - 1, Constants.SHOP_DIALOG_MIN_Y + 1, Constants.SHOP_DIALOG_MIN_Y + 2);
                        }
                        if (key == ConsoleKey.DownArrow)
                        {
                            cursor.PrevY = cursor.Y;
                            cursor.Y = Math.Clamp(cursor.Y + 1, Constants.SHOP_DIALOG_MIN_Y + 1, Constants.SHOP_DIALOG_MIN_Y + 2);
                        }

                        if (key == ConsoleKey.Enter)
                        {
                            if (cursor.Y == Constants.SHOP_DIALOG_MIN_Y + 1)
                            {
                                if (Player.Money >= 500)
                                {
                                    Player.Money -= 500;
                                    Card card = GameManager.Gacha(GameManager.CardsTable);
                                    GameManager.Cards[(int)card.Type]++;
                                    RenderManager.ShowGachaResult(card);
                                    break;
                                }
                                else
                                {
                                    Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y);
                                    Console.Write("돈이 부족합니다..");
                                    Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y + 1);
                                    Console.Write("계속하시려면 엔터를 눌러주세요.");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    
                    break;
                default:
                    GameManager.ExitWithError($"잘못된 NPC타입입니다. In SayToPlayer {npcType}");
                    return;
            }
        }
    }
}
