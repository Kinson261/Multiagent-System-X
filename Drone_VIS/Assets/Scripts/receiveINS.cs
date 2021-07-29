using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveINS : MonoBehaviour
{

    public Rigidbody rigidbody;

    public float x_coord, y_coord, z_coord;
    public float x_a_speed, y_a_speed, z_a_speed;
    public float x_a_orient, y_a_orient, z_a_orient;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        insCoord();
        ins_A_Speed();
        ins_A_Orient();

    }


    private void insCoord()
    {
        x_coord = rigidbody.position.x;
        y_coord = rigidbody.position.y;
        z_coord = rigidbody.position.z;

        Vector3 INS_coord;
        INS_coord = new Vector3(x_coord, y_coord, z_coord);
    }

    private void ins_A_Speed()
    {
        x_a_speed = rigidbody.angularVelocity.x;
        y_a_speed = rigidbody.angularVelocity.y;
        z_a_speed = rigidbody.angularVelocity.z;

        Vector3 INS_a_speed;
        INS_a_speed = new Vector3(x_a_speed, y_a_speed, z_a_speed);
    }

    private void ins_A_Orient()
    {
        x_a_orient = rigidbody.rotation.x;
        y_a_orient = rigidbody.rotation.y;
        z_a_orient = rigidbody.rotation.z;

        Vector3 INS_a_orient;
        INS_a_orient = new Vector3(x_a_orient, y_a_orient, z_a_orient);
    }
}
