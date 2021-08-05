using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingManager : MonoBehaviour
{
    private GameObject[] buildings = new GameObject[40];
    private GameObject objectToCopy;
    private GameObject objectToDestroy;
    private GameObject deleteFirst;

    public Slider slider;
    public int i;
    public int iMax;

    public Vector3 pos;


    //public int minHeight;
    //public int maxHeight;




    // Start is called before the first frame update
    void Start()
    {
        //deleteFirst = GameObject.Find("MobileRobot");
        //Destroy(deleteFirst);

        pos = new Vector3(Random.Range(-150.0f, 150.0f), 4f, Random.Range(-150.0f, 150.0f));

        objectToCopy = GameObject.Find("Building");

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
            buildings[i] = GameObject.Instantiate(objectToCopy);
            buildings[i].transform.position = new Vector3(Random.Range(-150.0f, 150.0f), 5f, Random.Range(-150.0f, 150.0f));
            buildings[i].transform.rotation = Quaternion.identity;
            buildings[i].transform.localScale = new Vector3(Random.Range(6f, 12f), Random.Range(12f,40f), Random.Range(7f, 15f));
            buildings[i].name = "Building" + i;
            pos.x++;
            i++;
        }
    }

    public void Delete()
    {
        if (iMax < i)
        {
            //string name = "MobileRobot" + iMax.ToString();
            objectToDestroy = GameObject.Find("Building" + iMax.ToString());
            Destroy(objectToDestroy);
            i--;
        }
    }
}
