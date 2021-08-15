using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownValueMobileRobots : MonoBehaviour
{
    [Space]
    [Space]
    public planScript PlanScript;
    private GameObject[] rovers = new GameObject[20];
    private GameObject objectToCopy;
    private GameObject objectToDestroy;

    //public Dropdown dropdown;
    public Dropdown m_Dropdown;
    public int m_DropdownValue;
    private int i;
    private int iMax;
    public GameObject[] NB;

    [Space]
    [Space]
    public Toggle m_Toggle;
    public bool random;

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
        inputFieldX.text = "0";
        inputFieldY.text = "0";
        inputFieldZ.text = "0";

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldX.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldY.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputFieldZ.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


        pos = new Vector3(initPosX, initPosY, initPosZ);

        
        objectToCopy = GameObject.Find("MobileRobot0");


        determineI();

        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });



        m_Toggle = GameObject.Find("ToggleMobileRobot").GetComponent<Toggle>();
        random = m_Toggle.isOn;

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
        random = m_Toggle.isOn;
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
            randPosY = 1;
            randPosZ = Random.Range(-PlanScript.boundZ, PlanScript.boundZ);

            random = m_Toggle.isOn;
            rovers[i] = GameObject.Instantiate(objectToCopy);

            if (random == true)
            {
                rovers[i].transform.position = new Vector3(randPosX, randPosY, randPosZ);
            }
            else
            {
                rovers[i].transform.position = new Vector3(initPosX, initPosY, initPosZ);
                initPosX = initPosX + 3f;
            }

            rovers[i].transform.rotation = Quaternion.identity;
            rovers[i].name = "MobileRobot" + i;

        }
        
    }

    public void Delete()
    {
        for (iMax = m_Dropdown.value; iMax <= i; iMax++)
        {
            objectToDestroy = GameObject.Find("MobileRobot" + iMax.ToString());
            Destroy(objectToDestroy);
        }
    }


    public void determineI()
    {
        NB = GameObject.FindGameObjectsWithTag("mobile robots");
        i = NB.Length;
    }
}
