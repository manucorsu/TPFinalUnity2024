using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryBotBehavior : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    public bool Patrolling { get; private set; }
    private int currentWaypoint = 0;
    private Transform targetTR;
    [SerializeField] private NavMeshAgent agent;
    private bool arrived;
    [SerializeField] private Color foundYouTxtColor;

    private void Awake()
    {
        if(!agent) agent = GetComponent<NavMeshAgent>();
        Patrolling = true;
    }

    public void StartAggro(Transform target)
    {
        if (Patrolling)
        {
            targetTR = target;
            Patrolling = false;
            UIManager.Instance.StartShowingTimedHint("¡Te vio el robot!", foundYouTxtColor, 5);
        }
    }

    void Update()
    {
        if (targetTR)
        {
            agent.destination = targetTR.position;
        }

        arrived = agent.remainingDistance < agent.stoppingDistance;

        if (Patrolling)
        {
            if (arrived)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            agent.destination = waypoints[currentWaypoint].position;
        }
    }
}
