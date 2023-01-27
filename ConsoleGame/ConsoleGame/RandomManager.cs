using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    internal class RandomManager : Random
    {
        private static RandomManager _instance;

        public static RandomManager GetInctance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RandomManager();
                }
                return _instance;
            }
            
        }


    }
}
