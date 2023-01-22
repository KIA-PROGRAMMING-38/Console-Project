namespace SnakeGame
{
    public class Renderer : Component
    {
        /// <summary>
        /// 렌더러 세팅
        /// </summary>
        /// <param name="icon">그려줄 아이콘</param>
        /// <param name="order">레이어 오더</param>
        public Renderer(string icon, int order) 
        { 
            _ownerIcon = icon;
            _order = order;
        }

        private int     _order;
        private string  _ownerIcon;
        
        public int      Order { get { return _order; } set{ _order = value; } }
        public string   OwnerIcon { get { return _ownerIcon; } set { _ownerIcon = value; } }

        public override void Start()
        {
            RenderManager.Instance.AddRenderer(this);
        }

        public override void Render()
        {
            Owner.Render();
        }
    }
}
