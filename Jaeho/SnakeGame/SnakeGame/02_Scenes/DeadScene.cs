namespace SnakeGame
{
    public class DeadScene : Scene
    {
        public override void Start()
        {
            SoundManager.Instance.Play(_soundName, true);
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            _died = new string[]{
"### #      ####   ### ###  ### #    ",
" ## ##      ##     ##  ##   ## ##   ",
" ##  ##     ##     ##       ##  ##  ",
" ##  ##     ##     ## ##    ##  ##  ",
" ##  ##     ##     ##       ##  ##  ",
" ## ##      ##     ##  ##   ## ##   ",
"### #      ####   ### ###  ### #    ",
"\n\n\n\n\n\n\n\n\n",
 "##   ##    ###      ###    ######  ",
 "###  ##   ## ##    ## ##   ##   ## ",
 "###  ##  ##   ##  ##   ##  ##   ## ",
 "## # ##  ##   ##  ##   ##  ######  ",
 "## # ##  ##   ##  ##   ##  ##   ## ",
 "##  ###   ## ##    ## ##   ##   ## ",
 "##   ##    ###      ###    ######  "
            };

        }
        private string[] _died;
        public override void Update()
        {
            if (InputManager.Instance.IsKeyDown(ConsoleKey.Enter))
            {
                SceneManager.Instance.ChangeFlagOn(_nextSceneName);
            }
        }

        
        public override void Render()
        {
            for(int i = 0; i < _died.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(44, 8 + i);
                Console.Write(_died[i]);
                Console.ForegroundColor = GameDataManager.DEFAULT_FOREGROUND_COLOR;
            }
        }

    }
}
