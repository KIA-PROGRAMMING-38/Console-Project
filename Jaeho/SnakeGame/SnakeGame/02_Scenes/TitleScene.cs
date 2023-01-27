using System.Text;

namespace SnakeGame
{
    public class TitleScene : Scene
    {
        private StringBuilder[] _titles;

        private string _startButton;
        private int _titleAnimationindex;
        private long _TitleAnimTimer;
        private long _startButtontimer;
        private bool _flicking;

        public override void Start()
        {
            base.Start();
            _titleAnimationindex = 0;
            _TitleAnimTimer = 0;
            _startButtontimer = 0;
            _flicking = false;

            SoundManager.Instance.Play(_soundName, true);

            _titles = new StringBuilder[5];
            _titles[0] = new StringBuilder();
            _titles[1] = new StringBuilder();
            _titles[2] = new StringBuilder();
            _titles[3] = new StringBuilder();
            _titles[4] = new StringBuilder();

            _titles[0].AppendLine("\n\n\n\n");
            _titles[0].AppendLine(@"     $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\  ");
            _titles[0].AppendLine(@"    $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ | ");
            _titles[0].AppendLine(@"    $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ | ");
            _titles[0].AppendLine(@"    \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ | ");
            _titles[0].AppendLine(@"     \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__| ");
            _titles[0].AppendLine(@"    $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                     ");
            _titles[0].AppendLine(@"    \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\  ");
            _titles[0].AppendLine(@"     \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__| ");

            _titles[1].AppendLine("\n\n\n\n");
            _titles[1].AppendLine(@"      $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\        $$\ $$\ ");
            _titles[1].AppendLine(@"    $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            _titles[1].AppendLine(@"       $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |          $$ |$$ |");
            _titles[1].AppendLine(@"    \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$  |");
            _titles[1].AppendLine(@"       \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|        \__|\__|");
            _titles[1].AppendLine(@"        $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                 ");
            _titles[1].AppendLine(@"     \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            _titles[1].AppendLine(@"       \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|    \__|\__|");

            _titles[2].AppendLine("\n\n\n\n");
            _titles[2].AppendLine(@"      $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\  ");
            _titles[2].AppendLine(@"     $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ | ");
            _titles[2].AppendLine(@"     $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ | ");
            _titles[2].AppendLine(@"     \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ | ");
            _titles[2].AppendLine(@"      \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__| ");
            _titles[2].AppendLine(@"     $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                     ");
            _titles[2].AppendLine(@"     \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\  ");
            _titles[2].AppendLine(@"      \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__| ");

            _titles[3].AppendLine("\n\n\n\n");
            _titles[3].AppendLine(@"     $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\  ");
            _titles[3].AppendLine(@"    $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ | ");
            _titles[3].AppendLine(@"    $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ | ");
            _titles[3].AppendLine(@"    \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ | ");
            _titles[3].AppendLine(@"     \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__| ");
            _titles[3].AppendLine(@"    $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                     ");
            _titles[3].AppendLine(@"    \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\  ");
            _titles[3].AppendLine(@"     \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__| ");

            _titles[4].AppendLine("\n\n\n\n");
            _titles[4].AppendLine(@"     $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\  ");
            _titles[4].AppendLine(@"    $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ | ");
            _titles[4].AppendLine(@"    $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ | ");
            _titles[4].AppendLine(@"    \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ | ");
            _titles[4].AppendLine(@"     \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__| ");
            _titles[4].AppendLine(@"    $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                     ");
            _titles[4].AppendLine(@"    \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\  ");
            _titles[4].AppendLine(@"     \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__| ");

            _startButton =
            @"
                                              $$\                           $$\    
                                              $$ |                          $$ |    
                                   $$$$$$$\ $$$$$$\    $$$$$$\   $$$$$$\  $$$$$$\   
                                  $$  _____|\_$$  _|   \____$$\ $$  __$$\ \_$$  _|  
                                  \$$$$$$\    $$ |     $$$$$$$ |$$ |  \__|  $$ |    
                                   \____$$\   $$ |$$\ $$  __$$ |$$ |        $$ |$$\ 
                                  $$$$$$$  |  \$$$$  |\$$$$$$$ |$$ |        \$$$$  |
                                  \_______/    \____/  \_______|\__|         \____/ ";
        }

        public override void Update()
        {

            _startButtontimer += TimeManager.Instance.ElapsedMs;
            _TitleAnimTimer += TimeManager.Instance.ElapsedMs;

            if (InputManager.Instance.IsKeyDown(ConsoleKey.Enter))
            {
                SceneManager.Instance.ChangeFlagOn(_nextSceneName);
            }

            if (InputManager.Instance.IsKeyDown(ConsoleKey.Escape))
            {
                Environment.Exit(1);
            }

            if (250 < _startButtontimer)
            {
                _flicking = !_flicking;
                _startButtontimer = 0;
            }

            if (10 < _TitleAnimTimer)
            {
                _titleAnimationindex = RandomManager.Instance.GetRandomInt(4);
                _TitleAnimTimer = 0;
            }
        }

        public override void Render()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, 0);
            Console.Write(_titles[_titleAnimationindex]);

            if (_flicking)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.SetCursorPosition(0, 15);
            Console.Write(_startButton);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 27);
            Console.Write("Press Enter...");
            Console.ForegroundColor = GameDataManager.DEFAULT_FOREGROUND_COLOR;
        }
    }
}
