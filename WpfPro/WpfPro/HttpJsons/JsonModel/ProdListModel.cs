using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.HttpJsons.JsonModel
{
    [DataContract]
    public class ProdListModel
    {
        public ProdListModel() { }
        [DataMember]
        public int BianHao { get; set; } = 0;
        [DataMember]
        public string auctionId { get; set; }="";
        [DataMember]
        public string zkPrice { get; set; }="";
        [DataMember]
        public string couponLeftCount { get; set; }="";
        [DataMember]
        public string userType { get; set; }="";
        [DataMember]
        public string planType { get; set; }="";
        [DataMember]
        public string couponAmount { get; set; }="";
        [DataMember]
        public string source { get; set; }="";
        [DataMember]
        public string maxCommissionRate { get; set; }="";
        [DataMember]
        public string afterCouponPrice { get; set; }="";
        [DataMember]
        public string id { get; set; }="";
        [DataMember]
        public string pictUrl { get; set; }="";
        [DataMember]
        public string miniPictUrl { get; set; }="";
        [DataMember]
        public string auctionUrl { get; set; }="";
        [DataMember]
        public string couponTotalCount { get; set; }="";
        [DataMember]
        public string biz30day { get; set; }="";
        [DataMember]
        public string dayLeft { get; set; }="";
        [DataMember]
        public string title { get; set; }="";
        [DataMember]
        public string sourceUrl { get; set; }="";
        [DataMember]
        public string createTime { get; set; }="";
        [DataMember]
        public string campaignId { get; set; }="";
        [DataMember]
        public string shopkeeperId { get; set; }="";
        [DataMember]
        public string category { get; set; }="";
        [DataMember]
        public string shopTitle { get; set; }="";
        [DataMember]
        public string couponUrl { get; set; }="";
        [DataMember]
        public string displayType { get; set; }="";
        [DataMember]
        public string syncFlag { get; set; }="";
        [DataMember]
        public string extendDesc { get; set; }="";
        [DataMember]
        public string activityId { get; set; }="";
        [DataMember]
        public string existCommissionRate { get; set; }="";
        [DataMember]
        public string showCommissionRate { get; set; }="";
        [DataMember]
        public string couponStatus { get; set; }="";
        [DataMember]
        public string ulandUrl { get; set; }="";
        [DataMember]
        public string applyFlag { get; set; }="";
        [DataMember]
        public string dxFlag { get; set; }="";
        [DataMember]
        public string effectiveEndTime { get; set; }="";
        [DataMember]
        public string categoryDesc { get; set; }="";
        [DataMember]
        public string couponType { get; set; }="";
        [DataMember]
        public string forceFlag { get; set; }="";
        [DataMember]
        public string couponLeftRate { get; set; }="";
        [DataMember]
        public string explosionFlag { get; set; }="";
        [DataMember]
        public string explosionBannerUrl { get; set; }="";
        [DataMember]
        public string platform { get; set; }="";
        [DataMember]
        public string fanicTime { get; set; }="";
        [DataMember]
        public string isVideoBuy { get; set; }="";
        [DataMember]
        public string isFanicBuy { get; set; }="";
        [DataMember]
        public string fanicStatus { get; set; }="";
        [DataMember]
        public string videoUrl { get; set; }="";
        [DataMember]
        public string isHideBuyRate { get; set; }="";
        [DataMember]
        public string itemTotal { get; set; }="";
        [DataMember]
        public string alreadyBuyedCount { get; set; }="";
        [DataMember]
        public string couponEffectTime { get; set; }="";
        [DataMember]
        public string bizHot { get; set; }="";
        [DataMember]
        public string deposit { get; set; }="";
        [DataMember]
        public string depositDeduction { get; set; }="";
        [DataMember]
        public string twoHoursBiz { get; set; }="";
        [DataMember]
        public string goodsType { get; set; }="";
        [DataMember]
        public string freeOrderNum { get; set; }="";
        [DataMember]
        public string freeOrderAmount { get; set; }="";
        [DataMember]
        public string categoryName { get; set; }="";
        [DataMember]
        public string categoryId { get; set; }="";
        [DataMember]
        public string combinationOrderCount { get; set; }="";
        [DataMember]
        public string combinationShopActivityAmount { get; set; }="";
        [DataMember]
        public string combinationDescription { get; set; }="";
        [DataMember]
        public string presalePayAmount { get; set; }="";
        [DataMember]
        public string presalePayDeductAmount { get; set; }="";
        [DataMember]
        public string presaleArrearsAmount { get; set; }="";
        [DataMember]
        public string extAfterCouponAmount { get; set; }="";
        [DataMember]
        public string goodsTypeDescription { get; set; }="";
        [DataMember]
        public string xjFreeship { get; set; }="";
    }

}
