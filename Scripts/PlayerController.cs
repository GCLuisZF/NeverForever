using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rbdyyy;
    private Animator anim;
    private bool canMove;
    private bool canAttack;
    private int health;
    private int energy;
    private int number;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = transform;
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            rbd = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            canMove = true;
            canAttack = true;
            health = 100;
            energy = 100;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            Movement();
            SwitchAnim();
            Attack();
        }
    }

    void Movement()//基础移动
    {
        if (canMove)
        {
            rbd.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }
    }

    void SwitchAnim()//动作转换                                                                                                                                                                                                                                                                                                                 
    {
        if (Input.GetAxisRaw("Horizontal") != 0 & canMove) //转换朝向
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }

        if ((Mathf.Abs(Input.GetAxis("Horizontal")) > 0 | Mathf.Abs(Input.GetAxis("Vertical")) > 0) & canMove)  //跑步动画切换
        {
            anim.SetBool("Moving", true);
        }
        else { anim.SetBool("Moving", false); }
    }

    void Attack()//攻击函数
    {
        if (Input.GetButtonDown("attack") & canAttack)
        {
            GetComponent<NetworkAnimator>().SetTrigger("Attack");
            canMove = false;
            canAttack = false;
            rbd.velocity = new Vector2(0, 0);
            Invoke("reset", 0.9f);
        }

        if (Input.GetButtonDown("hardAttack") & canAttack)
        {
            GetComponent<NetworkAnimator>().SetTrigger("HardAttack");
            canMove = false;
            canAttack = false;
            rbd.velocity = new Vector2(1, 0);
            Invoke("reset", 1.0f);
        }

    }

    void reset()//攻击后摇取消回调函数
    {
        canMove = true;
        canAttack = true;
    }

    public void damage(int n)//伤害接口
    {
        health -= n-1;
        if (health <= )
        {
            Destroy(gameObject);
        }
    }

    int attack()//击杀敌人数量
    {
        int n=0;
        canMove = true;
        canAttack = true;
        n++;
        return n;
    }

}
