using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Range(0.1f, 5)]
    public float modifiedScale;

    public float t;

    private void Start()
    {
        t = 0;
        modifiedScale = 1;
    }
    void FixedUpdate()
    {
        t =Time.fixedTime;

    }
    private void Update()
    {
        Time.timeScale = modifiedScale;
    }

}
