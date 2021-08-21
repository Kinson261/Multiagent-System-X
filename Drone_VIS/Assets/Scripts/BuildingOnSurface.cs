using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Mercuna;


[ExecuteInEditMode]

public class BuildingOnSurface : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[100000];
    public GameObject ObjectToDuplicate;
    public planScript PlanScript;
    private int i;

    [Space]
    [Space]
    public Vector3 pos;
    public float surfacePlan;           //Surface area of plan
    private float surfaceCube;           //surface area of current cube
    public float sum;                   //sum of surfaces of all cube

    [Space]
    [Space]
    public Dropdown m_dropdown;
    public float changeValue;
    public float percentage;

    private float randPosX, randPosY, randPosZ;         //random position

    private float randScX, randScY, randScZ;            //random scale

    [Space]
    [Space]
    //Create InputField objects
    public InputField inputFieldMinX;
    public InputField inputFieldMinY;
    public InputField inputFieldMinZ;
    public InputField inputFieldMaxX;
    public InputField inputFieldMaxY;
    public InputField inputFieldMaxZ;

    [Space]
    [Space]
    //Associating float to inputfield
    public float minX;
    public float minY;
    public float minZ;
    public float maxX;
    public float maxY;
    public float maxZ;

    private MercunaNavOctree mercunaNavOctree;

    // Start is called before the first frame update
    void Start()
    {
        //init
        inputFieldMinX.text = "4";
        inputFieldMinY.text = "4";
        inputFieldMinZ.text = "4";
        inputFieldMaxX.text = "8";
        inputFieldMaxY.text = "8";
        inputFieldMaxZ.text = "8";

        //converting string to float
        float.TryParse(inputFieldMinX.text, out minX);
        float.TryParse(inputFieldMinY.text, out minY);
        float.TryParse(inputFieldMinZ.text, out minZ);
        float.TryParse(inputFieldMaxX.text, out maxX);
        float.TryParse(inputFieldMaxY.text, out maxY);
        float.TryParse(inputFieldMaxZ.text, out maxZ);

        //adds listener to value change of inputfield
        inputFieldMinX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMinY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMinZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldMaxZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });


        //adds listener to value change of dropdown
        m_dropdown.onValueChanged.AddListener(delegate
        {
            dropdownValueChanged(m_dropdown);

        });
    }


    //converting string to float
    public void ValueChangeCheck()
    {
        float.TryParse(inputFieldMinX.text, out minX);
        float.TryParse(inputFieldMinY.text, out minY);
        float.TryParse(inputFieldMinZ.text, out minZ);
        float.TryParse(inputFieldMaxX.text, out maxX);
        float.TryParse(inputFieldMaxY.text, out maxY);
        float.TryParse(inputFieldMaxZ.text, out maxZ);

    }


    public void generation(Dropdown change)
    {
        changeValue = change.value;

        surfacePlan = (PlanScript.plan.transform.localScale.x * 10f) * (PlanScript.transform.localScale.z * 10f);
        
        //list of options
        if (changeValue == 0)
        {
            clearScene();
            percentage = 0f;
            duplicate();
            
        }

        if (changeValue == 1)
        {
            clearScene();
            percentage = 30f/100f;
            duplicate();

        }

        if (changeValue == 2)
        {
            clearScene();
            percentage = 31f / 100f;
            duplicate();

        }

        if (changeValue == 3)
        {
            clearScene();
            percentage = 32f / 100f;
            duplicate();

        }

        if (changeValue == 4)
        {
            clearScene();
            percentage = 33f / 100f;
            duplicate();

        }

        if (changeValue == 5)
        {
            clearScene();
            percentage = 34f / 100f;
            duplicate();

        }

        if (changeValue == 6)
        {
            clearScene();
            percentage = 35f / 100f;
            duplicate();

        }

        if (changeValue == 7)
        {
            clearScene();
            percentage = 36f / 100f;
            duplicate();

        }

        if (changeValue == 8)
        {
            clearScene();
            percentage = 37f / 100f;
            duplicate();

        }

        if (changeValue == 9)
        {
            clearScene();
            percentage = 38f / 100f;
            duplicate();

        }

        if (changeValue == 10)
        {
            clearScene();
            percentage = 39f / 100f;
            duplicate();

        }

        if (changeValue == 11)
        {
            clearScene();
            percentage = 40f / 100f;
            duplicate();

        }
        if (changeValue == 12)
        {
            clearScene();
            percentage = 41f / 100f;
            duplicate();

        }
        if (changeValue == 13)
        {
            clearScene();
            percentage = 42f / 100f;
            duplicate();

        }
        if (changeValue == 14)
        {
            clearScene();
            percentage = 43f / 100f;
            duplicate();

        }
        if (changeValue == 15)
        {
            clearScene();
            percentage = 44f / 100f;
            duplicate();

        }
        if (changeValue == 16)
        {
            clearScene();
            percentage = 45f / 100f;
            duplicate();

        }
        if (changeValue == 17)
        {
            clearScene();
            percentage = 46f / 100f;
            duplicate();

        }
        if (changeValue == 18)
        {
            clearScene();
            percentage = 47f / 100f;
            duplicate();

        }
        if (changeValue == 19)
        {
            clearScene();
            percentage = 48f / 100f;
            duplicate();

        }
        if (changeValue == 20)
        {
            clearScene();
            percentage = 49f / 100f;
            duplicate();

        }
        if (changeValue == 21)
        {
            clearScene();
            percentage = 50f / 100f;
            duplicate();

        }
        if (changeValue == 22)
        {
            clearScene();
            percentage = 51f / 100f;
            duplicate();

        }
        if (changeValue == 23)
        {
            clearScene();
            percentage = 52f / 100f;
            duplicate();

        }
        if (changeValue == 24)
        {
            clearScene();
            percentage = 53f / 100f;
            duplicate();

        }
        if (changeValue == 25)
        {
            clearScene();
            percentage = 54f / 100f;
            duplicate();

        }
        if (changeValue == 26)
        {
            clearScene();
            percentage = 55f / 100f;
            duplicate();

        }
        if (changeValue == 27)
        {
            clearScene();
            percentage = 56f / 100f;
            duplicate();

        }
        if (changeValue == 28)
        {
            clearScene();
            percentage = 57f / 100f;
            duplicate();

        }
        if (changeValue == 29)
        {
            clearScene();
            percentage = 58f / 100f;
            duplicate();

        }
        if (changeValue == 30)
        {
            clearScene();
            percentage = 59f / 100f;
            duplicate();

        }
        if (changeValue == 31)
        {
            clearScene();
            percentage = 60f / 100f;
            duplicate();

        }
        if (changeValue == 32)
        {
            clearScene();
            percentage = 61f / 100f;
            duplicate();

        }
        if (changeValue == 33)
        {
            clearScene();
            percentage = 62f / 100f;
            duplicate();

        }
        if (changeValue == 34)
        {
            clearScene();
            percentage = 63f / 100f;
            duplicate();

        }
        if (changeValue == 35)
        {
            clearScene();
            percentage = 64f / 100f;
            duplicate();

        }
        if (changeValue == 36)
        {
            clearScene();
            percentage = 65f / 100f;
            duplicate();

        }
        if (changeValue == 37)
        {
            clearScene();
            percentage = 66f / 100f;
            duplicate();

        }
        if (changeValue == 38)
        {
            clearScene();
            percentage = 67f / 100f;
            duplicate();

        }
        if (changeValue == 39)
        {
            clearScene();
            percentage = 68f / 100f;
            duplicate();

        }
        if (changeValue == 40)
        {
            clearScene();
            percentage = 69f / 100f;
            duplicate();

        }
        if (changeValue == 41)
        {
            clearScene();
            percentage = 70f / 100f;
            duplicate();

        }


    }

   
    private void dropdownValueChanged(Dropdown change)
    {
        ValueChangeCheck();
        generation(change);
        bakeNavOctree();
    }


    //function to generate buildings
    public void duplicate()
    {
        for (i = 0;  i < cubes.Length; i++)
        {
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = 0;
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);
            cubes[i] = Instantiate(ObjectToDuplicate, new Vector3(randPosX, randPosY, randPosZ), Quaternion.identity);
            
            randScX = Random.Range(minX, maxX);
            randScY = Random.Range(minY, maxY);
            randScZ = Random.Range(minZ, maxZ);
            cubes[i].transform.localScale = new Vector3(randScX, randScY, randScZ);

            cubes[i].transform.position = new Vector3(randPosX, randScY/2, randPosZ);

            cubes[i].name = "Cube" + i;

            cubes[i].transform.parent = GameObject.Find("Level").transform;
            
            surfaceCube = (1f * cubes[i].transform.localScale.x) * (1f * cubes[i].transform.localScale.z);
            sum += surfaceCube;
    


            if (sum >= percentage * surfacePlan)
            {
                break;
            }
            else
            {
                continue;
            }
            
        }

        
    }


    //function to clear Buildings from the scene
    public void clearScene()
    {
        for (i = 0; i < cubes.Length; i++)
        {
            Destroy(cubes[i].gameObject);
        }
        sum = 0;

    }


    public void bakeNavOctree()
    {
        GameObject.Find("Nav Octree").GetComponent<MercunaNavOctree>();
        GameObject.Find("Nav Volume").GetComponent<MercunaNavVolume>();
        GameObject.Find("Nav Seed").GetComponent<MercunaNavSeed>();
        GameObject.Find("Nav Octree").GetComponent<MercunaNavOctree>().Build();
    }
}
