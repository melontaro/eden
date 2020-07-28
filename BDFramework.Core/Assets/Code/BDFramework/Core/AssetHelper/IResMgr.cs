﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BDFramework.ResourceMgr
{
    public interface IResMgr
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onInitEnd"></param>
        void Init(string path, Action onInitEnd);

        /// <summary>
        /// 资源管理
        /// </summary>
        Dictionary<string, AssetBundleWapper> AssetbundleMap { get; }

        /// <summary>
        /// 卸载指定ab
        /// </summary>
        /// <param name="path"></param>
        void UnloadAsset(string path, bool isForceUnload = false);

        /// <summary>
        /// 卸载所有ab
        /// </summary>
        void UnloadAllAsset();

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        T Load<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// 加载所有资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="abName"></param>
        /// <returns></returns>
        T[] LoadAll_TestAPI_2020_5_23<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        int AsyncLoad<T>(string objName, Action<T> callback) where T : UnityEngine.Object;

        /// <summary>
        /// 异步加载资源表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <param name="onLoadEnd"></param>
        /// <param name="onLoadProcess"></param>
        /// <returns></returns>
        List<int> AsyncLoad(IList<string>    assetsPath, Action<IDictionary<string, Object>> onLoadEnd,
                            Action<int, int> onLoadProcess);

        /// <summary>
        /// 取消一个加载任务
        /// </summary>
        /// <param name="taskid"></param>
        void LoadCancel(int taskid);

        /// <summary>
        /// 取消所有加载任务
        /// </summary>
        void LoadCancelAll();

        /// <summary>
        /// 获取某个目录下文件
        /// 以runtime为根目录
        /// </summary>
        string[] GetAssets(string floder, string searchPattern = null);
    }
}