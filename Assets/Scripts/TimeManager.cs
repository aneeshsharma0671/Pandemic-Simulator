using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Range(0.1f, 15)]
    public float modifiedScale;

    public float t;
    public Slider speed;

    private void Start()
    {
        t = 0;
        modifiedScale = 1;
    }
    void FixedUpdate()
    {

        t = (float)System.Math.Round(t + Time.fixedDeltaTime, 2);
    }
    private void Update()
    {
        modifiedScale = speed.value;
        Time.timeScale = modifiedScale;

     
    }

 

}
