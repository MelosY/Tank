/****************************************
* 敌方坦克模块，由系统自动控制
******************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    /*坦克属性值*/
    public float moveSpeed = 3;   //移动速度
    
    private Vector3 bullectEulerAngles;   //子弹飞行角度
    
    private float v=-1; 
    // v控制坦克的垂直方向
    // v=1  表示向上
    // v=-1 表示向下   
    
    private float h;
    // h控制坦克的水平方向
    // h=1  表示向右
    // h=-1 表示向左   

    /*系统调用*/
    private SpriteRenderer sr;         //控制图片方向
    public Sprite[] tankSprite;        //上 右 下 左 方向的图片
    public GameObject bullectPrefab;   //出生特效
    public GameObject explosionPrefab; //爆炸特效

    /*计时器*/
    private float timeValAttack;           //攻击时间间隔
    private float timeValChangeDirection;  //转向时间间隔


    // Use this for initialization
    void Start()
    {
        //nothing  to do
    }

    // Update is called once per frame
    void Update()
    {
        //攻击的时间间隔
        if (timeValAttack >= 3)
        {
            //间隔时间到了就攻击
            Attack();
        }
        else
        {
            //间隔时间没到不能攻击，时间累积
            timeValAttack += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        Move();  //坦克移动方法
    }

    //坦克生成方法
    private void Awake()
    {
        //获取一个坦克对象
        sr = GetComponent<SpriteRenderer>();
    }

    //坦克的攻击方法
    private void Attack()
    {
        
       //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度。
       Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeValAttack = 0;
        
    }


    //坦克的移动方法
    private void Move()
    {
        //如果距离上一次改变朝向的时间间隔达到了4
        if (timeValChangeDirection>=4)
        {
            //生成一个（0,8）的随机数，根据范围来确定方向
            int num = Random.Range(0, 8);
            
            //向下
            if (num>5)
            {
                v = -1;
                h = 0;
            }

            //向上
            else if (num==0)
            {
                v = 1;
                h = 0;
            }

            //向左
            else if (num>0&&num<=2)
            {
                h = -1;
                v = 0;
            }

            //向右
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }

            timeValChangeDirection = 0;
        }
        else
        {
            //时间累积
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        //改变垂直坐标
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0) //向下
        {
            //坦克图片改为向下
            sr.sprite = tankSprite[2];
            
            //子弹角度改为向下
            bullectEulerAngles = new Vector3(0, 0, -180);
        }

        else if (v > 0)  //向上
        {
            //坦克图片改为向上
            sr.sprite = tankSprite[0];
            
            //子弹角度改为向上
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        if (v != 0) //不动
        {
            return;
        }

        
        //改变水平坐标
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        if (h < 0)  //向左
        {
            //坦克图片改为向左
            sr.sprite = tankSprite[3];
            
            //子弹角度改为向左
            bullectEulerAngles = new Vector3(0, 0, 90);
        }

        else if (h > 0)  //向右
        {
            //坦克图片改为向右
            sr.sprite = tankSprite[1];
            
            //子弹角度改为向右
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
    }

    //坦克死亡方法
    private void Die()
    {
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //销毁
        Destroy(gameObject);
        //玩家得分加一
        PlayerManager.Instance.playerScore++;
    }

    //坦克碰撞方法
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            //碰撞之后必须改变方向，将转向时间间隔调到4即可
            timeValChangeDirection = 4;
        }
    }

}
