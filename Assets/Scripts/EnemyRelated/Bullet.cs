using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_BulletSpeed = 5f;
    private Rigidbody m_RB;

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        m_RB.velocity = transform.forward * m_BulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Destroy(gameObject);
    }


}
