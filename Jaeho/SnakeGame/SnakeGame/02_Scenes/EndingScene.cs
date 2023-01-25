namespace SnakeGame
{
    public class EndingScene : Scene
    {
        public override void Start()
        {
            SoundManager.Instance.Play(_soundName, true);
            clear = new string[] { 
            @"   ####  ##       ######    ###    ######     ####     ####",   
            @" ###     ##       ##       ## ##   ##   ##    ####     ####",   
            @"###      ##       ##      ##   ##  ##   ##    ####     ####",   
            @"###      ##       #####   ##   ##  ##  ##      ##       ## ",   
            @"###      ##       ##      #######  #####       ##       ## ",   
            @"####     ##       ##      ##   ##  ## ###                  ",   
            @" ######  ######   ######  ##   ##  ##  ###     ##       ## "};
        }

        public string[] clear;
        public override void Update()
        {
            if(InputManager.Instance.IsKeyDown(ConsoleKey.Enter)) 
            {
                SceneManager.Instance.ChangeFlagOn(_nextSceneName);
            }
        }

        public override void Render()
        {

            
            for(int i = 0; i < clear.Length; ++i) 
            {
                Console.ForegroundColor = (ConsoleColor)RandomManager.Instance.GetRandomRangeInt(1, 14);
                Console.SetCursorPosition(30, 5 + i);
                Console.Write(clear[i]);
            }
            Console.ForegroundColor = GameDataManager.DEFAULT_FOREGROUND_COLOR;



        }

    }
}
