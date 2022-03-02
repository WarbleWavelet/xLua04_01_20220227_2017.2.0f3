using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> boss攻击玩家产生的震动方法（挂在相机上） </summary>
public class Shake : MonoBehaviour
{

    /// <summary>振动系数</summary>
    private float cameraShake = 2;
    public GameObject UI;

    void Update()
    {
        if (Gun.Instance.bossAttack)
        {
            ShakeFunc();

            cameraShake = cameraShake / 1.05f;
            if (cameraShake < 0.05f)
            {
                StopShake();
            }
        }
        else
        {
            cameraShake = 5;
        }
    }
    #region 辅助1
  /// <summary>震动</summary>
    void ShakeFunc()
        {
        UI.SetActive(true);

        float x = (Random.Range(0f, cameraShake)) - cameraShake * 0.5f;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        float z = (Random.Range(0f, cameraShake)) - cameraShake * 0.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    /// <summary>停止震动</summary>
    void StopShake()
    {
        cameraShake = 0;
        UI.SetActive(false);
        Gun.Instance.bossAttack = false;
    }
    #endregion
  
}
