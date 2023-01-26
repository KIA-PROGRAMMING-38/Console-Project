using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace ConsoleSoloProject
{
    internal class Game
    {
        public static int stage = 1;

        public static void ReGame()
        {
            Upgrade.Gold = 0;
            Upgrade.Damage = 5;
            Upgrade.Hp = 5;
            stage = 1;
            Item.itemFriendOnOff = false;
            gameWait();
        }



        public static void GamePlay()
        {

            //씬 불러오기
            Scene scene = new Scene();

            // 텍스트 위치
            Text text = new Text();

            // 총알 배열 불러오기
            List<Bullet> bullets = new List<Bullet>();
            List<Bullet> bullets2 = new List<Bullet>();

            // 몬스터 배열 불러오기
            List<Monster> monsters = new List<Monster>();

            //몬스터 클래스 불러오기
            Monster monster = new Monster();

            // 업그레이드
            Upgrade upGrade = new Upgrade();

            // 플레이어
            Player player = new Player();

            //스테이지실패 시간 측정
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //공격 딜레이 시간측정
            Stopwatch bulletSw = new Stopwatch();
            bulletSw.Start();

            int playerWeaponX2 = 13;
            int playerWeaponY2 = 9;



            const int WAIT_TICK = 1000 / 15;


            ConsoleKey key;
            key = Console.ReadKey().Key;


            Random random = new Random();
            int random1;

            while (Scene.gameScene)
            {
                Console.Clear();
                //--------------------- 랜  더 ---------------------------
                //랜더 불러오기
                StageBulider();






                //몬스터 출력
                if (monster.count < monster.unit)
                {

                    Monster monsterStatus = new Monster();
                    monsterStatus.X = monster.X;
                    monsterStatus.Y = monster.Y;
                    monsterStatus.Hp = Upgrade.Hp;


                    monsters.Add(monsterStatus);
                    monster.count++;
                    monster.deathCount = monster.count;

                }

                // 총알 생성
                if (bulletSw.Elapsed.TotalMilliseconds > 0)
                {
                    if (key == ConsoleKey.Spacebar)
                    {
                        if (bulletSw.Elapsed.Milliseconds > 480)
                        {
                            Bullet bullet = new Bullet();
                            bullet.X = Item.playerFire;
                            bullet.Y = Item.playerWeaponY;
                            bullet.Damage = Upgrade.Damage;
                            bullets.Add(bullet);

                            key = ConsoleKey.Clear;
                            bulletSw.Restart();

                            // 아이템 획득시 총알 생성
                            if (Item.itemFriendOnOff)
                            {
                                Bullet bullet2 = new Bullet();
                                bullet2.X = playerWeaponX2;
                                bullet2.Y = playerWeaponY2;
                                bullet2.Damage = Upgrade.Damage;
                                bullets2.Add(bullet2);


                            }
                        }

                    }
                }

                //---------------------사용자 입력------------------------
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;
                }

                // 타이틀 돌아가기
                if (key == ConsoleKey.R)
                {
                    Scene.titleScene = true;
                    Scene.TitleScene();
                    //함수 다시 불러오기
                    ReGame();

                }



                //---------------------업데이트 --------------------------

                // 아이템 뽑기
                if (key == ConsoleKey.D)
                {
                    Item.randomItem(random);
                    key = default;
                }



                // 총알 업그레이드
                if (key == ConsoleKey.G)
                {
                    upGrade.goldUpgrade();
                    key = default;

                }
                // 총알 진행 로직
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].X > 12)
                    {
                        if (bullets[i].X == 13)
                        { bullets.RemoveAt(i); }

                        if (Upgrade.Damage < 15)
                        {
                            bullets[i].BulletEffect(bullets[i].X--, bullets[i].Y, Upgrade.Icon);
                        }

                        if (Upgrade.Damage > 15 && Upgrade.Damage <= 30)
                        {
                            bullets[i].BulletEffect(bullets[i].X--, bullets[i].Y, Upgrade.damage15);
                        }

                        if (Upgrade.Damage > 30)
                        {
                            bullets[i].BulletEffect(bullets[i].X--, bullets[i].Y, Upgrade.damage30);
                        }

                    }
                }



                // 아이템 생성시 총알 진행 로직
                if (Item.itemFriendOnOff)
                {
                    for (int i = 0; i < bullets2.Count; i++)
                    {
                        if (bullets2[i].X < 42)
                        {
                            // 특정 좌표 도착시 삭제
                            if (bullets2[i].X == 41)
                            { bullets2.RemoveAt(i); }

                            if (Upgrade.Damage < 15)
                            {
                                bullets2[i].BulletEffect(bullets2[i].X++, bullets2[i].Y, Upgrade.Icon2);
                            }

                            if (Upgrade.Damage > 15 && Upgrade.Damage <= 30)
                            {
                                bullets2[i].BulletEffect(bullets2[i].X++, bullets2[i].Y, Upgrade.damage15);
                            }

                            if (Upgrade.Damage > 30)
                            {
                                bullets2[i].BulletEffect(bullets2[i].X++, bullets2[i].Y, Upgrade.damage30);
                            }
                        }
                    }
                }


                // 몬스터 이동
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    monsters[i].MonsterMove();
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                // 몬스터 , 총알 충돌처리
                for (int monsterId = 0; monsterId < monsters.Count; monsterId++)
                {

                    for (int bulletId = 0; bulletId < bullets.Count; bulletId++)
                    {
                        if (monsters[monsterId].X == bullets[bulletId].X && monsters[monsterId].Y == bullets[bulletId].Y)
                        {
                            monsters[monsterId].Hp -= bullets[bulletId].Damage;
                            bullets.RemoveAt(bulletId);
                        }
                        else if (monsters[monsterId].X == bullets[bulletId].X + 1 && monsters[monsterId].Y == bullets[bulletId].Y)
                        {
                            monsters[monsterId].Hp -= bullets[bulletId].Damage;
                            bullets.RemoveAt(bulletId);


                        }
                        // 몬스터 HP 0미만일때 몬스터 삭제
                        if (monsters[monsterId].Hp <= 0)
                        {
                            // 몬스터 처치시 각종 이벤트 발생
                            monsters.RemoveAt(monsterId);
                            Upgrade.Gold += 2;
                            --monster.deathCount;
                            break;
                        }


                    }

                }

                // 아이템 획득시 충돌처리
                for (int monsterId = 0; monsterId < monsters.Count; monsterId++)
                {

                    for (int bulletId = 0; bulletId < bullets2.Count; bulletId++)
                    {
                        if (monsters[monsterId].X == bullets2[bulletId].X && monsters[monsterId].Y == bullets2[bulletId].Y)
                        {
                            monsters[monsterId].Hp -= bullets2[bulletId].Damage;
                            bullets2.RemoveAt(bulletId);
                        }
                        else if (monsters[monsterId].X == bullets2[bulletId].X - 1 && monsters[monsterId].Y == bullets2[bulletId].Y)
                        {
                            monsters[monsterId].Hp -= bullets2[bulletId].Damage;
                            bullets2.RemoveAt(bulletId);


                        }
                        // 몬스터 HP 0미만일때 몬스터 삭제
                        if (monsters[monsterId].Hp <= 0)
                        {

                            monsters.RemoveAt(monsterId);
                            Upgrade.Gold += 2;
                            monster.deathCount--;
                            break;
                        }


                    }

                }


                // 몬스터를 다 처치 했을때
                if (monsters.Count == 0)
                {
                    ++stage;
                    monster.count = 0;
                    Upgrade.Hp += 4;
                    sw.Restart();
                }
                if (monsters.Count == 0)
                {
                    Scene.gameScene = false;
                    gameWait();
                }

                // 게임 실패
                Text.Position(Text.timeX, Text.timeY, $"제한 시간 : {30 - sw.Elapsed.Seconds} ");

                if (sw.Elapsed.Seconds > 30)
                {
                    Scene.gameScene = false;
                    Scene.EndScene();
                }

                if (stage > 20)
                {
                    Scene.gameScene = false;
                    Scene.titleScene = false;
                    Scene.waitScene = false;
                    Scene.EndScene();
                }

                Thread.Sleep(WAIT_TICK);
            }
            // 게임 루프 끝
            void StageBulider()
            {
                string[] MapContents = File.LoadStage();

                // 맵 그리기.
                for (int i = 0; i < MapContents.Length; i++)
                {
                    Console.WriteLine(MapContents[i]);
                }
                Text.Position(player.firstPlayerX, player.firstPlayerY, player.Icon);
                Text.Position(player.secondPlayerX, player.secondPlayerY, player.Icon);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Text.Position(player.WeaponX, player.WeaponY, "=");
                Console.ForegroundColor = ConsoleColor.Blue;

                Text.Position(Text.stageX, Text.stageY, $"스테이지 : {stage}");
                Text.Position(Text.monsterNumberX, Text.monsterHpY, $"몬스터 체력 : {Upgrade.Hp}");
                Text.Position(Text.goldX, Text.goldY, $"골드 : {Upgrade.Gold}");

                // 데미지 출력
                Text.Position(Text.damageX, Text.damageY, $"공격력 : {Upgrade.Damage}");
                // 몬스터 숫자
                Text.Position(Text.monsterNumberX, Text.monsterNumberY, $"몬스터 : {monster.deathCount}");

                Item.FriendText();
                Console.SetCursorPosition(0, 0);


            }
        }


        public static void gameWait()
        {
            // 텍스트 위치
            Text text = new Text();

            //몬스터 클래스 불러오기
            Monster monster = new Monster();

            // 업그레이드
            Upgrade upGrade = new Upgrade();

            // 플레이어
            Player player = new Player();


            const int WAIT_TICK = 1000 / 15;

            ConsoleKey key;
            key = Console.ReadKey().Key;
            Random random = new Random();

            Console.Clear();
            while (Scene.waitScene)
            {
                Console.Clear();
                //--------------------- 랜  더 ---------------------------
                //랜더 불러오기
                StageBulider();

                //---------------------사용자 입력------------------------
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey().Key;

                }

                //---------------------업데이트 --------------------------

                //타이틀 돌아가기
                if (key == ConsoleKey.R)
                {
                    Scene.titleScene = true;
                    Scene.TitleScene();
                    ReGame();
                }

                // 아이템 뽑기
                if (key == ConsoleKey.D)
                {
                    Item.randomItem(random);
                    key = default;
                }


                // 총알 업그레이드
                if (key == ConsoleKey.G)
                {
                    upGrade.goldUpgrade();
                    key = default;
                    
                }

                // 게임 진행
                if (key == ConsoleKey.Y)
                {
                    Scene.gameScene = true;
                    GamePlay();
                }
                // 스테이지 지나면 끝
                if (stage > 20)
                {
                    Scene.gameScene = false;
                    Scene.titleScene = false;
                    Scene.waitScene = false;
                    Scene.EndScene();
                }
                Thread.Sleep(WAIT_TICK);

            }// 게임 루프 끝
            void StageBulider()
            {
                string[] MapContents = File.LoadStage();

                // 맵 그리기.

                for (int i = 0; i < MapContents.Length; i++)
                {
                    Console.WriteLine(MapContents[i]);

                }

                Text.Position(player.firstPlayerX, player.firstPlayerY, player.Icon);
                Text.Position(player.secondPlayerX, player.secondPlayerY, player.Icon);
                Text.Position(Text.timeX, Text.timeY, $"제한 시간 : 30");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(player.WeaponX, player.WeaponY);
                Console.Write("=");
                Console.ForegroundColor = ConsoleColor.Blue;
                Text.Position(Text.stageX, Text.stageY, $"스테이지 : {stage}");
                Text.Position(Text.stageX - 5, Text.stageY + 10, "진행하시려면 Y키를 2번 눌러주세요.");
                Text.Position(Text.monsterNumberX, Text.monsterHpY, $"몬스터 체력 : {Upgrade.Hp}");
                Text.Position(Text.goldX, Text.goldY, $"골드 : {Upgrade.Gold}");
                // 데미지 출력
                Text.Position(Text.damageX, Text.damageY, $"공격력 : {Upgrade.Damage}");
                Text.Position(Text.monsterNumberX, Text.monsterNumberY, $"몬스터 : {monster.deathCount}");
                Item.FriendText();
                Console.SetCursorPosition(0, 0);


            }
        }
    }
}
