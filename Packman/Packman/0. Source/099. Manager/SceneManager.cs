using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class SceneManager : SingletonBase<SceneManager>
    {
        public enum SceneKind
        {
            None,
            Title,
            Stage,
            Ending
        }

        private Scene? _currentScene = null;
        private SceneKind _curSceneKind = SceneKind.None;
        private SceneKind _nextSceneKind = SceneKind.None;
        private bool _isChangeScene = false;

        /// <summary>
        /// 현재 씬을 갱신합니다..
        /// </summary>
        public void Update()
        {
            if ( true == _isChangeScene )   // 씬을 변경해야 한다면..
            {
                // 현재 씬 삭제..
                _currentScene?.Release();

                // 새로운 씬 생성..
                Scene? newScene = MakeScene( _nextSceneKind );
                Debug.Assert( null != newScene );   // 새로운 씬이 없으면 아나되니까 Assert씀..

                // 현재 씬에 넣고 상태도 변경..
                _curSceneKind = _nextSceneKind;
                _currentScene = newScene;
                _isChangeScene = false;
            }

            // 현재 씬 업데이트..
            if ( null != _currentScene )
            {
                _currentScene.Update();
            }
        }

        /// <summary>
        /// 현재 씬을 그립니다..
        /// </summary>
        public void Render()
        {
            // 현재 씬 그린다..
            if ( null != _currentScene )
            {
                _currentScene.Render();
            }
        }

        /// <summary>
        /// 씬을 변경합니다(바로 변경하진 않음)..
        /// </summary>
        /// <param name="sceneKind"> 변경할 씬 종류 </param>
        public void ChangeScene( SceneKind sceneKind )
        {
            _isChangeScene = true;
            _nextSceneKind = sceneKind;
        }

        /// <summary>
        /// 씬을 만듭니다.
        /// </summary>
        /// <param name="sceneKind"> 만들 씬 종류 </param>
        /// <returns></returns>
        private Scene? MakeScene( SceneKind sceneKind )
        {
            Scene? newScene = null;

            switch (sceneKind)
            {
                case SceneKind.Title:
                    newScene = new TitleScene();

                    break;
                case SceneKind.Stage:
                    newScene = new StageScene( 1 );

                    break;
                case SceneKind.Ending:
                    newScene = new EndingScene();

                    break;
                default:
                    Debug.Assert( false );
                    return null;
            }

            Debug.Assert( newScene.Initialize() );

            return newScene;
        }
    }
}
