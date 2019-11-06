using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfPro.ToolsCls
{
    class ShowTools
    {


        //RichTextBox显示
        public static void ShowRichTextBox(RichTextBox rtx, string cont)
        {
            rtx.Document.Blocks.Clear();    //清楚内容
            //richTextBox(高级文本框)后台方式添加内容
            string dataSTR = cont;
            Run run = new Run(dataSTR);
            Paragraph p = new Paragraph();
            p.Inlines.Add(run);
            rtx.Document.Blocks.Add(p);
        }

        public static void ShowTextBox(TextBox tb, string cont)
        {
            tb.Text = cont;
        }
    }
}
