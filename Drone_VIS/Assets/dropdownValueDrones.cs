using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownValueDrones : MonoBehaviour
{
    [Space]
    [Space]
    public planScript PlanScript;
    public dropdownHandler dropdownScript;
    public List<GameObject> allAgents;

    [Space]
    [Space]
    private GameObject[] drones = new GameObject[20];
    private GameObject objectToCopy;
    private GameObject objectToDestroy;

    //public Dropdown dropdown;
    public Dropdown m_Dropdown;
    public int m_DropdownValue;
    private int i;
    private int iMax;
    private GameObject[] NB;

    [Space]
    [Space]
    public Toggle m_Toggle;
    public bool custom;

    [Space]
    [Space]
    public InputField inputFieldX;
    public InputField inputFieldY;
    public InputField inputFieldZ;

    [Space]
    [Space]
    public Vector3 pos;
    private float initPosX;
    private float initPosY;
    private float initPosZ;

    [Space]
    [Space]
    private float randPosX;
    private float randPosY;
    private float randPosZ;

    // Start is called before the first frame update
    void Start()
    {

        //dropdownScript = GameObject.Find("DropdownID").GetComponent<dropdownHandler>();
        //allAgents = dropdownScript.allAgents;

        inputFieldX.text = "X";
        inputFieldY.text = "Y";
        inputFieldZ.text = "Z";

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldX.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldY.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldZ.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


        pos = new Vector3(initPosX, initPosY, initPosZ);


        objectToCopy = GameObject.Find("MAV0");


        determineI();

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

    }


    public void ValueChangeCheck()
    {
        float.TryParse(inputFieldX.text, out initPosX);
        float.TryParse(inputFieldY.text, out initPosY);
        float.TryParse(inputFieldZ.text, out initPosZ);

    }


    public void ToggleValueChanged(Toggle m_Toggle)
    {
        custom = m_Toggle.isOn;
    }


    void DropdownValueChanged(Dropdown change)
    {

        determineI();
        iMax = (int)m_Dropdown.value;
        ToggleValueChanged(m_Toggle);
        Duplicate();
        Delete();
    }


    public void Duplicate()
    {
        for (i = NB.Length; i <= iMax; i++)
        {
            randPosX = Random.Range(-PlanScript.boundX, PlanScript.boundX);
            randPosY = Random.Range(-PlanScript.boundY, PlanScript.boundY);
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);

            custom = m_Toggle.isOn;
            drones[i] = GameObject.Instantiate(objectToCopy);

            if (custom == false)
            {
                drones[i].transform.position = new Vector3(randPosX, randPosY, randPosZ);
            }
            else
            {
                drones[i].transform.position = new Vector3(initPosX, initPosY, initPosZ);
                initPosX = initPosX + 3f;
            }
           
            drones[i].transform.rotation = Quaternion.identity;
            drones[i].name = "MAV" + i;


            /////////////////////////
            //allAgents.Add(drones[i]);
            /////////////////////////
        }

    }

    public void Delete()
    {
        for (iMax = m_Dropdown.value; iMax <= i; iMax++)
        {
            objectToDestroy = GameObject.Find("MAV" + iMax.ToString());
            Destroy(objectToDestroy);
            //allAgents.Remove(drones[iMax]);
        }
    }


    public void determineI()
    {
        NB = GameObject.FindGameObjectsWithTag("Drone");
        i = NB.Length;
    }

}
