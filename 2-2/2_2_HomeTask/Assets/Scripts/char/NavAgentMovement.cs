using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private NavMeshAgent agent;
    public float defaultSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMesh.SamplePosition(agent.transform.position, out NavMeshHit nmHit, 1f, NavMesh.AllAreas);
        int index = IndexFromMask(nmHit.mask);
        if (index != -1)
        {
            agent.speed = defaultSpeed / NavMesh.GetAreaCost(index);
        }
        Debug.Log("hit_:" + index);
        Debug.Log("Cost_:" + NavMesh.GetAreaCost(index));
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private int IndexFromMask(int mask)
    {
        for (int i = 0; i < 32; ++i)
        {
            if ((1<<i) == mask)
            {
                return i;
            }
        }
        return -1;
    }

}
