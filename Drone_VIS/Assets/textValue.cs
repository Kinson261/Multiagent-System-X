using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textValue : MonoBehaviour
{

    public Slider slider;
    public Text slidervalue;

    // Update is called once per frame
    void Update()
    {
        slidervalue.text = slider.value.ToString();
    }
}
