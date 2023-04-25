using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    public Slider slider;
    public float fixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // fixedDeltaTime = slider.value;
        //
        // Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale ;
        Time.timeScale = slider.value;
    }
}
