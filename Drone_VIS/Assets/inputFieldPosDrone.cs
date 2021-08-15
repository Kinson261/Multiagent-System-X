using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputFieldPosDrone : MonoBehaviour
{
    public InputField inputFieldX;
    public InputField inputFieldY;
    public InputField inputFieldZ;

    public float initPosX;
    public float initPosY;
    public float initPosZ;


    // Start is called before the first frame update
    void Start()
    {
        inputFieldX.text = "0";
        inputFieldY.text = "0";
        inputFieldZ.text = "0";

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldX.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldY.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldZ.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        initPosX = float.Parse(inputFieldX.text);
        initPosY = float.Parse(inputFieldY.text);
        initPosZ = float.Parse(inputFieldZ.text);

    }
}
