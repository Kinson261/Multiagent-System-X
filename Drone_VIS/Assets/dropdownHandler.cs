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
    public Text textBox;

    public GameObject gameObject;
    public GameObject[] objectsWithTags;
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
        fillAgents();
        int index = dropdown.value;
        gameObject = GameObject.Find(dropdown.options[index].text);
        //textBox.text = dropdown.options[index].text;
        posX.text = "X:" + gameObject.transform.position.x.ToString();
        posY.text = "Y:" + gameObject.transform.position.y.ToString();
        posZ.text = "Z:" + gameObject.transform.position.z.ToString();
    }

    public void fillAgents()
    {
        
        List<GameObject> allAgents = new List<GameObject>();
        dropdown.options.Clear();
        objectsWithTags = GameObject.FindGameObjectsWithTag("Drone");
        for (i = 0; i < objectsWithTags.Length; i++)
        {
            allAgents.Add(objectsWithTags[i]);
        }

        //Fill dropdown with agents
        foreach (GameObject item in allAgents)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item.name });
        }
    }

    private void Update()
    {
        int index = dropdown.value;
        gameObject = GameObject.Find(dropdown.options[index].text);
        //textBox.text = dropdown.options[index].text;
        posX.text = "X:" + gameObject.transform.position.x.ToString();
        posY.text = "Y:" + gameObject.transform.position.y.ToString();
        posZ.text = "Z:" + gameObject.transform.position.z.ToString();
    }
}
