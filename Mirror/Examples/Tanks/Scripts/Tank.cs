using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.Tanks
{
    public class Tank : NetworkBehaviour
    {
        [Header("Components")]  //组件
        public NavMeshAgent agent; //导航网格
        public Animator animator;  //动画器
        public TextMesh healthBar; //血条
        public Transform turret;   //炮塔的位置  

        [Header("Movement")]    //移动
        public float rotationSpeed = 100;  //转动速度

        [Header("Firing")]     //开火
        public KeyCode shootKey = KeyCode.Space;  //开火键
        public GameObject projectilePrefab;       //弹药预制体
        public Transform projectileMount;         //发射架的位置

        [Header("Stats")]                         //状态
        [SyncVar] public int health = 4;          //血量

        void Update()
        {
            // 不断更新血条
            // (SyncVar hook would only update on clients, not on server)
            healthBar.text = new string('-', health);

            // 本地tank的移动
            if (isLocalPlayer)
            {
                // rotate
                float horizontal = Input.GetAxis("Horizontal");
                transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

                // move
                float vertical = Input.GetAxis("Vertical");
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
                animator.SetBool("Moving", agent.velocity != Vector3.zero);

                // shoot
                if (Input.GetKeyDown(shootKey))
                {
                    CmdFire();
                }

                RotateTurret();
            }
        }

        // this is called on the server
        [Command]
        void CmdFire()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
            NetworkServer.Spawn(projectile);
            RpcOnFire();
        }

        // this is called on the tank that fired for all observers
        [ClientRpc]
        void RpcOnFire()
        {
            animator.SetTrigger("Shoot");
        }

        [ServerCallback]
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Projectile>() != null)
            {
                --health;
                if (health == 0)
                    NetworkServer.Destroy(gameObject);
            }
        }

        void RotateTurret()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Vector3 lookRotation = new Vector3(hit.point.x, turret.transform.position.y, hit.point.z);
                turret.transform.LookAt(lookRotation);
            }
        }
    }
}
