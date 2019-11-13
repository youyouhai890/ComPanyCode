using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfPro.HttpJsons.JsonModel
{
    [DataContract]
    public class ProdDataModel
    {
        public ProdDataModel() { }

        [DataMember]
        public pageInfo pageInfo { get; set; }

        [DataMember]
        public List<TurnListModel> list { get; set; }
    }

    [DataContract]
    public class pageInfo
    {
        public pageInfo() { }

        [DataMember]
        public int curPage { get; set; }
        [DataMember]
        public int pageSize { get; set; }
        [DataMember]
        public int totalCount { get; set; }
    }

}
