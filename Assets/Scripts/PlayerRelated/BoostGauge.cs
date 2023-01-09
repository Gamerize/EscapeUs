using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostGauge : MonoBehaviour
{
    public Slider boostGauge;
    public BoostSystem fuelRemaining;

    void Start()
    {
        fuelRemaining = GameObject.FindGameObjectWithTag("Player").GetComponent<BoostSystem>();
        boostGauge = GetComponent<Slider>();
        boostGauge.maxValue = fuelRemaining.maxFuel;
        boostGauge.value = fuelRemaining.maxFuel;
    }

    public void setFuel(int boost)
    {
        boostGauge.value = boost;
    }
}
