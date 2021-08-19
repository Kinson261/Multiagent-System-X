using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* THIS SCRIPT IS FOR SHOWING/HIDING CANVAS OF PARAMETERS*/

public class hideMenu : MonoBehaviour
{
    public Button m_button;
    public Canvas canvas;

    public void Start()
    {
        //add a listener to check button state
        m_button.onClick.AddListener(delegate { TaskOnClick(); });
    }

    private void TaskOnClick()
    {
        if (canvas.enabled == true)
        {
            //deactivate
            canvas.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            //activate
            canvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
