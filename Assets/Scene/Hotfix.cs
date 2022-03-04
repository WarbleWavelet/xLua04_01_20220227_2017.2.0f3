using System.IO;
using UnityEngine;
using XLua;


#if USE_LUA

#endif


[Hotfix]
public class Hotfix : MonoBehaviour
    {
        #if true// USE_LUA
         LuaEnv xLuaEnv;
            #region 生命
                void Awake()
                {
                    xLuaEnv = new LuaEnv();
                    xLuaEnv.AddLoader(MyLoader_Return_ByteArr);
                    //xLuaEnv.DoString("require '01 宝箱奖励太挤/Treasour_CreatePrize'");
                    //xLuaEnv.DoString("require '02 金币钻石不够的处理/Gun_Attack'");
                    //xLuaEnv.DoString("require '04 技能扣钻石太多的数值/Fire.Start_Ice.Start_ButterFly.Start'");
                    //xLuaEnv.DoString("require '05 boss撞击玩家数值调整/Boss.Start_DeffendBoss.Start_InvisibleBoss.Start'");
                    //xLuaEnv.DoString("require '06 boss撞击玩家当钻石金币不够时的显示/Gun.GoldChange_Gun.DiamandsChange'");
                    xLuaEnv.DoString("require '07 子弹3的使用扣除方式的更改/Gun_Attack'");
                    xLuaEnv.DoString("require '08 用xlua修改产鱼方法/FishSpawner.CreateAFish'");
                }

                private void OnDestroy()
                {
                    //xLuaEnv.DoString("require '01 宝箱奖励太挤/Treasour_CreatePrize_Dispose'");
                    //xLuaEnv.DoString("require '02 金币钻石不够的处理/Treasour_CreatePrize_Dispose'");
                    //xLuaEnv.DoString("require '04 技能扣钻石太多的数值/Fire.Start_Ice.Start_ButterFly.Start_Dispose'");
                    //xLuaEnv.DoString("require '05 boss撞击玩家数值调整/Boss.Start_DeffendBoss.Start_InvisibleBoss.Start_Dispose'");
                    //xLuaEnv.DoString("require '06 boss撞击玩家当钻石金币不够时的显示/Gun.GoldChange_Gun.DiamandsChange_Dispose'");
                    xLuaEnv.DoString("require '07 子弹3的使用扣除方式的更改/Gun_Attack_Dispose'");
                    xLuaEnv.DoString("require '08 用xlua修改产鱼方法/FishSpawner.CreateAFish_Dispose'");
                    xLuaEnv.Dispose();
                }
            #endregion
            #region 辅助1
                /// <summary>返回值为字节数组</summary>
                byte[] MyLoader_Return_ByteArr( ref string filePath)
                {
                    print("有返回值");
                    string absPath = @"D:\Data\Projects\Unity\xLua04_01_20220227_2017.2.0f3\Assets\PlayerGamePackage\"+ filePath+ ".lua.txt";
                    return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath) );
                }

    #endregion
#endif



}


