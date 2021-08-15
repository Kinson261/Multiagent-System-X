using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownValueDrones : MonoBehaviour
{
    Dropdown m_Dropdown;
    public int m_DropdownValue;

    private GameObject[] rovers = new GameObject[20];
    private GameObject objectToCopy;
    private GameObject objectToDestroy;
    private GameObject deleteFirst;

    public Dropdown dropdown;
    public int i;
    public int iMax;
    public Vector3 pos;
    public GameObject[] NB;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(Random.Range(-150.0f, 150.0f), 1f, Random.Range(-150.0f, 150.0f));

        objectToCopy = GameObject.Find("MAV");


        m_Dropdown = GetComponent<Dropdown>();

        determineI();

        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {

        determineI();
        iMax = (int)m_Dropdown.value;
        Duplicate();
        Delete();
    }


    public void Duplicate()
    {
        for (i = NB.Length; i <= iMax; i++)
        {
            rovers[i] = GameObject.Instantiate(objectToCopy);
            rovers[i].transform.position = new Vector3(Random.Range(-150.0f, 150.0f), 1f, Random.Range(-150.0f, 150.0f));
            rovers[i].transform.rotation = Quaternion.identity;
            rovers[i].name = "MAV" + i;
            pos.x++;
            //i++;
        }

    }

    public void Delete()
    {
        for (iMax = m_Dropdown.value; iMax <= i; iMax++)
        {
            //string name = "MobileRobot" + iMax.ToString();
            objectToDestroy = GameObject.Find("MAV" + iMax.ToString());
            Destroy(objectToDestroy);
            //i--;
        }
    }


    public void determineI()
    {
        NB = GameObject.FindGameObjectsWithTag("Drone");
        i = NB.Length;
    }
}
