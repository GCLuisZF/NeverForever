using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 5f;
    private Rigidbody2D rbd;
    private Animator anim;
    private bool canMove;
    private bool canAttack;
    public Slider LeftHealth;
    public Slider RightHealth;

    [Tooltip("The current Health of our player")]
    public float Health = 1f;

    public int directionFlag;

    // Start is called before the first frame update
    private void Awake()
    {

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("我是左");
            directionFlag = 0;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("我是右");
            directionFlag = 1;
        }

    }

    void Start()
    {
        LeftHealth = GameObject.Find("Left Health Slider").GetComponentInChildren<Slider>();
        RightHealth = GameObject.Find("Right Health Slider").GetComponentInChildren<Slider>();
        if (photonView.IsMine)
        {
            rbd = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            canMove = true;
            canAttack = true;
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {
            Movement();
            SwitchAnim();
            Attack();
            if (Health <= 0f)//死亡判定
            {
                anim.SetTrigger("Death");
                Invoke("LoseGame", 0.8f);
            }

        }

        if (directionFlag == 0)
        {
            LeftHealth.value = Health;
        }
        if (directionFlag == 1)
        {
            RightHealth.value = Health;
        }
    }

    void LoseGame()//输掉游戏并且退出
    {
        GameManager.Instance.LeaveRoom();
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
            Invoke("reset", 0.7f);
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

    void OnTriggerEnter2D(Collider2D other)//受到伤害
    {
        Debug.Log("OnTriggerEnter");
        if (!photonView.IsMine)
        {
            return;
        }
        if (!other.name.Contains("HitBox"))
        {
            return;
        }
        anim.SetTrigger("Hurt");
        Health -= 0.1f;
        Invoke("reset", 0.5f);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //同步血量
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
        }
        else
        {
            this.Health = (float)stream.ReceiveNext();
        }
    }

}
