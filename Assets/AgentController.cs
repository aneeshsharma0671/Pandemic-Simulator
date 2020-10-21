using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    public Transform[] Buildings;
    public Transform[] Houses;
    private Vector3 lastpos;
    private float t_copy;
    private float p=0;
    private int random;
    private float t;

    public GameObject GameManager;

    private void Start()
    {
        agent.SetDestination(Houses[0].position);
        lastpos = Houses[0].position;
    }

    void Update()
    {
        t = GameManager.GetComponent<TimeManager>().t;

        if(t% Mathf.RoundToInt(Random.Range(30.0f, 60.0f)) == 0)
        {
            t_copy = t;
            p = p + 1;
            random =Mathf.RoundToInt(Random.Range(0.0f , 1.0f));
            Debug.Log(random);
            Debug.Log(t_copy);
          
        }
        if (t > t_copy)
        {
            if (p % 2 == 0)
            {
                agent.SetDestination(Buildings[random].position);
            }
            else
            {
                agent.SetDestination(Houses[random].position);
            }
        }






        if (Input.GetMouseButtonDown(0))
        {
         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
              // agent.SetDestination(hit.point);
            }
        }
    }
}
