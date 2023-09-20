using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public string state = "SCENE_INTRO";
    


    // Countdown
    public int countdownLength = 3;
    float timeRemaining;

    // Gameplay
    public int roundLength = 30; // 30 seconds
    public bool paused = false;

    // Score Screen
    public int score = 0;

   

    // SCENE_INTRO , COUNTDOWN , GAMEPLAY , TIMER_EXPIRE , SCENE_OUTRO , SCORE_SCREEN

    // PAUSED - [SCENE INTRO], CAMERA, SETTLE IN-
    // PAUSED - [COUNTDOWN] SEQUENCE, 3.. 2.. 1.. GO!!
    // UNPAUSED - [GAMEPLAY], TIMER COUNTS DOWN
    // UNPAUSED - TIMER IS ABOUT TO [EXPIRE]... 3.. 2.. 1.. STOP!!
    // PAUSED - [SCENE OUTRO], CAMERA, SETTLE IN-
    // PAUSED - [SCORE SCREEN], SCORE, DETAILS, Time permitting: Upgrades?

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){

        // Call the state's update method
        switch (state)
        {
        case "SCENE_INTRO":
            intro();
            break;
        case "COUNTDOWN":
            countdown();
            break;
        case "GAMEPLAY":
            gameplay();
            break;
        case "FINISH":
            finish();
            break;
        case "SCENE_OUTRO":
            outro();
            break;
        case "SCORE_SCREEN":
            scoreScreen();
            break;
        }
    }  

    void intro(){
        print ("Intro - Press Space to begin");
        if (Input.GetKey("space")){
            state = "COUNTDOWN";
            timeRemaining = countdownLength;
        } 
    }

    void countdown(){
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            print (timeRemaining);

        } else {
            state = "GAMEPLAY";
            timeRemaining = roundLength;

        }
    }

    void gameplay(){

        // Check if paused
        if (paused){

        } else {
            if (timeRemaining > 0){

                // Remove time
                timeRemaining -= Time.deltaTime;
                print (timeRemaining);

                // Update the prof
                if (Input.GetKey("space")){

                    print("THE PROF IS WATCHING");

                } else {
                    
                    print("THE PROF IS WRITING");

                }

                
            } else {
                state = "FINISH";
                timeRemaining = countdownLength;
            }
        }
    }

    void finish(){
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else {
            state = "OUTRO";
        }
    }

    void outro(){
        print ("Outro - Press Space to continue");
        if (Input.GetKey("space")){
            state = "SCORE_SCREEN";
        } 
    }

    void scoreScreen(){
        print ("Score Screen - Press Space to Restart");
        if (Input.GetKey("space")){
            state = "INTRO";
        } 
    }
}


