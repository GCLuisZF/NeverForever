using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rbd;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
    }

    void Movement()
    {

        rbd.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,Input.GetAxis("Vertical")*speed);

    }

    void SwitchAnim()
    {
        if(Input.GetAxisRaw("Horizontal") != 0) //转换朝向
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1,1);
        }
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 | Mathf.Abs(Input.GetAxis("Vertical")) > 0)  //跑步动画切换
        {
            anim.SetBool("Moving", true);
        }
        else { anim.SetBool("Moving", false); }
    }

}
