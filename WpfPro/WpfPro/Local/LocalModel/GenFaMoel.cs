using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;

namespace WpfPro.Local.LocalModel
{
    [DataContract]
    class GenFaMoel
    {
        [DataMember]
        public TurnDataModel TDObj { get; set; } = new TurnDataModel();      //包装的数据,从这里获取信息

        //下面都是往Listview显示数据的部分
        int GfId = MyInfo.GetInstance.GenfaList.Count+1;
        [DataMember]
        public int id { get; set; } = 0;
        [DataMember]
        public string time { get; set; } = "";
        [DataMember]
        public string title { get; set; } = "";
        [DataMember]
        public string state { get; set; } = "";


        /// ///////////////////////////////////////////////
        [DataMember]
        public string tkl { get; set; } = "";                 //淘口令,二合一链接
        [DataMember]
        public string shortLink { get; set; } = "";           //短链接
        [DataMember]
        public string longLink { get; set; } = "";            //长链接
        [DataMember]
        public object couponAmount { get; set; } = "";       //劵金额,劵面额
        [DataMember]
        public string clickUrl { get; set; } = "";            //应该是点击的url图片地址
        [DataMember]
        public string maxCommissionRate { get; set; } = "";      //佣金比例

        //构造函数
        public GenFaMoel(TurnDataModel obj)
        {
            
            this.id = GfId;
            this.time = obj.createTime;
            this.title = obj.title;
            this.state = "未发送";

            TDObj = obj;
            MyInfo.GetInstance.GenfaList.Add(this);     //自动关联对象
        }

    }
}
