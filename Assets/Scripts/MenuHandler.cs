using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public AudioSource soundPlayer;
    public Animator cameraAnimator;
    public GameObject MenuHUD;
    public GameObject ProfImg;
    public GameObject Prof;

    public GameObject[] MenuScreen;
    public TMP_Text TitleTEXT;
    // Update is called once per frame
    
    void Start() {

    }

    void Update()
    {
        
    }

    public void playSound() {
        soundPlayer.Play();
    }

    public void gameplayStart () {
        cameraAnimator.SetBool("Gameplay", true);
        soundPlayer.Play();
        MenuHUD.SetActive(false);
        ProfImg.GetComponent<SpriteRenderer>().flipX = false;
        Prof.transform.position = new Vector3(Prof.transform.position.x, Prof.transform.position.y, 1.46f);
    }

    public void instructionScreen() {
        MenuScreen[0].SetActive(false);
        MenuScreen[1].SetActive(true);
        MenuScreen[2].SetActive(false);
        TitleTEXT.text = "How to Play";
    }

    public void backMenu() {
        MenuScreen[0].SetActive(true);
        MenuScreen[1].SetActive(false);
        MenuScreen[2].SetActive(false);
        TitleTEXT.text = "CLASS WATCH";
    }

    public void creditScreen() {
    MenuScreen[0].SetActive(false);
    MenuScreen[1].SetActive(false);
    MenuScreen[2].SetActive(true);
    TitleTEXT.text = "CREDITS";
    }
}
