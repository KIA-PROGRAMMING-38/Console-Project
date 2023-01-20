using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class TitleScene : Scene
    {
        public TitleScene(string nextScene)
        {
            _nextSceneName = nextScene;
        }
        public InputKeyComponent InputKey;
        public StringBuilder builder;

        public override void Start()
        {
            InputKey = new InputKeyComponent();

            SoundManager.Instance.Play("TitleBackgroundMusic", true);

            builder = new StringBuilder();
            builder.Append("\n\n\n\n");
            builder.AppendLine(@"    /$$$$$$  /$$   /$$  /$$$$$$  /$$   /$$ /$$$$$$$$        /$$$$$$   /$$$$$$  /$$      /$$ /$$$$$$$$       /$$ /$$ ");
            builder.AppendLine(@"   /$$__  $$| $$$ | $$ /$$__  $$| $$  /$$/| $$_____/       /$$__  $$ /$$__  $$| $$$    /$$$| $$_____/      | $$| $$ ");
            builder.AppendLine(@"  | $$  \__/| $$$$| $$| $$  \ $$| $$ /$$/ | $$            | $$  \__/| $$  \ $$| $$$$  /$$$$| $$            | $$| $$ ");
            builder.AppendLine(@"  |  $$$$$$ | $$ $$ $$| $$$$$$$$| $$$$$/  | $$$$$         | $$ /$$$$| $$$$$$$$| $$ $$/$$ $$| $$$$$         | $$| $$ ");
            builder.AppendLine(@"   \____  $$| $$  $$$$| $$__  $$| $$  $$  | $$__/         | $$|_  $$| $$__  $$| $$  $$$| $$| $$__/         |__/|__/ ");
            builder.AppendLine(@"   /$$  \ $$| $$\  $$$| $$  | $$| $$\  $$ | $$            | $$  \ $$| $$  | $$| $$\  $ | $$| $$                     ");
            builder.AppendLine(@"  |  $$$$$$/| $$ \  $$| $$  | $$| $$ \  $$| $$$$$$$$      |  $$$$$$/| $$  | $$| $$ \/  | $$| $$$$$$$$       /$$ /$$ ");
            builder.AppendLine(@"   \______/ |__/  \__/|__/  |__/|__/  \__/|________/       \______/ |__/  |__/|__/     |__/|________/      |__/|__/ ");

            string[] StartButton = new string[2];
        }

        public override void Update()
        {
            InputKey.Update();
            switch (InputKey.Key)
            {
                case ConsoleKey.Enter:
                    SceneManager.Instance.ChangeScene(_nextSceneName);
                    return;
                    break;
            }
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(builder.ToString());
        }
    }
}
