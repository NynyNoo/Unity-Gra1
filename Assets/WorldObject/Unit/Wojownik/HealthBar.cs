using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class HealthBar : Wojownik
{
    public Slider slider;


    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Start()
    {
        slider.value = hitPoints / maxHitPoints;
    }
    protected override void Update() 
    { 
        slider.value = ActuaalHP();
    }
    protected float ActuaalHP()
    {
        return hitPoints/maxHitPoints;
        //return hitPoints / maxHitPoints;
    }
}
