using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

public class StartProgress : Editor
{
    private static string appName = "C:\\phpstudy_pro\\COM\\phpstudy_pro.exe";
    [MenuItem("Tools/打开文件服务器")]
    private static void Open()
    {
        Process.Start(appName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
