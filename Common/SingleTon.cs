using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SingleTon<T> where T : class, new()
    {
        public static object obj = new object();
        public static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                            return _instance;
                        }
                        else
                        {
                            return _instance;
                        }
                    }
                }
                else
                {
                    return _instance;
                }
            }
        }
    }
}
