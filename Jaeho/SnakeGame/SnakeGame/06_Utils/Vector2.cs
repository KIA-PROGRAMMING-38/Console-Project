namespace SnakeGame
{
    public struct Vector2
    {
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;

        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Top = new Vector2(0, -1);
        public static Vector2 Bottom = new Vector2(0, 1);

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator+(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static bool operator ==(Vector2 a, Vector2 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);
    }
}
