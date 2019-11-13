using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ManageAllCls
{
    class ParmObj
    {
        //用于参数
        public object[] ParmArray =null;
        public string[] ParmStrArray=null;

        public ParmObj()
        {

        }
        public ParmObj(int i)
        {
             ParmArray = new object[i];
                ParmStrArray = new string[i];
         }
    }
}
