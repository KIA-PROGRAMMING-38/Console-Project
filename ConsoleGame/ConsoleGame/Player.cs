﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    enum PlayerState
    {
        Idle,
        Move,
        Talk
    }

    internal class Player
    {
        private int _x;
        private int _y;
        private static int _money = 0;
        private string _icon;
        private Direction _playerDirection;
        private PlayerState _playerState;

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

        public static int Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
            }
        }

        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
            }
        }

        public Direction Dir
        {
            get
            {
                return _playerDirection;
            }
            set
            {
                _playerDirection = value;
            }
        }

        public PlayerState PlayerState
        {
            get
            {
                return _playerState;
            }
            set
            {
                _playerState = value;
            }
        }


        public void MovePlayer(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                _x = Math.Clamp(_x - 1, Constants.TOWN_MIN_X, Constants.TOWN_MAX_X);
                _playerDirection = Direction.Left;
                _playerState = PlayerState.Move;
            }
            if (key == ConsoleKey.RightArrow)
            {
                _x = Math.Clamp(_x + 1, Constants.TOWN_MIN_X, Constants.TOWN_MAX_X);
                _playerDirection = Direction.Right;
                _playerState = PlayerState.Move;
            }
            if (key == ConsoleKey.UpArrow)
            {
                _y = Math.Clamp(_y - 1, Constants.TOWN_MIN_Y, Constants.TOWN_MAX_Y);
                _playerDirection = Direction.Up;
                _playerState = PlayerState.Move;
            }
            if (key == ConsoleKey.DownArrow)
            {
                _y = Math.Clamp(_y + 1, Constants.TOWN_MIN_Y, Constants.TOWN_MAX_Y);
                _playerDirection = Direction.Down;
                _playerState = PlayerState.Move;
            }
        }

        public void TalkToNpc(ConsoleKey key, Npc[] npcs)
        {
            if (key == ConsoleKey.Spacebar)
            {
                int[] dirX = { 1, -1, 0, 0 };
                int[] dirY = { 0, 0, 1, -1 };

                // 플레이어의 상하좌우에 npc가 있는지 체크하는 for문.
                for (int i = 0; i < 4; ++i)
                {
                    for (int npcId = 0; npcId < npcs.Length; ++npcId)
                    {
                        if (_x + dirX[i] == npcs[npcId].X && _y + dirY[i] == npcs[npcId].Y)
                        {
                            string playerInput = "";
                            _playerState = PlayerState.Talk;
                            switch (npcs[npcId].Type)
                            {
                                case NpcType.RaceNpc:
                                    npcs[npcId].SayToPlayer(npcs[npcId].Type);
                                    Console.SetCursorPosition(Constants.TOWN_DIALOG_MIN_X + 2, Constants.TOWN_DIALOG_MIN_Y + 3);
                                    playerInput = Console.ReadLine();
                                    break;
                                case NpcType.ShopNpc:
                                    npcs[npcId].SayToPlayer(npcs[npcId].Type);
                                    Console.SetCursorPosition(Constants.SHOP_DIALOG_MIN_X + 2, Constants.SHOP_DIALOG_MIN_Y + 3);
                                    playerInput = Console.ReadLine();
                                    break;
                                default:
                                    GameManager.ExitWithError($"잘못된 NPC타입입니다. {npcs[npcId].Type}");
                                    break;
                            }

                            switch (playerInput)
                            {
                                case "1":
                                    if (npcs[npcId].Type == NpcType.RaceNpc)
                                    {
                                        SceneManager._sceneType = Scene.RaceTrack;
                                        SceneManager._prevSceneType = Scene.Town;
                                    }
                                    if (npcs[npcId].Type == NpcType.ShopNpc)
                                    {
                                        if (Money >= 500)
                                        {
                                            Money -= 500;
                                            Card card = GameManager.Gacha(GameManager.CardsTable);
                                            GameManager.Cards[(int)card.Type]++;
                                            RenderManager.ShowGachaResult(card);
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y);
                                            Console.Write("돈이 부족합니다..");
                                            Console.SetCursorPosition(Constants.SHOP_GACHA_DIALOG_MIN_X, Constants.SHOP_GACHA_DIALOG_MIN_Y + 1);
                                            Console.Write("계속하시려면 엔터를 눌러주세요.");
                                            Console.ReadLine();
                                        }
                                    }
                                    break;
                                case "2":
                                    break;
                            }

                        }
                    }
                }
            }
        }

    }
}
