using UnityEngine;
using System.Collections;


/// <summary> 泡泡，移动速度不一样（开始界面，游戏界面） </summary>
public class Pao : MonoBehaviour {

    /// <summary>移动速度</summary>
	public int moveSpeed;
    public bool isGamePao;

	void Start () 
    {

        DifferentMoveSpeed();


	}
	

	void Update ()
    {
        Move();
	}


    void DifferentMoveSpeed()
    {
        if (isGamePao)
        {
            moveSpeed = Random.Range(2, 4);
            Destroy(this.gameObject, Random.Range(0.5f, 1f));
        }
        else
        {
            moveSpeed = Random.Range(40, 100);
            Destroy(this.gameObject, Random.Range(7f, 10f));
        }
    }

    void Move()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.World);
    }
}
