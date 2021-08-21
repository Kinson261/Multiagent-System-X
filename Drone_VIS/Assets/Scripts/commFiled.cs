using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class commFiled : MonoBehaviour
{
    public selectionHandler selectionHandler;
    public List<GameObject> arrayRobots;
    public List<GameObject> arrayDrones;

    public void Start()
    {
        selectionHandler = GameObject.Find("DropdownID").GetComponent<selectionHandler>();
    }
    public void Update()
    {
        arrayRobots = selectionHandler.allRovers;
        arrayDrones = selectionHandler.allDrones;
    }

}
