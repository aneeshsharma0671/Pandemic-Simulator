using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AgentDetection : MonoBehaviour
{
    public List<Collider> NoA = new List<Collider>();
    public int num_of_agents = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(!NoA.Contains(other))
        {
            NoA.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(NoA.Contains(other))
        {
            NoA.Remove(other);
        }
    }
}
