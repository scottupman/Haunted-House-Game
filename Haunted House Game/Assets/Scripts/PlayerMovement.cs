using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Initiate variables for storing the components
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity; // Quaternions are used for storing rotations
    AudioSource m_AudioSource; // This is audioSource variable that we will use in our code
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the components at the start
        m_Animator = GetComponent<Animator>();  // Gets the component of the animator component

        m_Rigidbody = GetComponent<Rigidbody>();  // Gets the component of the rigidbody component

        m_AudioSource = GetComponent<AudioSource>();  // Gets the component of the audio source variable
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Part of the Vector3 Class
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        // Determine if the input is horizontal
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);

        // Determine if the input is vertical
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        // isWalking variable is either true or false from the previous variables
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // Sets the boolean value to the animator component (components are pretty much like classes)
        m_Animator.SetBool("IsWalking", isWalking);

        // Create an if statement that will play footsteps when the animation is played
        if (isWalking)
        {
            // Plays the audio source when John is walking and plays when the audio source isn't initially playing so it doesn't keep getting overwritten
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            // Stops the audio source when John is not walking
            m_AudioSource.Stop();
        }

        // Used the rotateTowards method
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        // Used the lookRotation method using the desiredForward variable as the parameter
        m_Rotation = Quaternion.LookRotation(desiredForward);        
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
