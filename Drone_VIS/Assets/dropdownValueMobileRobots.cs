using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownValueMobileRobots : MonoBehaviour
{
    Dropdown m_Dropdown;
    public int m_DropdownValue;
    public planScript PlanScript;

    public GameObject[] rovers = new GameObject[20];
    public GameObject objectToCopy;
    private GameObject objectToDestroy;

    public Dropdown dropdown;
    public int i;
    public int iMax;
    public Vector3 pos;
    public GameObject[] NB;

    private float randPosX;
    private float randPosY;
    private float randPosZ;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(Random.Range(-150.0f, 150.0f), 1f, Random.Range(-150.0f, 150.0f));

        objectToCopy = GameObject.Find("MobileRobot0");


        m_Dropdown = GetComponent<Dropdown>();

        determineI();

        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {
        Delete();
        determineI();
        iMax = (int)m_Dropdown.value;
        Duplicate();
        
    }


    public void Duplicate()
    {
        for (i = NB.Length; i <= iMax; i++)
        {
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = 0;
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);
            rovers[i] = Instantiate(objectToCopy, new Vector3 (randPosX, randPosY, randPosZ), Quaternion.identity);

            rovers[i].transform.position = new Vector3(Random.Range(-150.0f, 150.0f), 1f, Random.Range(-150.0f, 150.0f));
            rovers[i].transform.rotation = Quaternion.identity;
            rovers[i].name = "MobileRobot" + i;
            pos.x++;
            //i++;
        }
        
    }

    public void Delete()
    {
        
        for (i = 0; i<rovers.Length; i++)
        {
            Destroy(rovers[i].gameObject);
        }
    }


    public void determineI()
    {
        NB = GameObject.FindGameObjectsWithTag("mobile robots");
        i = NB.Length;
    }
}
