using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent; // Will allow us to assign the Nav Mesh Agent reference in inspector window
    public Transform[] waypoints; // This will be the array for the waypoints

    int m_CurreentWaypointIndex; // This will store the index value
    
    // Start is called before the first frame update
    void Start()
    {
        // Sets the initial destination of the nav mesh agent
        navMeshAgent.SetDestination(waypoints[0].position); 
    }

    // Update is called once per frame
    void Update()
    {
        // Determines if the remaining distance is less than the distance from the wall (stopping distance)
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) 
        {
            // Increments the index value to go to the next waypoint
            m_CurreentWaypointIndex = (m_CurreentWaypointIndex + 1) % waypoints.Length; 

            // Sets the nav mesh agent to go to the position at the new index value
            navMeshAgent.SetDestination(waypoints[m_CurreentWaypointIndex].position); 
        }
    }
}
