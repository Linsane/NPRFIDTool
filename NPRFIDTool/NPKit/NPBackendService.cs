using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace NPRFIDTool.NPKit
{
    class NPBackendService
    {
        /// <summary>
        /// 微信企业号操作
        /// </summary>
        public class WXQYHHelper
        {
            private WXQYHHelper() { }

            static string CORPID;
            static string SECRET;

            /// <summary>
            /// .Ctor
            /// </summary>
            static WXQYHHelper()
            {
                CORPID = ConfigurationManager.AppSettings["CorpID"];
                SECRET = ConfigurationManager.AppSettings["Secret"];
            }

            /// <summary>
            /// ACCESS_TOKEN最后一次更新时间
            /// </summary>
            static DateTime _lastGetTimeOfAccessToken = DateTime.Now.AddSeconds(-7201);

            /// <summary>
            /// 存储微信访问凭证
            /// </summary>
            static string _AccessToken;

            /// <summary>
            /// 获取微信访问凭证
            /// </summary>
            public static string GetAccessToken()
            {
                try
                {
                    if (_lastGetTimeOfAccessToken < DateTime.Now)
                    {

                        string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CORPID, SECRET);
                        string responseText = HttpHelper.Instance.get(url); // 封装的get请求
                                                                            /*
                                                                                API：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8#.E8.8E.B7.E5.8F.96AccessToken
                                                                                正确的Json返回示例:
                                                                                {
                                                                                   "access_token": "accesstoken000001",
                                                                                   "expires_in": 7200
                                                                                }
                                                                                错误的Json返回示例:
                                                                                {
                                                                                   "errcode": 43003,
                                                                                   "errmsg": "require https"
                                                                                }
                                                                            */
                        var rsEntity = new { access_token = "", expires_in = 0, errcode = 0, errmsg = "" };
                        dynamic en = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType<object>(responseText, rsEntity); // Newtonsoft.Json提供的匿名类反序列化
                        _lastGetTimeOfAccessToken = DateTime.Now.AddSeconds((double)en.expires_in - 1);
                        _AccessToken = en.access_token;
                    }
                    return _AccessToken;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
