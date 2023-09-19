using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class student : MonoBehaviour
{

    public float obedience = 0f;
    float randomNumber;

    // Obedience meter
    public Image meterBar;
    public float meterAmount = 100f;

    // Update is called once per frame
    void Update(){
        randomNumber = Random.Range(0, 100);
        if (randomNumber > 97){
            Misbehave();
        }

        if (randomNumber < 3){ // temp 
            increaseMeter(1); 
        }
    }

    void Misbehave(){
        reduceMeter(1);
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
