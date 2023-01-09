using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    //sighting var
    //public bool PlayerInRange;
    public bool PlayerSpotted;
    public float SightRange = 100f;
    public float AttackRange = 10f;

    //layermasks
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;

    public NavMeshAgent EnemyAgent;
    public Transform Player;
    public Transform Enemy;

    public AIState m_state;

    //Wander State
    public Vector3 NextPos;
    public bool NextPosSet = false;
    public float m_RandomDir = 10f;

    //Shoot State
    //public GameObject m_Bullet;
    //public Transform ShootPoint;
    //public bool Shooted;
    //public float Firerate = 1f;
    //float BulletSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        m_state = AIState.WANDER;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSpotted = Physics.CheckSphere(Enemy.position, SightRange, PlayerLayer);
        //PlayerInRange = Physics.CheckSphere(Enemy.position, AttackRange, PlayerLayer);

        switch (m_state)
        {
            case AIState.WANDER:
                {
                    Wander();

                    if (PlayerSpotted)
                    {
                        m_state = AIState.CHASE;
                    }
                    break;
                }
            case AIState.CHASE:
                {
                    Chase();

                    if (!PlayerSpotted)
                    {
                        m_state = AIState.WANDER;
                    }
                    //if (PlayerInRange)
                    //{
                    //    m_state = AIState.SHOOT;
                    //}
                    break;
                }
            //case AIState.SHOOT:
            //    {
            //        Shoot();

            //        if (!PlayerInRange)
            //        {
            //            m_state = AIState.CHASE;
            //        }
            //        break;
            //    }
        }
    }

    public void FindRandomDir()
    {
        float RandomX = Random.Range(-m_RandomDir, m_RandomDir);
        float RandomZ = Random.Range(-m_RandomDir, m_RandomDir);

        NextPos = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        NextPosSet = true;
    }

    public void Wander()
    {

        if (!NextPosSet)
        {
            FindRandomDir();
        }

        if (NextPosSet)
        {
            EnemyAgent.SetDestination(NextPos);
        }

        Vector3 WalkDist = transform.position - NextPos;

        if (WalkDist.magnitude < 1f)
        {
            NextPosSet = false;
        }
    }

    public void Chase()
    {
        EnemyAgent.SetDestination(Player.position);
    }

    //public void Shoot()
    //{
    //    EnemyAgent.SetDestination(Enemy.position);

    //    Enemy.LookAt(Player);

    //    if(!Shooted)
    //    {
    //        GameObject Bullet = Instantiate<GameObject>(m_Bullet, ShootPoint.position, Quaternion.identity);
    //        //Bullet.GetComponent<Rigidbody>().AddForce(ShootPoint.up * BulletSpeed, ForceMode.Impulse); 

    //        Debug.Log("shooting Player");

    //        Shooted = true;
    //        //coroutine doesn't work, IDK
    //        Invoke("CanAttack", Firerate);
    //    }
    //}

    //private void CanAttack()
    //{
    //    Shooted = false;
    //}

    public enum AIState
    {
        WANDER,
        CHASE
        //SHOOT
    }
}
