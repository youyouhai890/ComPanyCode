using Common.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ToolsCls
{
    class PathTools
    {
        public static string StartPath = "";      //程序启动路径(exe文件在的路径)
        public static string _getRootConf ;          //根路径下的config
        public static string _debugConf;         //debug下的config
        public static string _getHttpInf;         //接口路径文件


        public static string _localdatapath;         //用来存储本地数据的目录
        public static string _localdatagenfafile;    //跟发文件
        public static string _localdatawechatfile;      //微信群文件
        //项目根目录下config目录(配置文件目录)
        public static string RootConf
        {
            get
            {

                // string rootpath=string.Empty;
                if (StartPath.Trim() == "" || StartPath.Trim() == null)
                {
                    // StartPath = "D:\\MyWorkspase\\WPF_Project\\WpfPro\\WpfPro";
                    StartPath = GetProjectRootPath()+"\\";   //获取项目根路径
                }

                // StartPath = "D:\\MyWorkspase\\WPF_Project\\WpfPro\\WpfPro\\Configs";
                _getRootConf = StartPath + "Configs\\";

                //配置文件目录
                return _getRootConf;

            }

        }


        //bin下的debug下的config目录(独立运行的目录)
        public static string DebugConf 
        {
            get
            {
                //D:\\MyWorkspase\\WPF_Project\W\pfPro\\WpfPro\\bin\Debug\\Configs\\
                _debugConf = AppDomain.CurrentDomain.BaseDirectory + "Configs\\";

                if (!Directory.Exists(_debugConf))     //目录不存在自动创建
                {
                    Directory.CreateDirectory(_debugConf);
                }

                //配置文件目录
                return _debugConf;

            }

        }

        //用来存储本地数据的目录
        public static string LocalDataPath
        {
            get
            {
                //D:\\MyWorkspase\\WPF_Project\W\pfPro\\WpfPro\\bin\Debug\\Configs\\
                _localdatapath = AppDomain.CurrentDomain.BaseDirectory + "LocalData\\";

                if (!Directory.Exists(_localdatapath))     //目录不存在自动创建
                {
                    Directory.CreateDirectory(_localdatapath);
                }

                //配置文件目录
                return _localdatapath;

            }

        }

        //本地跟发数据文件
        public static string LocalDataGenFaFile
        {
            get
            {
               string path  = LocalDataPath + "LocGenFa.txt";

                _localdatagenfafile = path;

                return _localdatagenfafile;
            }
        }

        //本地微信群文件
        public static string LocalDataWeChatFile
        {
            get
            {
                string path = LocalDataPath + "LocWeChat.txt";

                _localdatawechatfile = path;

                return _localdatawechatfile;
            }
        }

        /// <summary>
        /// 获得项目的根路径
        /// </summary>
        /// <returns></returns>
        public static string GetProjectRootPath()
        {
            string rootPath = "";
            // D:\MyWorkspase\WPF_Project\WpfPro\WpfPro\bin\Debug\,向上回退三级，才能到项目的根目录
            string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            // 第一个\是转义符，所以要写两个,D:\MyWorkspase\WPF_Project\WpfPro\WpfPro\bin\Debug
            rootPath = BaseDirectoryPath.Substring(0, BaseDirectoryPath.LastIndexOf("\\"));

            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\")); //D:\MyWorkspase\WPF_Project\WpfPro\WpfPro\bin
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\")); //D:\MyWorkspase\WPF_Project\WpfPro\WpfPro
            return rootPath;
        }

        //独立运行的接口文件
        public static string HttpInfPath
        {
            get
            {
                
                //检测配置文件是否存在,在的话拷贝
                if (Directory.Exists(PathTools.RootConf))
                {
                    //拷贝配置到debug里
                    string src = PathTools.RootConf + "AllInfsTxt.ini";
                    string des = PathTools.DebugConf + "AllInfsTxt.ini";
                    IOTools.CopyFile(src, des);
                    _getHttpInf = des; //获取文件信息

                }
                else
                {
                    _getHttpInf = PathTools.DebugConf + "AllInfsTxt.ini";
                }

                return _getHttpInf;
            }

        }


    }
}
