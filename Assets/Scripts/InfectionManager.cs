using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public GameObject[] agents;
    public AgentDetection agentdetection;
    private int random;

    public int no_of_infected;
    public int no_of_healthy;
    public int no_of_cured;
    public float chance_of_infection = 75;
    void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            agents[i] = GameObject.Find("Agent (" + (i) + ")");
        }
        random = Random.Range(0, 15);
        no_of_healthy = 16;
        no_of_infected = 0;
        no_of_cured = 0;
        agents[random].GetComponent<AgentController>().infect();
        StartCoroutine(agentdetection.Cure_after(agents[random],100f));
    }

    void Update()
    {
        
    }
}
