using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AgentController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    public Transform[] Buildings;
    public Transform[] Houses;
    public Transform lastbuild;
    private Vector3 lastpos;
    private float t_copy=0;
    private bool p=true;
    private int random;
    private float t;
    private bool record=true;

    public GameObject GameManager;

    private void Start()
    {
        cam = Camera.main;
        GameManager = GameObject.Find("GameManager");
        for (int i = 0; i < 8; i++)
        {
            Buildings[i] = GameObject.Find("WS" + (i+1)).transform;
            Houses[i] = GameObject.Find("House" + (i+1)).transform;
        }
        if (houseLimit(Houses[0]))
        {
            initialMove(Houses[0]);
        }
        else if (houseLimit(Houses[1]))
        {
            initialMove(Houses[1]);
        }
        else if(houseLimit(Houses[2]))
        {
            initialMove(Houses[2]);
        }
    }

    void Update()
    {

        t = GameManager.GetComponent<TimeManager>().t;
        if (Vector3.Distance(agent.transform.position,lastpos) < 10)
        {
            if (record)
            {
                t_copy = t;
                record = false;
            }
            if ( t > (t_copy+10))
            {
               if(p)
                {
                    if (houseLimit(Houses[0]))
                    {
                        moveto(Houses[0]);
                    }
                    else if (houseLimit(Houses[1]))
                    {
                        moveto(Houses[1]);
                    }
                    else if (houseLimit(Houses[2]))
                    {
                        moveto(Houses[2]);
                    }
                       
                    p = false;
                }
               else
                {
                    if (houseLimit(Buildings[0]))
                    {
                        moveto(Buildings[0]);
                    }
                    else if (houseLimit(Buildings[1]))
                    {
                        moveto(Buildings[1]);
                    }
                    else if(houseLimit(Buildings[2]))
                    {
                        moveto(Buildings[2]);
                    }
                    p = true;
                }
                record = true;
            }

        }

    }

    void moveto(Transform house)
    {
        agent.SetDestination(house.position);
        house.GetComponent<AgentDetection>().num_of_agents++;
        lastbuild.GetComponent<AgentDetection>().num_of_agents--;
        lastbuild = house;
        lastpos = house.position;
    }
    void initialMove(Transform house)
    {
        agent.SetDestination(house.position);
        house.GetComponent<AgentDetection>().num_of_agents++;
        lastbuild = house;
        lastpos = house.position;
    }
    bool houseLimit(Transform house)
    {
        if(house.GetComponent<AgentDetection>().num_of_agents < 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
        
}

