using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public void PlayGameWithDelay()
    {
        // Invoke the LoadNextScene method after a 2-second delay
        Invoke("LoadNextScene", 2f);
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadSceneAsync(1);
    }
}
