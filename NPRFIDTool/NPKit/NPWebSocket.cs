using System;
using WebSocketSharp;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NPRFIDTool.NPKit
{
    delegate void WebSocketErrorHandler(ErrorEventArgs err);
    delegate void WebSocketStartInStoreHandler(EventArgs e); // 通知开始读入库端口
    delegate void WebSocketStopInStoreHandler(EventArgs e); // 通知结束读入库端口
    delegate void WebSocketCloseHandler(EventArgs e); // 通知结束读入库端口
    class NPWebSocket
    {
        private static WebSocket ws;
        public static WebSocketErrorHandler errorHandler;
        public static WebSocketStartInStoreHandler startInStoreHandler;
        public static WebSocketStopInStoreHandler stopInStoreHandler;
        public static WebSocketCloseHandler connectStopHandler;

        // 建立长链接
        public static void connect()
        {
            if (ws != null)
            {
                ws.Connect();
                return;
            }
            ws = new WebSocket("ws://123.207.54.83:1359/");

            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Websocket says: " + e.Data);
            };

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Websocket Open");
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

            
        }

        public static void disconnect()
        {
            if(ws != null)
            {
                ws.Close();
            }
        }

        public void testSend()
        {
            JObject obj = new JObject();
            obj.Add("act", "scan_elabel");
            obj.Add("client_type", "app");
            JArray arry = new JArray();
            arry.Add("300833B2DDD9014AB0001013");
            arry.Add("300833B2DDD9014AB0001015");
            obj.Add("data", arry);
            obj.Add("status", "1");
            string json = obj.ToString(Formatting.None);
            ws.Send(json);
        }
    }
}
