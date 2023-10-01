using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuide : MonoBehaviour
{

    public GameObject Panel;

    public void OpenPanel() {
       
        if (Panel != null)
         {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
     }

}

// Tutorial source for this code:
// https://www.youtube.com/watch?v=LziIlLB2Kt4