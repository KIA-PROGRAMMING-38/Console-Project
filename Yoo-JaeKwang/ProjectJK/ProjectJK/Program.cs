namespace ProjectJK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game
            {
                IsGameDoing = true,
                IsTitleDoing = true,
                IsStageOneDoing = false,
            };

            Player player = new Player
            {
                X = 21,
                Y = 20,
                pastX = 21,
                pastY = 20,
                MaxHP = 350,
                RemainHP = 350,
                ATK = 10,
                DEF = 10,
                MoveDirection = Direction.None,
            };

            ConsoleKey key;

            // 전체 게임루프
            while(game.IsGameDoing)
            {
                // 타이틀
                // 타이틀루프의 아웃랜더
                if(game.IsTitleDoing)
                {
                    Title.Function.TitleRender();
                }
                // 타이틀루프
                while(game.IsTitleDoing)
                {
                    key = Console.ReadKey().Key;
                    if(ConsoleKey.E == key)
                    {
                        game.IsTitleDoing = false;
                        game.IsStageOneDoing = true;
                        Console.Clear();
                        Game.Function.DefaultMapUI();
                    }
                }

                // 스테이지1
                if(game.IsStageOneDoing)
                {
                    StageOne.Function.OutRender();
                }
                while(game.IsStageOneDoing)
                {
                    StageOne.Function.InRender(player);
                    key = Console.ReadKey().Key;
                    StageOne.Function.Update(key, player);
                }


            }
        }
    }
}



/*
 *      마계동 오마주
 *      1. 객체의 종류 : 플레이어, 몬스터, 공격력 증가, 방어력 증가, 체력 증가, 최대 체력 증가, 문, 열쇠, 골드, // NPC와 쉴드는 나중에 시간나면 추가
 *      2. 객체들 속성 : 플레이어, 몬스터 > 공격력 방어력 체력을 가지고 서로 비교
 *                      공증, 방증, 체증, 최체증 > 획득시 플레이어의 데이터 증가
 *      3. 스테이지 형식
 */