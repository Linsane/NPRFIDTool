using System;
using WebSocketSharp;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NPRFIDTool.NPKit
{
    delegate void WebSocketErrorHandler(ErrorEventArgs err);
    delegate void WebSocketStartInStoreHandler(EventArgs e); // 通知开始读入库数据
    delegate void WebSocketStartOutStoreHandler(EventArgs e); // 通知开始读出库数据
    delegate void WebSocketStopInStoreHandler(EventArgs e); // 通知结束读入库端口
    delegate void WebSocketCloseHandler(EventArgs e); // 通知结束读入库端口
    class NPWebSocket
    {
        private static WebSocket ws;
        public static WebSocketErrorHandler errorHandler;
        public static WebSocketStartInStoreHandler startInStoreHandler;
        public static WebSocketStartOutStoreHandler startOutStoreHandler;
        public static WebSocketStopInStoreHandler stopScanHandler;
        public static WebSocketCloseHandler connectStopHandler;

        // 建立长链接
        public static void connect()
        {
            if (ws != null && (ws.ReadyState != WebSocketState.Connecting || ws.ReadyState != WebSocketState.Open))
            {
                ws.Connect();
                return;
            }
            ws = new WebSocket("ws://123.207.54.83:1359/");

            ws.OnMessage += (sender, e) =>
            {
                JObject msgObj = JObject.Parse(e.Data);
                if (msgObj["act"] == null) return; 
                switch (msgObj["act"].ToString())
                {
                    case "enter_scan":
                        {
                            Console.WriteLine("后台通知开始扫描入库标签");
                            startInStoreHandler(e);
                        }
                        break;
                    case "out_scan":
                        {
                            Console.WriteLine("后台通知开始扫描出库标签");
                            startOutStoreHandler(e);
                        }
                        break;
                    case "stop_scan":
                        {
                            Console.WriteLine("后台通知停止扫描出入库标签");
                            stopScanHandler(e);
                        }
                        break;
                }
            };

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Websocket Open");
                initial();
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Websocket Close");
                connectStopHandler(e);
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Websocket Err:" + e.Message);
                errorHandler(e);
            };

            ws.Connect();
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
    }
}
