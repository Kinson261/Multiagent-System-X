using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*THIS SCRIPT IS USED FOR MANUAL CONTROL OF THE ROVER
   IT DOES NOT WORK WHILE AI OR PATHFINDING IS WORKING*/

public class roverMovement : MonoBehaviour
{
    public float MotorForce, SteerForce, BrakeForce;
    public WheelCollider FL, FR, RL, RR;


    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical") * MotorForce;
        float h = Input.GetAxis("Horizontal") * SteerForce;

        RR.motorTorque = v;
        RL.motorTorque = v;

        FL.steerAngle = h;
        FR.steerAngle = h;

        if (Input.GetKey(KeyCode.Space))
        {
            RL.brakeTorque = BrakeForce;
            RR.brakeTorque = BrakeForce;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RL.brakeTorque = 0;
            RR.brakeTorque = 0;

        }

        if (Input.GetAxis("Vertical") == 0)
        {
            RL.brakeTorque = BrakeForce;
            RR.brakeTorque = BrakeForce;
        }
        else
        {
            RL.brakeTorque = 0;
            RR.brakeTorque = 0;
        }

    }
}
