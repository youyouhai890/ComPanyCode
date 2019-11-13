using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfPro.Controls;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;
using static WpfPro.ManageAllCls.ManaEnumCls;

namespace WpfPro.HttpJsons
{
    //抽象类
    public abstract class AbsInterfaces<T>
    {

       
        /// </summary>
        /// <param name="fun">方法名</param>
        /// <param name="inf">接口名</param>
        /// <param name="pram">参数</param>
        public static void AppInfFun(HttpInterf inf,Action<MLoginJson<T>> fun, params string[] pram)
        {
            try
            {
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                //string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.ShangPinLieBiao,
                //   keyword, "1011", "1", "2", "15", uid);
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result = TestCls.FlagTest(AllInterfaceCls.TestFlag ,inf , pram);

                //字符串反序列化,泛型为Data类型
                //  ProdJsonObj<ProdDataModel> model = SerializationTools<ProdJsonObj<ProdDataModel>>.RevJsonObj(result);
                MLoginJson<T> model = SerializationTools<MLoginJson<T>>.RevJsonObj(result);

                if (model.code == 200)  //200为成功
                {
                    ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");
                    // MessageBox.Show(model.message);

                    //必须得是主线程才有值,并且必须在InitializeComponent后面
                    //  sync = SynchronizationContext.Current;
                    fun(model);  //执行方法
                }
                else if (model.code == 500) //设置PID
                {
                    MessageBox.Show(model.message);

                }
                else if(model.code == 5000)
                {
                    MessageBox.Show(model.message);
                }
                else
                {
                    MessageBox.Show("找不到接口匹配项...");
                    //return;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //用于转联页面
        public static MLoginJson<T> AppInfFun2(HttpInterf inf, params string[] pram)
        {
            try
            {
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                //string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.ShangPinLieBiao,
                //   keyword, "1011", "1", "2", "15", uid);
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, inf, pram);

                //字符串反序列化,泛型为Data类型
                //  ProdJsonObj<ProdDataModel> model = SerializationTools<ProdJsonObj<ProdDataModel>>.RevJsonObj(result);
                MLoginJson<T> model = SerializationTools<MLoginJson<T>>.RevJsonObj(result);

                return model;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
