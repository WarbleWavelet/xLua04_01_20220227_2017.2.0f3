using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>开始游戏按钮</summary>
public class StartGame : MonoBehaviour {

    private Button but;


    #region 生命
    void Start () {
        but = GetComponent<Button>();
        but.onClick.AddListener(() =>{ 
            SceneManager.LoadScene(1);
        });
	}
    #endregion	
}
