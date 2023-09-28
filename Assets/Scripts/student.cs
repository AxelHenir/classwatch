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

    // Escape bar
    public Image meterBar;
    public float meterAmount = 100f;
    public float escapeTimer;

    // Escape meter
    public Canvas canvas;
    public float obedience = 100f;

    Animator _animator;

    
    // Start is called before the first frame update
    void Start() {
        _animator = gameObject.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update(){


        

        // Check button state
        if (Input.GetKey("space")){

            // Escaping?
            if (tryingToEscape){

                // Caught! - reset obedience and bool
                tryingToEscape = false;

            } 

        } 

        // Update student state
        if(!tryingToEscape){

            tryingToEscape = true;
            escapeTimer = Random.Range(2, 6);
            timeUntilEscape = escapeTimer;
            

        } else {

            // Decrement time remaining until escape
            timeUntilEscape -= Time.deltaTime;

            timeUntilEscape = Mathf.Clamp(timeUntilEscape, 0, 100);
            meterBar.fillAmount = timeUntilEscape / escapeTimer;

            // If no time left, the student escapes
            if(timeUntilEscape <= 0){
                escaped = true;
                Destroy(gameObject);
            }

        }

    }
    
}
