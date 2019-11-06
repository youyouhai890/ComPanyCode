
using System;
using System.Runtime.Serialization;



//要发送(序列化)的Json封装的对象
namespace WpfPro.HttpJsons
{

    //  {"code":200, "message":"发送成功", "data":""}
    [DataContract]
    class T
    {
        private int _code = 0;
        private string _message = "";
        private string _data = "";
        //private List<FarmInfo> _farmList = new List<FarmInfo>();

        public T()
        { }

        [DataMember]
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }
        [DataMember]

         public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        [DataMember]
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        internal static void EnpJsonObj<T>(T model)
        {
            throw new NotImplementedException();
        }
        //[DataMember]
        //public List<FarmInfo> FarmList
        //{
        //    get { return _farmList; }
        //    set { _farmList = value; }
        //}




    }
}
