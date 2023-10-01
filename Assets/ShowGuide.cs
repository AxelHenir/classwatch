using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuide : MonoBehaviour
{
    public GameObject Panel;

    // This method will be called when the button is clicked to open/close the panel
    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

    // This method will be called when the canvas is clicked to close the panel
    public void ClosePanel()
    {
        if (Panel != null && Panel.activeSelf)
        {
            Panel.SetActive(false);
        }
    }
}


// Tutorial source for this code:
// https://www.youtube.com/watch?v=LziIlLB2Kt4