using ProjectJK.Objects;
using System.Drawing;

namespace ProjectJK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectCursor selectCursor = new SelectCursor
            {
                X = 27,
                Y = 18,
            };
            Player player = new Player
            {
                X = Game.Center_X,
                Y = Game.Center_Y,
                Level = 1,
                MaxEXP = 10,
                MaxHP = 10,
                CurrentHP = 10,
                ATK = 1,
                DEF = 1,
            };

            Wall[] walls = default;
            VillageNPC[] villageNPCs = default;
            StageUpPortal stageUpPortal = default;
            StageDownPortal stageDownPortal = default;

            Slime[] slimes = default;
            Fox[] foxes = default;
            Goblin[] goblins = default;
            KingSlime kingSlime = new KingSlime
            {
                X = 12,
                Y = 10,
                ATK = 100,
                DEF = 100,
                MaxHP = 500,
                CurrentHP = 500,
                Money = 10000,
                EXP = 999,
                Alive = true,
            };

            Game.Initializing();

            Game.Run(player, walls, villageNPCs, stageUpPortal, stageDownPortal,
                         slimes, foxes, goblins, kingSlime, selectCursor);
        }
    }
}