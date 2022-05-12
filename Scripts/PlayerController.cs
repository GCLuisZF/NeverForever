using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rbd;
    private Animator anim;
    private bool canMove;
    private bool canAttack;
    private int health;
    private int energy;

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

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
        Attack();
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
            canMove = false;
            canAttack = false;
            anim.SetTrigger("Attack");
            rbd.velocity = new Vector2(0, 0);
            Invoke("reset", 1);
        }

        if (Input.GetButtonDown("hardAttack") & canAttack)
        {
            canMove = false;
            canAttack = false;
            anim.SetTrigger("HardAttack");
            rbd.velocity = new Vector2(0, 0);
            Invoke("reset", 1);
        }

    }

    void reset()//攻击后摇取消回调函数
    {
        canMove = true;
        canAttack = true;
    }

    public void damage(int n)//伤害接口
    {
        health -= n;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
