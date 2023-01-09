 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    EnemyHealth Enemy;
    public int MaxCount = 6;
    public bool Destroyed;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<EnemyHealth>();
    }

    public void Reduce()
    {
        MaxCount--;
        if(MaxCount == 0)
        {
            FindObjectOfType<AudioManager>().playAudio("Button Sound");
            Destroyed = true;
            Destroy(gameObject);
        }
    }
}
