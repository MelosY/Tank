using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour {

    //属性值
    //坦克的移动速度
    public float moveSpeed=10;
    //炮弹的初始角度
    private Vector3 bullectEulerAngles;
    //时间间隔
    private float timeVal;
    //无敌时间
    private float defendTimeVal=3;
    //是否处在无敌状态
    private bool isDefended=true;

    //引用
    private SpriteRenderer sr;
    //上 右 下 左
    public Sprite[] tankSprite;
    //子弹预制体
    public GameObject bullectPrefab;
    //爆炸预制体
    public GameObject explosionPrefab;
    //无敌特效预制体
    public GameObject defendEffectPrefab;
    //移动声音
    public AudioSource moveAudio;
    //炮弹声音
    public AudioClip[] tankAudio;

    private void Awake()
    {
       sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //是否处于无敌状态
        if (isDefended)
        {
            //将无敌预制体开启
            defendEffectPrefab.SetActive(true);
            //递减无敌时间
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal<=0)
            {
                //无敌时间过后就将无敌设置为假
                isDefended = false;
                //无敌特效预制体关闭
                defendEffectPrefab.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        //调用移动的方法
        Move();
        //调用攻击的方法
        Attack();
        //攻击的CD
        /*if (timeVal >= 0.1f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }*/
    }

    //坦克的攻击方法
    private void Attack()
    {
        //判断玩家是否按下K键发射炮弹
        if (Input.GetKeyDown(KeyCode.K))
        {

            //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度。
            Instantiate(bullectPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
            //如果设定攻击时间间隔，该方法用于设置间隔时间
            timeVal = 0;
            Debug.Log("1");
        }
    }


    //坦克的移动方法
    private void Move()
    {
        //获取输入，这里的“Vertical2”在项目的Project Settings里面进行了预设
        float v = Input.GetAxisRaw("Vertical2");
        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        //
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }

        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        if (Mathf.Abs(v)>0.05f)
        {
            moveAudio.clip = tankAudio[1];
            
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (v != 0)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal2");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }

        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    //坦克的死亡方法
    private void Die()
    {
        //如果坦克处在无敌状态则不会死亡，直接返回
        if (isDefended)
        {
            return;
        }
        
        //记录坦克的死亡次数
        PlayerManager.Instance.isDead = 1;

        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
