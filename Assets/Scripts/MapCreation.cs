using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapCreation : MonoBehaviour {


    //用来装饰初始化地图所需物体的数组。
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] item;

    //已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();
    Scene scene;



//初始生成
private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("当前场景: " + scene.name);
        //如果是双人对战，则生成第二个heart等
        if(scene.name =="Game1")
        {
            
            //实例化老家
            CreateItem(item[0], new Vector3(0, 8, 0), Quaternion.identity);
            //用墙把老家围起来
            CreateItem(item[1], new Vector3(-1, 8, 0), Quaternion.identity);
            CreateItem(item[1], new Vector3(1, 8, 0), Quaternion.identity);
           
            for (int i = -1; i < 2; i++)
            {
                CreateItem(item[1], new Vector3(i, 7, 0), Quaternion.identity);
            }
            //生成玩家2
            GameObject go1 = Instantiate(item[3], new Vector3(-2, 8, 0), Quaternion.identity);
            go1.GetComponent<Born>().createPlayer = 2;
        }
        InitMap();
    }
    
    //随机生成地图
    private void InitMap()
    {
        //实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        //生成实例化外围墙，也即周围的一圈空气墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = 1;

        //产生敌人
        if(scene.name == "Game")
        {
            CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
            CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
            CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

            InvokeRepeating("CreateEnemy", 4, 5);
        }



        //随机生成实例化地图
        for (int i = 0; i < 60; i++)
        {
            //生成可被摧毁墙
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            //生成河
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            //生成刚墙
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            //生成草
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    //私有方法，生成在对应位置，对应角度的对应物体。
    private void CreateItem(GameObject createCameObject,Vector3 createPosition,Quaternion createRotation)
    {
        //生成物体
        GameObject itemGo = Instantiate(createCameObject, createPosition, createRotation);
        //设置父物体
        itemGo.transform.SetParent(gameObject.transform);
        //添加该位置
        itemPositionList.Add(createPosition);

    }

    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        //不生成x=-10,10的两列，y=-8,8正两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
            
        }
    }

    //用来判断位置列表中是否有这个位置
    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos==itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        //判断现在该生成的位置
        if (num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num==1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }

}
