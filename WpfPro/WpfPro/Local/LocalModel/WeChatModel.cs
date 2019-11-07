using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfPro.ManageAllCls;

namespace WpfPro.Local.LocalModel
{
    [DataContract]
    class WeChatModel
    {
       
        static int AddOne = 1;
        [DataMember]
        public int id { get; set; } = 0;
        [DataMember]
        public string QunMingCheng { get; set; } = "";


        //有参构造
        public WeChatModel( string QUNMINGCHENG)
        {

            this.id = AddOne++;
            this.QunMingCheng = QUNMINGCHENG;
            MyInfo.GetInstance.WeChatList.Add(this);     //自动关联对象
        }
    }
}
