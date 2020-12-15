using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Controls : MonoBehaviour
{
    public Transform freelook;
    public Camera Cam;
    public GameObject Cinemachine;
    public GameObject Gamemanager;

    public void Start()
    {
        
    }
    public void Update()
    {
     
    }
    public void SendRay()
    {
        Ray ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            freelook.position = hit.point;
        }
    }
    public void updateSpeed()
    {
        Cinemachine.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 1 / Gamemanager.GetComponent<TimeManager>().speed.value;
        Cinemachine.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 150 / Gamemanager.GetComponent<TimeManager>().speed.value;
    }
}
