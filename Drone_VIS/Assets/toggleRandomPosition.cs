using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleRandomPosition : MonoBehaviour
{
    public Toggle m_Toggle;
    public bool random;                     //toggle random position


    // Start is called before the first frame update
    void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        random = m_Toggle.isOn;             //state of random toggle

        //Add listener for when the state of the Toggle changes, and output the state
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

    }

    public void ToggleValueChanged(Toggle m_Toggle)
    {
        random =  m_Toggle.isOn;                //state of random toggle
    }
}
