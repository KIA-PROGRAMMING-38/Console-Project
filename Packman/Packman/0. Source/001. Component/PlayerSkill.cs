using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class PlayerSkill : Component
    {
        public enum SkillKind
        {
            FireStungun,
            End
        }

        private struct SkillInfo
        {
            public SkillKind Kind;
            public int RemainUseCount;
        }

        SkillInfo[] _skillInfoes = null;

        public PlayerSkill()
        {
            _skillInfoes = new SkillInfo[(int)SkillKind.End];
            for(int index = 0; index < _skillInfoes.Length; ++index ) 
            {
                _skillInfoes[index].Kind = (SkillKind)index;
                _skillInfoes[index].RemainUseCount = 0;
            }
        }

        public void AddSkill( SkillKind skillKind )
        {
            ++_skillInfoes[(int)skillKind].RemainUseCount;
        }

        public void SetSkillRemainUseCount(SkillKind skillKind, int remainUseCount ) 
        {
            _skillInfoes[(int)skillKind].RemainUseCount = remainUseCount;
        }

        public void UseSkill(SkillKind skillKind )
        {
            if( 0 >= _skillInfoes[(int)SkillKind].RemainUseCount )
            {
                return;
            }
        }
    }
}
