using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSounds : MonoBehaviour
{
    // SOunds
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;
    [SerializeField] private AudioSource audioSource5;
    [SerializeField] private AudioSource audioSource6;

    private string currentState; // Declare currentState at the class level
    private GameHandler gameHandler;
    private float countdownDelay = 1f; // Set the delay time in seconds
    private float countdownTimer;

    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        // if (gameHandler == null)
        // {
        //     Debug.LogError("GameHandler not found!");
        // }

        // // Make sure to assign audio sources through the Unity Editor
        // if (audioSource1 == null || audioSource2 == null || audioSource3 == null)
        // {
        //     Debug.LogError("One or more audio sources are not assigned!");
        // }

        // Initialize the timer
        countdownTimer = countdownDelay;


    }

private void Update()
{
    currentState = gameHandler.state;
    Debug.Log("Current State: " + currentState);

    // Stop audioSource4 and audioSource5 when spacebar is pressed
    if (currentState == "GAMEPLAY" && Input.GetKeyDown(KeyCode.Space))
    {
        audioSource4.Stop();
        audioSource5.Stop();
    }

    // Play audioSource4 or audioSource5 when spacebar is not pressed and neither is already playing
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

    if (currentState == "COUNTDOWN" && countdownTimer <= 0f)
    {
        audioSource3.Play();
        countdownTimer = countdownDelay;
    }

    countdownTimer -= Time.deltaTime;

    if (currentState == "FINISH")
    {
        // When the level is complete play the ring
        audioSource6.Play();

        // Stop the chalk sounds
        audioSource4.Stop();
        audioSource5.Stop();
    }

}


}