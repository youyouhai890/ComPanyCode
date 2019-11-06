using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ManageAllCls
{
    public class ObserverCls<T1>        //泛型为参数类型
    {
        
        //无返回值的函数
        //public delegate void Action()  ;
        //public delegate void Action<t1>(t1 CanShu1);
        //public delegate void Action<t1, t2>(t1 pram1, t2 pram2);

        ////有返回值的函数
        //public delegate void Func<t1>();
        //public delegate void Func<t1, t2>( t2 pram2);
        //public delegate void Func<t1, t2, t3>( t2 pram2, t3 pram3);


        public static void FunExec()
        {
            
        }


        //--------------------------------------

        //执行的方法没有返回值
        public static void DelegateFun(Action Fun )
        {
            //执行方法
            Fun(); 
        }

        //带参数的方法,没有返回值
        public static void DelegateFun(Action<T1> Fun , T1 pram1)
        {
            //执行方法
            Fun(pram1);
        }
        //带参数的方法,没有返回值
        //public static void DelegateFun(Action<T1, T2> Fun, T1 pram1 , T2 pram2)
        //{
        //    //执行方法
        //    Fun(pram1, pram2);
        //}

        //------------------------------------------------------


        //带返回值的方法,返回类型为T
        public static void DelegateFun(Func<T1> Fun)
        {
            //执行方法
            Fun();
        }

        //带返回值的方法,返回类为第一个T,带参数类型为第二个T
        //public static void DelegateFun(Func<T1, T2> Fun, T2 pram2)
        //{
        //    //执行方法
        //    Fun(pram2);
        //}

        //带返回值的方法,返回类为第一个T,带参数类型为第二个T
        //public static void DelegateFun(Func<T1, T2,T3> Fun, T2 pram2,T3 pram3)
        //{
        //    //执行方法
        //    Fun(pram2,pram3);
        //}
    }
}
