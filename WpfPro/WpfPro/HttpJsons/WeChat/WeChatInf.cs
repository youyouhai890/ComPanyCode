using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfPro.Forms.Products;
using WpfPro.ManageAllCls;
using WpfPro.ToolsCls;

namespace WpfPro.HttpJsons.WeChat
{
    class WeChatInf
    {
        /// <summary>
        /// 调用微信接口凭证access_token
        /// </summary>
        private static string test_access_token
        {
            get
            {
                return "XXXXXXXXXXXX";
            }
        }

        /// <summary>
        /// 新增其他类型永久素材，返回值{"media_id":"eZh1QTjGGSyE-i9k8uHZqrd5LpHfYBsKtUrSfnjf8k0",
        /// "url":"http:\/\/mmbiz.qpic.cn\/mmbiz_png\/gHnmqhvpvh5HoibMEcGEAK4eAKvIR18kuKoXbjCiaRa1p1WTBgicYMDvqkjTadib21KUWYpibzfuXj6ibRw8ibw\/0?wx_fmt=png"}
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <param name="path">图片物理文件路径</param>
        /// <returns></returns>
        public string add_material()
        {
            //图片（image）: 2M，支持bmp/png/jpeg/jpg/gif格式
            //语音（voice）：2M，播放长度不超过60s，mp3/wma/wav/amr格式
            //视频（video）：10MB，支持MP4格式
            //缩略图（thumb）：64KB，支持JPG格式

            // var file = Request.Files[0];
            // string fileName = file.FileName;
            ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");
           // BitmapImage bmg = (BitmapImage)pw.HeadImage.Source;
            BitmapImage bmg = new BitmapImage(new Uri("https://img.alicdn.com/imgextra/i1/721690846/O1CN017w8YYv1I7Vb8R1DdI_!!721690846.jpg"));
            string fileName = "D:\\ComPanyCode\\WpfPro\\WpfPro\\Resource\\HeadPict.jpeg";
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}", test_access_token, "image");

            //读取上载文件流
            //System.IO.Stream fileStream = file.InputStream;
            System.IO.Stream fileStream = null;
            //byte[] fileByte = new byte[fileStream.Length];
            byte[] fileByte = IOTools.BitmapImageToByteArray(bmg);  //BMP -> byte[]
            fileStream.Read(fileByte, 0, fileByte.Length);

            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //请求头部信息
            StringBuilder sbHeader =
                new StringBuilder(
                    string.Format(
                        "Content-Disposition:form-data;name=\"media\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n",
                        fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(fileByte, 0, fileByte.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }







        /// <summary>
        /// 上传图文消息素材，返回格式：{"type":"news","media_id":"mF1J9boYDAQlYew4wbvbxQKMBkLPa1WzhGbDW7FVak","created_at":1391857799}
        /// </summary>
        /// <returns></returns>
        public string add_news()
        {
            var news = "{\"articles\":[{\"thumb_media_id\":\"mF1J9boYDAQlYew4wbvbxTgoKle16WjhsxuwhV9ZtQ\",\"author\":\"PDF\",\"title\":\"车行易.违章查询\",\"content_source_url\":\"www.qq.com\",\"content\":\"\",\"digest\":\"为车主朋友们提供优质让人满意的服务\",\"show_cover_pic\":1}]}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Post(newsUrl, news, null, "applicaion/json");
            MessageBox.Show("add_news RESULT：" + result);
            return result;
        }



        /// <summary>
        /// 预览接口(发送给指定的openId)
        /// </summary>
        /// <returns></returns>
        public string preview()
        {
            //说明：media_id值来自add_news接口返回值中的media_id值
            var news = "{\"touser\":\"oTD55jj52uIhOObiwrxCjjrCl9g\",\"mpnews\":{\"media_id\":\"mF1J9boYDAQlYew4wbbxQKMBkLPa1WzwhGbDW7FVak\"},\"msgtype\":\"mpnews\"}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Post(newsUrl, news, null, "applicaion/json");
            MessageBox.Show("preview RESULT：" + result);
            return result;
        }



        /// <summary>
        /// 根据标签进行群发【警告，谨慎调用】
        /// </summary>
        /// <returns></returns>
        public string sendall()
        {
            //说明：media_id值来自add_news接口返回值中的media_id值
            var news = "{\"filter\":{\"is_to_all\":false,\"tag_id\":215},\"mpnews\":{\"media_id\":\"mF1J9boYDAQlYew4wbbxQKMBkLPa1WzwhGbDW7FVak\"},\"msgtype\":\"mpnews\",\"send_ignore_reprint\":1,\"clientmsgid\":\"20171107\"}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Post(newsUrl, news, null, "applicaion/json");
            MessageBox.Show("sendall RESULT：" + result);
            return result;
        }



        /// <summary>
        /// 获取永久素材的列表   
        /// </summary>
        /// <returns></returns>
        public string batchget_material()
        {
            var news = "{\"type\":\"news\",\"offset\":0,\"count\":3}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Post(newsUrl, news, null, "applicaion/json");
            MessageBox.Show("batchget_material RESULT：" + result);
            return result;
        }



        /// <summary>
        /// 获取永久素材详细
        /// </summary>
        /// <returns></returns>
        public string get_material()
        {
            var news = "{\"media_id\":\"mF1J9boYDAQlYew4wbvbxQKMBkLa1WzwhGbDW7FVak\"}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Post(newsUrl, news, null, "applicaion/json");
            MessageBox.Show("get_material RESULT：" + result);
            return result;
        }



        /// <summary>
        /// 获取微信用户分组（用户标记）
        /// </summary>
        /// <returns></returns>
        public string gettags()
        {
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}";
            newsUrl = string.Format(newsUrl, test_access_token);
            var result = HttpHelper.Get(newsUrl);
            MessageBox.Show("tags RESULT：" + result);
            return result;
        }


    }



