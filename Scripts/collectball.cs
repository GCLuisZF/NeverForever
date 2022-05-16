using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
/********触发回血************/
    void OnTriggerEnter2D(Collider2D other) {
            chara_move pc = other.GetComponent<chara_move>();
            if(pc!=null)
            {
                Debug.Log("玩家碰到了恢复药水！");
                if(pc.MyCurrentHealth < pc.MyMaxHealth)
                pc.ChangeHealth(1);
                Destroy(this.gameObject);

            }
            
        }
}
