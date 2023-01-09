using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoostSystem : MonoBehaviour
{
    public int fuel = 0;
    public int maxFuel = 100;
    public bool OverHeat;
    public float m_Cooldown = 5f;
    public int timer;

    public BoostGauge boostGauge;
    public TextMeshProUGUI m_OverHeatText;

    // Start is called before the first frame update
    void Start()
    {
        fuel = maxFuel;
        OverHeat = false;
        m_OverHeatText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set timer to slow down
        if (Input.GetButton("Boost") && fuel > 0)
        {
            FindObjectOfType<AudioManager>().playAudio("Boost Sound");
            Boosting(10);          
        }
        else if (fuel < maxFuel && fuel > 0)
        {
            Refill(5);
            FindObjectOfType<AudioManager>().playAudio("Refill");
        }
        else if (fuel <= 0)
        {
            CoolDown();
            FindObjectOfType<AudioManager>().playAudio("OverHeat");
            m_OverHeatText.text = "OVERHEAT!";
            timer++;
            if (timer == 5)
            {
                Refill(5);
                m_OverHeatText.text = "";
                timer = 0;
            }
        }
    }

    public void Boosting(int boost)
    {
        fuel -= boost;

        boostGauge.setFuel(fuel);
    }

    public void Refill(int boost)
    {
        fuel += boost;

        boostGauge.setFuel(fuel);
    }

    public void CoolDown()
    {
        OverHeat = true;
        StartCoroutine(CoolDownBuffer());
        OverHeat = false;
    }

    IEnumerator CoolDownBuffer()
    {
        if (OverHeat == true)
        {
            yield return new WaitForSeconds(m_Cooldown);
        }
    }
}
