using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPoints : MonoBehaviour
{
    public GameObject gameObject;
    private float getX, getY, getZ;
    public Vector3 currentPosition;

    public float setX, setY, setZ;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPosition();
        setPosition();
    }

    void getPosition()
    {
        getX = gameObject.transform.position.x;
        getY = gameObject.transform.position.y;
        getZ = gameObject.transform.position.z;
        currentPosition = new Vector3(getX, getY, getZ);
    }

    void setPosition()
    {
        targetPosition = new Vector3(setX, setY, setZ);
    }
}
