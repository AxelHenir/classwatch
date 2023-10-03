using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour{
    // Game state
    // MAINMENU , COUNTDOWN , GAMEPLAY , FINISH , SCORE_SCREEN
    public string state = "MAINMENU";
    
    // Debug UI
    public bool  debugUI;
    public TMP_Text lessonMultiplierTEXT;
    public TMP_Text sceneStateDEBUG;
    public TMP_Text timeRemainingTEXT;
    public TMP_Text announcementTEXT;
    public TMP_Text scoreTEXT;

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
    public Image bookshelfTime;
    public Image clockTime;

    // Gameplay

    public Canvas gameplayUICanvas;
    public int roundLengthSeconds = 30; 
    public bool paused = false;

    public GameObject studentPrefabFemale;
    public GameObject studentPrefabMale;
    public List<GameObject> students = new List<GameObject>();

    // Scoring
    private int score = 0;
    private int escapedStudents;
    public int lostPointsPerStudent;
    private int pointsPerLessonPercent = 100;

    private string scoreBreakdownText;
    public int sessionHighScore;
    public Animator cameraAnimator;

    // Animation
    Animator prof_animator;

    public GameObject profImage;


// ============================================================

    // Start is called before the first frame update
    public void Start(){
        prof_animator = profImage.GetComponent<Animator>();
        gameplayUICanvas.enabled = false;
    }

    public void Update(){

        // Debug UI
        if (debugUI){
            sceneStateDEBUG.text = "SCENE STATE: " + state;
        } else {
            sceneStateDEBUG.text = "";
        }

        // Call the state's update method
        switch (state){
            case "MAIN_MENU":
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
            case "SCORE_SCREEN":
                scoreScreen();
                break;
        }
    }  

    public void restartGame(){

        state = "COUNTDOWN";
        timeRemaining = countdownLength;
        gameplayUICanvas.enabled = true;

    }

    void countdown(){

        announcementTEXT.text = "";

        // While there's time in the countdown, decrement time
        if (timeRemaining > 0){

            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 2.5 && timeRemaining > 0){
                timeRemainingTEXT.text = (timeRemaining + 1).ToString("0"); // +1 so it counts down, "3 2 1"
            } else {
                timeRemainingTEXT.text = "";
            }
            

        // When time runs out, setup gameplay!
        } else {

            timeRemainingTEXT.text = "";

            // Setup for gameplay
            state = "GAMEPLAY";
            timeRemaining = roundLengthSeconds;
            lessonRemaining = lessonLengthSeconds; 
            lessonRateMultiplier = baseLessonRate;
            

            // Spawn a grid of students
            for (int i = 3; i > 0; i--){
                for(int j = 3; j > 0; j--){
                    
                    Vector3 spawnSpot = new Vector3(4*i-16,1,4*j-5); // don't touch the magic numbers ;)

                    // 50/50 to spawn M or F student
                    var random = Random.Range(-10,10);
                    if(random > 0){

                        GameObject newStudent = Instantiate(studentPrefabFemale, spawnSpot, Quaternion.identity);
                        students.Add(newStudent);

                    } else {

                        GameObject newStudent = Instantiate(studentPrefabMale, spawnSpot, Quaternion.identity);
                        students.Add(newStudent);

                    }
                }
            }
        }
    }

    void gameplay(){

        lessonMultiplierTEXT.text = "LESSON MULTIPLIER: " + lessonRateMultiplier.ToString("0.00") + "x ";
        
        announcementTEXT.text = "";

        // While there is time in the round,
        if (timeRemaining >= 0){

            // Remove time
            timeRemaining -= Time.deltaTime;

            // Update timer visual
            clockTime.fillAmount += 1.0f / roundLengthSeconds * Time.deltaTime;

            // Check space being held down
            if (Input.GetKey("space")){

                // Turn prof
                prof_animator.SetBool("isTurning", true);

                // Reset lesson multiplier :(
                lessonRateMultiplier = baseLessonRate;

            } else {

                prof_animator.SetBool("isTurning", false);
                    
                lessonRemaining -= baseLessonPerTick * lessonRateMultiplier;
                lessonRateMultiplier += lessonMultiplierGrowth;

                meterAmount = Mathf.Clamp(lessonRemaining, 0, lessonLengthSeconds);
                bookshelfTime.fillAmount = meterAmount / lessonLengthSeconds;
            }

        // If there is no more time in the round  
        } 
        else {

            // Set next state
            state = "FINISH";
            timeRemaining = countdownLength - 1;

            // calculate the score
            calculateScore();

            // Remove the evidence
            foreach (var prefab in students){
                Destroy(prefab);
            }
            students.Clear();

        }
    }

    void calculateScore(){

        escapedStudents = 0;
        foreach (var prefab in students){
            if(prefab == null){
                escapedStudents++;
            }
        }

        // Calculate the score
        // % of lesson complete * 100, minus the number of students * 200
        int lessonCompleted = 100 - Mathf.CeilToInt(lessonRemaining);
        int escapedStudentsScore = (-1)*(lostPointsPerStudent)*(escapedStudents);
        int lessonCompletionScore = lessonCompleted * pointsPerLessonPercent;
        score = lessonCompletionScore + escapedStudentsScore;

        scoreBreakdownText = $"Lesson completed: {lessonCompleted}% x {pointsPerLessonPercent} = {lessonCompletionScore} <br> Escaped Students: {escapedStudents} x -{lostPointsPerStudent} = {escapedStudentsScore} <br> Final score: {score}";
    }

    void finish(){

        announcementTEXT.text = ("Finished!");
        if (timeRemaining >= 0){
            timeRemaining -= Time.deltaTime;

        } else {
            state = "SCORE_SCREEN";

            // Disable gameplay UI
            //gameplayUICanvas.enabled = false;
        }
    }

    void scoreScreen(){
        cameraAnimator.SetBool("Score", true);
        announcementTEXT.text = ("Press Space to Restart");

        scoreTEXT.text = scoreBreakdownText;

        if (Input.GetKey("space")){
            cameraAnimator.SetBool("Score", false);
            state = "COUNTDOWN";
            scoreTEXT.text = "";
        }
    }
}


