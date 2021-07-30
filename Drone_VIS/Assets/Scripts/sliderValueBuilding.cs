using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValueBuilding : MonoBehaviour
{
    public Slider slider;
    public int sliderValStart;
    public int sliderValEnd;

    public GameObject objectToDuplicate;
    public int diff;
    private int i = 1;




    void Start()
    {
        sliderValStart = (int)GetComponent<Slider>().value;

    }

    public void OnValuechanged(int value)
    {
        sliderValEnd = (int)GetComponent<Slider>().value;
        diff = sliderValEnd - sliderValStart;
        sliderValStart = sliderValEnd;
        sliderValEnd = 0;
    }

    public void Duplicate()
    {
        if (diff > 0)
        {
            objectToDuplicate = GameObject.Find("Building");
            //Instantiate(objectToDuplicate, new Vector3(i * 7.0F, 0 * 30.0F + 5.0f, 0 * 30.0F+10.0f), Quaternion.identity);
            Instantiate(objectToDuplicate, new Vector3(Random.Range(-150.0f, 150.0f), 0f, Random.Range(-150.0f, 150.0f)), Quaternion.identity);
            i++;
        }


        //if (diff > 0)
        //{
        //    for (int i = 0; i < diff - 1; i++)
        //    {
        //        objectToDuplicate = GameObject.Find("Full_Mav");
        //       Instantiate(objectToDuplicate, new Vector3(i * 3.0F, i * 3.0F, i * 0.0F), Quaternion.identity);
        //    }

        //}
    }

    public void Delete()
    {
        if (diff < 0)
        {
            objectToDuplicate = GameObject.Find("Building(Clone)");
            Destroy(objectToDuplicate, 1.0F);
            //for (int i = diff; i > 0; i--)
            //{
            // Destroy(objectToDuplicate, 1.0F);
            //}
        }
    }
}
