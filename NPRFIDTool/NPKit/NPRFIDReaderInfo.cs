using Newtonsoft.Json.Linq;

namespace NPRFIDTool.NPKit
{
    class NPRFIDReaderInfo
    {
        public string readerIP;     // 读写器IP
        public int readerAntNum;     // 读写器端口数
        public JArray usedPorts;     // 读写器使用的端口
        public PortType portType;       // 读写器的功能：入库/盘点

        public NPRFIDReaderInfo(PortType portType, string ip, int readerAntNum, JArray usedPorts)
        {
            this.portType = portType;
            this.readerIP = ip;
            this.readerAntNum = readerAntNum;
            this.usedPorts = usedPorts;
        }
    }
}
