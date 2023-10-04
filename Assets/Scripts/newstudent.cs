using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class newStudent : MonoBehaviour
{
    // Timer data and state tracking
    public float timeUntilPacking;
    public float timeUntilEscaping;
    public float timeUntilEscaped;

    // State - IDLE PACKING ESCAPING
    public string state = "IDLE";

    // Movement
    public float deskPosX;
    public float deskPosZ;
    public Transform studentPos;
    public float limitFront = 0f;
    public float limitDoor = 0f;

    public float moveSpeed = 1f;

    // Meter bar
    public Image meterBar;
    public float meterTotal;
    public Canvas meterCanvas;

    // Track audio
  private float resetFillAmount = 1.0f; // Adjust this value based on your meter reset condition
    private bool audioPlayed = false; // Flag to track whether audio has been played for this instance
    private static bool globalAudioPlayed = false; // Static flag to track whether audio has been played globally

    //DEBUG
    public TMP_Text debugText;

    Animator _animator;

    public AudioClip[] audioClips; // Array of audio clips

    private AudioSource audioSource; // Reference to the AudioSource component


    void Start() {

        _animator = gameObject.GetComponentInChildren<Animator>();

        timeUntilPacking = Random.Range(2.0f, 12.0f);

        meterCanvas.enabled = false;

    // Initialize the audioSource variable to point to the AudioSource component on this GameObject
    audioSource = GetComponent<AudioSource>();
        // audioSourceStudent = gameObject.GetComponent<AudioSource>();
    }

    void Update(){

        debugText.text = state;
        Debug.Log("Meter total is: " + meterBar.fillAmount);

        // Call the state's update method
        switch (state){
        case "IDLE":
            idle();
            break;
        case "PACKING":
            packing();
            break;
        case "ESCAPING":
            escaping();
            break;
        }


    }

    // Handles idle behavior
    void idle(){

        // If space is being held down, freeze
        if (Input.GetKey("space")){
       
        } else {
            // Reduce hidden timer until meter appears (3 - 10 sec.)
            timeUntilPacking -= Time.deltaTime;


            if(timeUntilPacking <= 0f){
                // Initiate packing sequence when timer expires
                meterCanvas.enabled = true;
                meterTotal = Random.Range(2.0f, 4.0f);
                timeUntilEscaping = meterTotal;
                state = "PACKING";              

            }
        }
    }

    // Handles packing behavior
    void packing(){
        // If space is being held down, refill the meter to full
        if (Input.GetKey("space")){
            timeUntilEscaping = meterTotal;
            setMeter();

        } else {
            // If space is not held, reduce timer until escape 
            timeUntilEscaping -= Time.deltaTime;
            setMeter();

  
                                            
            if(timeUntilEscaping <= 0f){
                // Initiate escaping sequence when meter is gone
                
                deskPosX = studentPos.position.x; 
                deskPosZ = studentPos.position.z;
                meterCanvas.enabled = false;
                state = "ESCAPING";
            }
        }
    }

    // Handles escaping behavior
    void escaping(){

        // Escaping is broken into 2 parts, walking forward then walking to the door

        // If prof is watching
        if (Input.GetKey("space")){

            // If student hasnt reached front of class, move left
            if(studentPos.position.x < limitFront){
                
                studentPos.position = new Vector3(studentPos.position.x - (moveSpeed * 0.75f * Time.deltaTime), studentPos.position.y, studentPos.position.z );

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", false); //walking Straight

                // If they are sent back to their seat, set them back to idle
                if(studentPos.position.x <= deskPosX){
                    

                    // Disable walking animations
                    _animator.SetBool("isWalking", false);
                    _animator.SetBool("isWalkingStraight", false);

                    timeUntilPacking = Random.Range(1.0f, 6.0f);
                    meterCanvas.enabled = false;
                    state = "IDLE";
                }
                
            } 
            // If student has reached front of class but hasnt reached door, move up
            else if(studentPos.position.z > limitDoor){

                studentPos.position = new Vector3(studentPos.position.x, studentPos.position.y, studentPos.position.z + (moveSpeed * 0.75f * Time.deltaTime));

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", true); //walking Straight


                if(studentPos.position.z > deskPosZ){

                    studentPos.position = new Vector3(studentPos.position.x - (moveSpeed  * 0.75f * Time.deltaTime), studentPos.position.y, studentPos.position.z );

                }

            }

        // If Prof is not watching
        } else {

            // If student hasnt reached front of class, move right
            if(studentPos.position.x < limitFront){
                
                studentPos.position = new Vector3(studentPos.position.x + (moveSpeed * Time.deltaTime), studentPos.position.y, studentPos.position.z );

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", false); //walking Straight

            } 
            // If student has reached front of class but hasnt reached door, move down
            else if(studentPos.position.z > limitDoor){

                studentPos.position = new Vector3(studentPos.position.x, studentPos.position.y, studentPos.position.z - (moveSpeed * Time.deltaTime));

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", true); //walking Straight

            }
            // If student has reached front of class and out the door, the escape!
            else{

              Destroy(gameObject);

            }
        }
        
    }

    // Handles meter operations
    void setMeter(){
        timeUntilEscaping = Mathf.Clamp(timeUntilEscaping, 0f, meterTotal);
        meterBar.fillAmount = timeUntilEscaping / meterTotal;
            audioTriggers();

    }

// Function to manage auto triggers
void audioTriggers() {
    
       // Check if the current fill amount is more than or equal to the reset fill amount
        if (meterBar.fillAmount >= resetFillAmount)
        {
            // Reset the audio playing trackers if it is so that the sound can trigger again.
            audioPlayed = false;
            globalAudioPlayed = false;

        }

        // Calculate a random fill amount between 0.1 and 0.6
        float randomFillAmount = Random.Range(0.1f, 0.6f);

        // Check if the current fill amount is less than or equal to the random fill amount
        if (meterBar.fillAmount <= randomFillAmount)
        {
            // Check if the audio has not been played yet for this instance and globally
            if (!audioPlayed && !globalAudioPlayed)
            {
                // Get a random index from the audioClips array
            int randomIndex = Random.Range(0, audioClips.Length);

           // Play the randomly selected audio clip using the AudioSource component
            audioSource.PlayOneShot(audioClips[randomIndex]);
        


                // Set the flags to true so the audio doesn't play again for this instance and globally
                audioPlayed = true;
                globalAudioPlayed = true;
            }
        }
    }

    void handleAnimation(){

    }


}
