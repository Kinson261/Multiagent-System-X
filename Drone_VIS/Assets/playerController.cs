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
    public GameObject currentSelection;
    public NavMeshAgent agentCurrentSelection;
    public dropdownHandler dropdownHandler;

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

    [Space]
    [Space]
    public Toggle m_Toggle;
    public bool target;


    // Start is called before the first frame update
    void Start()
    {
        inputFieldTargetX.text = "X";
        inputFieldTargetY.text = "Y";
        inputFieldTargetZ.text = "Z";

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldTargetX.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetY.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetZ.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        m_Toggle = GameObject.Find("ToggleTarget").GetComponent<Toggle>();
        target = m_Toggle.isOn;
    }

    public void ValueChangeCheck()
    {
        float.TryParse(inputFieldTargetX.text, out SetPosX);
        float.TryParse(inputFieldTargetY.text, out SetPosY);
        float.TryParse(inputFieldTargetZ.text, out SetPosZ);
    }

    public void Update()
    {
        currentSelection = dropdownHandler.currentSelection;
        agentCurrentSelection = currentSelection.GetComponent<NavMeshAgent>();
        ValueChangeCheck();

        target = m_Toggle.isOn;

        if (target == true)
        {
            SetPos = new Vector3(SetPosX, SetPosY, SetPosZ);
        }
        else
        {
            SetPos = new Vector3(currentSelection.transform.position.x, currentSelection.transform.position.y, currentSelection.transform.position.z);
        }
        
        //agent.SetDestination(SetPos);
        agentCurrentSelection.SetDestination(SetPos);
    }
}
