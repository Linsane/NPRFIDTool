using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections;
using WebSocketSharp;

namespace NPRFIDTool.NPKit
{
    class NPBackendService
    {
        // 5941e58d8d04afd0cb6f33df4aed15b3
        private static HttpClient client = new HttpClient();
        private static Dictionary<string,string> commonParams = new Dictionary<string, string>
            {
                {"token","5941e58d8d04afd0cb6f33df4aed15b3" },
                {"device","5941e58d8d04afd0cb6f33df4aed15b3" },
                {"ip","192.168.100.188"}
            };
        public static void configHttpClient()
        {
            client.BaseAddress = new Uri("http://rfidv2.radeit.cn");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // 云库存接口 获取Remain表数据
        public static async Task<string> getStockInit()
        {
            var parameter = new Dictionary<string, string>
            {
                {"action","init"},
                {"device","RFID0012"}
            };

            var values = assembleParams(parameter, commonParams);
            var param = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/getStockInit", param);
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        // 组装公共参数
        static public Dictionary<string, string> assembleParams(Dictionary<string, string> first, Dictionary<string, string> second)
        {
            if (first == null) first = new Dictionary<string, string>();
            if (second == null) return first;

            foreach (var item in second)
            {
                if (!first.ContainsKey(item.Key))
                    first.Add(item.Key, item.Value);
            }

            return first;
        }

        static public void WebSocketConnect()
        {
            using (var ws = new WebSocket("ws://123.207.54.83:1349"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Websocket says: " + e.Data);

                ws.OnOpen += (sender, e) =>
                    Console.WriteLine("Websocket Open");

                ws.Connect();
            }
        }
    }
}
