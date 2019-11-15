using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ManageAllCls
{
    class ManageAllStateClscs<T>
    {

        private static ManageAllStateClscs<T> _instance = null;
        private static readonly object StatSynObject = new object();    //锁


        public bool ThreadState = true;     //用于线程的状态


        public static ManageAllStateClscs<T> GetInstance
        {
            get
            {
                // Syn operation.
                lock (StatSynObject)
                {
                    if (_instance == null)
                    {
                        _instance = new ManageAllStateClscs<T>();
                    }

                    return _instance;
                }
            }
        }






    }
}
