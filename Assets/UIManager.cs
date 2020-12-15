using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator panelAnimator;
    public GameObject DetailsPanel;
    public void TriggerPanel()
    {
            panelAnimator.SetTrigger("Shrink-Expand"); 
    }
    public void DetailsPanle()
    {
        if(DetailsPanel.activeSelf)
        {
            DetailsPanel.SetActive(false);
        }
        else
        {
            DetailsPanel.SetActive(true);
        }
    }
    public void DetailsPanels()
    {
      
            DetailsPanel.SetActive(true);
        
    }
    public void ExitSimulation()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
