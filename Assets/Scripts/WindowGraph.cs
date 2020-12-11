using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Reflection.Emit;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite dotsprite;
    private RectTransform graphContainer;
    private RectTransform labeltemplatex;
    private RectTransform labeltemplatey;
    private List<GameObject> gameobjectList;
    public GameObject GameManager;
    public  List<int> valueList = new List<int> {};
    public bool infected;
    public bool cured;
    public int width_of_graph;
    private void Awake()
    {
        graphContainer = transform.Find("Graph_container").GetComponent<RectTransform>();
        labeltemplatex = graphContainer.Find("LabelTemplatex").GetComponent<RectTransform>();
        labeltemplatey = graphContainer.Find("LabelTemplatey").GetComponent<RectTransform>();
        gameobjectList = new List<GameObject>();
      

    }
    private void FixedUpdate()
    {
        if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
        {
            width_of_graph = (Mathf.RoundToInt(GameManager.GetComponent<TimeManager>().t) / 20)+1;
        }
        if (infected)
        {
            if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
            {
                valueList.Add(GameManager.GetComponent<InfectionManager>().no_of_infected);
              //  ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            }
        }
        else if (cured)
        {
            if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
            {
                valueList.Add(GameManager.GetComponent<InfectionManager>().no_of_cured);
              //  ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            }
        }
        else
        {
            if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
            {
                valueList.Add(GameManager.GetComponent<InfectionManager>().no_of_healthy);
              //  ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            }

        }
       
    }
    private GameObject CreateDot(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("dot", typeof(Image));

        if(infected)
        {
            gameObject.GetComponent<Image>().color = new Color(255,0,0,0);
        }
        else if (cured)
        {
            gameObject.GetComponent<Image>().color = new Color(1,1,1,0);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
     
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = dotsprite;
        RectTransform recttransform = gameObject.GetComponent<RectTransform>();
        recttransform.anchoredPosition = anchoredPosition;
        recttransform.sizeDelta = new Vector2(11, 11);
        recttransform.anchorMin = new Vector2(0, 0);
        recttransform.anchorMax = new Vector2(0, 0);
        return gameObject;

    }

    private void ShowGraph(List<int> valuelist,int maxvisibleValueAmount = -1, Func<int,string> getAxisLabelX=null, Func<float, string> getAxisLabelY=null)
    {
        if(getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        if(maxvisibleValueAmount <= 0)
        {
            maxvisibleValueAmount = valueList.Count;
        }

        foreach(GameObject gameoject in gameobjectList)
        {
            Destroy(gameoject);
        }
        gameobjectList.Clear();
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;

        float ymaximun = valueList[0];
        float yminimum = valueList[0];

        for (int i =Mathf.Max((valueList.Count - maxvisibleValueAmount),0); i < valuelist.Count; i++)
        {
            int value = valueList[i];
            //    if (value > ymaximun)
            //    {
            //        ymaximun = value;
            //    }
            //    if(value < yminimum)
            //    {
            //        yminimum = value;
            //    }

            ymaximun = 135;
            yminimum = 0;





        }
        float ydifference = ymaximun - yminimum;
        if(ydifference <= 0)
        {
            ydifference = 5f;
        }
        ymaximun = ymaximun + (ydifference) * 0.2f;
        yminimum = yminimum - (ydifference) * 0.2f;

        float xsize = graphWidth / (maxvisibleValueAmount+1);

        int xIndex = 0;
         GameObject lastdotgameobject = null;
        for (int i = Mathf.Max((valueList.Count - maxvisibleValueAmount), 0); i < valuelist.Count; i++)
        {
            float xposition =xsize+ xIndex * xsize;
            float yposition = ((valuelist[i]-yminimum) / (ymaximun-yminimum)) * graphHeight;
          //  GameObject barGameObject =  CreateBar(new Vector2(xposition,yposition), xsize);
          //  gameobjectList.Add(barGameObject);

            
          GameObject dotgameobject =  CreateDot(new Vector2(xposition, yposition));
            gameobjectList.Add(dotgameobject);
            if(lastdotgameobject != null) 
            {
             GameObject dotconnectiongameobject = CreateDotConnection(lastdotgameobject.GetComponent<RectTransform>().anchoredPosition, dotgameobject.GetComponent<RectTransform>().anchoredPosition);
                gameobjectList.Add(dotconnectiongameobject);
            }
            lastdotgameobject = dotgameobject;
            


            RectTransform labelX = Instantiate(labeltemplatex);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xposition, -20f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);
            gameobjectList.Add(labelX.gameObject);

            xIndex++;
        }
        int seperatorCount = 10;
        for (int i = 0; i <= seperatorCount; i++)
        {
            RectTransform labely = Instantiate(labeltemplatex);
            labely.SetParent(graphContainer);
            labely.gameObject.SetActive(true);
            float normalizedvalue = i * 1.0f / seperatorCount;
            labely.anchoredPosition = new Vector2(-7f,normalizedvalue*graphHeight);
            labely.GetComponent<Text>().text =getAxisLabelY(yminimum+ (normalizedvalue * (ymaximun-yminimum)));
            gameobjectList.Add(labely.gameObject);
        } 
    }
    private GameObject CreateDotConnection(Vector2 dotPositionA,Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
    
        


        if(infected)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 0, 0, 0.5f);
        } 
        else if (cured)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0, 255, 0, 0.5f);
        }
    


        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 5f);
        rectTransform.anchoredPosition = dotPositionA + dir*distance*0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0,UtilsClass.GetAngleFromVectorFloat(dir));

        return gameObject;
    }

    private GameObject CreateBar(Vector2 graphPosition , float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
      
       if (infected)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 0, 0, 255);
        }
        else if (cured)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 0, 255);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 255, 1, 255);
        } 
       
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform recttransform = gameObject.GetComponent<RectTransform>();
        recttransform.anchoredPosition = new Vector2(graphPosition.x,0f);
        recttransform.sizeDelta = new Vector2(barWidth , graphPosition.y);
        recttransform.anchorMin = new Vector2(0, 0);
        recttransform.anchorMax = new Vector2(0, 0);
        recttransform.pivot = new Vector2(.5f,0f);
        return gameObject;
    }

public void DrawGraph()
    {
        if (infected)
        {
          
               
                ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            
        }
        else if (cured)
        {
           
              
                ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
           
        }
        else
        {
           
               
                ShowGraph(valueList, width_of_graph, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            

        }
    }
}
