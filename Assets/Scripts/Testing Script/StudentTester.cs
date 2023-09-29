using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentTester: MonoBehaviour{


    Animator _animator;
    // Start is called before the first frame update
    void Start() {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Vertical") >0) {

            _animator.SetBool("isWalking", true); // walking on the side
            _animator.SetBool("isWalkingStraight", true); //walking Straight
        }

        if (Input.GetAxisRaw("Horizontal") >0) {

            _animator.SetBool("isWalking", true); // walking on the side
            _animator.SetBool("isWalkingStraight", false); //walking Straight
        }
    }
}