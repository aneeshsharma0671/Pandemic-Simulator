
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class SceneManagment : MonoBehaviour
{
    public int no_of_agents;
    public int no_of_buildings;
    public int building_limit;
    public int no_of_initial_infections;
    public float chance_of_infection;
    public InputField no_of_agent_field;
    public InputField no_of_building_field;
    public InputField building_limit_field;
    public TMP_Text no_of_agent_text;
    public Slider no_of_agent_slider;
    public TMP_Text no_of_infected_text;
    public Slider no_of_infected_slider;
    public TMP_Text infection_rate_text;
    public Slider infection_rate_slider;
    public GameObject MainPanel;
    public GameObject SimulationPanel;
    public GameObject AboutPanel;
  

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        no_of_agent_text.text ="" + no_of_agent_slider.value;
        no_of_infected_text.text = "" + no_of_infected_slider.value;
        infection_rate_text.text = "" + infection_rate_slider.value;
    }

    public void LoadSimulation()
    {
        no_of_agents =Mathf.FloorToInt(no_of_agent_slider.value);
        no_of_initial_infections = Mathf.FloorToInt(no_of_infected_slider.value);
        chance_of_infection = Mathf.FloorToInt(infection_rate_slider.value);
        
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
    public void Play()
    {
        MainPanel.gameObject.active = false;
        AboutPanel.gameObject.active = false;
        SimulationPanel.gameObject.active = true;
    }

    public void About()
    {
        MainPanel.gameObject.active = false;
        AboutPanel.gameObject.active = true;
    }

    public void Back()
    {
        SimulationPanel.gameObject.active = false;
        AboutPanel.gameObject.active = false;
        MainPanel.gameObject.active = true;
      
    }
    public void Exit()
    {
        Application.Quit();
    }
}
