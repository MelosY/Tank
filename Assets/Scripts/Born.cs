using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    //存放需要的坦克
    public GameObject[] playerPrefab;
    //存放需要的敌人
    public GameObject[] enemyPrefabList;
    //生成的模式
    public int createPlayer;

	//生成一个敌人，后摧毁自己
	void Start () {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //诞生坦克
    private void BornTank()
    {
        //诞生玩家1
        if (createPlayer == 1)
        {
            Instantiate(playerPrefab[0], transform.position, Quaternion.identity);

        }
        //诞生玩家2
        else if(createPlayer == 2)
        {
            Instantiate(playerPrefab[1], transform.position, Quaternion.identity);
        }
        //诞生敌人
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
        
    }
}
