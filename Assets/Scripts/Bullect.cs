using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour {
    
    //子弹移动速度
    public float moveSpeed = 10;
    //判别是谁的子弹
    public bool isPlayerBullect;


	
	void Start () {
		
	}
	

	//子弹每秒移动
	void Update () {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

    //判断碰到了谁
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞的标签
        switch (collision.tag)
        {
            //给碰撞的物体死亡信息
            case "Tank":
                if (!isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            //墙壁则摧毁
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullect)
                {
                    collision.SendMessage("PlayAudio");
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
