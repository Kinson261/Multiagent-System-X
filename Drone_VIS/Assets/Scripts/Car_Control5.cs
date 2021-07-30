using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Control5 : MonoBehaviour
{
    public float MotorForce, SteerForce, BrakeForce;
    public WheelCollider FL, FR, RL, RR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
