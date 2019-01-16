using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using WebSocketSharp;
using ModuleTech;
using System.Windows.Forms;


namespace NPRFIDTool.NPKit
{
    delegate void RspHandler(JObject obj);
    class NPBackendService
    {
        private HttpClient client;
        private JObject commonParams;
        private static WebSocket ws;

        public NPBackendService(string ip, string device)
        {
            client = new HttpClient();
            commonParams = new JObject();
            client.BaseAddress = new Uri("http://rfidv2.radeit.cn");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            commonParams.Add("token", "5941e58d8d04afd0cb6f33df4aed15b3");
            commonParams.Add("ip", ip);
            commonParams.Add("device", device); //"RFID0012"
        }

        // 云库存接口 获取Remain表数据
        public async Task<JObject> getStockInit(RspHandler handler)
        {
            JObject obj = new JObject();
            obj.Add("action", "init");
            obj.Merge(commonParams);
            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/getStockInit", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            handler(resultObj);
            return resultObj;
        }

        // 建立长链接
        static public void WebSocketConnect()
        {
            ws = new WebSocket("ws://123.207.54.83:1359/");

            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Websocket says: " + e.Data);
            };
                    

            ws.OnOpen += (sender, e) =>
            {
                MessageBox.Show("Websocket Open");
            };

            ws.OnClose += (sender, e) =>
            {
                MessageBox.Show("Websocket Close" + e.Reason);
            };

            ws.OnError += (sender, e) =>
            {
                MessageBox.Show("Websocket Err:" + e.Message);
            };

            ws.Connect();
        }

        // 上报入库数据
        public async Task<JObject> sendInStoreTags(ArrayList tagsList, TagReadData tagData)
        {
            if (tagsList.Count <= 0) return null;
            JArray array = new JArray();
            foreach (string tag in tagsList)
            {
                array.Add(tag);
            }

            JObject obj = new JObject();
            obj.Add("action", "inscan");
            obj.Add("antenna", String.Format("天线{0}",tagData.Antenna));
            obj.Add("antenna_param", "");
            obj.Add("label",array);
            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/warehousing", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        // 上报出库数据
        public async Task<JObject> sendOutStoreTags(ArrayList tagsList, TagReadData tagData)
        {
            if (tagsList.Count <= 0) return null;
            JArray array = new JArray();
            foreach (string tag in tagsList)
            {
                array.Add(tag);
            }

            JObject obj = new JObject();
            obj.Add("action", "outscan");
            obj.Add("antenna", String.Format("天线{0}", tagData.Antenna));
            obj.Add("antenna_param", "");
            obj.Add("label", array);
            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/outwarehousing", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        // 上报盘点成功
        public async Task<JObject> reportCheckSuccess(TagReadData tagData)
        {
            JObject obj = new JObject();
            obj.Add("action", "check");
            obj.Add("antenna", String.Format("天线{0}", tagData.Antenna));
            obj.Add("antenna_param", "");
            obj.Add("check", "ok");

            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/check", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        // 上报盘点差异数据
        public async Task<JObject> reportCheckDiff(ArrayList tagsList, TagReadData tagData)
        {
            JObject obj = new JObject();
            obj.Add("action", "utscan");
            obj.Add("antenna", String.Format("天线{0}", tagData.Antenna));
            obj.Add("antenna_param", "");
            obj.Add("check", "err");

            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/check", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        public static void testSend()
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
