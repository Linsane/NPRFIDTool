using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPRFIDTool.NPKit
{
    class NPLogger
    {
        public static RichTextBox rtb;
        public static void log(string log)
        {
            string Time = DateTime.Now.ToLongTimeString().ToString();

            if (rtb.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => { rtb.AppendText(x); };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                rtb.Invoke(actionDelegate, Time + "  " + log + "\n");
            }
            else
            {
                rtb.AppendText(Time + "  " + log + "\n");
            }
        }
    }
}
