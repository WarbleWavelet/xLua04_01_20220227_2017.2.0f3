using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 负责UI显示的枪 </summary>
public class GunImage : MonoBehaviour
{
    #region 字段
    public Sprite[] Guns;
    private Image img;

    public Transform idlePos;
    public Transform attackPos;

    private float rotateSpeed = 5f;
    #endregion

    #region 生命
    private void Awake()
    {
        img = transform.GetComponent<Image>();
    }

    void Update()
    {
        //旋转枪的方法
        RotateGun();
        img.sprite = Guns[Gun.Instance.gunLevel - 1];


        //攻击的方法
        if (Gun.Instance.attack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
    }
    #endregion

    #region 辅助1
    private void Attack()
    {
        transform.position = Vector3.Lerp(transform.position, attackPos.position, 0.5f);
        Invoke("Idle", 0.4f);
    }
    private void Idle()
    {
        transform.position = Vector3.Lerp(transform.position, idlePos.position, 0.2f);
    }

    /// <summary>旋转枪</summary>
    private void RotateGun()
    {

        float h = Input.GetAxisRaw("Mouse Y");
        float v = Input.GetAxisRaw("Mouse X");

        transform.Rotate(-Vector3.forward * v * rotateSpeed);
        transform.Rotate(Vector3.forward * h * rotateSpeed);

        ClampAngle();
    }

    /// <summary>限制角度</summary>
    private void ClampAngle()
    {
        float z = transform.eulerAngles.z;
        if (z <= 35)
        {
            z = 35;
        }
        else if (z >= 150)
        {
            z = 150;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
    #endregion

}