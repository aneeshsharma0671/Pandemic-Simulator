using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public AgentDetection agentdetection;
    private int random;
    public int no_of_infected;
    public int no_of_healthy;
    public int no_of_cured;
    public float chance_of_infection;
    void Start()
    {
        
        random = Random.Range(0, (gameObject.GetComponent<SpawnAgent>().no_of_agents - 1));
        no_of_healthy = gameObject.GetComponent<SpawnAgent>().no_of_agents;
        no_of_infected = 0;
        no_of_cured = 0;
        gameObject.GetComponent<SpawnAgent>().Agents[random].GetComponent<AgentController>().infect();
        StartCoroutine(agentdetection.Cure_after(gameObject.GetComponent<SpawnAgent>().Agents[random], 100f));
    }

    void Update()
    {
        
    }
}
