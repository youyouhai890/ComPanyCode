
using System.Runtime.Serialization;
using WpfPro.HttpJsons.JsonModel;


namespace WpfPro.HttpJsons
{
    [DataContract]
      public class MLoginJson<T>
    {
        public MLoginJson()
        { }

        [DataMember] 
        public int code { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public T data { get; set; }
    }
}
