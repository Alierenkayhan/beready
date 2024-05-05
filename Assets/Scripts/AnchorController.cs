using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour
{
    public string CheckString;
    
    private void Start()
    {
        if (CheckString == "masa" && PlayerPrefs.GetInt("masaDone", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        
        if (CheckString == "pencere" && PlayerPrefs.GetInt("pencereDone", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        
        if (CheckString == "dolap" && PlayerPrefs.GetInt("dolapDone", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        
        if (CheckString == "exit" && PlayerPrefs.GetInt("exitDone", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        
        if (CheckString == "nothing" && PlayerPrefs.GetInt("nothingDone", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    // private void Update()
    // {
    //     if (string.IsNullOrEmpty(CheckString))
    //     {
    //         if (Input.GetKeyDown(KeyCode.J))
    //         {
    //             PlayerPrefs.SetInt("masaDone", 1);
    //             PlayerPrefs.SetInt("pencereDone", 1);
    //             PlayerPrefs.SetInt("dolapDone", 1);
    //             PlayerPrefs.SetInt("nothingDone", 1);
    //             PlayerPrefs.Save();
    //         }
    //     }
    // }
}
