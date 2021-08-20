using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildiingManager : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[100000];
    public GameObject ObjectToDuplicate;
    public planScript PlanScript;
    private int i;


    [Space]
    [Space]
    public Vector3 pos;
    public float surfacePlan;               //surface area of the plan
    private float surfaceCube;              //surface area of currentcube
    public float sum;                       // sum of surface area of cubes

    private float randPosX, randPosY, randPosZ;             //var random position

    private float randScX, randScY, randScZ;                //var random scale

    [Space]
    [Space]
    //Create inputField Objects
    public InputField inputFieldMinX;
    public InputField inputFieldMinY;
    public InputField inputFieldMinZ;
    public InputField inputFieldMaxX;
    public InputField inputFieldMaxY;
    public InputField inputFieldMaxZ;
    public InputField inputOccupancy;

    [Space]
    [Space]
    //Associate float to value in inputfield
    private float minX;
    private float minY;
    private float minZ;
    private float maxX;
    private float maxY;
    private float maxZ;
    private float buildingOccupancy;


    // Start is called before the first frame update
    void Start()
    {
        //init
        inputFieldMinX.text = "8";
        inputFieldMinY.text = "12";
        inputFieldMinZ.text = "14";
        inputFieldMaxX.text = "20";
        inputFieldMaxY.text = "30";
        inputFieldMaxZ.text = "24";
        inputOccupancy.text = "0";

        //converting string to float
        float.TryParse(inputFieldMinX.text, out minX);
        float.TryParse(inputFieldMinY.text, out minY);
        float.TryParse(inputFieldMinZ.text, out minZ);
        float.TryParse(inputFieldMaxX.text, out maxX);
        float.TryParse(inputFieldMaxY.text, out maxY);
        float.TryParse(inputFieldMaxZ.text, out maxZ);
        float.TryParse(inputOccupancy.text, out buildingOccupancy);

        //Adds listener for any value change
        inputFieldMinX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMinY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMinZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputOccupancy.onEndEdit.AddListener(delegate { ValueChangeCheck(); });

    }

    public void ValueChangeCheck()
    {
        clearScene();

        //converting string to float
        float.TryParse(inputFieldMinX.text, out minX);
        float.TryParse(inputFieldMinY.text, out minY);
        float.TryParse(inputFieldMinZ.text, out minZ);
        float.TryParse(inputFieldMaxX.text, out maxX);
        float.TryParse(inputFieldMaxY.text, out maxY);
        float.TryParse(inputFieldMaxZ.text, out maxZ);
        float.TryParse(inputOccupancy.text, out buildingOccupancy);

        duplicate();

    }


    // Copy Gameobject to Scene
    public void duplicate()
    {
        for (i = 0; i < cubes.Length; i++)
        {
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = 0;
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);
            cubes[i] = Instantiate(ObjectToDuplicate, new Vector3(randPosX, randPosY, randPosZ), Quaternion.identity);

            randScX = Random.Range(minX, maxX);
            randScY = Random.Range(minY, maxY);
            randScZ = Random.Range(minZ, maxZ);

            cubes[i].transform.localScale = new Vector3(randScX, randScY, randScZ);
            cubes[i].transform.position = new Vector3(randPosX, randScY / 2, randPosZ);
            cubes[i].name = "Cube" + i;
            cubes[i].transform.parent = GameObject.Find("Level").transform;

            surfaceCube = (1f * cubes[i].transform.localScale.x) * (1f * cubes[i].transform.localScale.z);
            sum += surfaceCube;

            surfacePlan = (PlanScript.plan.transform.localScale.x * 10f) * (PlanScript.transform.localScale.z * 10f);
            if (sum >= buildingOccupancy * surfacePlan)
            {
                break;
            }
            else
            {
                continue;
            }

        }


    }

    //Delete GameObjects
    public void clearScene()
    {
        for (i = 0; i < cubes.Length; i++)
        {
            Destroy(cubes[i].gameObject);
        }
        sum = 0;

    }
}
