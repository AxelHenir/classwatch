using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Student : MonoBehaviour
{
    // escaping tracker
    public bool tryingToEscape = false;
    public float timeUntilEscape;
    float randomNumber;
    public bool escaped = false;

    // Obedience meter
    public float obedience = 100f;
    public Image meterBar;
    public float meterAmount = 100f;

    // Update is called once per frame
    void Update(){

        // Check button state
        if (Input.GetKey("space")){

            // Escaping?
            if (tryingToEscape){

                // Caught! - reset obedience and bool
                obedience = 100;
                tryingToEscape = false;

            } else {

                // Not escaping - reduce obedience meter fast
                reduceMeter(0.05f);

            }

        } else {         
            reduceMeter(0.0005f);
        }

        // Update student state
        if(!tryingToEscape){

            // Not escaping, induce a chance to escape
            randomNumber = Random.Range(0,85);

            // Students are more likely to escape if their obedience is low.
            if(obedience + randomNumber < 100f){

                // Set a timer between 5 and 10 seconds unti they escape.
                timeUntilEscape = Random.Range(5,10);

                // Initiate running away animation here?** =======================================
            }

        } else {

            // Decrement time remaining until escape
            timeUntilEscape -= Time.deltaTime;

            // If no time left, the student escapes
            if(timeUntilEscape <= 0){
                escaped = true;
            }

        }

    }

    public void reduceMeter(float amount){
        meterAmount -= amount;
        meterAmount = Mathf.Clamp(meterAmount, 0, 100);
        meterBar.fillAmount = meterAmount / 100f;
    }

    public void increaseMeter(float amount){
        meterAmount += amount;
        meterAmount = Mathf.Clamp(meterAmount, 0, 100);
        meterBar.fillAmount = meterAmount / 100f;
    }
}
