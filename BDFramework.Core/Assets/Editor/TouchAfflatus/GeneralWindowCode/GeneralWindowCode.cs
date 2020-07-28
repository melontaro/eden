using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace TouchAfflatus.com
{
    internal class GeneralWindowCode
    {
        public GeneralWindowCode()
        {
        }

        //Window_xxx
        [SceneObjectsOnly] [HideLabel] [Title("选择场景中的WindowUI")]
        public GameObject WindowGameObject;

        [ChildGameObjectsOnly(IncludeSelf = false)]
        public GameObject[] ChildGameObjects;


        [ChildGameObjectsOnly(IncludeSelf = false)]
        public GameObject ChildGameObjectItem;

        [PropertySpace(SpaceBefore = 30)] [HideLabel] [Title("选择要生成到的文件夹")] [FolderPath]
        public String GeneralToFolderPath;

        public bool isCreateNewFolder = true;

        [HideLabel] [Title("是否生成PropsFile")] [PropertySpace(SpaceBefore = 30)]

        public bool isGeneralPropsFile = true;



        public String FileName;

        [PropertySpace(SpaceBefore = 30)]
        [Sirenix.OdinInspector.Button("生成UI代码", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void GeneralCode()
        {
            if (this.WindowGameObject == null) return;
            if (String.IsNullOrEmpty(this.GeneralToFolderPath)) return;
            string fullCodePath = System.Environment.CurrentDirectory + "/" + this.GeneralToFolderPath;
            if (this.isCreateNewFolder)
            {
                fullCodePath = fullCodePath + "/Window_" + this.FileName;
                if (!Directory.Exists(fullCodePath))
                {
                    Directory.CreateDirectory(fullCodePath);
                }

                String window_filePath = fullCodePath + "/Window_" + this.FileName + ".cs";
                GeneralWindowFileCode(window_filePath);

                if (this.isGeneralPropsFile)
                {
                    String props_filePath = fullCodePath + "/Props_" + this.FileName + ".cs";
                    GeneralPropsFileCode(props_filePath);
                }

            }


        }

        string[] usingPropsArray = {"BDFramework.UFlux", "BDFramework.UFlux.View.Props", "UnityEngine.UI"};
        private string nameSpace = "Game.";

        void GeneralPropsFileCode(string pathFile)
        {
            // 覆盖清空文本
            using (new FileStream(pathFile, FileMode.Create))
            {
            }

            using (StreamWriter sw = new StreamWriter(pathFile))
            {
                for (int i = 0; i < usingPropsArray.Length; i++)
                {
                    sw.WriteLine("using " + usingPropsArray[i] + ";");
                }

                sw.WriteLine("// Code Generate By Tool : " + DateTime.Now);
                sw.WriteLine("\nnamespace " + this.nameSpace + this.FileName + "_UFlux");
                sw.WriteLine("{");

                //

                sw.WriteLine("\t[ObjectSystem]");
                sw.WriteLine("\tpublic class Props_" + this.FileName + ": PropsBase");

                sw.WriteLine("\t{");

                sw.WriteLine("\t\t#region Property");

                // 属性
                foreach (var result in ChildGameObjects)
                {
                    // sw.WriteLine("\t\tprivate " + result.Value + " " + result.Value + "_" + result.Key + ";");
                }

                sw.WriteLine("\t\t#endregion");

                // Awake
                sw.WriteLine("\n\t\tpublic void Awake()");
                sw.WriteLine("\t\t{");

                sw.WriteLine(
                    "\t\t\tReferenceCollector rc = this.GetParent<UI_Z>().GameObject.GetComponent<ReferenceCollector>();");
                /*
               // 获取组件
               foreach (var result in resultDict)
               {
                   // 这里根据自己需要的写法去获得RC身上的组件
                   string _name = result.Value + "_" + result.Key;
                   if (result.Value == "GameObject")
                   {
                       sw.WriteLine("\t\t\tthis." + _name + " = rc.Get<GameObject>(" + '"' + _name + '"' + ");");
                   }
                   else
                   {
                       sw.WriteLine("\t\t\tthis." + _name + " = UIHelper.Get<" + result.Value + ">(rc, " + ('"' + _name + '"') + ");");
                   }
               }

               // 绑定按钮事件
               sw.WriteLine("\n\t\t\t// 绑定按钮点击回调");
               foreach (var result in resultDict)
               {
                   if (result.Value == "Button")
                   {
                       string _name = result.Value + "_" + result.Key;
                       sw.WriteLine("\t\t\tthis." + _name + ".onClick.Add(On" + _name + "Click);");
                   }
               }

               sw.WriteLine("\t\t}");

               // 生成按钮绑定的对应方法
              foreach (var result in resultDict)
               {
                   if (result.Value == "Button")
                   {
                       string _name = result.Value + "_" + result.Key;
                       sw.WriteLine("\n\t\tprivate void On" + _name + "Click()");
                       sw.WriteLine("\t\t{");
                       sw.WriteLine("\t\t}");
                   }
               }
              */
                sw.WriteLine("\t}");

                //

                sw.WriteLine("}");
            }



        }


        string[] usingWindowArray = { "System.Collections","System.Collections.Generic","BDFramework.UFlux","UnityEngine" };
        void GeneralWindowFileCode(string pathFile)
        {
            // 覆盖清空文本
            using (new FileStream(pathFile, FileMode.Create))
            {
            }

            using (StreamWriter sw = new StreamWriter(pathFile))
            {
                for (int i = 0; i < usingWindowArray.Length; i++)
                {
                    sw.WriteLine("using " + usingWindowArray[i] + ";");
                }

                sw.WriteLine("// Code Generate By Tool : " + DateTime.Now);
                sw.WriteLine("\nnamespace " + this.nameSpace + this.FileName + "_UFlux");
                sw.WriteLine("{");

                //

                sw.WriteLine("\t[ObjectSystem]");
                sw.WriteLine("\tpublic class Component_" + this.FileName + " :Component<Props_"+this.FileName+">");

                sw.WriteLine("\t{");

                sw.WriteLine("\t\t#region Property");

                // 属性
                foreach (var result in ChildGameObjects)
                {
                    // sw.WriteLine("\t\tprivate " + result.Value + " " + result.Value + "_" + result.Key + ";");
                }

                sw.WriteLine("\t\t#endregion");

                // Awake
                sw.WriteLine("\n\t\tpublic override void Open()");
                sw.WriteLine("\t\t{");
                sw.WriteLine(" \t\tbase.Open();");
                sw.WriteLine(
                    "\t\t\tReferenceCollector rc = this.GetParent<UI_Z>().GameObject.GetComponent<ReferenceCollector>();");
                /*
               // 获取组件
               foreach (var result in resultDict)
               {
                   // 这里根据自己需要的写法去获得RC身上的组件
                   string _name = result.Value + "_" + result.Key;
                   if (result.Value == "GameObject")
                   {
                       sw.WriteLine("\t\t\tthis." + _name + " = rc.Get<GameObject>(" + '"' + _name + '"' + ");");
                   }
                   else
                   {
                       sw.WriteLine("\t\t\tthis." + _name + " = UIHelper.Get<" + result.Value + ">(rc, " + ('"' + _name + '"') + ");");
                   }
               }

               // 绑定按钮事件
               sw.WriteLine("\n\t\t\t// 绑定按钮点击回调");
               foreach (var result in resultDict)
               {
                   if (result.Value == "Button")
                   {
                       string _name = result.Value + "_" + result.Key;
                       sw.WriteLine("\t\t\tthis." + _name + ".onClick.Add(On" + _name + "Click);");
                   }
               }

               sw.WriteLine("\t\t}");

               // 生成按钮绑定的对应方法
              foreach (var result in resultDict)
               {
                   if (result.Value == "Button")
                   {
                       string _name = result.Value + "_" + result.Key;
                       sw.WriteLine("\n\t\tprivate void On" + _name + "Click()");
                       sw.WriteLine("\t\t{");
                       sw.WriteLine("\t\t}");
                   }
               }
              */
                sw.WriteLine("\t}");

                //

                sw.WriteLine("}");
            }



        }
    }
}