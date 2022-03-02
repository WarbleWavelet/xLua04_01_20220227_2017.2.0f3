using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 产鱼Spawner </summary>

public class FishSpawner : MonoBehaviour
{

    #region 字段
    //引用
    public GameObject[] fishList;
    public GameObject[] item;
    public GameObject boss;
    public GameObject boss2;
    public GameObject boss3;
    public Transform[] specialFishSchoolSpawner;


    private float ItemtimeVal = 0;//游戏物体计时器
    private float createManyFish;
    private float timeVals = 0;

    //成员变量
    private int posNum;
    private int itemNum;
    /// <summary>产生特殊鱼群的位置数量</summary>
    private int specialFishSchoolSpawnerNum;
    /// <summary>产生鱼群的模式</summary>
    private int CreateMorden;



    //x:-26  -   26
    //z:-16  -   16

    #endregion



    #region 生命
    void Start()
    {
        Init();
    }


    void Update()
    {
        SpawnFishGroup();
        CreateAFish();


 

    }
    #endregion




    #region 辅助1
    void Init()
    {
        fishList = new GameObject[8];
        fishList[0] = (LoadEnemy("Fish1"));
        fishList[1] = (LoadEnemy("Fish2"));
        fishList[2] = (LoadEnemy("Fish3"));
        fishList[3] = (LoadEnemy("level2fish1"));
        fishList[4] = (LoadEnemy("level2fish2"));
        fishList[5] = (LoadEnemy("level2fish3"));
        fishList[6] = (LoadEnemy("level3fish2"));
        fishList[7] = (LoadEnemy("level3fish3"));

        item = new GameObject[4];
        item[0] = (LoadItem("Shell"));
        item[1] = (LoadItem("Missile1"));
        item[2] = (LoadItem("Missile2"));
        item[3] = (LoadItem("QiPao"));

        boss = (LoadEnemy("Boss1"));
        boss2 = (LoadEnemy("Boss2"));
        boss3 = (LoadEnemy("Boss3"));

        specialFishSchoolSpawner = new Transform[2];
        specialFishSchoolSpawner[0] = GameObject.Find("Place1").transform;
        specialFishSchoolSpawner[1] = GameObject.Find("Place2").transform;
    }

    /// <summary>产生只鱼</summary>
    private void CreateAFish()
    {
        //单种鱼的生成
        if (ItemtimeVal >= 0.5)
        {

            posNum = Random.Range(0, 4);//位置随机数
            itemNum = Random.Range(1, 101); //游戏物体随机数



            //产生气泡
            if (itemNum < 20)
            {
                CreateGameObject(item[3]);
                CreateGameObject(fishList[6]);
            }
            //贝壳10% 85-94 
            //第一种鱼42% 42
            if (itemNum <= 42)
            {
                CreateGameObject(fishList[0]);
                CreateGameObject(item[0]);
                CreateGameObject(fishList[3]);
                CreateGameObject(item[0]);
            }
            //第二种鱼30% 43-72
            else if (itemNum >= 43 && itemNum < 72)
            {
                CreateGameObject(fishList[1]);
                CreateGameObject(item[0]);
                CreateGameObject(fishList[4]);
            }
            //第三种鱼10% 73-84
            else if (itemNum >= 73 && itemNum < 84)
            {
                CreateGameObject(fishList[2]);
                CreateGameObject(fishList[5]);
            }

            //第一种美人鱼5%，第二种3%  95-98  99-100


            else if (itemNum >= 94 && itemNum <= 98)
            {
                CreateGameObject(item[1]);
            }

            else if (itemNum >= 84 && itemNum < 86)
            {

                CreateGameObject(boss2);
            }

            else if (itemNum > 98 && itemNum < 100)
            {
                CreateGameObject(item[2]);
                CreateGameObject(boss);
            }


            else
            {
                CreateGameObject(item[0]);
                CreateGameObject(boss3);
            }
            ItemtimeVal = 0;
        }
        else
        {
            ItemtimeVal += Time.deltaTime;
        }
    }

