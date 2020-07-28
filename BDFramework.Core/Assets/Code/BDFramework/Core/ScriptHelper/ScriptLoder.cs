using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Code.BDFramework.Core.Tools;
using UnityEngine;

namespace BDFramework
{
    static public class ScriptLoder
    {
        static public Assembly Assembly { get; private set; }

        /// <summary>
        /// 开始热更脚本逻辑
        /// </summary>
        static public void Load(string root, HotfixCodeRunMode mode)
        {
            if (root != "")
            {
                IEnumeratorTool.StartCoroutine(IE_LoadScript(root, mode));
            }
            else
            {
                BDebug.Log("内置code模式!");
                //反射调用，防止编译报错
                var assembly = Assembly.GetExecutingAssembly();
                var type     = assembly.GetType("BDLauncherBridge");
                var method   = type.GetMethod("Start", BindingFlags.Public | BindingFlags.Static);
                method.Invoke(null, new object[] {false, false});
            }
        }

        private static string DLLPATH = "/Hotfix/hotfix.dll";

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="source"></param>
        /// <param name="copyto"></param>
        /// <returns></returns>
        static IEnumerator IE_LoadScript(string root, HotfixCodeRunMode mode)
        {
            string dllPath = "";
            if (Application.isEditor)
            {
                dllPath = root + "/" + BApplication.GetPlatformPath(Application.platform) + DLLPATH;
            }
            else
            {
                //这里情况比较复杂,Mobile上基本认为Persistent才支持File操作,
                //可寻址目录也只有 StreamingAsset
                var firstPath = Application.persistentDataPath + "/" + BApplication.GetPlatformPath(Application.platform) + DLLPATH;
                var secondPath = Application.streamingAssetsPath + "/" + BApplication.GetPlatformPath(Application.platform) + DLLPATH;
                if (!File.Exists(firstPath))
                {
                    var www = new WWW(secondPath);
                    yield return www;
                    if (www.isDone && www.error == null)
                    {
                        BDebug.Log("拷贝dll成功:" + firstPath);
                        FileHelper.WriteAllBytes(firstPath, www.bytes);
                    }
                    else
                    {
                        BDebug.Log(www.error + "\n 拷贝dll失败:" + secondPath);
                    }
                }

                dllPath = firstPath;
            }


            //反射执行
            if (mode == HotfixCodeRunMode.ByReflection)
            {
                BDebug.Log("Dll路径:" + dllPath, "red");
                var bytes = File.ReadAllBytes(dllPath);
                var mdb   = dllPath + ".mdb";
                if (File.Exists(mdb))
                {
                    var bytes2 = File.ReadAllBytes(mdb);
                    Assembly = Assembly.Load(bytes, bytes2);
                }
                else
                {
                    Assembly = Assembly.Load(bytes);
                }

                BDebug.Log("代码加载成功,开始执行Start");
                var type   = Assembly.GetType("BDLauncherBridge");
                var method = type.GetMethod("Start", BindingFlags.Public | BindingFlags.Static);
                method.Invoke(null, new object[] {false, true});
            }
            //解释执行
            else if (mode == HotfixCodeRunMode.ByILRuntime)
            {
                BDebug.Log("Dll路径:" + dllPath, "red");
                //解释执行模式
                ILRuntimeHelper.LoadHotfix(dllPath);
                ILRuntimeHelper.AppDomain.Invoke("BDLauncherBridge", "Start", null, new object[] {true, false});
            }
            else
            {
                BDebug.Log("Dll路径:内置", "red");
            }
        }
    }
}