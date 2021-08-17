using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownHandler : MonoBehaviour
{
    public Dropdown dropdown;

    [Space]
    [Space]
    public Text posX;
    public Text posZ;
    public Text posY;

    public GameObject currentSelection;
    public GameObject[] objectsWithTagsDrone;
    public GameObject[] objectsWithTagsMobileRobot;
    public int i;

    public List<GameObject> allAgents;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();


        fillAgents();
        dropdownAgentSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { dropdownAgentSelected(dropdown); });
    }
    
    public void dropdownAgentSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        currentSelection = GameObject.Find(dropdown.options[index].text);
        //textBox.text = dropdown.options[index].text;
        //posX.text = "X:" + gameObject.transform.position.x.ToString();
        //posY.text = "Y:" + gameObject.transform.position.y.ToString();
        //posZ.text = "Z:" + gameObject.transform.position.z.ToString();
    }

    public void fillAgents()
    {
        
        List<GameObject> allAgents = new List<GameObject>();
        dropdown.options.Clear();
        allAgents.Add(GameObject.Find("None"));

        objectsWithTagsDrone = GameObject.FindGameObjectsWithTag("Drone");
        for (i = 0; i < objectsWithTagsDrone.Length; i++)
        {
            allAgents.Add(objectsWithTagsDrone[i]);
        }

        objectsWithTagsMobileRobot = GameObject.FindGameObjectsWithTag("mobile robots");
        for (i = 0; i < objectsWithTagsMobileRobot.Length; i++)
        {
            allAgents.Add(objectsWithTagsMobileRobot[i]);
        }

        //Fill dropdown with agents
        foreach (GameObject item in allAgents)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item.name });
        }
        
    }

    public void Update()
    {
        fillAgents();
        //int index = dropdown.value;
        //currentSelection = GameObject.Find(dropdown.options[index].text);
        posX.text = "X:" + currentSelection.transform.position.x.ToString();
        posY.text = "Y:" + currentSelection.transform.position.y.ToString();
        posZ.text = "Z:" + currentSelection.transform.position.z.ToString();
    }
}
