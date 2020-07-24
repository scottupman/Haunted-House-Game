using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player; // We will be looking at the characters transform instead of the actual player character
                             // This makes it easier to access the player GameObject's position

    public GameEnding gameEnding; // GameEnding is a class just like GameObject and Transform

    bool m_IsPlayerInRange;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;  // Now be careful with this, say if there's a wall in the way, we shouldn't automatically make the game end if thats the case.
        }
    }
    // Use the OnTriggerExit method
    void OnTriggerExit (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
    
    // Add an update method that checks for any walls
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            // This is where RayCast comes in which checks for colliders alont the Ray
            // The origin is the PointOfView GameObject's position
            // This is how to find the direction
            Vector3 direction = player.position - transform.position + Vector3.up; // JohnLemon’s position minus the PointOfView GameObject’s position.
                                                                                   // Vector3.up is for centering the direction on the center of JohnLemon

            // Create a new Ray object
            Ray ray = new Ray(transform.position, direction); // Gets these parameters from Ray class constructor

            RaycastHit raycastHit;  // RaycastHit is a data type that has an out parameter

            if (Physics.Raycast(ray, out raycastHit)) // returns a value to the raycastHit variable
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer(); // <-- This can happen because the CaughtPlayer method from the GameEnding class is public
                }
            }
        }
    }
}    

