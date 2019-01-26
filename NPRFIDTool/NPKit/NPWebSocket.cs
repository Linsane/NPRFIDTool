using System;
using WebSocketSharp;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Timers;

namespace NPRFIDTool.NPKit
{
    delegate void WebSocketErrorHandler(ErrorEventArgs err);
    delegate void WebSocketStartInStoreHandler(EventArgs e); // 通知开始读入库数据
    delegate void WebSocketStartOutStoreHandler(EventArgs e); // 通知开始读出库数据
    delegate void WebSocketStopInStoreHandler(EventArgs e); // 通知结束读入库端口
    delegate void WebSocketCloseHandler(EventArgs e); // 通知结束读入库端口
    delegate void WebSocketStatusHandler(EventArgs e); // 通知反馈硬件设备的连接状态

    class ConnectStauts
    {
        public static int Connected = 1;        // 已连接
        public static int NotStarted = -1;      // 未启动
        public static int Disconnected = -2;    // 无法连接
        public static int PortError = -3;       // 端口无法使用
    }

    class NPWebSocket
    {
        private static WebSocket ws;
        public static WebSocketErrorHandler errorHandler;
        public static WebSocketStartInStoreHandler startInStoreHandler;
        public static WebSocketStartOutStoreHandler startOutStoreHandler;
        public static WebSocketStopInStoreHandler stopScanHandler;
        public static WebSocketCloseHandler connectStopHandler;
        public static WebSocketStatusHandler statusHandler;
        public static string currentAddress;
        public static System.Timers.Timer heartBeatTimer;

        // 建立长链接
        public static void connect(string address)
        {
            
            if (ws != null && (ws.ReadyState == WebSocketState.Connecting || ws.ReadyState == WebSocketState.Open) && currentAddress == address)
            {
                return;
            }
            else
            {
                if (ws!= null && ws.ReadyState == WebSocketState.Open) ws.Close();
                ws = null;
            }
            currentAddress = address;
            //123.207.54.83:1359
            ws = new WebSocket("ws://" + address + "/");

            ws.OnMessage += (sender, e) =>
            {
                JObject msgObj = JObject.Parse(e.Data);
                if (msgObj["act"] == null) return; 
                switch (msgObj["act"].ToString())
                {
                    case "enter_scan":
                        {
                            Console.WriteLine("后台通知开始扫描入库标签");
                            NPLogger.log("后台通知开始扫描入库标签");
                            startInStoreHandler(e);
                        }
                        break;
                    case "out_scan":
                        {
                            Console.WriteLine("后台通知开始扫描出库标签");
                            NPLogger.log("后台通知开始扫描出库标签");
                            startOutStoreHandler(e);
                        }
                        break;
                    case "stop_scan":
                        {
                            Console.WriteLine("后台通知停止扫描出入库标签");
                            NPLogger.log("后台通知停止扫描出入库标签");
                            stopScanHandler(e);
                        }
                        break;
                    case "conntect_status":
                        {
                            Console.WriteLine("后台检测RFID硬件连接状态");
                            NPLogger.log("后台检测RFID硬件连接状态");
                            statusHandler(e);
                        }
                        break;
                }
            };

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Websocket Open");
                NPLogger.log("Websocket Open");
                initial();
                startHeartBeat();
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Websocket Close");
                NPLogger.log("Websocket Close");
                if (e.Reason != null && e.Reason != "")
                {
                    MessageBox.Show("websocket close" + e.Reason);
                }
                connectStopHandler(e);
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Websocket Err:" + e.Message);
                errorHandler(e);
            };

            ws.ConnectAsync();
        }

        // 断开长连接
        public static void disconnect()
        {
            if(ws != null && (ws.ReadyState == WebSocketState.Connecting || ws.ReadyState == WebSocketState.Open))
            {
                ws.Close();
            }
        }

        // 发送入库数据
        public static void sendTagData(JArray tags, int scanType)
        {
            JObject obj = new JObject();
            if (scanType == 1)
            {
                obj.Add("act", "out_scan");
            }
            else
            {
                obj.Add("act", "enter_scan");
            }
            
            obj.Add("client_type", "app");
            obj.Add("data", tags);
            obj.Add("status", 1);
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }

        public static void initial()
        {
            JObject obj = new JObject();
            obj.Add("client_type", "app");
            obj.Add("data", "");
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }

        public static void heartBeat()
        {
            JObject obj = new JObject();
            obj.Add("client_type", "app");
            obj.Add("data", "heartBeat");
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }

        public static void startHeartBeat()
        {
            heartBeatTimer = new System.Timers.Timer(120*1000);
            heartBeatTimer.AutoReset = true;
            heartBeatTimer.Elapsed += (src, e) =>
            {
                heartBeat();
            };
            heartBeatTimer.Enabled = true;
        }

        public static void sendStopEnter()
        {
            JObject obj = new JObject();
            obj.Add("client_type", "app");
            obj.Add("data", false);
            obj.Add("act", "enter_stop_scan");
            obj.Add("status",1);
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }

        public static void sendStopOut()
        {
            JObject obj = new JObject();
            obj.Add("client_type", "app");
            obj.Add("data", false);
            obj.Add("act", "out_stop_scan");
            obj.Add("status", 1);
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }

        public static void sendRFIDConnectStatus(int status)
        {
            JObject obj = new JObject();
            obj.Add("client_type", "app");
            obj.Add("act", "connect_status");
            obj.Add("code", status);
            string msg = "";
            switch (status)
            {
                case 1:
                    msg = "设备连接正常。";
                    break;
                case -1:
                    msg = "客户端未启动,尚未连接设备。";
                    break;
                case -2:
                    msg = "设备连接失败，请检查客户端配置。";
                    break;
                case -3:
                    msg = "选择了不可用的天线，请检查配置。";
                    break;
            }
            obj.Add("msg", msg);
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }
    }
}
