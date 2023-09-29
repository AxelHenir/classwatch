using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class newstudent : MonoBehaviour
{
    // Timer data and state tracking
    public float timeUntilPacking;
    public float timeUntilEscaping;
    public float timeUntilEscaped;

    // State - IDLE PACKING ESCAPING
    public string state = "IDLE";

    // Meter bar
    public Image meterBar;
    public float meterTotal;
    public Canvas meterCanvas;

    //DEBUG
    public TMP_Text debugText;

    Animator _animator;


    void Start() {
        _animator = gameObject.GetComponent<Animator>();

        timeUntilPacking = Random.Range(2.0f, 7.0f);

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
                meterTotal = Random.Range(2.0f, 6.0f);
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
                
                meterCanvas.enabled = false;
                state = "ESCAPING";
            }
        }
    }

    // Handles escaping behavior
    void escaping(){

        Destroy(gameObject);

    }

    // Handles meter operations
    void setMeter(){
        timeUntilEscaping = Mathf.Clamp(timeUntilEscaping, 0f, meterTotal);
        meterBar.fillAmount = timeUntilEscaping / meterTotal;
    }


}
