using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgent : MonoBehaviour
{
    public int no_of_agents;
    public int no_of_buildings;
    public int building_limit;
    public GameObject AgentPrefab;
    public Transform[] buildings;
    public GameObject[] Agents;
    public GameObject sceneManager;
    public Transform platform;

    private void Awake()
    {
        platform = GameObject.Find("Platform").transform;
      //  sceneManager = GameObject.Find("SceneManager");
      //  no_of_agents = sceneManager.GetComponent<SceneManagment>().no_of_agents;
      //  no_of_buildings = sceneManager.GetComponent<SceneManagment>().no_of_buildings;
      //  building_limit = sceneManager.GetComponent<SceneManagment>().building_limit;

        buildings = new Transform[no_of_buildings];
        Agents = new GameObject[no_of_agents];
        for (int i = 0; i < no_of_buildings; i++)
        {
            buildings[i] = GameObject.Find("WS" + (i + 1)).transform;
        }

        for (int i = 0; i < no_of_agents; i++)
        {
            string count = "Agent";
            count = count + i.ToString();
            Agents[i] = GameObject.Instantiate(AgentPrefab,platform);
            Agents[i].name = count;
            Agents[i].transform.position = buildings[Mathf.RoundToInt(i / building_limit + 0.01f)].transform.position;
        }
    }

}
