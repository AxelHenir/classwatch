using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSounds : MonoBehaviour
{
    // Sounds
    [SerializeField] private AudioSource audioSource1; // disgruntled teacher sound
    [SerializeField] private AudioSource audioSource2; // disgruntled teacher sound
    [SerializeField] private AudioSource audioSource3; // countdown beeping sound
    [SerializeField] private AudioSource audioSource4; // chalk writing sound
    [SerializeField] private AudioSource audioSource5; // chalk writing sound
    [SerializeField] private AudioSource audioSource6; // bell ring sound

    // String to track state
    private string currentState; 
    private GameHandler gameHandler;
    private float countdownTimer;

    private int currentTimeRemaining = 0;

    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();

    }

private void Update()
{

   // Trigger beeps during countdown
    if (currentState == "COUNTDOWN" && currentTimeRemaining != (int)gameHandler.timeRemaining) {
        audioSource3.Play();
        currentTimeRemaining = (int)gameHandler.timeRemaining;
    }

    currentState = gameHandler.state;
    Debug.Log("Current State: " + currentState);

    // Stop chalk sounds when spacebar is pressed
    if (currentState == "GAMEPLAY" && Input.GetKeyDown(KeyCode.Space))
    {
        audioSource4.Stop();
        audioSource5.Stop();
    }

    // Play chalk writing sounds when spacebar is not pressed and neither is already playing
    if (currentState == "GAMEPLAY" && !Input.GetKey(KeyCode.Space) && !audioSource4.isPlaying && !audioSource5.isPlaying)
    {
        int randomSourceIndex = Random.Range(1, 3);
        if (randomSourceIndex == 1)
        {
            audioSource4.Play();
        }
        else
        {
            audioSource5.Play();
        }
    }


    // If the user presses space randomly trigger one of the teacher disgruntled sounds
    if (currentState == "GAMEPLAY" && Input.GetKeyDown(KeyCode.Space))
    {
        int randomSourceIndex = Random.Range(1, 3);
        if (randomSourceIndex == 1)
        {
            audioSource1.Play();
        }
        else
        {
            audioSource2.Play();
        }
    }


    countdownTimer -= Time.deltaTime;

    if (currentState == "FINISH")
    {
        if(audioSource6.isPlaying == false) {
        // When the level is complete play the ring
        audioSource6.Play();
        // Stop the chalk sounds
        audioSource4.Stop();
        audioSource5.Stop();

       }
  
     }

   }

}