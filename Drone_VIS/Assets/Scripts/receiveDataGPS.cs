using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveDataGPS : MonoBehaviour
{
    public static receiveDataGPS Instance { set; get; }

    public float latitude;
    public float longitude;
    public float altitude;


    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }


    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(1.0f, 1.0f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
        }


        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        altitude = Input.location.lastData.altitude;

        yield break;
        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }


    public void receiveGPSCoord(float latitude, float longitude, float altitude)
    {
        
        


        // Coordinates
        Vector3 GPS_coord;
        GPS_coord = new Vector3(latitude, longitude, altitude);

        //Angular speed
        Vector3 GPS_a_speed;
        //GPS_a_speed = new Vector3(x, y, z);

        //frequency
        int GPS_chast;
        GPS_chast = 5000;

        

    }
}
