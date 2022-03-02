using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 星星闪耀的特效 </summary>
public class Shine : MonoBehaviour {

    private Image img;
    public float speed=4;
    /// <summary>是否明显</summary>
    private bool add;

    public void Awake()
    {
        img = GetComponent<Image>();
    }


	void Update () 
    {
        Move();
        FadeOut();
    }

    #region 辅助1
  void Move()
    {
        transform.Rotate(Vector3.forward * 4, Space.World);
    }
    /// <summary>渐隐渐现</summary>
    void FadeOut()
    {
        if (!add)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a <= 0.2f)
            {
                add = true;
            }
        }
        else
        {
            img.color += new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a >= 0.8f)
            {
                add = false;
            }
        }
    }
    #endregion
  
}
