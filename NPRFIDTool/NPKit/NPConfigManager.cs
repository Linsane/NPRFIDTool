using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Newtonsoft.Json.Linq;

namespace NPRFIDTool.NPKit
{
    enum PortType { PortTypeInStore, PortTypeCheck };

    class NPConfigManager
    {
        public string configURL = ""; // 服务器配置URL
        public JObject dbConfig;     // 本地数据配置

        public string inStoreIP = ""; // 入库天线IP
        public int inStoreAntNums = 4; // 天线端口数
        public ArrayList inStorePorts = new ArrayList(); // 入库使用端口

        public string checkIP = ""; // 盘点天线IP
        public int checkAntNums = 4; // 天线端口数
        public ArrayList checkPorts = new ArrayList(); // 盘点使用端口

        public int readPortTime = 5; // 扫描天线
        public int readPortCycle = 5; // 定时扫描周期，单位分
        public int analyzeCycle = 30; // 盘点结果发送周期，单位分

        public NPConfigManager()
        {
            dbConfig = new JObject();
            dbConfig.Add("dbAddress", "localhost:3306"); // 数据库地址
            dbConfig.Add("dbName", "RFIDdb"); // 数据库名
            dbConfig.Add("username", "root"); // 数据库用户名 
            dbConfig.Add("password", "123456"); // 数据库密码
        }

        public JObject loadUpConfiguration()
        {
            string configString = File.ReadAllText("config.json");
            JObject config = JObject.Parse(configString);
            return config;
        }

        public void markDownConfiguration()
        {
            JObject config = new JObject();
            config.Add("configURL","www.baidu.com");
            config.Add("dbConfig", dbConfig);
            config.Add("inStoreIP","192.168.100.188");
            File.WriteAllText("config.json", config.ToString());
        }

    }
}
