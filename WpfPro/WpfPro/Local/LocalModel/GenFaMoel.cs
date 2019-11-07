using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPro.ManageAllCls;

namespace WpfPro.Local.LocalModel
{
    class GenFaMoel
    {
        public int id { get; set; } = 0;
        public string time { get; set; } = "";
        public string title { get; set; } = "";
        public string state { get; set; } = "";

        //构造函数
        public GenFaMoel(int ID, string TIME, string TITLE, string STATE)
        {
            
            this.id = ID;
            this.time = TIME;
            this.title = TITLE;
            this.state = STATE;
             MyInfo.GetInstance.GenfaList.Add(this);     //自动关联对象
        }

    }
}
