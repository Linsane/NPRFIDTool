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
    class DBConfig
    {
        public string dbAddress;
        public string dbName;
        public string username;
        public string password;
    }

    class NPConfigManager
    {
        public string configURL; // 服务器配置URL
        public string wsAddress; // websocket地址
        public DBConfig dbConfig;     // 本地数据配置

        public string inStoreIP; // 入库天线IP
        public ushort inStorePower; // 入库天线功率
        public int inStoreAntNums; // 天线端口数
        public JArray inStorePorts = new JArray(); // 入库使用端口

        public string checkIP; // 盘点天线IP
        public int checkAntNums; // 天线端口数
        public JArray checkPorts = new JArray(); // 盘点使用端口

        public int readPortTime = -1; // 扫描天线
        public int readPortCycle = -1; // 定时扫描周期，单位秒
        public int analyzeCycle = -1; // 盘点结果发送周期，单位秒

        public NPConfigManager()
        {
            dbConfig = new DBConfig();
            loadUpConfiguration();
        }

        private void loadUpConfiguration()
        {
            if (!File.Exists("config.json")) return;
            string configString = File.ReadAllText("config.json");
            JObject config = JObject.Parse(configString);
            configURL = config["configURL"].ToString();
            dbConfig = config["dbConfig"].ToObject<DBConfig>();
            inStoreIP = config["inStoreIP"].ToString();
            inStorePower = (ushort)config["inStorePower"];
            inStoreAntNums = (int)config["inStoreAntNums"];
            inStorePorts = (JArray)config["inStorePorts"];
            checkIP = config["checkIP"].ToString();
            checkAntNums = (int)config["checkAntNums"];
            checkPorts = (JArray)config["checkPorts"];
            readPortTime = (int)config["readPortTime"];
            readPortCycle = (int)config["readPortCycle"];
            analyzeCycle = (int)config["analyzeCycle"];
            wsAddress = config["wsAddress"].ToString();
        }

        public void markDownConfiguration()
        {
            JObject config = new JObject();
            config.Add("configURL", configURL);
            config.Add("dbConfig", JObject.FromObject(dbConfig));
            config.Add("inStoreIP", inStoreIP);
            config.Add("inStorePower", inStorePower);
            config.Add("inStoreAntNums", inStoreAntNums);
            config.Add("inStorePorts", inStorePorts);
            config.Add("checkIP", checkIP);
            config.Add("checkAntNums", checkAntNums);
            config.Add("checkPorts", checkPorts);
            config.Add("readPortTime", readPortTime);
            config.Add("readPortCycle", readPortCycle);
            config.Add("analyzeCycle", analyzeCycle);
            config.Add("wsAddress", wsAddress);

            File.WriteAllText("config.json", config.ToString());
        }

        public bool validateConfiguration()
        {
            bool validate = false;
            return validate;
        }
    }
}
