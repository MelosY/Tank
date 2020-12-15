using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    //生命值
    private int lifeValue1 = 3;
    private int lifeValue2 = 3;

    //玩家得分
    public int playerScore = 0;
    //是否死亡
    public int isDead;
    //是否被打败，被打败则返回
    public bool isDefeat;


    //born的物体
    public GameObject born;
    //玩家的得分快
    public Text playerScoreText;
    public Text PlayerLifeValueText;
    //打败的UI
    public GameObject isDefeatUI;

    //单例模式设置
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //判断是否结束流程
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        //谁死亡了就复活谁
        if (isDead==1)
        {
            Recover1();
        }
        else if (isDead == 2)
        {
            Recover2();
        }
        //设置
        playerScoreText.text = lifeValue2.ToString();
        PlayerLifeValueText.text = lifeValue1.ToString();
        Debug.Log(lifeValue1.ToString());
	}
    
    //游戏玩家1复活
    private void Recover1()
    {
        if (lifeValue1<=0)
        {
            //游戏失败，返回主界面
            isDefeat = true;
            //协程
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            //生命--
            lifeValue1--;
            //重新生成
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = 1;
            isDead = 0;
        }
    }
    //游戏玩家2复活
    private void Recover2()
    {
        if (lifeValue2 <= 0)
        {
            //游戏失败，返回主界面
            isDefeat = true;
            //协程
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            //生命--
            lifeValue2--;
            //重新生成
            GameObject go = Instantiate(born, new Vector3(-2, 8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = 2;
            isDead = 0;
        }
    }
    
    //回到主界面
    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
