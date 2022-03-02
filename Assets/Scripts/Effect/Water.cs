using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 水纹播放的特效 </summary>
public class Water : MonoBehaviour 
{

    private SpriteRenderer sr;
    public Sprite[] pictures;
    private int count=0;


	void Start () 
    {
        sr = GetComponent<SpriteRenderer>();
	}
	

	void Update () 
    {
        sr.sprite = pictures[count];
        count++;
        if (count==pictures.Length)
        {
            count = 0;
        }
	}
}
