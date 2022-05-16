using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_control : MonoBehaviour
{
   

    public bool isVertival;

    public float speed = 30f;
    public float changeDirectionTime=2f;
    private Vector2 moveDirection;
    private float changeTimer;
    private Rigidbody2D rbody;

    private  Animator anim;
        
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        
        moveDirection = isVertival ? Vector2.up : Vector2.right;//如果是垂直移动，方向就朝向上；否则就朝右
        
        changeTimer= changeDirectionTime;

        
    }

    // Update is called once per frame
    
    void Update()
    {

        changeTimer -= Time.deltaTime;
        if(changeTimer<0)
        {
            moveDirection *=-1;
            changeTimer =changeDirectionTime;
        }

        Vector2 position =rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
        rbody.MovePosition(position);

    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        chara_move pc =other.gameObject.GetComponent<chara_move>();
        if(pc !=null)
        {
            pc.ChangeHealth(-1);
        }

    }
}
