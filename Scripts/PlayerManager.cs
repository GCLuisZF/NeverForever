using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    private int health;
    private int energy;
    // Start is called before the first frame update

    void Start()
    {
        health = 3;
        energy = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            --health;
            NetworkIdentity identity = GetComponent<NetworkIdentity>();
            if (health == 0)
            {
                showAnim(identity.connectionToClient, "Death");
                Invoke(nameof(DestroyPlayer), 2);
            }
            showAnim(identity.connectionToClient, "Hurt");
            Debug.Log(other);
        }

        //Debug.Log(other);
    }

    void DestroyPlayer()
    {
        NetworkServer.Destroy(gameObject);
    }

    [TargetRpc]
    void showAnim(NetworkConnection conn, string animTrigger)
    {
        GetComponent<NetworkAnimator>().SetTrigger(animTrigger);
    }

}