    public class HttpHelper
    {
        /// <summary>
        /// 发起GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            var webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));

            webReq.KeepAlive = false;
            webReq.Method = "GET";
            webReq.Timeout = 20000;
            webReq.ProtocolVersion = HttpVersion.Version11;
            webReq.ContentType = "application/x-www-form-urlencoded";

            var response = (HttpWebResponse)webReq.GetResponse();
            var readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            Char[] read = new Char[256];
            var count = readStream.Read(read, 0, 256);
            var result = string.Empty;
            while (count > 0)
            {
                result += new String(read, 0, count);
                count = readStream.Read(read, 0, 256);
            }
            response.Close();
            readStream.Close();
            return result;
        }


        /// <summary>
        ///  发起POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string Post(string url, string postData, Dictionary<string, string> headers = null, string contentType = null)
        {
            var webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            byte[] bytes = encode.GetBytes(postData);

            webReq.KeepAlive = false;
            webReq.Method = "POST";
            webReq.Timeout = 20000;
            webReq.ProtocolVersion = HttpVersion.Version11;
            if (contentType == null)
                webReq.ContentType = "application/x-www-form-urlencoded";
            else
                webReq.ContentType = contentType;

            webReq.ContentLength = bytes.Length;
            webReq.UserAgent = "Mozilla/5.0";
            if (headers != null)
            {
                foreach (var header in headers)
                    webReq.Headers.Add(header.Key, header.Value);
            }

            Stream outStream = webReq.GetRequestStream();
            outStream.Write(bytes, 0, bytes.Length);
            outStream.Close();

            var response = (HttpWebResponse)webReq.GetResponse();
            var readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            Char[] read = new Char[256];
            var count = readStream.Read(read, 0, 256);
            var result = string.Empty;
            while (count > 0)
            {
                result += new String(read, 0, count);
                count = readStream.Read(read, 0, 256);
            }
            response.Close();
            readStream.Close();
            return result;
        }


        /// <summary>
        /// 获取Post值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static string GetPostValue(HttpContext context)
        //{
        //    System.IO.Stream s = context.Request.InputStream;
        //    int count = 0;
        //    byte[] buffer = new byte[s.Length];
        //    StringBuilder builder = new StringBuilder();
        //    while ((count = s.Read(buffer, 0, buffer.Length)) > 0)
        //    {
        //        builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
        //    }
        //    s.Flush();
        //    s.Close();
        //    return builder.ToString();
        //}
    }
}