    /// <summary>生成鱼群</summary>
    private void SpawnFishGroup()
    {
        if (createManyFish >= 15)
        {
            if (CreateMorden == 2)
            {
                FishGroup_Bloom();
            }
            else if (CreateMorden == 0 || CreateMorden == 1)
            {
                FishGroup_Timer();
            }
        }
        else
        {
            createManyFish += Time.deltaTime;
            specialFishSchoolSpawnerNum = Random.Range(0, 2);
            CreateMorden = Random.Range(0, 3);
        }
    }


    private void CreateFishs(GameObject go)
    {
        Instantiate(go, RandomPos(posNum), Quaternion.Euler(go.transform.eulerAngles));
    }



    #endregion



    #region 辅助2
    /// <summary>产生游戏物体</summary>
    private void CreateGameObject(GameObject go)
    {
        Instantiate(go, RandomPos(posNum), Quaternion.Euler(RandomAngle(posNum) + go.transform.eulerAngles));
    }
    /// <summary>计时产生鱼群(18s内每隔0.2s产生鱼)</summary>
    private void FishGroup_Timer()
    {
        createManyFish += Time.deltaTime;
        if (createManyFish >= 18)
        {
            createManyFish = 0;
        }
        if (timeVals >= 0.2f)
        {
            int num = Random.Range(0, 2);
            Vector3 offset = new Vector3(0, 0, Random.Range(-2, 2));
            Vector3 place = specialFishSchoolSpawner[specialFishSchoolSpawnerNum].position + offset;
            GameObject itemGo = Instantiate(fishList[num], place, specialFishSchoolSpawner[specialFishSchoolSpawnerNum].rotation);
            itemGo.GetComponent<Fish>().cantRotate = true;
            timeVals = 0;
        }
        else
        {
            timeVals += Time.deltaTime;
        }
    }
    /// <summary>开花鱼群</summary> 
    void FishGroup_Bloom()
    {
        GameObject go = fishList[Random.Range(2, fishList.Length)];
        for (int i = 0; i < 11; i++)
        {
            GameObject itemGo = Instantiate(go, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
            itemGo.GetComponent<Fish>().cantRotate = true;
        }
        createManyFish = 0;
    }
    /// <summary>"Prefabs/Enemy/" + name</summary>
    GameObject LoadEnemy(string name)
    {
        return Resources.Load<GameObject>("Prefabs/Enemy/" + name);
    }
    /// <summary>"Prefabs/Item/" + name</summary>
    GameObject LoadItem(string name)
    {
        return Resources.Load<GameObject>("Prefabs/Item/" + name);
    }


    private Vector3 RandomPos(int num)
    {
        Vector3 Vpositon = new Vector3();

        switch (num)
        {
            case 0:
                Vpositon = new Vector3(-24, 1, Random.Range(-14f, 14f));//-30  -  30
                break;
            case 1:
                Vpositon = new Vector3(Random.Range(-24f, 24f), 1, 14);//60 - 120
                break;
            case 2:
                Vpositon = new Vector3(24, 1, Random.Range(-14f, 14f));//150-210
                break;
            case 3:
                Vpositon = new Vector3(Random.Range(-24f, 24f), 1, -14);//-60-  -120
                break;
            default:
                break;
        }
        return Vpositon;
    }
    //随机角度
    private Vector3 RandomAngle(int num)
    {
        Vector3 Vangle = new Vector3();
        switch (num)
        {
            case 0:
                Vangle = new Vector3(0, Random.Range(-30f, 30f), 0);//-30  -  30
                break;
            case 1:
                Vangle = new Vector3(0, Random.Range(60f, 120f), 0);//60 - 120
                break;
            case 2:
                Vangle = new Vector3(0, Random.Range(150f, 210f), 0);//150-210
                break;
            case 3:
                Vangle = new Vector3(0, Random.Range(-60f, -120f), 0);//-60-  -120
                break;
            default:
                break;
        }
        return Vangle;
    }
    #endregion


}