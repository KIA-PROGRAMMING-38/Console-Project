using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class FindWayPointAction : ActionBase
    {
        private WayPointGroup _wayPointGroup;
        private WayPoint _curWayPoint = null;

        public FindWayPointAction( Monster monster, MonsterAI monsterAI )
            : base( monster, monsterAI )
        {

        }

        public override void Action()
        {
            // WayPoint 그룹이 없다면 찾는다..
            if( null == _wayPointGroup )
            {
                _wayPointGroup = ObjectManager.Instance.GetGameObject<WayPointGroup>();
                if(null == _wayPointGroup )
                {
                    return;
                }
            }

            // 현재 WayPoint 가 없는 경우 찾는다..
            if(null == _curWayPoint )
            {
                _curWayPoint = _wayPointGroup.FindNearWayPoint( _monster );
                if( null == _curWayPoint )
                {
                    return;
                }

                _monsterAI.SetAIInfomation( "Target", _curWayPoint );
            }
        }
    }
}
