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
            rtb.AppendText(Time + "  " + log + "\n");
        }
    }
}
