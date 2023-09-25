using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerTester : MonoBehaviour
{
    public Image bookshelfTime;
    public float time = 30f;
    
    public void BookshelfTimer () {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bookshelfTime.fillAmount += 1.0f / time * Time.deltaTime;
        // bookshelfTime.fillAmount += 0.05f;
    }
}
