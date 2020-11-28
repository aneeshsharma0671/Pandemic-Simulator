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
            foreach(var agent in NoA)
            {
                if(CheckInfection(agent))
                {
                  other.gameObject.transform.parent.gameObject.GetComponent<AgentController>().infect();
                    StartCoroutine(Cure_after(other.gameObject, 100f));
                }
            }
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
   public bool CheckInfection(Collider agent)
    {
        return agent.gameObject.transform.parent.gameObject.GetComponent<AgentController>().infected;
    }

    IEnumerator Cure_after(GameObject agent , float t)
    {
        Debug.Log(agent.transform.parent.gameObject.name + "got infected");
        yield return new WaitForSeconds(t);
        agent.transform.parent.gameObject.GetComponent<AgentController>().Cure();
        Debug.Log(agent.transform.parent.gameObject.name + "got cured");
    }
}
