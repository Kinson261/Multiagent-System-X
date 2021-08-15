using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOnSurface : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[100000];
    public int i;
  
    public GameObject ObjectToDuplicate;

    public planScript PlanScript;

    
    [Space]
    [Space]
    public Vector3 pos;
    public float surfaceCube;
    public float surfacePlan;
    public float sum;
    
    [Space]
    [Space]
    public Dropdown m_dropdown;
    public float changeValue;
    public float percentage;

    private float randPosX;
    private float randPosY;
    private float randPosZ;


    private float randScX;
    private float randScY;
    private float randScZ;


    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(Random.Range(-PlanScript.boundX, PlanScript.boundX), 4f, Random.Range(-PlanScript.boundZ, PlanScript.boundZ));
        
        

        m_dropdown.onValueChanged.AddListener(delegate
        {
            dropdownValueChanged(m_dropdown);

        });

    }

    private void dropdownValueChanged(Dropdown change)
    {
        generation(change);
        
    }

    public void generation(Dropdown change)
    {
        changeValue = change.value;

        surfacePlan = (PlanScript.plan.transform.localScale.x * 10f) * (PlanScript.transform.localScale.z * 10f);
        

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
            percentage = 35f / 100f;
            duplicate();

        }

        if (changeValue == 3)
        {
            clearScene();
            percentage = 40f / 100f;
            duplicate();

        }

        if (changeValue == 4)
        {
            clearScene();
            percentage = 45f / 100f;
            duplicate();

        }

        if (changeValue == 5)
        {
            clearScene();
            percentage = 50f / 100f;
            duplicate();

        }

        if (changeValue == 6)
        {
            clearScene();
            percentage = 55f / 100f;
            duplicate();

        }

        if (changeValue == 7)
        {
            clearScene();
            percentage = 60f / 100f;
            duplicate();

        }

        if (changeValue == 8)
        {
            clearScene();
            percentage = 65f / 100f;
            duplicate();

        }

        if (changeValue == 9)
        {
            clearScene();
            percentage = 70f / 100f;
            duplicate();

        }


    }

    public void duplicate()
    {
        for (i = 0;  i < cubes.Length; i++)
        {
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = 0;
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);
            cubes[i] = Instantiate(ObjectToDuplicate, new Vector3(randPosX, randPosY, randPosZ), Quaternion.identity);
            
            randScX = Random.Range(8f, 20f);
            randScY = Random.Range(12f, 30f);
            randScZ = Random.Range(14f, 24f);
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

            //sum += surfaceCube;
            //pos.x++;
            //i++;
            
        }

        
    }

    public void clearScene()
    {
        for (i = 0; i < cubes.Length; i++)
        {
            Destroy(cubes[i].gameObject);
        }
        sum = 0;

    }
}
