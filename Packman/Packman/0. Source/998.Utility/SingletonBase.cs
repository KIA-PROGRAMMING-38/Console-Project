using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packman
{
    internal class SingletonBase<T> where T : class, new()
    {
        private static T instance = new T();
        public static T Instance {  get { return instance; } }

        // 생성자 차단..
        protected SingletonBase() { }
    }
}
