using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float magnitude = 0.1f; // Deprem büyüklüğü
    public float duration = 10f;   // Deprem süresi
    public float shakeSpeed = 5f;  // Titreme hızı

    private float startTime;

    void Start()
    {
        Invoke("StartEarthquake", 10f);
    }

    void StartEarthquake()
    {
        startTime = Time.time;
        InvokeRepeating("ShakeObjects", 0f, 0.1f); // Her 0.1 saniyede bir ShakeObjects fonksiyonunu çağır
        Invoke("StopEarthquake", duration);
    }

    void StopEarthquake()
    {
        CancelInvoke("ShakeObjects");
    }

    void ShakeObjects()
    {
        GameObject[] objectsToShake = GameObject.FindGameObjectsWithTag("Shakable"); // "Shakable" etiketine sahip nesneler

        foreach (GameObject obj in objectsToShake)
        {
            ShakeObject(obj);
        }
    }

    void ShakeObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        // Rastgele bir kuvvet uygula (x, y, z yönlerinde ayrı ayrı)
        float forceX = Random.Range(-magnitude, magnitude);
        float forceY = Random.Range(-magnitude, magnitude);
        float forceZ = Random.Range(-magnitude, magnitude);

        // Rigid body varsa kuvveti uygula
        if (rb != null)
        {
            rb.AddForce(new Vector3(forceX, forceY, forceZ), ForceMode.Impulse);
        }
    }
}
