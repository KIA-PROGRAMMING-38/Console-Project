namespace SnakeGame
{
    public class Renderer : Component
    {
        /// <summary>
        /// 렌더러 세팅
        /// </summary>
        /// <param name="icon">그려줄 아이콘</param>
        /// <param name="order">레이어 오더</param>
        public Renderer(char icon, int order, ConsoleColor color = ConsoleColor.White) 
        { 
            _ownerIcon = icon;
            _order = order;
            _color = color;
        }

        private int     _order;
        private char  _ownerIcon;
        private ConsoleColor _color;
        
        public int          Order { get { return _order; } set{ _order = value; } }
        public char         OwnerIcon { get { return _ownerIcon; } set { _ownerIcon = value; } }
        public ConsoleColor Color { get { return _color; } set { _color = value; } }

        public override void Start()
        {
            RenderManager.Instance.AddRenderer(this);
        }

        public override void Render()
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = _color;
            Owner.Render();
            Console.ForegroundColor = prev;
        }
    }
}
