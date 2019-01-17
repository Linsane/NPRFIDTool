using System;
using WebSocketSharp;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NPRFIDTool.NPKit
{
    class NPWebSocket
    {
        private WebSocket ws;
        public WebSocketErrorHandler errorHandler;
        public WebSocketOpenHandler openHandler;

        // 建立长链接
        public void connect()
        {
            ws = new WebSocket("ws://123.207.54.83:1359/");

            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Websocket says: " + e.Data);
            };


            ws.OnOpen += (sender, e) =>
            {
                openHandler(e);
            };

            ws.OnClose += (sender, e) =>
            {
                MessageBox.Show("Websocket Close" + e.Reason);
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Websocket Err:" + e.Message);
                errorHandler(e);
            };

            ws.Connect();
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
