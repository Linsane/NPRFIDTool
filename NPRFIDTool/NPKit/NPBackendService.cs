using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections;

namespace NPRFIDTool.NPKit
{
    class NPBackendService
    {
        public static HttpClient client = new HttpClient();
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
            // 5941e58d8d04afd0cb6f33df4aed15b3
            var values = new Dictionary<string, string>
            {
                {"token","5941e58d8d04afd0cb6f33df4aed15b3" },
                {"action","init"},
                {"device","5941e58d8d04afd0cb6f33df4aed15b3" },
                {"ip","192.168.100.188"}
            };
            var param = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/getStockInit", param);
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
