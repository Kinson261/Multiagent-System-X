using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GPSCoordinates : MonoBehaviour
{
    public Text id;
    public Text Lat;
    public Text Lon;
    public Text Alt;

    public GameObject gameObject;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject = Selection.activeGameObject;
        //gameObject = (GameObject)Selection.activeObject;
        gameObject = GameObject.Find("MobileRobot");
    }

    // Update is called once per frame
    public void Update()
    {
        gameObject = (GameObject)Selection.activeObject;
        id.text = "id: " + gameObject.name.ToString();
        Lat.text = "Lat:" + gameObject.transform.position.x.ToString();
        Lon.text = "Lon: " + gameObject.transform.position.z.ToString();
        Alt.text = "Alt: " + gameObject.transform.position.y.ToString();
    }
}
