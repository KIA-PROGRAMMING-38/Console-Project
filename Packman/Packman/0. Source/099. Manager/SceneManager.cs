using System;
using System.Collections.Generic;
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
            Stage
        }

        private Scene currentScene;
        private SceneKind curSceneKind = SceneKind.None;
        private SceneKind nextSceneKind = SceneKind.None;

        public void Update()
        {
            if(curSceneKind != nextSceneKind)
            {
                Scene newScene = MakeScene( nextSceneKind );
                if(null != newScene)
                {
                    currentScene?.Release();
                    curSceneKind = nextSceneKind;
                    currentScene = newScene;
                }
            }

            if ( null != currentScene )
            {
                currentScene.Update();
            }
        }

        public void Render()
        {
            if ( null != currentScene )
            {
                currentScene.Render();
            }
        }

        public void ChangeScene( SceneKind sceneKind )
        {
            nextSceneKind = sceneKind;
        }

        private Scene MakeScene( SceneKind sceneKind )
        {
            Scene newScene = null;

            switch (sceneKind)
            {
                case SceneKind.Title:
                    newScene = new TitleScene();

                    break;
                case SceneKind.Stage:
                    newScene = new StageScene();

                    break;
                default:
                    return null;
            }

            if( false == newScene?.Initialize() )
            {
                return null;
            }

            return newScene;
        }
    }
}
