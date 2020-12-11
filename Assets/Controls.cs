using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public Transform freelook;
    public Camera Cam;
  
    public void SendRay()
    {
        Ray ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            freelook.position = hit.point;
        }
    }
}
