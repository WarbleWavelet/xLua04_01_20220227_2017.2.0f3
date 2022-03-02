﻿using System.IO;
using UnityEngine;
using XLua;
#if USE_LUA

#endif


[Hotfix]
public class Test04_LuaEnv : MonoBehaviour
    {
        #if true// USE_LUA
         LuaEnv xLuaEnv;
            #region 生命
                void Start()
                {
                    xLuaEnv = new LuaEnv();
                    xLuaEnv.AddLoader(MyLoader_Return_ByteArr);
                    xLuaEnv.DoString("require 'fish'");
                }

                private void OnDestroy()
                {
                    xLuaEnv.DoString("require 'fishDispose'");
                    xLuaEnv.Dispose();
                }
            #endregion
            #region 辅助1
                /// <summary>返回值为字节数组</summary>
                byte[] MyLoader_Return_ByteArr(ref string filePath)
                {
                    print("有返回值");
                    string absPath = @"D:\Data\Projects\Unity\xLua04_01_20220227_2017.2.0f3\Assets\PlayerGamePackage\" + filePath+".lua.txt";
                    return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath) );
                }

    #endregion
#endif



}


