using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTester : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] private float moveSpeed;
    private int pointsIndex;
    Vector3 oldPos;
    Animator _animator;

    public bool isCaught;

    void Start()
    {
        oldPos = transform.position;
        // transform.position = Points[pointsIndex].transform.position;
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey("space")) {
            isCaught = true;
        }

        testingpath();
        
    }


    void testingpath() {
        if(pointsIndex <= Points.Length -1 && !isCaught) {

            transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed* Time.deltaTime);

            //walk towards the next point
            if (transform.position == Points[pointsIndex].transform.position) {
                pointsIndex +=1;
            }
            // Looping  but we dont need
            if (pointsIndex == Points.Length) {
                // pointsIndex = 0;
            }

            //If they move on the x axis
            if (transform.position.x != oldPos.x) {
                _animator.SetBool("isWalking", true); // walking side

            }
            // If they reach a certain distance on the z axis, switch the sprite to walking front at a much faster rate
            if( transform.position.z < 2f) {
                _animator.SetBool("isWalkingStraight", true); // walking front
                moveSpeed =5f;
            }
        }

        // If they get caught by the prof
        if(pointsIndex < Points.Length  && isCaught) {
            
            transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed* Time.deltaTime);

            // Its rewind time
            if (transform.position == Points[pointsIndex].transform.position) {
                pointsIndex -=1;

                // 0 = 0, -1 = 0 --> make it so the array count doesnt go under 0
                if (pointsIndex <0) {
                        pointsIndex = 0;
                }
            }
        
            _animator.SetBool("isWalkingStraight", false); // walking on the side
            
            // Put the sprite back to idle mode
            // resets the iscaught
            if (transform.position == Points[0].transform.position) {
                _animator.SetBool("isWalking", false); // walking on the side
                isCaught = false;
            }
        }
    }
}
