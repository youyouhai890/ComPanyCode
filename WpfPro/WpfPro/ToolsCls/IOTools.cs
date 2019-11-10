using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfPro.ManageAllCls;

namespace WpfPro.ToolsCls
{
    class IOTools
    {
        

        //写入内容,创建或覆盖文件
        public static void WriteFile(string TxtPath , string Content)
        {
            //将创建文件流对象的过程写在using当中，会自动的帮助我们释放流所占用的资源
            using (FileStream fsWrite = new FileStream(TxtPath, FileMode.Create, FileAccess.Write))
            {
                //byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(Content);
                byte[] buffer = Encoding.UTF8.GetBytes(Content);
                fsWrite.Write(buffer, 0, buffer.Length);
                fsWrite.Flush();
                fsWrite.Close();
                //释放流所占用的资源
                fsWrite.Dispose();

            }

        }

        //读取文件
        public static string ReadFile(string TxtPath)
        {

            //1.创建FileStream类对象
            FileStream fsread = new FileStream(TxtPath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024 * 1024 * 2];    //定义一个2M的字节数组
             //返回本次实际读取到的有效字节数,好处在于不用考虑换位符
            int r = fsread.Read(buffer, 0, buffer.Length);    //每次读取2M放到字节数组里面


            //将字节数组中每一个元素按照指定的编码格式解码成字符串
            string Content = Encoding.UTF8.GetString(buffer, 0, r);
            //string Content = Encoding.GetEncoding("GB2312").GetString(buffer, 0, r);
            fsread.Flush();
            //关闭流
            fsread.Close();
            //释放流所占用的资源
           // fsread.Dispose();
            return Content;
        }




        //拷贝文件,拷贝已有文件
        public static void CopyFile2(string source, string target)    //自定义文件复制函数
        {
            //创建负责读取的流
            using (FileStream fsread = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read))
            {
                //创建一个负责写入的流
                using (FileStream fswrite = new FileStream(target, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];    //声明一个5M大小的字节数组
                    //因为文件有58.3M,要循环读取
                    while (true)
                    {
                        int r = fsread.Read(buffer, 0, buffer.Length);    //返回本次实际读取到的字节数
                        //如果返回一个0时，也就意味着什么都没有读到，读取完了
                        if (r == 0)
                            break;
                        fswrite.Write(buffer, 0, r);

                    }

                }

            }
        }


        //拷贝文件,没有文件时
        public static void CopyFile(string source, string target)    //自定义文件复制函数
        {
           string SrcCont= ReadFile(source); //读取
            WriteFile(target, SrcCont); //写入
        }



        /// <summary>
        /// 创建,读取,写入模板内容
        /// </summary>
        /// <param name="path">模板路径</param>
        /// <param name="TextCont">获取的模板内容</param>
        /// <returns></returns>
        public static string WRTemp(string path, string Cont)
        {
            if (File.Exists(path))     //检测模版是否存在
            {
                //读取内容
                Cont = IOTools.ReadFile(path);
            }
            else  //如果不存在需要先创建文件并写入默认内容,然后再读取
            {
                //先写入
                IOTools.WriteFile(path, Configs.Config.SetTemplateText_1());
                //后读取
                Cont = IOTools.ReadFile(path);
            }
                return Cont;
        }

        //读取本地数据
        public static string WRLoc(string path)
        {
            string Cont;
            if (File.Exists(path))     //检测模版是否存在
            {
                //读取内容
                Cont = IOTools.ReadFile(path);
            }
            else  //如果不存在需要先创建文件并写入默认内容,然后再读取
            {
                //先写入
                IOTools.WriteFile(path, "");
                //后读取
                Cont = IOTools.ReadFile(path);
            }

            return Cont;

        }

        //字符串转成字节数组
        public static byte[] StrToBytes(string TxtContent)
        {

            byte[] bytes = Encoding.UTF8.GetBytes(TxtContent); 
            return bytes;

        }
        //字节转字符串
        public static string BytesToStr(byte[] TxtContent)
        {

           string str = Encoding.UTF8.GetString(TxtContent);  
            return str;

        }


        //Uri图片转成字节数组 , Uri为图片资源https://img.alicdn.com/imgextra/i4/3012913363/O1CN011kQCrv1aiIRxYd7xt_!!3012913363.jpg
        public static byte[] UriToBytes(string Uri)
        {

            FileStream fs = new FileStream(Uri, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
            byte[] ImgBytes = br.ReadBytes((int)fs.Length); //将流读入到字节数组中
            return ImgBytes;
        }


        //BitmapImage图片转换为byte[]：
        public static byte[] ImgToBytes(BitmapImage bmp)
        {

            Bitmap bp = new Bitmap(bmp.StreamSource);
            MemoryStream ms = new MemoryStream();
            bp.Save(ms, ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
            ms.Close();

            return bytes;

        }




        //  将Bitmap转换成BitmapImage对象：
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }


        //合并Bytes数组
        public static byte[] MergeBytes(byte[] Bytes_1 ,byte[] Bytes_2)
        {

            byte[] AllBytes = new byte[Bytes_1.Length + Bytes_2.Length];
            Bytes_1.CopyTo(AllBytes, 0);    //应该是Bytes_1拷贝到AllBytes里,从0开始
            Bytes_2.CopyTo(AllBytes, Bytes_1.Length); //应该是Bytes_1拷贝到AllBytes里,从 Bytes_1.Length开始


            //MessageBox.Show("合并字节后---------"+Encoding.Default.GetString(all));

            return AllBytes;
        }

    }


}
