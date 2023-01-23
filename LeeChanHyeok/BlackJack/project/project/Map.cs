using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Map
    {
        //메뉴 선택
        public int playerX = 123;
        public int playerY = 12;
        //메뉴 범위 좌표
        public int MIN_Y = 12;
        public int MAX_Y = 18;
        //소지금
        public int[] money = { 1000000 };
        //메뉴
        public Menu[] menu = new Menu[]
        {
                new Menu{X = 126, Y= 12, menuButten = "▶ " + "Raise"},
                new Menu{X = 126, Y = 15, menuButten = "▶ " + "Check"},
                new Menu{X = 126, Y = 18, menuButten = "▶ " + "Fold"}
        };
        #region 맵 배열

        public int[] mapX = { 30, 155, 119, 153, 120, 36, 50, 65, 80, 62, 77, 92 };
        public int[] mapY = { 4, 38, 5, 7, 6, 10, 20, 8, 16, 23, 31 };
        #endregion
        public void MapRender()
        {
            MapDrow();
        }

        private void MapExtent(int x, int y, string mapObject)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(mapObject);
        }

        void MapDrow()
        {
            #region 맵 카운트
            const int ALL_MAPX_COUNT = 125;
            const int ALL_MAPY_COUNT = 35;
            const int COMMEND_MAPX_COUNT = 35;
            const int COMMEND_MAPY_COUNT = 3;
            const int MENU_MAPX_COUNT = 35;
            const int MENU_MAPY_COUNT = 10;
            const int MENU_COUNT = 3;
            const int DEALER_MAPX_COUNT = 12;
            const int DEALER_MAPY_COUNT = 9;
            const int PLAYER_MAPX_COUNT = 12;
            const int PLAYER_MAPY_COUNT = 9;
            #endregion
            #region 전체 맵 테두리
            for (int allMapId = 0; allMapId < ALL_MAPX_COUNT; ++allMapId)
            {
                MapExtent(mapX[0] + allMapId, mapY[0], "#");
                MapExtent(mapX[0] + allMapId, mapY[1], "#");

            }
            for (int allMapId = 0; allMapId < ALL_MAPY_COUNT; ++allMapId)
            {
                MapExtent(mapX[0], mapY[0] + allMapId, "#");
                MapExtent(mapX[1], mapY[0] + allMapId, "#");

            }
            #endregion
            #region 명령어 맵 테두리
            for (int commendMapId = 0; commendMapId < COMMEND_MAPX_COUNT; ++commendMapId)
            {
                MapExtent(mapX[2] + commendMapId, mapY[2], "-");
                MapExtent(mapX[2] + commendMapId, mapY[3], "-");
            }

            for (int commendMapId = 0; commendMapId < COMMEND_MAPY_COUNT; ++commendMapId)
            {
                MapExtent(mapX[2], mapY[2] + commendMapId, "l");
                MapExtent(mapX[3], mapY[2] + commendMapId, "l");
            }
            #endregion
            #region 소지금 테두리
            MapExtent(mapX[4], mapY[4], $"소지금 : {money[0]:C}");
            #endregion
            #region 메뉴 테두리
            for (int menuId = 0; menuId < MENU_MAPX_COUNT; ++menuId)
            {
                MapExtent(mapX[2] + menuId, mapY[5], "#");
                MapExtent(mapX[2] + menuId, mapY[6], "#");
            }
            for (int menuId = 0; menuId < MENU_MAPY_COUNT; ++menuId)
            {
                MapExtent(mapX[2], mapY[5] + menuId, "#");
                MapExtent(mapX[3], mapY[5] + menuId, "#");
            }
            #endregion
            #region 메뉴
            for (int menuId = 0; menuId < MENU_COUNT; ++menuId)
            {
                MapExtent(menu[menuId].X, menu[menuId].Y, menu[menuId].menuButten);
            }
            #endregion
            #region 메뉴 선택 오브젝트
            MapExtent(playerX, playerY, "→");
            #endregion
            #region 딜러 카드 테두리
            for (int dealerId = 0; dealerId < DEALER_MAPX_COUNT; ++dealerId)
            {
                MapExtent(mapX[5], mapY[7], "< Dealer >");
                MapExtent(mapX[6] + dealerId, mapY[7], "-");
                MapExtent(mapX[6] + dealerId, mapY[8], "-");
                MapExtent(mapX[7] + dealerId, mapY[7], "-");
                MapExtent(mapX[7] + dealerId, mapY[8], "-");
                MapExtent(mapX[8] + dealerId, mapY[7], "-");
                MapExtent(mapX[8] + dealerId, mapY[8], "-");
            }
            for (int dealerId = 0; dealerId < DEALER_MAPY_COUNT; ++dealerId)
            {
                MapExtent(mapX[6], mapY[7] + dealerId, "I");
                MapExtent(mapX[9], mapY[7] + dealerId, "I");
                MapExtent(mapX[7], mapY[7] + dealerId, "I");
                MapExtent(mapX[10], mapY[7] + dealerId, "I");
                MapExtent(mapX[8], mapY[7] + dealerId, "I");
                MapExtent(mapX[11], mapY[7] + dealerId, "I");
            }
            #endregion
            #region 플레이어 카드 테두리
            for (int playerId = 0; playerId < PLAYER_MAPX_COUNT; ++playerId)
            {
                MapExtent(mapX[5], mapY[9], "< player >");
                MapExtent(mapX[6] + playerId, mapY[9], "-");
                MapExtent(mapX[6] + playerId, mapY[10], "-");
                MapExtent(mapX[7] + playerId, mapY[9], "-");
                MapExtent(mapX[7] + playerId, mapY[10], "-");
                MapExtent(mapX[8] + playerId, mapY[9], "-");
                MapExtent(mapX[8] + playerId, mapY[10], "-");

            }

            for (int playerId = 0; playerId < PLAYER_MAPY_COUNT; ++playerId)
            {
                MapExtent(mapX[6], mapY[9] + playerId, "I");
                MapExtent(mapX[9], mapY[9] + playerId, "I");
                MapExtent(mapX[7], mapY[9] + playerId, "I");
                MapExtent(mapX[10], mapY[9] + playerId, "I");
                MapExtent(mapX[8], mapY[9] + playerId, "I");
                MapExtent(mapX[11], mapY[9] + playerId, "I");
            }
            #endregion
        }

    }
}
