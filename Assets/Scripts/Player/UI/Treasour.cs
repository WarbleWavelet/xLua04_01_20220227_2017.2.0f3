using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if true//USE_LUA
using XLua;



//[Hotfix]
#endif
[Hotfix]
public class Treasour : MonoBehaviour
{
    #region 字段
    private Button but;
    private Image img;

    public GameObject gold;
    public GameObject diamands;
    public GameObject cdView;

    public Transform canvas;
    private bool isDrease;
    #endregion


    #region 生命
    private void Awake()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(OpenTreasour);
        img = GetComponent<Image>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    void Update()
    {
        if (isDrease)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime * 10);
            if (img.color.a <= 0.2)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                isDrease = false;
            }
        }
        else
        {
            img.color += new Color(0, 0, 0, Time.deltaTime * 0.01f);
            if (img.color.a >= 0.9)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
                cdView.SetActive(false);
            }
        }

    }
    #endregion


    #region 辅助1
    /// <summary>开宝箱</summary>
    void OpenTreasour()
    {
        if (img.color.a != 1)
        {
            return;
        }
        cdView.SetActive(true);
        Gun.Instance.GoldChange(Random.Range(100, 200));
        Gun.Instance.DiamandsChange(Random.Range(10, 50));
        CreatePrize();
        isDrease = true;
    }
#if true//USE_LUA

#endif
    [LuaCallCSharp]//会被Lua调用的CS
    private void CreatePrize()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(gold, transform.position + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go.transform.SetParent(canvas);
            GameObject go1 = Instantiate(diamands, transform.position + new Vector3(0, 30, 0) + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go1.transform.SetParent(canvas);
        }
    }
    #endregion
}
