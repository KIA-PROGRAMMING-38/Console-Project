using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class MonsterAI
    {
        private Monster _monster = null;

        // 행동해야할 패턴들( 행동 우선순위로 정렬할 예정 )..
        private List<PatternBase> aiPatterns = new List<PatternBase>();
        private Dictionary<string, ActionBase> aiActions = new Dictionary<string, ActionBase>();    // 실제 행동들..

        private PatternBase curAIPattern;
        private Stack<PatternBase> returnPatterns;      // 다시 되돌아갈 패턴들..

        private Dictionary<string, object> aiInfomation = new Dictionary<string, object>();

        public void SetAIInfomation(string key, object value)
        {
            aiInfomation[key] = value;
        }

        public object GetAIInfomation(string key)
        {
            return aiInfomation[key];
        }

        public MonsterAI( Monster monster )
        {
            Debug.Assert( null != monster );
            _monster = monster;

            InitializeAIAction();
            InitializeAIPattern();
        }

        public void Update()
        {
            foreach ( var pattern in aiPatterns )
            {
                // 이 다음 원소들은 행동 우선순위가 낮기 때문에 검사안한다..
                if ( pattern == curAIPattern )
                {
                    break;
                }

                // 실행 조건 갱신..
                // 실행해야할 조건들이 모두 클리어되면 실행한다..
                if ( pattern.ChecClearActionCondition() )
                {
                    AddActPatternList( pattern );

                    break;
                }
            }

            // 현재 실행해야할 패턴 실행..
            if ( null != curAIPattern )
            {
                curAIPattern.Update();
            }
        }

        private void InitializeAIPattern()
        {
            PatternBase newPattern = new MoveToTargetPattern( _monster, 10 );
        }

        private void InitializeAIAction()
        {
            // MoveToDirection Action 생성 및 저장..
            ActionBase newAction = new MoveToDirAction( _monster, this );
            aiActions.Add( "MoveToDirection", newAction );

            // FindWayPointAction Action 생성 및 저장..
            newAction = new FindWayPointAction( _monster, this );
            aiActions.Add( "FindWayPoint", newAction );
        }

        private void AddActPatternList( PatternBase newPattern )
        {
            Debug.Assert( null != newPattern );

            if ( null != curAIPattern )
            {
                returnPatterns.Push( curAIPattern );
            }

            ChangePattern( newPattern );
        }

        private void ChangePattern( PatternBase newPattern )
        {
            if ( null != curAIPattern )
            {
                curAIPattern.OnDisable();
            }

            curAIPattern = newPattern;
            curAIPattern.OnEnable();
        }

        private void ReturnPrevPattern()
        {
            if( returnPatterns.Count <= 0 )
            {
                return;
            }

            PatternBase pattern = returnPatterns.Pop();
            Debug.Assert( null != pattern );

            ChangePattern( pattern );
        }
    }
}
