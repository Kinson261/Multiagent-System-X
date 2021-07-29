using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateGPSText : MonoBehaviour
{
    public Text coordinates1;
    public Text coordinates2;
    public Text coordinates3;

    private void Update()
    {
        coordinates1.text = "Lat: " + receiveDataGPS.Instance.latitude.ToString();
        coordinates2.text = "Lon: " + receiveDataGPS.Instance.longitude.ToString();
        coordinates3.text = "Altitude: " + receiveDataGPS.Instance.altitude.ToString();
    }
}
