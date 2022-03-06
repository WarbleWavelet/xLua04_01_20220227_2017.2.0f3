using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary> 枪 </summary>
[Hotfix]
public class Gun : MonoBehaviour
{
    #region 子弹 单例
  //属性
    public int gold = 100;
    public int diamands = 50;
    public int gunLevel = 1;
    private float rotateSpeed = 5f;
    public float attackCD = 1;
    private float GunCD = 4;
    public int level = 1;

    //引用

    public AudioClip[] bullectAudios;
    private AudioSource bullectAudio;
    public Transform attackPos;
    public GameObject[] Bullects;
    public GameObject net;
    public GunChange[] gunChange;


    public Transform goldPlace;
    public Transform diamondsPlace;
    public Transform imageGoldPlace;
    public Transform imageDiamandsPlace;


    public Text goldText;
    public Text diamandsText;

 public   GameObject canvas;


    /// <summary>达到贝壳，免费射击</summary>
    private bool canShootForFree = false;
    private bool canGetDoubleGold = false;
    public bool canShootNoCD = false;
    public bool canChangeGun = true;
    public bool bossAttack = false;
    public bool Fire = false;
    public bool Ice = false;
    /// <summary>散弹</summary>
    public bool Butterfly = false;
    public bool attack = false;


    public bool changeAudio;


    private static Gun instance;
    public static Gun Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }


    public Hotfix hotfixScript;
    #endregion


    #region 生命
    private void Awake()
    {
        instance = this;
        gold = 1000;
        diamands = 1000;
        level = 2;
        bullectAudio = GetComponent<AudioSource>();
        Init();

        
    }
    void Init()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        goldPlace= GameObject.Find("GoldPlace").transform; 
        diamondsPlace= GameObject.Find("DiamandsPlace").transform;
        imageGoldPlace = canvas.transform.Find("UI_LeftTable/GoldImage").transform;
        imageDiamandsPlace = canvas.transform.Find("UI_RightTable/GoldImage").transform;


    }
    void Update()
    {
        goldText.text = gold.ToString();
        diamandsText.text = diamands.ToString();

        //旋转枪的方法

        RotateGun();

        if (GunCD <= 0)
        {
            canChangeGun = true;
            GunCD = 4;

        }
        else
        {
            GunCD -= Time.deltaTime;
        }

        //攻击的方法
        if (canShootNoCD)
        {
            Attack();
            attack = true;
            return;
        }

        if (attackCD >= 1 - gunLevel * 0.3)
        {
            Attack();
            attack = true;
        }
        else
        {
            attackCD += Time.deltaTime;
        }
    }
    #endregion


    #region 辅助1

    #region 换枪
    /// <summary>换枪</summary>
    public void ChangeGun(int change)
    {
        gunLevel += change;
        if (gunLevel == 4)
        {
            gunLevel = 1;
        }
        if (gunLevel == 0)
        {
            gunLevel = 3;
        }
        gunChange[0].ToGray();
        gunChange[1].ToGray();
        canChangeGun = false;
    }
  
    #endregion

    #region 旋转
    [LuaCallCSharp]
    /// <summary>旋转枪</summary>
    private void RotateGun()
    {

        float h = Input.GetAxisRaw("Mouse Y");
        float v = Input.GetAxisRaw("Mouse X");

        transform.Rotate(-Vector3.forward * v * rotateSpeed);
        transform.Rotate(Vector3.forward * h * rotateSpeed);

        ClampAngle();
        //245,115
    }

    /// <summary>限制角度</summary>
    private void ClampAngle()
    {
        float y = transform.eulerAngles.y;
        if (y <= 35)
        {
            y = 35;
        }
        else if (y >= 150)
        {
            y = 150;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }
    #endregion

    #region 攻击
    /// <summary>攻击方法</summary>
    [LuaCallCSharp]
    private void Attack()
    {

        if (Input.GetMouseButtonDown(0))
        {

            bullectAudio.clip = bullectAudios[gunLevel - 1];
            bullectAudio.Play();

            if (Butterfly)
            {
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, 20));
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, -20));
            }

            Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation);


            if (!canShootForFree)
            {
                GoldChange(-1 - (gunLevel - 1) * 2);

            }
            attackCD = 0;
            attack = false;
        }
    }
    #endregion


    #region 金币 钻石
    [LuaCallCSharp]
    /// <summary>增减金钱</summary>
    public void GoldChange(int number)
    {
        if (canGetDoubleGold)
        {
            if (number > 0)
            {
                number *= 2;
            }
        }


        gold += number;
    }
    [LuaCallCSharp]
    /// <summary>增减钻石</summary>
    public void DiamandsChange(int number)
    {

        diamands += number;
    }
    #endregion

    #region 免费射击
    /// <summary> 贝壳触发的一些效果方法 </summary>
    public void CanShootForFree()
    {
        canShootForFree = true;
        Invoke("CantShootForFree", 5);//n秒后恢复原价
    }
    /// <summary>需要费用</summary>
    public void CantShootForFree()
    {
        canShootForFree = false;
    }
    #endregion

    #region 双倍金币
    public void CanGetDoubleGold()
    {
        canGetDoubleGold = true;
        Invoke("CantGetDoubleGold", 5);
    }

    public void CantGetDoubleGold()
    {
        canGetDoubleGold = false;
    }
    #endregion

    #region 无限射击
    public void CanShootNoCD()
    {
        canShootNoCD = true;
        Invoke("CantShootNoCD", 5);
    }

    public void CantShootNoCD()
    {
        canShootNoCD = false;
    }
    #endregion


    /// <summary>boss攻击的方法</summary>
    public void BossAttack()
    {
        bossAttack = true;
    }
    #endregion


}
