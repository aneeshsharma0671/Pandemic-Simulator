using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public GameObject[] agents;
    public int no_of_infected;
    public int no_of_healthy;
    public int no_of_cured;
    void Start()
    {
        no_of_healthy = 16;
        no_of_infected = 0;
        no_of_cured = 0;
        agents[Random.Range(0, 15)].GetComponent<AgentController>().infect();
    }
    void Update()
    {
        
    }
}
