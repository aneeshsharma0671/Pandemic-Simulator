using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneManagment : MonoBehaviour
{
    public int no_of_agents;
    public int no_of_buildings;
    public int building_limit;
    public int no_of_initial_infections;
    public float chance_of_infection;
    public InputField no_of_agent;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadSimulation()
    {
        no_of_agents = Convert.ToInt32(no_of_agent.text);
        SceneManager.LoadScene(1,LoadSceneMode.Single);

    }
}
