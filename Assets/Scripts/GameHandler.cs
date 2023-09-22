using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Game state
    // SCENE_INTRO , COUNTDOWN , GAMEPLAY , TIMER_EXPIRE , SCENE_OUTRO , SCORE_SCREEN
    public string state = "SCENE_INTRO";
    
    // Debug UI
    public bool  debugUI;
    public TMP_Text lessonMultiplierTEXT;
    public TMP_Text sceneStateDEBUG;
    public TMP_Text timeRemainingTEXT;
    public TMP_Text announcementTEXT;

    // Lesson (level length, difficulty)
    public float lessonLengthSeconds = 25;
    public float lessonRemaining;
    public Image lessonBar;
    public float meterAmount = 100f;

    public float baseLessonRate = 1.0f;
    public float baseLessonPerTick = 0.005f;
    public float lessonRateMultiplier;
    public float lessonMultiplierGrowth = 0.005f;

    // Countdown
    public int countdownLength = 3;
    float timeRemaining;

    // Gameplay
    public int roundLengthSeconds = 30; 
    public bool paused = false;

    // Scoring
    public int score = 0;
    public int escapedStudents = 0;

// ============================================================

    void Update(){

        // Debug UI
        if (debugUI){
            sceneStateDEBUG.text = "SCENE STATE: " + state;
        } else {
            sceneStateDEBUG.text = "";
        }

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
        announcementTEXT.text = ("Intro - Press Space to begin");
        if (Input.GetKey("space")){
            state = "COUNTDOWN";
            timeRemaining = countdownLength;
        } 
    }

    void countdown(){
        announcementTEXT.text = ("");
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeRemainingTEXT.text = timeRemaining.ToString("0");

        } else {
            state = "GAMEPLAY";
            timeRemaining = roundLengthSeconds;
            lessonRemaining = lessonLengthSeconds; 
            lessonRateMultiplier = baseLessonRate;

            //spawn students?

        }
    }

    void gameplay(){

        lessonMultiplierTEXT.text = "LESSON MULTIPLIER: " + lessonRateMultiplier.ToString("0.00") + "x ";

        // Check if paused
        if (paused){

            announcementTEXT.text = ("Paused");

        } else {

            announcementTEXT.text = ("");
            if (timeRemaining >= 0){

                // Remove time
                timeRemaining -= Time.deltaTime;
                timeRemainingTEXT.text = timeRemaining.ToString("0.00");

                // Update the prof
                if (Input.GetKey("space")){

                    lessonRateMultiplier = baseLessonRate;
                    print("THE PROF IS WATCHING");

                } else {
                    
                    lessonRemaining -= baseLessonPerTick * lessonRateMultiplier;
                    lessonRateMultiplier += lessonMultiplierGrowth;

                    meterAmount = Mathf.Clamp(lessonRemaining, 0, lessonLengthSeconds);
                    lessonBar.fillAmount = meterAmount / lessonLengthSeconds;

                }

                
            } else {
                state = "FINISH";
                timeRemaining = countdownLength;
            }
        }
    }

    void finish(){

        announcementTEXT.text = ("Finished!");
        if (timeRemaining >= 0){
            timeRemaining -= Time.deltaTime;
            timeRemainingTEXT.text = timeRemaining.ToString("0");

        } else {
            state = "SCENE_OUTRO";
        }
    }

    void outro(){
        announcementTEXT.text = ("Outro - Press Space to continue");
        if (Input.GetKey("space")){
            state = "SCORE_SCREEN";
        } 
    }

    void scoreScreen(){
        announcementTEXT.text = ("Score Screen - Press enter to Restart");
        if (Input.GetKey("return")){
            state = "SCENE_INTRO";
        }
    }

}


