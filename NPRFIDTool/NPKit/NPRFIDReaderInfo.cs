using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NPRFIDTool.NPKit;

namespace NPRFIDTool.NPKit
{
    class NPRFIDReaderInfo
    {
        public string readerIP;     // 读写器IP
        public int readerAntNum;     // 读写器端口数
        public ArrayList usedPorts;     // 读写器使用的端口
        public PortType portType;       // 读写器的功能：入库/盘点
    }
}
