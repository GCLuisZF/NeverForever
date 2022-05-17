using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
            rbd = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            canMove = true;
            canAttack = true;
            health = 100;
            energy = 100;
    }


     void OnTrigerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            Debug.Log("123");
        }

    }
    // Update is called once per frame
        AudioSource ads;
        void update（）
        {
            float moveX = Input.GetAxisRaw("Horizontal");//水平方向 A：-1 ,D:  1. 无输入返回0.
            float moveY = Input.GetAxisRaw("Vertical");//垂直方向 W：1,S:-1.无输入返回0.
            Vector2 moveVector = new Vector2(moveX, moveY);

            if (moveVector.x != 0 || moveVector.y != 0)
            {
                lookDircetion = moveVector;

            }
            animplayer.SetFloat("Look X", lookDircetion.x);
            animplayer.SetFloat("Look Y", lookDircetion.y);
            animplayer.SetFloat("Speed", moveVector.magnitude);//magnitude:移动量
            Vector2 position = rbody.position;//获取玩家当前自身位置。
            position += moveVector * Speed * Time.deltaTime;
            if (moveX != 0 || moveY != 0 )//判断是否在移动
            {
                
                ads.enabled = true;//在动，播音乐
            }
            else
            {
                ads.enabled = false;//没动，关掉
            }
                   if (kaiguan == true)
            {
                zhongbiao -= Time.deltaTime;
                if (zhongbiao < 0)
                {
                    kaiguan = false;
                }
            }
    }

    public void ChangeHealth(int amount) {
            //==========检测走进了陷阱，先检测开关是否打开，如果打开，减少血量，计时，直到下一次开关打开
            if (amount<0)
            {
                
                if (kaiguan ==true)
                {
                    return;
                }
                kaiguan = true;
                zhognbiao = buchang;
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        }


}
