using System;
using Common.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Net;
using WpfPro.ToolsCls;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WpfPro.HttpJsons
{
    //序列化和反序列化
    class SerializationTools<T>
    {

        //序列化JSON(封装字符串),泛型为要序列化的类型
        public static string EnpJsonObj(T  obj )
        {

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            //将序列化之后的Json格式数据写入流中
            js.WriteObject(ms, obj);
            ms.Position = 0;
            //从0这个位置开始读取流中的数据
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string json = sr.ReadToEnd();
            sr.Close();
            ms.Close();

           // MessageBox.Show("序列化后----------" + json.ToString());

            return json.ToString().Trim();

        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////

        //反序列化(解析Json返回对象),参数为Json字符串
        public static T RevJsonObj(string JsonObj)
        {


            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            string strRecv = webClient.DownloadString(JsonObj);
            //检测空值
            string mod = RegularTools.NullJsonMod(strRecv);
            T t = JsonHelper.Deserialize<T> (mod);    //返回对象,用泛型的类型转换对象

           // MessageBox.Show("解析后的对象----------" + SerializationTools<T>.EnpJsonObj(t));
            return t;       //返回实体对象

        }

    }
}
