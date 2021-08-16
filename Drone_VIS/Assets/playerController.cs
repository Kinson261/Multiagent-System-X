using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEditor;

public class playerController : MonoBehaviour
{
    public NavMeshAgent agent;
    //public GameObject gameObject;

    [Space]
    [Space]
    public InputField inputFieldTargetX;
    public InputField inputFieldTargetY;
    public InputField inputFieldTargetZ;

    [Space]
    [Space]
    private float SetPosX;
    private float SetPosY;
    private float SetPosZ;

    public Vector3 SetPos;


    // Start is called before the first frame update
    void Start()
    {
        inputFieldTargetX.text = "0";
        inputFieldTargetY.text = "0";
        inputFieldTargetZ.text = "0";

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldTargetX.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetY.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetZ.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        float.TryParse(inputFieldTargetX.text, out SetPosX);
        float.TryParse(inputFieldTargetY.text, out SetPosY);
        float.TryParse(inputFieldTargetZ.text, out SetPosZ);
    }

    public void Update()
    {
        //gameObject = (GameObject)Selection.activeGameObject;
        ValueChangeCheck();
        SetPos = new Vector3(SetPosX, SetPosY, SetPosZ);
        agent.SetDestination(SetPos);
    }
}
