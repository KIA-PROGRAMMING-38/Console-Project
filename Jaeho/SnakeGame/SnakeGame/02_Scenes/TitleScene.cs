using System.Text;

namespace SnakeGame
{
    public class TitleScene : Scene
    {
        public StringBuilder[] Titles;
     
        public string StartButton;
        private int  _titleAnimationindex;
        private long _TitleAnimTimer;
        private long _startButtontimer;
        private bool _flicking;
        public override void Start()
        {
            _titleAnimationindex =0;
            _TitleAnimTimer = 0;
            _startButtontimer = 0;
            _flicking = false;

            //InputKey = new InputKeyComponent();
            //InputKey.Start();

            SoundManager.Instance.Play("TitleBackgroundMusic", true);

            Titles = new StringBuilder[5];
            Titles[0] = new StringBuilder();
            Titles[1] = new StringBuilder();
            Titles[2] = new StringBuilder();
            Titles[3] = new StringBuilder();
            Titles[4] = new StringBuilder();

            Titles[0].AppendLine("\n\n\n\n");
            Titles[0].AppendLine(@"   $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\ ");
            Titles[0].AppendLine(@"  $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            Titles[0].AppendLine(@"  $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ |");
            Titles[0].AppendLine(@"  \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ |");
            Titles[0].AppendLine(@"   \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__|");
            Titles[0].AppendLine(@"  $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                    ");
            Titles[0].AppendLine(@"  \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            Titles[0].AppendLine(@"   \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__|");

            Titles[1].AppendLine("\n\n\n\n");
            Titles[1].AppendLine(@"    $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\        $$\ $$\ ");
            Titles[1].AppendLine(@"  $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            Titles[1].AppendLine(@"     $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |          $$ |$$ |");
            Titles[1].AppendLine(@"  \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$  |");
            Titles[1].AppendLine(@"     \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|        \__|\__|");
            Titles[1].AppendLine(@"      $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                 ");
            Titles[1].AppendLine(@"   \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            Titles[1].AppendLine(@"     \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|    \__|\__|");

            Titles[2].AppendLine("\n\n\n\n");
            Titles[2].AppendLine(@"    $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\ ");
            Titles[2].AppendLine(@"   $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            Titles[2].AppendLine(@"   $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ |");
            Titles[2].AppendLine(@"   \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ |");
            Titles[2].AppendLine(@"    \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__|");
            Titles[2].AppendLine(@"   $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                    ");
            Titles[2].AppendLine(@"   \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            Titles[2].AppendLine(@"    \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__|");

            Titles[3].AppendLine("\n\n\n\n");
            Titles[3].AppendLine(@"   $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\ ");
            Titles[3].AppendLine(@"  $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            Titles[3].AppendLine(@"  $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ |");
            Titles[3].AppendLine(@"  \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ |");
            Titles[3].AppendLine(@"   \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__|");
            Titles[3].AppendLine(@"  $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                    ");
            Titles[3].AppendLine(@"  \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            Titles[3].AppendLine(@"   \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__|");

            Titles[4].AppendLine("\n\n\n\n");
            Titles[4].AppendLine(@"   $$$$$$\  $$\   $$\  $$$$$$\  $$\   $$\ $$$$$$$$\        $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\       $$\ $$\ ");
            Titles[4].AppendLine(@"  $$  __$$\ $$$\  $$ |$$  __$$\ $$ | $$  |$$  _____|      $$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$ |$$ |");
            Titles[4].AppendLine(@"  $$ /  \__|$$$$\ $$ |$$ /  $$ |$$ |$$  / $$ |            $$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ |$$ |");
            Titles[4].AppendLine(@"  \$$$$$$\  $$ $$\$$ |$$$$$$$$ |$$$$$  /  $$$$$\          $$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |$$ |");
            Titles[4].AppendLine(@"   \____$$\ $$ \$$$$ |$$  __$$ |$$  $$<   $$  __|         $$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         \__|\__|");
            Titles[4].AppendLine(@"  $$\   $$ |$$ |\$$$ |$$ |  $$ |$$ |\$$\  $$ |            $$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |                    ");
            Titles[4].AppendLine(@"  \$$$$$$  |$$ | \$$ |$$ |  $$ |$$ | \$$\ $$$$$$$$\       \$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\       $$\ $$\ ");
            Titles[4].AppendLine(@"   \______/ \__|  \__|\__|  \__|\__|  \__|\________|       \______/ \__|  \__|\__|     \__|\________|      \__|\__|");

            StartButton =
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
            //InputKey.Update();
            _startButtontimer += TimeManager.Instance.ElapsedMs;
            _TitleAnimTimer += TimeManager.Instance.ElapsedMs;
            switch (InputManager.Instance.Key)
            {
                case ConsoleKey.Enter:
                    SceneManager.Instance.ChangeScene(_nextSceneName);
                    return;
                case ConsoleKey.Escape:
                    Environment.Exit(1);
                    break;
            }
            if(_startButtontimer > 250)
            {
                _flicking = !_flicking;
                _startButtontimer = 0;
            }
            if(_TitleAnimTimer > 10)
            {
                _titleAnimationindex = RandomManager.Instance.GetRandomInt(4);
                _TitleAnimTimer = 0;
            }
        }

        public override void Render()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, 0);
            Console.Write(Titles[_titleAnimationindex]);

            if (_flicking == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.SetCursorPosition(0, 15);
            Console.Write(StartButton);

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(50, 27);
            Console.Write("Press Enter...");
        }
    }
}
