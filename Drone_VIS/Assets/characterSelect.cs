using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class characterSelect : MonoBehaviour
{
    public GameObject[] Agents;
    public GameObject currentAgent;

    private int i;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activeAgent();
    }

    public void activeAgent()
    {
        currentAgent = (GameObject)Selection.activeObject;

        for (i=0;  i < Agents.Length; i++)
        {
            if (currentAgent != Agents[i])
            {
                Agents[i].SetActive(false);

                //if (Agents[i] = GameObject.FindGameObjectWithTag("Drone"))
                //{
                //   Agents[i].GetComponent<DroneMovementScript>().enabled = false;
                //}

                //if (Agents[i] = GameObject.FindGameObjectWithTag("mobile robots"))
                //{
                //    Agents[i].GetComponent<Car_Control5>().enabled = false;
                //}

                    
            }
            else
            {
                Agents[i].SetActive(true);
            }
        }
    }
}
