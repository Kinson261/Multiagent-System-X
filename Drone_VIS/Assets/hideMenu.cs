using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideMenu : MonoBehaviour
{
    public Button m_button;
    public Canvas canvas;
    private bool isShowing;

    public void Start()
    {
        m_button.onClick.AddListener(delegate { TaskOnClick(); });
    }

    private void TaskOnClick()
    {
        if (canvas.enabled == true)
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            canvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
