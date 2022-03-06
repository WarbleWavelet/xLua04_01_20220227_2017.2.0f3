using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]

public class HotfixEmpty : MonoBehaviour 
{

	void Start () {
		
	}
	
	void Update () {
		
	}
	[LuaCallCSharp]
	void OnTriggerEnter(Collider other)
    {

    }


	[LuaCallCSharp]
	public void BehaviourMethod()
	{ 
	
	}
}
