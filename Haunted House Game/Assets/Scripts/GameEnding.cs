using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Now we have access to the classes and methods that you need to reload the scene

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;  // Fade duration of the image
    public GameObject player; 
    bool m_IsPlayerAtExit;
    float m_Timer;
    public CanvasGroup exitBackgroundImageCanvasGroup;

    bool m_HasAudioPlayed;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    public float displayImageDuration = 1f; // Since we made it public we can edit these values

    public CanvasGroup caughtBackgroundImageCanvasGroup;
    bool m_IsPlayerCaught;

    void OnTriggerEnter (Collider other) // Passes the collider class to the parameter
    {
        if (other.gameObject == player)  // Determines if the collider property is equal to the player controlled gameObject
        {
            m_IsPlayerAtExit = true;  // Bool value that says the player is at the exit
        }
    }

    public void CaughtPlayer()  // public method
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {

        if (m_IsPlayerAtExit) // Ends the level when the player is at exit
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);  // Calls the endLevel method
        }
        else if (m_IsPlayerCaught) // Ends the level when the player is caught
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }
    
    // Edit the EndLevel method for managing whether JohnLemon is caught or won
    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true; // Since we only want the audio to play once, we change the value to true so that it doesn't loop
        }
        
        m_Timer += Time.deltaTime; // EndLevel starts to count up the m_Timer variable
        imageCanvasGroup.alpha = m_Timer / fadeDuration; // Allows the image to fade in when JohnLemon is caught

        // now we need to quit the game
        if (m_Timer > fadeDuration + displayImageDuration) // Allows the image to fade and be seen for about one second each
        {
            if (doRestart) // Easiest way to restart a scene is to load it again
            {
                SceneManager.LoadScene(0); // Calls static method LoadScene <-- Index 0 is in the parameter which is the mainScene that we are using
            }
            else
            {
                Application.Quit();
            }
        }       
    }
}
