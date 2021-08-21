using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    public GameObject currentSelection;
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
    private float GetPosX;
    private float GetPosY;
    private float GetPosZ;
    public Vector3 GetPos;
  

    [Space]
    [Space]
    public float maxSpeedMobileRobot;
    public float maxSpeedDrone;

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

        maxSpeedMobileRobot = 8f;                   //define speed of mobile robots
        maxSpeedDrone = 15f;



    }


    //function to update values from inputFields
    public void ValueChangeCheck()
    {
        //converting string to float
        float.TryParse(inputFieldTargetX.text, out SetPosX);
        float.TryParse(inputFieldTargetY.text, out SetPosY);
        float.TryParse(inputFieldTargetZ.text, out SetPosZ);

        SetPos = new Vector3(SetPosX, SetPosY, SetPosZ);

        SetPosition(currentSelection, SetPos);

    }



    public Vector3 getPosition(GameObject agent)
    {
        GetPosX = agent.transform.position.x;
        GetPosY = agent.transform.position.x;
        GetPosZ = agent.transform.position.x;

        GetPos = new Vector3(GetPosX, GetPosY, GetPosZ);

        return GetPos;
    }


    public void SetPosition(GameObject agent, Vector3 destination)
    {

        if (agent.tag == "mobile robots")
        {
            if (agent.name == "MobileRobot0")
            {
                agent.GetComponent<NavMeshAgent>().enabled = true;
                /* COMPONENT NavMeshAgent DO NOT ALLOW US TO SPAWN OBJECTS AT RANDOM PLACES.
                 * SO I ACTIVATE IT ONLY WHEN IT IS NECESSARY.
                 * ONCE A CLONE SPAWNS, WE CAN ACTIVATE ITS NavMeshAgent COMPONENT
                 */
            }

            agent.GetComponent<NavMeshAgent>().SetDestination(destination);           //activates pathfinding to set destination
            agent.GetComponent<NavMeshAgent>().speed = maxSpeedMobileRobot;      //set rover speed
        }


        if (agent.tag == "Drone")
        {
            /*FLYING OBJECTS DOESN'T WORK WELL WITH "NavMesh".
             * FOR DRONES I DECIDED TO USE "Mercuna3DNavigation"
             * TO CONTROL DRONE SPEED, PLEASE REFER TO COMPONENT "Mercuna Move controller" ATTACHED TO DRONE"
             */

            agent.GetComponent<Mercuna.Mercuna3DNavigation>().NavigateToLocation(destination);

            //currentSelection.transform.position = Vector3.MoveTowards(transform.position, SetPos, maxSpeedDrone * Time.deltaTime);
            ////currentSelection.transform.position = Vector3.Lerp(transform.position, SetPos, maxSpeedDrone * Time.deltaTime);


        }
    }

    public void Update()
    {
        currentSelection = selectionHandler.currentSelection;
    }
}
