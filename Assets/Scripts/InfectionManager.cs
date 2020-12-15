using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfectionManager : MonoBehaviour
{
    public AgentDetection agentdetection;
    public int no_of_infected;
    public int no_of_healthy;
    public int no_of_cured;
    public int no_of_initial_infections;
    public float chance_of_infection;
    public GameObject sceneManager;
    public TMP_Text no_of_infected_text;
    public TMP_Text no_of_healthy_text;
    public TMP_Text no_of_cured_text;
    public TMP_Text R_Text;

    public float R;
    public float Avg_R;
    private float Sum;
    public List<float> R_list;
    public float R_last;
    public float NI = 0;
    public GameObject Gamemanager;

    private int[] randomNumbers;
    void Start()
    {
        R_last = no_of_initial_infections;
        Gamemanager = GameObject.Find("GameManager");
        sceneManager = GameObject.Find("SceneManager");
        no_of_initial_infections = sceneManager.GetComponent<SceneManagment>().no_of_initial_infections;
        chance_of_infection = sceneManager.GetComponent<SceneManagment>().chance_of_infection;

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

    private void FixedUpdate()
    {
        if(Gamemanager.GetComponent<TimeManager>().t % 10 == 0)
        {
            R = NI / R_last;
            R_last = NI;
            if(R < 5)
            {
                R_list.Add(R);
                Sum += R;
            }
            NI = 0;
        }
       
        Avg_R = Sum / R_list.Count;
    }

    void Update()
    {
        no_of_healthy_text.text = "" + no_of_healthy;
        no_of_cured_text.text = "" + no_of_cured;
        no_of_infected_text.text = "" + no_of_infected;
        R_Text.text = "" +System.Math.Round(Avg_R,2);
    }
}
