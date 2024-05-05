using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Earthquake : MonoBehaviour
{
    public float magnitude = 0.1f; // Deprem büyüklüğü
    public float duration = 10f;   // Deprem süresi
    public float shakeSpeed = 5f;  // Titreme hızı
    public float startTime = 15f;  // Titreme hızı
    public bool isShakeStart = false;
    public resetCurrentLvl manager;
    public AlertController2 alert2;
    public TMP_Text text;

    void Start()
    {
        if (!PlayerPrefs.HasKey("OngoingEarthquake"))
        {
            PlayerPrefs.SetString("OngoingEarthquake", "false");
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("OngoingEarthquake") == "true")
        {
            Invoke("StartEarthquake", startTime);
            isShakeStart = false;
            text.text = "Yeniden başlamak için R ye basın";
        }
    }

    public void DoEarthquake()
    {
        // Invoke("StartEarthquake", startTime);
        // isShakeStart = false;
        PlayerPrefs.SetString("OngoingEarthquake", "true");
        PlayerPrefs.Save();
        manager.ResetLevel();
    }

    public void StartEarthquake()
    {
        PlayerPrefs.Save();
        startTime = Time.time;
        isShakeStart = true;
        InvokeRepeating("ShakeObjects", 0f, 0.1f); 
        Invoke("StopEarthquake", duration);
    }

    public void StopEarthquake()
    {
        CancelInvoke("ShakeObjects");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerPrefs.GetString("OngoingEarthquake") == "false")
        {
            alert2.gameObject.SetActive(true);
            Invoke(nameof(DoEarthquake), 5f);
        }
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
