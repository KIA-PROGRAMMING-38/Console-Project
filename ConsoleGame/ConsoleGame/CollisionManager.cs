using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class CollisionManager
    {
        public static void OnCollisionWithWallInTown(Player player, Wall[] walls)
        {
            for (int wallId = 0; wallId < Constants.TOWN_WALL_COUNT; ++wallId)
            {
                if (player.X == walls[wallId].X && player.Y == walls[wallId].Y)
                {
                    switch (player.Dir)
                    {
                        case Direction.Left:
                            player.X = walls[wallId].X + 1;
                            break;
                        case Direction.Right:
                            player.X = walls[wallId].X - 1;
                            break;
                        case Direction.Up:
                            player.Y = walls[wallId].Y + 1;
                            break;
                        case Direction.Down:
                            player.Y = walls[wallId].Y - 1;
                            break;
                        default:
                            return;
                    }
                }
            }
            
        }

        public static void OnCollisionWithNpcInTown(Player player, Npc[] npcs)
        {
            for (int npcId = 0; npcId < Constants.TOWN_NPC_COUNT; ++npcId)
            {
                if (player.X == npcs[npcId].X && player.Y == npcs[npcId].Y)
                {
                    switch (player.Dir)
                    {
                        case Direction.Left:
                            player.X = npcs[npcId].X + 1;
                            break;
                        case Direction.Right:
                            player.X = npcs[npcId].X - 1;
                            break;
                        case Direction.Up:
                            player.Y = npcs[npcId].Y + 1;
                            break;
                        case Direction.Down:
                            player.Y = npcs[npcId].Y - 1;
                            break;
                        default:
                            GameManager.ExitWithError($"[ERROR] 잘못된 플레이어 방향입니다. {player.Dir}");
                            return;
                    }
                }
            }
        }

        public static void OnCollisionWithWallInShop(Player player, Wall[] walls)
        {
            for (int wallId = 0; wallId < Constants.SHOP_WALL_COUNT; ++wallId)
            {
                if (player.X == walls[wallId].X && player.Y == walls[wallId].Y)
                {
                    switch (player.Dir)
                    {
                        case Direction.Left:
                            player.X = walls[wallId].X + 1;
                            break;
                        case Direction.Right:
                            player.X = walls[wallId].X - 1;
                            break;
                        case Direction.Up:
                            player.Y = walls[wallId].Y + 1;
                            break;
                        case Direction.Down:
                            player.Y = walls[wallId].Y - 1;
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        public static void OnCollisionWithNpcInShop(Player player, Npc[] npcs)
        {
            for (int npcId = 0; npcId < Constants.SHOP_NPC_COUNT; ++npcId)
            {
                if (player.X == npcs[npcId].X && player.Y == npcs[npcId].Y)
                {
                    switch (player.Dir)
                    {
                        case Direction.Left:
                            player.X = npcs[npcId].X + 1;
                            break;
                        case Direction.Right:
                            player.X = npcs[npcId].X - 1;
                            break;
                        case Direction.Up:
                            player.Y = npcs[npcId].Y + 1;
                            break;
                        case Direction.Down:
                            player.Y = npcs[npcId].Y - 1;
                            break;
                        default:
                            GameManager.ExitWithError($"[ERROR] 잘못된 플레이어 방향입니다. {player.Dir}");
                            return;
                    }
                }
            }
        }
    }
}
