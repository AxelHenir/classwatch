using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playButtonSound : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(){
        audioSource1.Play();
    }
}
