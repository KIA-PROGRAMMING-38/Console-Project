using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class StageManager : SingletonBase<StageManager>
    {
        GoldGroup _goldGroup;

        public StageManager() 
        {
            _goldGroup = null;
        }

        public void Update()
        {
            if ( null == _goldGroup )
            {
                _goldGroup = ObjectManager.Instance.GetGameObject<GoldGroup>();

                if(null == _goldGroup )
                {
                    return;
                }    
            }

            if( 0 >= _goldGroup.RemainGoldCount)
            {
                ClearStage();
            }
        }

        private void ClearStage()
        {
            Game.Instance.Exit( 0 );
        }
    }
}
