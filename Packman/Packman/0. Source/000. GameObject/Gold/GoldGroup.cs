using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class GoldGroup : GameObject
    {
        // 룩업테이블로 사용할 예정( y, x 좌표를 넣으면 Gold 인스턴스를 뱉게끔 )
        private Gold[,] _goldTable;
        private Player player;
        private List<Character> characters;

        public GoldGroup( int mapWidth, int mapHeight )
            : base( 0 )
        {
            _goldTable = new Gold[mapHeight + 1, mapWidth + 1];
            player = null;
            characters = new List<Character>();
        }

        public void Initialize()
        {
            Debug.Assert( base.Initialize() );

            Gold[] golds = _objectManager.GetAllGameObject<Gold>();
            int goldCount = golds.Length;
            for ( int index = 0; index < goldCount; ++index )
            {
                _goldTable[golds[index].Y, golds[index].X] = golds[index];
                golds[index].Render();
            }
        }

        public override void Update()
        {
            if ( null == player )
            {
                player = _objectManager.GetGameObject<Player>();
                Character[] allCharacters = _objectManager.GetAllGameObject<Character>();

                foreach(var character in allCharacters )
                {
                    if(player.GetType() == character.GetType())
                    {
                        continue;
                    }

                    //characters.OnMoveCharacterEvent += 
                }
            }

            base.Update();

            if ( null != player )
            {
                Gold findGold = GetGold( player.X, player.Y );
                if ( null != findGold )
                {
                    _objectManager.RemoveObject( findGold );

                    _goldTable[player.Y, player.X] = null;
                }
            }

            foreach ( var character in characters )
            {
                if(  )
            }
        }

        private Gold GetGold(int x, int y)
        {
            return _goldTable[y, x];
        }
    }
}
