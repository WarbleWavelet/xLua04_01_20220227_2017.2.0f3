using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;


#if USE_LUA

#endif


[Hotfix]
public class Hotfix : MonoBehaviour
{
#if true// USE_LUA
     LuaEnv xLuaEnv;


    [SerializeField] public static Dictionary<string,GameObject> prefabDict=new Dictionary<string,GameObject>();
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
        //xLuaEnv.DoString("require '07 子弹3的使用扣除方式的更改/Gun_Attack'");
        //xLuaEnv.DoString("require '08 用xlua修改产鱼方法/FishSpawner.CreateAFish'");
        //xLuaEnv.DoString("require '09 修改鱼的捕捉条件/Fish.TakeDamage_Boss.TakeDamage'");
        //xLuaEnv.DoString("require '10 炮台移动方式/Gun.RotateGun_GunImage.RotateGun'");
        //xLuaEnv.DoString("require '11 用AB包生成新鱼/FishSpawner.Start_FishSpawner.CreateAFish'");
        //xLuaEnv.DoString("require '12 用空的MoneBehaviour生成海浪类/HotfixEmpty.Start_HotfixEmpty.Update_HotfixEmpty.OnTriggerEnter_HotfixEmpty.BehaviourMethod'");
        xLuaEnv.DoString("require 'fish'");


    }

    private void OnDestroy() 
    {
        //xLuaEnv.DoString("require '01 宝箱奖励太挤/Treasour_CreatePrize_Dispose'");
        //xLuaEnv.DoString("require '02 金币钻石不够的处理/Treasour_CreatePrize_Dispose'");
        //xLuaEnv.DoString("require '04 技能扣钻石太多的数值/Fire.Start_Ice.Start_ButterFly.Start_Dispose'");
        //xLuaEnv.DoString("require '05 boss撞击玩家数值调整/Boss.Start_DeffendBoss.Start_InvisibleBoss.Start_Dispose'");
        //xLuaEnv.DoString("require '06 boss撞击玩家当钻石金币不够时的显示/Gun.GoldChange_Gun.DiamandsChange_Dispose'");
        //xLuaEnv.DoString("require '07 子弹3的使用扣除方式的更改/Gun_Attack_Dispose'");
        //xLuaEnv.DoString("require '08 用xlua修改产鱼方法/FishSpawner.CreateAFish_Dispose'");
        //xLuaEnv.DoString("require '09 修改鱼的捕捉条件/Fish.TakeDamage_Boss.TakeDamage_Dispose'");
        //xLuaEnv.DoString("require '10 炮台移动方式/Gun.RotateGun_GunImage.RotateGun_Dispose'");
        //xLuaEnv.DoString("require '11 用AB包生成新鱼/FishSpawner.Start_FishSpawner.CreateAFish_Dispose'");
        //xLuaEnv.DoString("require '12 用空的MoneBehaviour生成海浪类/HotfixEmpty.Start_HotfixEmpty.Update_HotfixEmpty.OnTriggerEnter_HotfixEmpty.BehaviourMethod_Dispose'");
        xLuaEnv.DoString("require 'fish_Dispose'");
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

   public static void ReadAssetBundle(string resName, string filePath)
    {

        //AssetBundle ab = AssetBundle.LoadFromFile("AssetBundles/plane.unity3d");//必须加后缀
        AssetBundle ab = AssetBundle.LoadFromFile(@"D:\Data\Projects\Unity\xLua04_01_20220227_2017.2.0f3\AssetBundles\"+filePath);//必须加后缀
        //    
        GameObject prefab = ab.LoadAsset<GameObject>(resName);//可以不加后缀

        if (prefabDict.ContainsKey(resName) == false)
        {
            prefabDict.Add(resName, prefab);
        }
        
    }
    public static GameObject GetGameObject(string resName)
    {
        GameObject go=null; 
        prefabDict.TryGetValue(resName, out go);
        if (go != null)
        {
            return go;
        }
            
        else
        {
            throw new System.Exception("异常");
        }
            
    }


    #region 网络
    [LuaCallCSharp]
    public void ReadAssetBundle_FromUnityWebRequestAsync(string resName, string filePath)
    {
        StartCoroutine(LoadAssetBundle_FromUnityWebRequestAsync(resName,filePath));
    }

    IEnumerator LoadAssetBundle_FromUnityWebRequestAsync(string resName, string filePath)//内存异步读取
    {
        string path_download = @"http://localhost/AssetBundles/" + filePath;
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(resName);
        //
        yield return request.SendWebRequest();
        //

        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        //
        GameObject prefab = ab.LoadAsset<GameObject>(resName);//可以不加后缀

        if (prefabDict.ContainsKey(resName) == false)
        {
            prefabDict.Add(resName, prefab);
        }
    }


    #endregion





    #endregion
#endif



}
