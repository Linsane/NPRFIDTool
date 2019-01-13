using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NPRFIDTool.NPKit
{
    struct DBConfig
    {
        public string dbAddress; // 数据库地址
        public string dbName; // 数据库名
        public string username; // 数据库用户名 
        public string password; // 数据库密码
    }

    enum PortType { PortTypeInStore, PortTypeCheck };

    class NPConfigManager
    {
        public string configURL = ""; // 服务器配置URL
        public DBConfig config;     // 本地数据配置

        public string inStoreIP = ""; // 入库天线IP
        public int inStoreAntNums = 4; // 天线端口数
        public ArrayList inStorePorts = new ArrayList(); // 入库使用端口

        public string checkIP = ""; // 盘点天线IP
        public int checkAntNums = 4; // 天线端口数
        public ArrayList checkPorts = new ArrayList(); // 盘点使用端口

        public int readPortTime = 5; // 扫描天线
        public int readPortCycle = 5; // 定时扫描周期，单位分
        public int analyzeCycle = 30; // 盘点结果发送周期，单位分

    }
}
