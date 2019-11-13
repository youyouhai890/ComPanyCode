using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static WpfPro.ManageAllCls.ManaEnumCls;

namespace WpfPro.HttpJsons.JsonModel
{

    //调用转链接口后用于接受的模型
    [DataContract]
      public  class TurnListModel : ProdListModel
    {
        public TurnListModel() { }


        [DataMember]
        public string tkl { get;  set; }="";                 //淘口令,二合一链接
        [DataMember]
        public string shortLink { get;  set; }="";           //短链接
        [DataMember]
        public string longLink { get;  set; }="";            //长链接
        [DataMember]
        public string res1 { get;  set; }="";
        [DataMember]
        public string tklTips { get;  set; }="";             
        //[DataMember]
        //public object couponAmount { get; set; } = "";       //劵金额,劵面额
        [DataMember]
        public string clickUrl { get;  set; }="";            //应该是点击的url图片地址
        //[DataMember]
        //public string maxCommissionRate { get;  set; }="";      //佣金比例

        /// ///////////////////不是从接口里获取的,是从商品列表获取的, 包含了子类要用的东西/////////////////////

        //[DataMember]
        //public string createTime { get; set; } = "";      //
        //[DataMember]
        //public string title { get; set; } = "";      //标题
        //[DataMember]
        //public string pictUrl { get; set; } = "";
        [DataMember]
        public string time { get; set; } = "";
        [DataMember]
        public string state { get; set; } = "";

    }



}
