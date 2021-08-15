using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planScript : MonoBehaviour
{
    public GameObject plan;
    public int sizeX;
    public int sizeY;
    public int sizeZ;

    public float boundX;
    public float boundY;
    public float boundZ;


    public float surfacePlane;
    // Start is called before the first frame update
    public void Start()
    {
        //plan = GameObject.Find("Plane");
        plan.transform.position = new Vector3(0, 0, 0);
        plan.transform.localScale = new Vector3(10, 10, 10);

        calculateSurfacePlane();
        calculateBoundaries();
    }

    // Update is called once per frame
    public void Update()
    {
        plan.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
        calculateSurfacePlane();
        calculateBoundaries();
    }


    public void calculateSurfacePlane()
    {
        surfacePlane = (plan.transform.localScale.x * 10f) * (plan.transform.localScale.z * 10f);
    }


    public void calculateBoundaries()
    {
        boundX = (plan.transform.localScale.x * 10f) / 2f;
        boundY = (plan.transform.localScale.y * 10f) / 2f;
        boundZ = (plan.transform.localScale.z * 10f) / 2f;
    }
}
