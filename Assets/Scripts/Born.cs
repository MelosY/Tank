using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public GameObject[] playerPrefab;

    public GameObject[] enemyPrefabList;

    public int createPlayer;

	// Use this for initialization
	void Start () {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BornTank()
    {
        if (createPlayer == 1)
        {
            Instantiate(playerPrefab[0], transform.position, Quaternion.identity);

        }
        else if(createPlayer == 2)
        {
            Instantiate(playerPrefab[1], transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
        
    }
}
