using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planScript : MonoBehaviour
{
    private GameObject plan;
    public int sizeX;
    public int sizeY;
    public int sizeZ;

    // Start is called before the first frame update
    void Start()
    {
        plan = GameObject.Find("Plan");
        plan.transform.position = new Vector3(0, 0, 0);
        plan.transform.localScale = new Vector3(30, 30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        plan.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
    }


}
