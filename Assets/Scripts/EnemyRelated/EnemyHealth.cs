using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int m_eHealth = 0;
    public int m_eMaxHealth = 50;
    public int m_EnemyKillCount = 0;

    public Gun m_Gun;
    public TextMeshProUGUI m_BulletText;
    public TextMeshProUGUI m_EnemyText;
    public Transform m_EnemyParent;

    public UIManager UI;
    [SerializeField] ParticleSystem Explosion = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CHILDREN LEFT: " + m_EnemyParent.childCount);
        m_EnemyText.text = "Enemies Remaining: " + m_EnemyParent.childCount;
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
        Instantiate(Explosion, transform.position, Quaternion.identity);
        m_Gun.m_bulletsRemain = m_Gun.m_bulletsRemain + 15;
        m_BulletText.text = "Bullet: " + m_Gun.m_bulletsRemain;
        DestroyImmediate(gameObject);
        Debug.Log("CHILDREN LEFT: " + m_EnemyParent.childCount);
        m_EnemyText.text = "Enemies Remaining: " + m_EnemyParent.childCount;
        if (CheckAllKilled() == true)
        {
            Debug.Log("All Dead");
            UI.Victory();
        }
    }

    public bool CheckAllKilled()
    {
        if (m_EnemyParent.childCount == 0)
        {
            return true;
        }
        else return false;
    }
}
