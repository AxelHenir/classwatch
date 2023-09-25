using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfTester : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")) {

            _animator.SetBool("isTurning", true); // Turn prof
        }
        else {
            _animator.SetBool("isTurning", false); // Turn prof
        }
    }
}
