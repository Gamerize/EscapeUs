using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int Health = 0;
    public int maxHealth = 100;
    public bool IsPlayerDead;

    public HealthBar healthBar;
    public UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        IsPlayerDead = false;
    }

    public void Damage (int damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        if (Health <= 0)
        {
            FindObjectOfType<AudioManager>().playAudio("Death Sound");
            IsPlayerDead = true;
            UI.GameOver(IsPlayerDead);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Damage(5);
    }
}
