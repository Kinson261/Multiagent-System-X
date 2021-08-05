using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class droneManager : MonoBehaviour
{
    private GameObject[] drones = new GameObject[10];
    private GameObject objectToCopy;
    private GameObject objectToDestroy;
    private GameObject deleteFirst;

    public Slider slider;
    public int i;
    public int iMax;

    public Vector3 pos;




    // Start is called before the first frame update
    void Start()
    {
        //deleteFirst = GameObject.Find("MobileRobot");
        //Destroy(deleteFirst);

        pos = new Vector3(Random.Range(-150.0f, 150.0f), 1f, Random.Range(-150.0f, 150.0f));

        objectToCopy = GameObject.Find("MAV");

    }


    public void onValueChanged()
    {
        iMax = (int)GetComponent<Slider>().value;
        Duplicate();
        Delete();
    }


    public void Duplicate()
    {
        if (i < iMax)
        {
            drones[i] = GameObject.Instantiate(objectToCopy);
            drones[i].transform.position = new Vector3(Random.Range(-150.0f, 150.0f), Random.Range(10f,15f), Random.Range(-150.0f, 150.0f));
            drones[i].transform.rotation = Quaternion.identity;
            drones[i].name = "MAV" + i;
            pos.x++;
            i++;
        }
    }

    public void Delete()
    {
        if (iMax < i)
        {
            //string name = "MobileRobot" + iMax.ToString();
            objectToDestroy = GameObject.Find("MAV" + iMax.ToString());
            Destroy(objectToDestroy);
            i--;
        }
    }
}
