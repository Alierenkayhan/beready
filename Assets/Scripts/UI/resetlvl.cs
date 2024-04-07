using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using Unity.VisualScripting;

public class resetlvl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();

            // Leveli sıfırla (istersek 1 olarak ayarlayabiliriz)
            PlayerPrefs.SetInt("CurrentLevel", 1);

            PlayerPrefs.Save();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
