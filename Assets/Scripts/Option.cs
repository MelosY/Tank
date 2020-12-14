/********************
 * 选择游戏模式
 * ****************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{

    public Transform posOne;
    public Transform posTwo;

    private int op = 1;
    // 游戏模式选择变量
    // op=1 表示选择单人模式
    // op=2 表示选择双人模式

    // Use this for initialization
    void Start()
    {
        //nothing to be done
    }

    // Update is called once per frame
    void Update()
    {
        // 输入W键表示选择单人模式
        if (Input.GetKeyDown(KeyCode.W))
        {
            op = 1;
            transform.position = posOne.position;
        }
        // 输入S键表示选择双人模式
        else if (Input.GetKeyDown(KeyCode.S))
        {
            op = 2;
            transform.position = posTwo.position;
        }
        // 空格键确认选择，进入游戏
        if (op == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            //单人模式进入场景1
            SceneManager.LoadScene(1);
        }
        if (op == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            //双人模式进入场景2
            SceneManager.LoadScene(2);
        }
    }
}
