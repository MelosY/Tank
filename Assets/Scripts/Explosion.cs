/**************************************
* 当坦克或Heart被敌方子弹击中时就会爆炸
**************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		// 爆炸后销毁目标即可
        Destroy(gameObject, 0.167f);
	}
	
	// Update is called once per frame
	void Update () {
		// 销毁只需要一次，所以这里什么也不用做
	}
}
