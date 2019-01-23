using Newtonsoft.Json.Linq;

namespace NPRFIDTool.NPKit
{
    public class NPRFIDReaderInfo
    {
        public string readerIP;     // 读写器IP
        public int readerAntNum;     // 读写器端口数
        public JArray usedPorts;     // 读写器使用的端口
        public PortType portType;       // 读写器的功能：入库/盘点
        public ushort portPower; // 端口的功率

        public NPRFIDReaderInfo(PortType portType, string ip, int readerAntNum, JArray usedPorts, ushort portPower)
        {
            this.portType = portType;
            this.readerIP = ip;
            this.readerAntNum = readerAntNum;
            this.usedPorts = usedPorts;
            this.portPower = portPower;
        }
    }
}
