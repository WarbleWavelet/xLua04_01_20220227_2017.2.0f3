using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 普通鱼的类 </summary>

public class Fish : MonoBehaviour
{

    #region 字段
    //属性
    public float moveSpeed = 2;
    public int GetCold = 10;
    public int GetDiamands = 10;
    public int hp = 5;

    //计时器
    private float rotateTime;
    private float timeVal;

    //引用
    public GameObject gold;
    public GameObject diamands;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    private SpriteRenderer sr;
    public GameObject pao;

    //开关
    private bool hasIce = false;
    public bool isnet;
    private bool isDead = false;
    public bool cantRotate = false;
    #endregion


    #region 生命
    void Start()
    {
        Init();
        Destroy(this.gameObject, 20);

    }

    void Update()
    {
        Timer_DeadEffect();
        //
        Listener_Dead();
        Listener_IceEffect();
        Listener_FireEffect();
        //
        fishMove();
    }

    #endregion

    #region 辅助1
    private void Init()
    {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    public void fishMove()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (cantRotate)
        {
            return;
        }
        if (rotateTime >= 5)
        {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else
        {
            rotateTime += Time.deltaTime;
        }
    }
    public void TakeDamage(int attackValue)
    {
        //灼烧伤害*2
        if (Gun.Instance.Fire)
        {
            attackValue *= 2;
        }
        //
        hp -= attackValue;
        if (hp <= 0)
        {
            isDead = true;
            for (int i = 0; i < 9; i++)
            {
                Instantiate(pao, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
            }

            gameObjectAni.SetTrigger("Die");
            Invoke("Prize", 0.7f);
        }
    }
    /// <summary>奖励</summary>
    private void Prize()
    {
        Gun.Instance.GoldChange(GetCold);
        if (GetDiamands != 0)
        {
            Gun.Instance.DiamandsChange(GetDiamands);
            Instantiate(diamands, transform.position, transform.rotation);
        }

        Instantiate(gold, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }

    /// <summary>灼烧方法</summary>
    private void Listener_FireEffect()
    {
        if (Gun.Instance.Fire)
        {
            fire.SetActive(true);

        }
        else
        {
            fire.SetActive(false);
        }

        if (Gun.Instance.Ice)
        {
            return;
        }
        if (isnet)
        {
            Invoke("Net", 0.5f);//后解除网住
            return;
        }
    }
    /// <summary>冰冻效果</summary>
    private void Listener_IceEffect()
    {
        if (Gun.Instance.Ice)
        {
            gameObjectAni.enabled = false;
            ice.SetActive(true);
            if (!hasIce)
            {
                iceAni.SetTrigger("Ice");
                hasIce = true;
            }
        }
        else
        {
            gameObjectAni.enabled = true;
            hasIce = false;
            ice.SetActive(false);
        }
    }
    /// <summary>死了</summary> 
    private void Listener_Dead()
    {
        if (isDead)
        {
            return;
        }
    }
    /// <summary>计时器，死亡渐隐效果</summary>
    void Timer_DeadEffect()
    {
        if (timeVal >= 14 || isDead)
        {
            sr.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    #endregion
    #region 辅助2
    /// <summary>网住了</summary>
    public void Net()
    {
        if (isnet)
        {
            isnet = false;
        }

    }
    #endregion




}
