using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Student : MonoBehaviour
{
    // State tracker
    bool tryingToEscape = false;
    
    float randomNumber;

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
            reduceMeter(0.005f);
        }

        // Update student state
        if(!tryingToEscape){

            // Not escaping, induce a chance to escape

        } else {

            // Proceed in escaping...

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
