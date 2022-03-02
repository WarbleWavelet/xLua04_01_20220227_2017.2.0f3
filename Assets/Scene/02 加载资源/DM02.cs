using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM02 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject go = Resources.Load<GameObject>("Prefabs/Enemy/Fish1");
		if(go!=null)
		Instantiate(go);
		else

            print("sda");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
