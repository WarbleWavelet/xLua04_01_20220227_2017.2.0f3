﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour {

    float gameScale;




    // Use this for initialization
    void Start () 
    {
        gameScale = 1f;
        GetComponent<Slider>().onValueChanged.AddListener((value)=> {
            gameScale = value*10f;
        });


        print(Random.Range(0,100));
    }
	
    void Update()
    {
        UnityEngine.Time.timeScale = gameScale;  


    }
}
