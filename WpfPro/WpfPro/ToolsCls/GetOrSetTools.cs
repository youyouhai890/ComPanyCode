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
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;

namespace WpfPro.ToolsCls
{
    //类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时，new() 约束必须最后指定。
    class GetOrSetTools<T> where T : class, new()
    {

#region 自动匹配模板内容字段


        //处理转链后的对象 , 第一个为替换的模版内容 ,  参数2替换的对象
        public static string GetAssocaObj( string TempTxt, T obj)
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



        
#endregion





        //获取默认模版 , 对应对象 , 默认模版ID
        public static string GetDefTemp()
        {
            string DeFTempl = MyInfo<object>.GetInstance.MYTEMPLATE; //默认模版ID
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



#region 反射部分,

        //利用反射来创建对象(用字符串类型创建对象)
        public static T ReflectCreatObj(string TypeName)
        {
            //注意类型名称必须是全名 , 包含命名空间
            Type type = Type.GetType(TypeName);
            dynamic obj = Activator.CreateInstance(type,true);  //返回object对象

            return (T)obj;
        }



        //遍历对象所有属性
        public static IDictionary<string, object> GetAllVariable(T obj)
        {

            T tdm = obj as T;
            IDictionary<string, object> dic = new Dictionary<string, object>();

            StringBuilder msg = new StringBuilder();
            //T entity = new T();

            //遍历对象所有属性
            foreach (PropertyInfo p in tdm.GetType().GetProperties())
            {
                if (p.GetValue(tdm) == null)
                {
                    dic.Add(p.Name, "");//变量名和变量的值

                }
                else
                {
                    dic.Add(p.Name, p.GetValue(tdm));//变量名和变量的值
                }
                //msg.AppendFormat("{0},{1}", p.Name, p.GetValue(tdm));
            }

            return dic;
        }




        //反射获取变量名称和值,判断变量类型

        public static void ReflectGetNameOrVal(T obj,T obj2)
        {
            // T RObj = ReflectCreatObj(obj.GetType().ToString());  //用某个对象的类型来,反射实例化

            string values = string.Empty;
            foreach (PropertyInfo p in obj.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string))   //判断类型
                {
                    values += string.Format("{0}='{1}', ", p.Name, p.GetValue(obj));
                    p.SetValue(obj2, Convert.ChangeType(p.GetValue(obj), p.PropertyType),null);   // Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型
                }
                if (p.PropertyType == typeof(int)|| p.PropertyType == typeof(uint))  //判断类型
                {
                    values += string.Format("{0}={1},", p.Name, p.GetValue(obj));
                    p.SetValue(obj2, Convert.ChangeType(p.GetValue(obj), p.PropertyType), null);   //Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型

                }
                if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(double)|| p.PropertyType == typeof(float))
                {
                    values += string.Format("{0}={1}, ", p.Name, p.GetValue(obj));
                    p.SetValue(obj2, Convert.ChangeType(p.GetValue(obj), p.PropertyType), null);   // Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型

                }
                if (p.PropertyType == typeof(bool))
                {
                    values += string.Format("{0}={1}, ", p.Name, p.GetValue(obj));
                    p.SetValue(obj2, Convert.ChangeType(p.GetValue(obj), p.PropertyType), null);   //Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型

                }
                if (p.PropertyType == typeof(long) || p.PropertyType == typeof(ulong))
                {
                    values += string.Format("{0}={1}, ", p.Name, p.GetValue(obj));
                }
                if (p.PropertyType == typeof(DateTime))
                {
                    values += string.Format("{0}='{1}', ", p.Name, p.GetValue(obj));
                }

                if (p.PropertyType == typeof(sbyte))
                {
                    values += string.Format("{0}={1}, ", p.Name, p.GetValue(obj));
                }
                if (p.PropertyType == typeof(byte) || p.PropertyType == typeof(short) || p.PropertyType == typeof(ushort))
                {
                    values += string.Format("{0}={1}, ", p.Name, p.GetValue(obj));
                }

                values += string.Format("{0}={1},", p.Name, p.GetValue(obj));
                Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(obj));
            }

        }


        //合并对象的数据
        public static T ReflectMergeData(T obj1, T obj2)
        {
            //要存储数据的新对象
              T RObj = ReflectCreatObj(obj1.GetType().ToString()); //用某个对象的类型来,反射实例化

            //Type type = typeof(T);
           // PropertyInfo[] pi = type.GetProperties();
           //同上
            PropertyInfo[] p0 = typeof(T).GetProperties();  //获取没有实例化的属性集合(没有值)

            PropertyInfo[] p1 =  obj1.GetType().GetProperties();     //获取已经实例化的属性集合(带有值)
            PropertyInfo[] p2 =  obj2.GetType().GetProperties();     //获取已经实例化的属性集合(带有值)


            for (int i = 0; i < p1.Length; i++)
            {
                var val = p1[i].GetValue(obj1); //遍历第一个对象的所有值

                //第一个对象的值为空时,获取第二个对象的值
                    if (val == null|| val.ToString().Trim() == "" || val.ToString().Trim() == "0" ||  val.ToString().Trim() == "0.0")       //如果第一个对象的属性(变量)为空时,用第二个属性值
                    {
                        //如果值为空将第二个对象的值p2[i].GetValue(obj2)赋给对象一 , RObj为被赋值的对象
                        p0[i].SetValue(RObj, Convert.ChangeType(p2[i].GetValue(obj2), p2[i].PropertyType), null);   // Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型
                    }
                    else
                    {
                        p0[i].SetValue(RObj, Convert.ChangeType(p1[i].GetValue(obj1), p1[i].PropertyType), null);   // Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型
                    }

            }

            return RObj;          //返回的应该是序列化的字符串
        }




        //克隆对象,第二个为被克隆的对象
        public static void ReflectCloneObj(T obj1, T obj2)
        {


            PropertyInfo[] p1 = obj1.GetType().GetProperties();     //获取已经实例化的属性集合(带有值)
            PropertyInfo[] p2 = obj1.GetType().GetProperties();     //获取已经实例化的属性集合(带有值)


            for (int i = 0; i < p1.Length; i++)
            {
                var val = p1[i].GetValue(obj1); //遍历第一个对象的所有值
                p1[i].SetValue(obj2, Convert.ChangeType(val, p1[i].PropertyType), null);   // Convert.ChangeType(p.GetValue(obj), p.PropertyType),应该是把值转换为指定类型
            }

        }

        #endregion


    }
}
