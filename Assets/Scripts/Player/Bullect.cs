using UnityEngine;
using System.Collections;

public class Bullect : MonoBehaviour
{
    #region 字段
    public GameObject explosions;

    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    public float moveSpeed;
    private float timeVal;
    public float defineTime;
    private float timeVal1;
    public float defineTime1;
    private float timeVal2;
    public float defineTime2;
    public Transform CreatePos;
    public GameObject net;
    public int level;

    public float attackValue;
    #endregion



    void Update()
    {
        timeVal = InistStar(timeVal, defineTime, star);
        timeVal1 = InistStar(timeVal1, defineTime1, star1);
        timeVal2 = InistStar(timeVal2, defineTime2, star2);
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }


    #region 系统
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" || other.tag == "boss")
        {
            other.SendMessage("TakeDamage", attackValue);
        }
        else if (other.tag == "missile")
        {
            other.SendMessage("Lucky", attackValue);
        }
        else if (other.tag == "Shell")
        {
            other.SendMessage("GetEffects");
        }
        else if (other.tag == "Qipao")
        {

        }
        InstantiateEffect();
        //
        if (other.tag == "Wall")
        {
            TriggerWall(other);
        }
    }
    #endregion
    #region 辅助1
    private float InistStar(float timeVals, float defineTimes, GameObject stars)
    {

        if (timeVals >= defineTimes)
        {
            Instantiate(stars, CreatePos.transform.position, Quaternion.Euler(CreatePos.transform.eulerAngles.x, CreatePos.transform.eulerAngles.y, CreatePos.transform.eulerAngles.z + Random.Range(-40f, 40f)));
            timeVals = 0;
        }
        else
        {
            timeVals += Time.deltaTime;
        }

        return timeVals;
    }




    #endregion
    #region 辅助2
    void InstantiateEffect()
    {
        GameObject go = Instantiate(net, transform.position + new Vector3(0, 1, 0), transform.rotation);
        go.transform.localScale = new Vector3(level, level, level);
        Instantiate(explosions, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    /// <summary>撞墙</summary>
    void TriggerWall(Collider other)
    {
        float angleValue = Vector3.Angle(transform.up, other.transform.up);
        if (angleValue < 90)
        {
            transform.eulerAngles += new Vector3(0, 0, 2 * angleValue);
        }
        else if (Vector3.Angle(transform.up, other.transform.up) > 90)
        {
            transform.eulerAngles -= new Vector3(0, 0, 360 - 2 * angleValue);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, 180);
        }
    }
    #endregion
}
