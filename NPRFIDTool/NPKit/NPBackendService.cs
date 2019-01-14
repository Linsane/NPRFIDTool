using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections;
using ModuleTech;
using System.Net;
using System.Collections.Generic;


namespace NPRFIDTool.NPKit
{
    delegate void RspHandler(JObject obj);
    class NPBackendService
    {
        private HttpClient client;
        private JObject commonParams;


        public NPBackendService(string baseURL, string ip, string device)
        {
            client = new HttpClient();
            commonParams = new JObject();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            commonParams.Add("token", "5941e58d8d04afd0cb6f33df4aed15b3");
            commonParams.Add("ip", GetIpAddress());
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
            JObject rspObj = JObject.Parse(responseString);
            if (int.Parse(rspObj["code"].ToString()) != 1)
            {
                handler(null);
                return null;
            }

            List<string> labels = rspObj["data"]["label"].ToObject<List<string>>();
            JObject resultObj = new JObject();
            foreach(string tag in labels)
            {
                resultObj.Add(tag, DateTime.Now);
            }
            handler(resultObj);
            return resultObj;
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
            JArray emptyArray = new JArray();
            JObject obj = new JObject();
            obj.Add("action", "check");
            obj.Add("check", "ok");
            obj.Add("label", emptyArray);

            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/check", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        // 上报盘点差异数据
        public async Task<JObject> reportCheckDiff(JArray diffArray)
        {
            JObject obj = new JObject();
            obj.Add("action", "check");
            obj.Add("check", "err");
            obj.Add("label", diffArray);

            obj.Merge(commonParams);

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "api/rwapp/check", content);
            string responseString = await response.Content.ReadAsStringAsync();
            JObject resultObj = JObject.Parse(responseString);
            return resultObj;
        }

        // 获取IP地址
        private string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }
    }
}
