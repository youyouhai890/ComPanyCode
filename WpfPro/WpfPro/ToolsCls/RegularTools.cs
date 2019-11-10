using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfPro.ToolsCls
{
    class RegularTools
    {

        //正则检测手机账号 , 匹配[_xxx_]
        public static string IsPid(string pid)
        {

            //判断一串字符必须是数字开头,长度必须为11位
            // Regex rgx = new Regex(@"^\\d{ 11}");//@表示后面的是字符

            //是否是纯数字,匹配11位
            Regex rgx = new Regex("_.*?_");//@表示后面的是字符    
            Match match = rgx.Match(pid);

            string res = string.Empty;
            if (match.Success)
            {
                //Groups的序号是从1开始的，0有特殊含义
                //  MessageBox.Show(match.Groups[0].Value.ToString());
                res = match.Groups[0].Value.ToString();

                res = res.Replace("_", "").Trim();  //再一次转换字符串

            }

            return res;
        }

        //json里的NULL检测
        public static string  NullJsonMod(string JsonStr)
        {
            //将Json空值 :, 修改为 :"",
            string str = JsonStr.Replace(@":," , ":\"\",").Trim();  //再一次转换字符串

            return str;
        }



        //正则检测手机账号, 匹配11位数字
        public static bool IsAccountPhone(string acp)
        {

            //判断一串字符必须是数字开头,长度必须为11位
            // Regex rgx = new Regex(@"^\\d{ 11}");//@表示后面的是字符

            //是否是纯数字,匹配11位
            Regex rgx = new Regex(@"^\d{11}");//@表示后面的是字符        
            return rgx.IsMatch(acp);
        }




        //正则检测,密码
        public static bool IsPassword(string pwd)
        {

            //^和$表示开始和结束位置,(?:。。。。。)表示这是一个非捕获组，提高匹配速度,
            //(?i)是模式修改符，表示不区分大小写, [a - z]表示是字母从a到z都可以 , + 表示最少一个，最多不限制
            //   Regex rgx = new Regex(@" ^(?: (? i)[a - z] +)$");      //不区分英文大小写

            //--------------------------
            // /^[a-zA-Z\d]+$/
            //^:表示字符串开始
            //[a - zA - Z\d] +:分为几部分:
            //a - z:表示小写字母a到z中任一个
            //A - Z:表示大写字母A到Z中任一个
            //\d: 表示任一数字
            //[xxx]:表示xxx集合内的字符
            //[xxx] +:表示xxx集合内的字符,一个或更多个。


            //判断一串字符是不是8-16 位字符,同时包含大小写字母和数字
            Regex rgx = new Regex(@"[a-zA-Z0-9]{6,15}");//@表示后面的是字符
            return rgx.IsMatch(pwd);
        }


        //获取符号{}内的内容
        public static Match GetTurnObjs(string str)
        {

            Regex rgx = new Regex("(?<=(" +   "{"   + "))[.\\s\\S]*?(?=(" +    "}"   + "))");//@表示后面的是字符    
            Match match = rgx.Match(str);

            return match;
        }



    }
}
