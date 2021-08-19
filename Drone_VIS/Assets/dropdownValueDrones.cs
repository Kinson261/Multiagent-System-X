using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class dropdownValueDrones : MonoBehaviour
{
    [Space]
    [Space]
    public planScript PlanScript;
    public List<GameObject> allAgents;

    [Space]
    [Space]
    private GameObject[] drones = new GameObject[20];           //limit drone number to 20
    private GameObject objectToCopy;
    private GameObject objectToDestroy;

    public Dropdown m_Dropdown;
    public int m_DropdownValue;
    private int i;
    private int iMax;
    private GameObject[] NB;

    [Space]
    [Space]
    public Toggle m_Toggle;
    public bool custom;                             //custom spawn point

    [Space]
    [Space]
    //create InputField objects
    public InputField inputFieldX;
    public InputField inputFieldY;
    public InputField inputFieldZ;

    [Space]
    [Space]
    //Vector for given position
    public Vector3 pos;
    private float initPosX;
    private float initPosY;
    private float initPosZ;

    [Space]
    [Space]
    //var with random values
    private float randPosX;
    private float randPosY;
    private float randPosZ;

    // Start is called before the first frame update
    void Start()
    {
        //init
        inputFieldX.text = "X";
        inputFieldY.text = "Y";
        inputFieldZ.text = "Z";
        inputFieldX.interactable = false;
        inputFieldY.interactable = false;
        inputFieldZ.interactable = false;

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldX.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldY.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        inputFieldZ.onEndEdit.AddListener(delegate { ValueChangeCheck(); });


        //Adds a listener to the dropdown and invokes a method when the value changes.
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });


        m_Toggle = GameObject.Find("ToggleDrone").GetComponent<Toggle>();
        custom = m_Toggle.isOn;
        //Add listener for when the state of the Toggle changes, and output the state
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });


        pos = new Vector3(initPosX, initPosY, initPosZ);    //given coordinates
        objectToCopy = GameObject.Find("MAV0");             //sample
        determineI();                                       

    }


    //function for converting string to float
    public void ValueChangeCheck()
    {
        float.TryParse(inputFieldX.text, out initPosX);
        float.TryParse(inputFieldY.text, out initPosY);
        float.TryParse(inputFieldZ.text, out initPosZ);

    }

    //function to evaluate the state of toggle button
    public void ToggleValueChanged(Toggle m_Toggle)
    {
        custom = m_Toggle.isOn;
    }


    void DropdownValueChanged(Dropdown change)
    {

        determineI();
        iMax = (int)m_Dropdown.value;           //new value
        ToggleValueChanged(m_Toggle);
        Duplicate();
        Delete();
    }



    //function to generate drones
    public void Duplicate()
    {
        for (i = NB.Length; i <= iMax; i++)
        {
            //generate random numbers within the limits of the plan
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = Random.Range(-PlanScript.boundY, PlanScript.boundY);
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);

            
            drones[i] = Instantiate(objectToCopy);          //generate drones
            custom = m_Toggle.isOn;

            if (custom == false)
            {
                drones[i].transform.position = new Vector3(randPosX, randPosY, randPosZ);
            }
            else
            {
                drones[i].transform.position = new Vector3(initPosX, initPosY, initPosZ);
                initPosX = initPosX + 3f;                   //shifting to give space to the next drone
            }
           
            drones[i].transform.rotation = Quaternion.identity;
            drones[i].name = "MAV" + i;

        }

    }


    //function to delete drones
    public void Delete()
    {
        for (iMax = m_Dropdown.value; iMax <= i; iMax++)
        {
            objectToDestroy = GameObject.Find("MAV" + iMax.ToString());
            Destroy(objectToDestroy);
        }
    }


    //function to evaluate how many drones are on the scene
    public void determineI()
    {
        NB = GameObject.FindGameObjectsWithTag("Drone");
        i = NB.Length;
    }

}
