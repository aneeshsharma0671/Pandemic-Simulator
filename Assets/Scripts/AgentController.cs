using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] buildings;
    public Transform[] houses;
    public Transform lastBuilding;
    private float t;
    private float t_clone=0;
    private bool p=true;
    private int random;
    private bool record=true;


    public bool not_infected = true;
    public bool infected = false;
    public bool cured = false;


    public Material infected_color;
    public Material cured_color;

    public GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");

        for (int i = 0; i < 8; i++)
        {
            buildings[i] = GameObject.Find("WS" + (i+1)).transform;
            houses[i] = GameObject.Find("House" + (i+1)).transform;
        }

        foreach(var house in houses)
        {
            if (houseLimit(house))
            {
                initialMove(house);
                break;
            }
        }

    }

    void Update()
    {

        t = GameManager.GetComponent<TimeManager>().t;

        if (Vector3.Distance(agent.transform.position,lastBuilding.position) < 10)
        {
            if (record)
            {
                t_clone = t;
                record = false;
            }
            if ( t > (t_clone+10))
            {
               if(p)
                {

                    foreach (var house in houses)
                    {
                        if (houseLimit(house))
                        {
                            moveto(house);
                            break;
                        }
                    }

                    p = false;
                }
               else
                {
                    
                    foreach (var building in buildings)
                    {
                        if (houseLimit(building))
                        {
                            moveto(building);
                            break;
                        }
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
        lastBuilding.GetComponent<AgentDetection>().num_of_agents--;
        lastBuilding = house;
    }
    void initialMove(Transform house)
    {
        agent.SetDestination(house.position);
        house.GetComponent<AgentDetection>().num_of_agents++;
        lastBuilding = house;
    }
    bool houseLimit(Transform house)
    {
        return house.GetComponent<AgentDetection>().num_of_agents < 2;
    }
    public void infect()
    {
        GameObject GameManager;
        GameManager = GameObject.Find("GameManager");
     
        if (!infected && !cured)
        {
        not_infected = false;
        infected = true;
        cured = false;
        agent.GetComponentInChildren<Renderer>().material = infected_color;
        GameManager.GetComponent<InfectionManager>().no_of_infected++;
        GameManager.GetComponent<InfectionManager>().no_of_healthy--;
        }
    }

    public void Cure()
    {
        GameObject GameManager;
        GameManager = GameObject.Find("GameManager");

        if(infected && !cured)
        {
            not_infected = false;
            infected = false;
            cured = true;

            agent.GetComponentInChildren<Renderer>().material = cured_color;
            GameManager.GetComponent<InfectionManager>().no_of_infected--;
            GameManager.GetComponent<InfectionManager>().no_of_cured++;
        }
    }
        
}

