using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;

namespace WpfPro.ToolsCls
{
    //类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时，new() 约束必须最后指定。
    class GetOrSetTools<T> where T : Window, new()
    {

#region 自动匹配ListView匹配的字段内容


        //处理转链后的对象 , 第一个为替换的模版内容 ,  参数2替换的对象
        public static string GetAssocaObj( string TempTxt, TurnDataModel obj)
        {
            //字符串
            string Content = TempTxt.Trim();
             Content = RegularTools.NullJsonMod(Content); //检测空值

            //获取对象属性
            IDictionary<string,object> ObjNamVal= GetAllVariable(obj);

            //转换格式{xxx},yyy
            //遍历键值对, 获取对象的所有变量名和值 , key变量名称 , val变量值
            foreach (KeyValuePair<string, object> item in ObjNamVal)
            {
                //替换占位符{}里面的内容
                Content = GetStrReplace(Content, "{"+item.Key+"}".Trim(), item.Value.ToString().Trim());
            }
            return Content.Trim();

        }

        //重载方法,遍历显示对象
        public static string GetAssocaObj(string TempTxt, ProdListModel obj)
        {
            //字符串
            string Content = TempTxt.Trim();
            Content = RegularTools.NullJsonMod(Content); //检测空值

            //获取对象属性
            IDictionary<string, object> ObjNamVal = GetAllVariable(obj);


            for(int count = 0; count < ObjNamVal.Count; count++)
            {
                var element = ObjNamVal.ElementAt(count);
                string Key = element.Key;
                string val = Convert.ToString(element.Value) ;

                Content = GetStrReplace(Content, "{" + Key + "}".Trim(), val);

            }

            return Content.Trim();

        }

#endregion



#region 遍历对象的所有属性



        //遍历对象所有属性
        public static IDictionary<string, object> GetAllVariable(TurnDataModel obj)
        {
            
            TurnDataModel tdm = obj as TurnDataModel;
            IDictionary<string,object> dic = new Dictionary<string, object>();

            StringBuilder msg = new StringBuilder();
            //T entity = new T();

            //遍历对象所有属性
            foreach (PropertyInfo p in tdm.GetType().GetProperties())
            {
                dic.Add(p.Name, p.GetValue(tdm));//变量名和变量的值
                //msg.AppendFormat("{0},{1}", p.Name, p.GetValue(tdm));
            }

             return dic;
        }


//重载方法
        public static IDictionary<string, object> GetAllVariable(ProdListModel obj)
        {

            ProdListModel tdm = obj as ProdListModel;
            IDictionary<string, object> dic = new Dictionary<string, object>();

            StringBuilder msg = new StringBuilder();
            //T entity = new T();

            //遍历对象所有属性
            foreach (PropertyInfo p in tdm.GetType().GetProperties())
            {
                dic.Add(p.Name, p.GetValue(tdm));//变量名和变量的值
                //msg.AppendFormat("{0},{1}", p.Name, p.GetValue(tdm));
            }

            return dic;
        }

        //重载方法(泛型)
        public static IDictionary<object, object> GetAllVariable(T obj)
        {

            T tdm = obj as T;
            IDictionary<object, object> dic = new Dictionary<object, object>();

            StringBuilder msg = new StringBuilder();
            //T entity = new T();

            //遍历对象所有属性
            foreach (PropertyInfo p in tdm.GetType().GetProperties())
            {
                dic.Add(p.Name, p.GetValue(tdm));//获取变量名和变量的值
                //msg.AppendFormat("{0},{1}", p.Name, p.GetValue(tdm));
            }

            return dic;
        }

#endregion


        //获取默认模版 , 对应对象 , 默认模版ID
        public static string GetDefTemp()
        {
            string DeFTempl = MyInfo.GetInstance.MYTEMPLATE; //默认模版ID
            //ListView lv = obj;
            //Window targetWindow = Window.GetWindow(lv); //通过控件找窗体
            //ProductsListWin pwin = (ProductsListWin)targetWindow;		//窗口类型转换

            //获取配置文件路径
            string PathConfig = PathTools.DebugConf;

            string TextCont1 = string.Empty; //用于存储读取后的内容
            //默认模板路径
            string TempPath1 = PathConfig + "Template_"+ DeFTempl+".txt";
            //获取模板内容,如果模板不存在则重新创建
            TextCont1 = IOTools.WRTemp(TempPath1, TextCont1);

            return TextCont1;
        }


        //获取对象类型
        public static Type ToolsGetType(T obj)
        {
            //typef(obj)
            //方法1
            Type t = obj.GetType();
            MessageBox.Show("输出的类型为------" + t);
            return t;
        }


 #region 替换占位符对应字符串
        //参数1为字符串 , 2占位符,3要替换的内容
        public static string GetStrReplace(string cont, string PlacStr, string RepStr)
        {
            cont = cont.Replace(PlacStr, RepStr);  //电话
            return cont;

        }
 #endregion



 #region 获取指定控件的窗体
        //获取指定控件的窗体
        public static T GetParentWin(object obj)
        {
            Button CurrObj = obj as Button;
            Window targetWindow = Window.GetWindow(CurrObj); //通过控件找窗体
            T Win = targetWindow as T;		//类型转换

            return Win;
        }

        public static T GetItemParentWin(object mi)
        {

                MenuItem CurrObj = mi as MenuItem;
                Window targetWindow = Window.GetWindow(CurrObj); //通过控件找窗体
                T Win = targetWindow as T;      //类型转换
                return Win;
            
        }
 #endregion



    }
}
