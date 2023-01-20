using Packman.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class GameObject
    {
        // ===================================== Field.. ===================================== //
        // 필요한 매니저들 미리 인스턴스로 가지고 있는다..
        protected TimeManager _timer;
        protected ObjectManager _objectManager;

        // 현재 GameObject가 가지고 있는 Component 들..
        private Dictionary<string, Component> components = new Dictionary<string, Component>();
        protected Renderer _renderer = new Renderer();

        protected int _x = 0;
        protected int _y = 0;
        protected string _image = "";
        protected ConsoleColor _color = 0;

        private bool _isActive;

        // ===================================== Property.. ===================================== //
        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public GameObject()
        {
            _timer = TimeManager.Instance;
            _objectManager = ObjectManager.Instance;
        }

        public GameObject( int x, int y )
            : this()
        {
            _x = x;
            _y = y;
        }

        public GameObject( int x, int y, string image, ConsoleColor color )
            : this( x, y )
        {
            _image = image;
            _color = color;
        }

        // ===================================== Property.. ===================================== //
        public bool IsActive 
        { 
            get { return _isActive; }
            set
            {
                if ( _isActive != value )
                {
                    // 바뀐 _isActive 값에 따라 함수 호출..
                    _isActive = value;
                    if ( _isActive )
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                }
            }
        }

        // ===================================== Function.. ===================================== //
        public bool Initialize()
        {
            AddComponent( "Renderer", _renderer );

            OnEnable();

            return true;
        }

        /// <summary>
        /// GameObject가 활성화 시 호출됩니다..
        /// </summary>
        public virtual void OnEnable() { }

        /// <summary>
        /// GameObject가 비활성화 시 호출됩니다..
        /// </summary>
        public virtual void OnDisable() { }

        public virtual void Update()
        {
            foreach ( var component in components )
            {
                component.Value.UpdateComponent();
            }
        }

        public virtual void Render()
        {
            ConsoleColor tempColor = Console.ForegroundColor;

            Console.SetCursorPosition( (int)_x, (int)_y );
            Console.ForegroundColor = _color;
            Console.Write( _image );

            Console.ForegroundColor = tempColor;
        }

        public virtual void Release()
        {

        }


        /// <summary>
        /// 컴포넌트를 찾습니다.
        /// </summary>
        /// <typeparam name="T"> 리턴받을 컴포넌트의 타입 </typeparam>
        /// <param name="componentId"> 찾을 컴포넌트의 ID </param>
        /// <returns> 찾은 컴포넌트를 T 타입으로 반환합니다.. </returns>
        public T? GetComponent<T>(string componentId) where T : Component
        {
            Component? findComponent = null;

            // 컴포넌트를 찾고 있다면 반환, 없다면 null 반환..
            if( components.TryGetValue(componentId, out findComponent) )
            {
                return (T)findComponent;
            }

            return null;
        }

        /// <summary>
        /// 컴포넌트를 추가합니다.
        /// </summary>
        /// <param name="componentId"> 컴포넌트의 ID </param>
        /// <param name="component"> 컴포넌트 인스턴스 </param>
        /// <returns> 컴포넌트를 추가하는데 성공했는지 실패했는지 여부를 반환 </returns>
        protected bool AddComponent( string componentId, Component component )
        {
            // 컴포넌트가 null일 경우 안넣는다..
            if ( null == component )
            {
                return false;
            }

            // 이미 컴포넌트 ID가 사용중이라면..
            if ( true == components.ContainsKey( componentId ) )
            {
                return false;
            }

            // 컴포넌트의 gameobject 를 나(현재 GameObject 인스턴스)로 바꾸고 추가합니다..
            component.gameobject = this;
            components.Add( componentId, component );

            return true;
        }
    }
}
