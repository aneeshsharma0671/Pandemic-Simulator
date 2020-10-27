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
            foreach(var agent in NoA)
            {
                if(CheckInfection(agent))
                {
                  other.gameObject.transform.parent.gameObject.GetComponent<AgentController>().infect();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(NoA.Contains(other))
        {
            NoA.Remove(other);
        }
    }
   public bool CheckInfection(Collider agent)
    {
       return agent.gameObject.transform.parent.gameObject.GetComponent<AgentController>().infected;
    }
}
