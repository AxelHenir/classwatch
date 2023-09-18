using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prof : MonoBehaviour
{
    // To track which state the prof is in
    bool writing = true;

    // Update is called once per frame
    void Update(){

        // if the button is pressed, look at class (change state)
        if (Input.GetKey("space")){
            print("THE PROF IS WATCHING");
            writing = false;
        } 
        else {
            print("THE PROF IS WRITING");
            writing = true;
        }
    }
}
