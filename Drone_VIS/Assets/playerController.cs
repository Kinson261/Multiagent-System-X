using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    private GameObject currentSelection;
    public selectionHandler selectionHandler;

    [Space]
    [Space]
    //Create InputField objects
    public InputField inputFieldTargetX;
    public InputField inputFieldTargetY;
    public InputField inputFieldTargetZ;

    [Space]
    [Space]
    //Associate a float to each inputfield
    private float SetPosX;
    private float SetPosY;
    private float SetPosZ;
    public Vector3 SetPos;

    [Space]
    [Space]
    //Create a toggle object
    public Toggle m_Toggle;
    public bool target;                     //target is on

    [Space]
    [Space]
    public float maxSpeedMobileRobot;   
    //public float maxSpeedDrone;

    /*
     maxSpeedDrone IS NOT NEEDED SINCE WE DON'T USE NAV MESH TO CONTROL IT
     IT IS CONTROLED BY MERCUNA AI.
     TO CONTROL DRONE SPEED, PLEASE REFER TO COMPONENT "Mercuna Move controller" ATTACHED TO DRONE"
    */


    
    // Start is called before the first frame update
    void Start()
    {
        //init
        inputFieldTargetX.text = "X";
        inputFieldTargetY.text = "Y";
        inputFieldTargetZ.text = "Z";
        inputFieldTargetX.interactable = false;
        inputFieldTargetY.interactable = false;
        inputFieldTargetZ.interactable = false;

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldTargetX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldTargetZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });

        m_Toggle = GameObject.Find("ToggleTarget").GetComponent<Toggle>();
        target = m_Toggle.isOn;                     //state of the toggle button

        maxSpeedMobileRobot = 8f;                   //define speed of mobile robots
        //maxSpeedDrone = 15f;
    }

    //function to update values from inputFields
    public void ValueChangeCheck()
    {
        //converting string to float
        float.TryParse(inputFieldTargetX.text, out SetPosX);
        float.TryParse(inputFieldTargetY.text, out SetPosY);
        float.TryParse(inputFieldTargetZ.text, out SetPosZ);

    }


    public void SetPosition()
    {
        currentSelection = selectionHandler.currentSelection;       //getting selected agent(drone or mobile robot)
        target = m_Toggle.isOn;                 //state of the toggle button
        ValueChangeCheck();                     //getting updated value


        if (target == true)
        {
            SetPos = new Vector3(SetPosX, SetPosY, SetPosZ);            //Set position to user's input
        }
        else
        {
            //Set position to current position
            SetPos = new Vector3(currentSelection.transform.position.x, currentSelection.transform.position.y, currentSelection.transform.position.z);
        }

        if (currentSelection.tag == "mobile robots")
        {
            if (currentSelection.name == "MobileRobot0")
            {
                currentSelection.GetComponent<NavMeshAgent>().enabled = true;
                /* COMPONENT NavMeshAgent DO NOT ALLOW US TO SPAWN OBJECTS AT RANDOM PLACES.
                 * SO I ACTIVATE IT ONLY WHEN IT IS NECESSARY.
                 * ONCE A CLONE SPAWNS, WE CAN ACTIVATE ITS NavMeshAgent COMPONENT
                 */
            }

            currentSelection.GetComponent<NavMeshAgent>().SetDestination(SetPos);           //activates pathfinding to set destination
            currentSelection.GetComponent<NavMeshAgent>().speed = maxSpeedMobileRobot;      //set rover speed
        }


        if (currentSelection.tag == "Drone")
        {
            /*FLYING OBJECTS DOESN'T WORK WELL WITH "NavMesh".
             * FOR DRONES I DECIDED TO USE "Mercuna3DNavigation"
             * TO CONTROL DRONE SPEED, PLEASE REFER TO COMPONENT "Mercuna Move controller" ATTACHED TO DRONE"
             */
            currentSelection.GetComponent<Mercuna.Mercuna3DNavigation>().NavigateToLocation(SetPos);

            
        }
    }
}
