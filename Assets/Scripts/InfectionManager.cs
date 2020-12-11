using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public AgentDetection agentdetection;
    public int no_of_infected;
    public int no_of_healthy;
    public int no_of_cured;
    public int no_of_initial_infections;
    public float chance_of_infection;
    public GameObject sceneManager;

    private int[] randomNumbers;
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
      //  no_of_initial_infections = sceneManager.GetComponent<SceneManagment>().no_of_initial_infections;
      //  chance_of_infection = sceneManager.GetComponent<SceneManagment>().chance_of_infection;

        randomNumbers = new int[no_of_initial_infections];
        var numbers = new List<int> (gameObject.GetComponent<SpawnAgent>().no_of_agents);
        for (int i = 0; i < gameObject.GetComponent<SpawnAgent>().no_of_agents; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            int random = Random.Range(0, numbers.Count);
            randomNumbers[i] = numbers[random];
            numbers.RemoveAt(random);
        }


        no_of_healthy = gameObject.GetComponent<SpawnAgent>().no_of_agents;
        no_of_infected = 0;
        no_of_cured = 0;

        foreach (int random in randomNumbers)
        {
         gameObject.GetComponent<SpawnAgent>().Agents[random].GetComponent<AgentController>().infect();
         StartCoroutine(agentdetection.Cure_after(gameObject.GetComponent<SpawnAgent>().Agents[random], 100f));
        }
    
    }

    void Update()
    {
        
    }
}
