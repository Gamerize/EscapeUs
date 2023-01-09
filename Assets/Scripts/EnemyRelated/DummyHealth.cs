using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummyHealth : MonoBehaviour
{
    public int m_eHealth = 0;
    public int m_eMaxHealth = 50;
    public int m_EnemyKillCount = 0;

    public OpenDoor m_Door;
    public Gun m_Gun;

    public UIManager UI;
    [SerializeField] ParticleSystem Explosion = null;

    // Start is called before the first frame update
    void Start()
    {
        m_eHealth = m_eMaxHealth;
    }

    public void Damage(int damage)
    {
        m_eHealth -= damage;
        if (m_eHealth <= 0)
        {
            FindObjectOfType<AudioManager>().playAudio("Death Sound");
            KillEnemy();
        }

    }
    public void KillEnemy()
    {
        m_EnemyKillCount++;
        if (m_EnemyKillCount <= 6)
        {
            m_Door.Reduce();
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        DestroyImmediate(gameObject);
    }
}

