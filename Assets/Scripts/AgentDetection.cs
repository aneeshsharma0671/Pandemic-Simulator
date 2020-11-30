using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AgentDetection : MonoBehaviour
{
    public List<Collider> NoA = new List<Collider>();
    public GameObject Gamemanger;
    public int num_of_agents = 0;

    void Start()
    {
        Gamemanger = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!NoA.Contains(other))
        {
            foreach(var agent in NoA)
            {
                if(CheckInfection(agent))
                {
                    if(Random.value > (1-((Gamemanger.GetComponent<InfectionManager>().chance_of_infection)/100)))
                    {
                        other.gameObject.transform.parent.gameObject.GetComponent<AgentController>().infect();
                        StartCoroutine(Cure_after(other.gameObject.transform.parent.gameObject, 100f));
                 
                    }
                    else
                    {
                        Debug.Log(other.gameObject.transform.parent.gameObject.name + "didn't get infection");
                    }
                
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

 public IEnumerator Cure_after(GameObject agent , float t)
    {
        Debug.Log(agent.name + "got infected");
        yield return new WaitForSeconds(t);
        agent.GetComponent<AgentController>().Cure();
        Debug.Log(agent.name + "got cured");
    }
}
