using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;
using WpfPro.ToolsCls;

namespace WpfPro.Local.LocalModel
{
    [DataContract]
    class GenFaMoel : TurnListModel
    {

        //下面都是往Listview显示数据的部分
        int GfId = MyInfo<object>.GetInstance.GenfaList.Count+1;
        [DataMember]
        public string gid { get; set; } = "";                
     
        public  GenFaMoel() { }

        //构造函数
        public GenFaMoel(TurnListModel tm)
        {
            //基类的属性
            this.gid = GfId.ToString().Trim();     //用继承的属性
            base.state = "未发送";


            //克隆对象的值,第二个为被克隆的对象
            GetOrSetTools<TurnListModel>.ReflectCloneObj(tm, this);
           MyInfo<object>.GetInstance.GenfaList.Add(this);     //自动关联对象
        }

    }
}
