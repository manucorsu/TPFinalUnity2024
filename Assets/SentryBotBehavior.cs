using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryBotBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    public bool isPatrolling;
    public int currentWaypoint = 0;
    public Transform targetTR;
    public NavMeshAgent agent;
    public bool isArrived;
    // Start is called before the first frame update
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (targetTR)
        {
            agent.destination = targetTR.position;
        }

        isArrived = agent.remainingDistance < agent.stoppingDistance;

        if (isPatrolling)
        {
            if (isArrived)
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
