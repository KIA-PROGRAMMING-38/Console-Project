using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Game
    {
        //함수 호출
        public Map mapRender = new Map();
        Random random = new Random();
        Card cardUser = new Card();
        Card cardDealer = new Card();
        CardInfo cardInfo = new CardInfo();
        Coordinate coordinate = new Coordinate();

        // 레이스 정보 저장
        int bet = 0;

        //판돈
        int backGroundMoney = 200000;

        // 덱 저장
        int user = 0;
        int dealer = 0;

        // 덱 합계 저장
        int saveDealer = 0;
        int saveUser = 0;

        // 랜덤 적용
        int patternIndex = 0;
        string patterns = "";
        private void GameCoordinate(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        
        //첫번째 게임
        public void FristGameExecution()
        {
            
            if (cardDealer.cardIndex == 0)
            {
                mapRender.money -= backGroundMoney;
                
                patternIndex = random.Next(cardInfo.pattern.Length);
                patterns = cardInfo.pattern[patternIndex];
                GameCoordinate(coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                GameCoordinate(coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                dealer = random.Next(1, 13);
                GameCoordinate(coordinate.dealerNumberCoordinate[cardDealer.cardIndex].X, coordinate.dealerNumberCoordinate[cardDealer.cardIndex].Y);
                Console.Write(cardInfo.number[dealer]);

                cardDealer.value[cardDealer.cardIndex] = cardInfo.number[dealer];
                saveDealer += dealer;
                ++cardDealer.cardIndex;

                cardUser.cardIndex = 0;

                patternIndex = random.Next(cardInfo.pattern.Length);
                patterns = cardInfo.pattern[patternIndex];
                GameCoordinate(coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                Console.Write(patterns);
                GameCoordinate(coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                Console.Write(patterns);
                user = random.Next(1, 13);
                GameCoordinate(coordinate.userNumberCoordinate[cardUser.cardIndex].X, coordinate.userNumberCoordinate[cardUser.cardIndex].Y);
                Console.Write(cardInfo.number[user]);

                cardUser.value[cardUser.cardIndex] = cardInfo.number[user];
                ++cardUser.cardIndex;
                saveUser += user;
            }
        }

        public void NextGameExecution()
        {
            // 레이스 선택 시
            if (mapRender.playerX + 3 == mapRender.menu[0].X && mapRender.playerY == mapRender.menu[0].Y)
            {
                mapRender.money = mapRender.money - (backGroundMoney * 2);
                
                patternIndex = random.Next(cardInfo.pattern.Length);
                patterns = cardInfo.pattern[patternIndex];
                GameCoordinate(coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                GameCoordinate(coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                dealer = random.Next(1, 13);
                GameCoordinate(coordinate.dealerNumberCoordinate[cardDealer.cardIndex].X, coordinate.dealerNumberCoordinate[cardDealer.cardIndex].Y);
                Console.Write(cardInfo.number[dealer]);

                cardDealer.value[cardDealer.cardIndex] = cardInfo.number[dealer];
                ++cardDealer.cardIndex;
                saveDealer += dealer;
                
                if(DealerBurst() == false)
                {
                    patternIndex = random.Next(cardInfo.pattern.Length);
                    patterns = cardInfo.pattern[patternIndex];
                    GameCoordinate(coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    user = random.Next(1, 13);
                    GameCoordinate(coordinate.userNumberCoordinate[cardUser.cardIndex].X, coordinate.userNumberCoordinate[cardUser.cardIndex].Y);
                    Console.Write(cardInfo.number[user]);

                    cardUser.value[cardUser.cardIndex] = cardInfo.number[dealer];
                    ++cardUser.cardIndex;
                    saveUser += user;
                    ++bet;

                    UserBurst();
                }             


            }


            // 체크 선택시
            if (mapRender.playerX + 3 == mapRender.menu[1].X && mapRender.playerY == mapRender.menu[1].Y)
            {

                mapRender.money -= 0;
                                
                patternIndex = random.Next(cardInfo.pattern.Length);
                patterns = cardInfo.pattern[patternIndex];
                GameCoordinate(coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                GameCoordinate(coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                dealer = random.Next(1, 13);
                GameCoordinate(coordinate.dealerNumberCoordinate[cardDealer.cardIndex].X, coordinate.dealerNumberCoordinate[cardDealer.cardIndex].Y);
                Console.Write(cardInfo.number[dealer]);
                cardDealer.value[cardDealer.cardIndex] = cardInfo.number[dealer];
                ++cardDealer.cardIndex;
                saveDealer += dealer;

                if(DealerBurst() == false)
                {
                    patternIndex = random.Next(cardInfo.pattern.Length);
                    patterns = cardInfo.pattern[patternIndex];
                    GameCoordinate(coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userFrontCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    GameCoordinate(coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].X, coordinate.userEndCardPatternCoordinate[cardUser.cardIndex].Y);
                    Console.Write(patterns);
                    user = random.Next(1, 13);
                    GameCoordinate(coordinate.userNumberCoordinate[cardUser.cardIndex].X, coordinate.userNumberCoordinate[cardUser.cardIndex].Y);
                    Console.Write(cardInfo.number[user]);
                    cardUser.value[cardUser.cardIndex] = cardInfo.number[user];
                    ++cardUser.cardIndex;
                    saveUser += user;

                    UserBurst();
                }
                                

            }

            // 넘겨 선택 시
            if (mapRender.playerX + 3 == mapRender.menu[2].X && mapRender.playerY == mapRender.menu[2].Y)
            {
                
                patternIndex = random.Next(cardInfo.pattern.Length);
                patterns = cardInfo.pattern[patternIndex];
                GameCoordinate(coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerFrontCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                GameCoordinate(coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].X, coordinate.dealerEndCardPatternCoordinate[cardDealer.cardIndex].Y);
                Console.Write(patterns);
                dealer = random.Next(1, 13);
                GameCoordinate(coordinate.dealerNumberCoordinate[cardDealer.cardIndex].X, coordinate.dealerNumberCoordinate[cardDealer.cardIndex].Y);
                Console.Write(cardInfo.number[dealer]);
                cardDealer.value[cardDealer.cardIndex] = cardInfo.number[dealer];
                ++cardDealer.cardIndex;
                saveDealer += dealer;

                if(DealerBurst() == false)
                {
                    Console.Write(cardInfo.number[0]);
                    saveDealer += 0;
                    saveUser += 0;
                    cardUser.value[cardUser.cardIndex] = cardInfo.number[0];
                    ++cardUser.cardIndex;
                }
                                
            }

            // 포기 선택 시
            if (mapRender.playerX + 3 == mapRender.menu[3].X && mapRender.playerY == mapRender.menu[3].Y)
            {
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
            }
        }

        public void GameEnd()
        {
            //게임이 끝난 후
            if (cardDealer.cardIndex == 3 && cardUser.cardIndex == 3)
            {
                GameResult();
            }

            //파산 후
            if(cardDealer.cardIndex == 3 && cardUser.cardIndex == 3)
            {

                if (mapRender.money < 1)
                {
                    Console.Clear();
                    Console.Write("파산!");
                    Environment.Exit(0);
                }
            }
        }

        private void GameResult()
        {
            if (saveDealer < saveUser && saveUser < 22)
            {
                if (1 <= bet)
                {
                    mapRender.money = ((backGroundMoney * 2) * 2) * bet;
                }
                else
                {
                    mapRender.money += backGroundMoney;
                }
                Console.SetCursorPosition(71, 20);
                Console.Write("Player가 승리하였습니다.");
                Thread.Sleep(1000);
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
                return;
                
            }

            if (saveUser < saveDealer && saveDealer < 22)
            {
                Console.SetCursorPosition(71, 20);
                Console.Write("Dealer가 승리하였습니다.");
                Thread.Sleep(1000);
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
                return;

            }

            if (saveUser == saveDealer && saveDealer < 22 && saveUser < 22)
            {
                Console.SetCursorPosition(71, 20);
                Console.Write("비겼습니다.");
                Thread.Sleep(1000);
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
                
            }
        }

        private void UserBurst()
        {
            if (21 < saveUser)
            {
                Console.SetCursorPosition(71, 20);
                Console.Write("Player 버스트!");
                Thread.Sleep(1000);
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
                
            }
        }

        private bool DealerBurst()
        {
            if (21 < saveDealer)
            {
                Console.SetCursorPosition(71, 20);
                Console.Write("Dealer 버스트!");
                Thread.Sleep(1000);
                if (1 <= bet)
                {
                    mapRender.money = ((backGroundMoney * 2) * 2) * bet;
                }
                else
                {
                    mapRender.money += backGroundMoney;
                }
                saveDealer = 0;
                saveUser = 0;
                cardDealer.cardIndex = 0;
                cardUser.cardIndex = 0;
                Console.Clear();
                return true;
            }
            return false;
        }
    }
}
