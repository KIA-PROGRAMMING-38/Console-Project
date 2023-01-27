using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    // TEMP 후에 활용할 수 있으면 활용..
    internal class Singleton<T> where T : class, new()
    {
        private static Lazy<T> _instance = new Lazy<T>(()=> new T());
        public static T Instance { get { return _instance.Value; } }
    }
}
