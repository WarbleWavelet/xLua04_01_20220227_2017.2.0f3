using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

/// <summary>加载游戏</summary>
public class LoadGame : MonoBehaviour 
{

    public Slider processView;
    public int sceneIndex;

    void Start () 
    {
        sceneIndex =2;
        LoadGameMethod();
        
	}

    #region 辅助12
    public void LoadGameMethod()
    {
        StartCoroutine(A(@"http://localhost/" + "fish.lua.txt", @"D:\Data\Projects\Unity\xLua04_01_20220227_2017.2.0f3\Assets\PlayerGamePackage\fish.lua.txt"));
        StartCoroutine(A(@"http://localhost/" + "fish_Dispose.lua.txt", @"D:\Data\Projects\Unity\xLua04_01_20220227_2017.2.0f3\Assets\PlayerGamePackage\fish_Dispose.lua.txt"));

        StartCoroutine(StartLoading_4(sceneIndex));
    }

    /// <summary>加载协程</summary>
    private IEnumerator StartLoading_4(int scene)
    {
        int displayProgress = 0;//当前进度
        int toProgress = 0;//目标进度
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;//加载好了，也先别出来
        //
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();//等待当前帧完成
            }
        }
        //最后10慢点加载
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }
    /// <summary>设置滑动条百分比</summary>
    private void SetLoadingPercentage(float v)
    {
        processView.value = v / 100;
    }
    #endregion


    #region 加載

    
    IEnumerator A(string from, string to)
    {
        UnityWebRequest request = UnityWebRequest.Get(from);
        yield return request.SendWebRequest();
        string fromRes = request.downloadHandler.text;
        File.WriteAllText(to, fromRes);
    }
    #endregion

}
