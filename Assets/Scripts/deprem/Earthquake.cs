using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float magnitude = 0.1f; // Deprem büyüklüğü
    public float duration = 10f;   // Deprem süresi
    public float shakeSpeed = 5f;  // Titreme hızı
    public bool isShakeStart = false;
    private float startTime;

    void Start()
    {
        Invoke("StartEarthquake", 10f);
    }

    void StartEarthquake()
    {
        startTime = Time.time;
        InvokeRepeating("ShakeObjects", 0f, 0.1f); 
        Invoke("StopEarthquake", duration);
    }

    void StopEarthquake()
    {
        CancelInvoke("ShakeObjects");
    }

    void ShakeObjects()
    {
        GameObject[] objectsToShake = GameObject.FindGameObjectsWithTag("Shakable"); 

        foreach (GameObject obj in objectsToShake)
        {
            ShakeObject(obj);
        }
    }

    void ShakeObject(GameObject obj)
    {
        isShakeStart = true;
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        float forceX = Random.Range(-magnitude, magnitude);
        float forceY = Random.Range(-magnitude, magnitude);
        float forceZ = Random.Range(-magnitude, magnitude);

        if (rb != null)
        {
            rb.AddForce(new Vector3(forceX, forceY, forceZ), ForceMode.Impulse);
        }
    }
}
