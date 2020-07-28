﻿using System.Net;
using System.Threading.Tasks;
using BDFramework.UFlux.Reducer;
using LitJson;

namespace BDFramework.UFlux.Test
{
    public enum Reducer06Enum
    {
        MsgRequestSrver,
    }

    public class ReducerDemo06 : AReducers<State_Hero>
    {
        public override void RegisterReducers()
        {
            base.RegisterReducers();

            //这里用显式注册，避免函数签名错误
            this.AddAsyncRecucer(Reducer06Enum.MsgRequestSrver, RequestServer);
        }


        /// <summary>
        /// url
        /// </summary>
        readonly public string url = "https://1843236967254885.cn-shanghai.fc.aliyuncs.com/2016-08-15/proxy/BDFramework/DemoForUFlux/";
        /// <summary>
        /// 这里是做了个网络请求
        /// </summary>
        /// <param name="old"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        async  private Task<State_Hero>RequestServer(State_Hero old, object @param)
        {
            var api = url + "api/bdframework/getherodata";
            WebClient  wc=new WebClient();
            string ret = await  wc.DownloadStringTaskAsync(api);
            var hero = JsonMapper.ToObject<State_Hero>(ret);
            return hero;
        }
    }
}