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

    //DEBUG
    public TMP_Text debugText;

    Animator _animator;


    void Start() {

        _animator = gameObject.GetComponentInChildren<Animator>();

        timeUntilPacking = Random.Range(2.0f, 3.0f);

        meterCanvas.enabled = false;
    }

    void Update(){

        debugText.text = state;

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
                meterTotal = Random.Range(2.0f, 3.0f);
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
                
                studentPos.position = new Vector3(studentPos.position.x - (moveSpeed * Time.deltaTime), studentPos.position.y, studentPos.position.z );

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", false); //walking Straight

                if(studentPos.position.x <= deskPosX){
                    // If they are sent back to their seat, set them back to packing
                meterCanvas.enabled = true;
                meterTotal = Random.Range(2.0f, 3.0f);
                timeUntilEscaping = meterTotal;
                state = "PACKING";
                }
                
            } 
            // If student has front of class but hasnt reached door, move up
            else if(studentPos.position.z > limitDoor){

                studentPos.position = new Vector3(studentPos.position.x, studentPos.position.y, studentPos.position.z + (moveSpeed * Time.deltaTime));

                _animator.SetBool("isWalking", true); // walking on the side
                _animator.SetBool("isWalkingStraight", true); //walking Straight

                if(studentPos.position.z > deskPosZ){
                    studentPos.position = new Vector3(studentPos.position.x - (moveSpeed * Time.deltaTime), studentPos.position.y, studentPos.position.z );

                    // Disable walking animations
                    _animator.SetBool("isWalking", false);
                    _animator.SetBool("isWalkingStraight", false);
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
    }

    void handleAnimation(){

    }


}
