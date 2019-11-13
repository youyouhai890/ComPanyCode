using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ToolsCls
{
    class StringReplaceCls
    {

        public void add_news()
        {
            var news = "{\"articles\":[{\"thumb_media_id\":\"mF1J9boYDAQlYew4wbvbxTgoKle16WjhsxuwhV9ZtQ\",\"author\":\"PDF\",\"title\":\"车行易.违章查询\",\"content_source_url\":\"www.qq.com\",\"content\":\"\",\"digest\":\"为车主朋友们提供优质让人满意的服务\",\"show_cover_pic\":1}]}";
            var newsUrl = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
            newsUrl = string.Format(newsUrl, news); //可替换{0}的内容

        }
    }
}
