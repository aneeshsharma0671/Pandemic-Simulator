using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Reflection.Emit;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circlesprite;
    private RectTransform graphContainer;
    private RectTransform labeltemplatex;
    private RectTransform labeltemplatey;
    private List<GameObject> gameobjectList;
    public GameObject GameManager;
    public  List<int> valueList = new List<int> {0};
    public bool infected;
    private void Awake()
    {
        graphContainer = transform.Find("Graph_container").GetComponent<RectTransform>();
        labeltemplatex = graphContainer.Find("LabelTemplatex").GetComponent<RectTransform>();
        labeltemplatey = graphContainer.Find("LabelTemplatey").GetComponent<RectTransform>();
        gameobjectList = new List<GameObject>();
      

    }
    private void FixedUpdate()
    {
        if (infected)
        {
            if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
            {
                valueList.Add(GameManager.GetComponent<InfectionManager>().no_of_infected);
                ShowGraph(valueList, 10, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            }
        }
        else
        {
            if (GameManager.GetComponent<TimeManager>().t % 20 == 0)
            {
                valueList.Add(GameManager.GetComponent<InfectionManager>().no_of_healthy);
                ShowGraph(valueList, 10, (int _i) => "Time" + (_i + 1), (float _f) => "I" + Mathf.RoundToInt(_f));
            }
        }
       
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circlesprite;
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
            if (value > ymaximun)
            {
                ymaximun = value;
            }
            if(value < yminimum)
            {
                yminimum = value;
            }
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
        GameObject lastcirclegameobject = null;
        for (int i = Mathf.Max((valueList.Count - maxvisibleValueAmount), 0); i < valuelist.Count; i++)
        {
            float xposition = xIndex * xsize;
            float yposition = ((valuelist[i]-yminimum) / (ymaximun-yminimum)) * graphHeight;
          GameObject circlegameobject =  CreateCircle(new Vector2(xposition, yposition));
            gameobjectList.Add(circlegameobject);
            if(lastcirclegameobject != null) 
            {
             GameObject dotconnectiongameobject = CreateDotConnection(lastcirclegameobject.GetComponent<RectTransform>().anchoredPosition, circlegameobject.GetComponent<RectTransform>().anchoredPosition);
                gameobjectList.Add(dotconnectiongameobject);
            }
            lastcirclegameobject = circlegameobject;

            RectTransform labelX = Instantiate(labeltemplatex);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xposition, -7f);
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
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
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
}
