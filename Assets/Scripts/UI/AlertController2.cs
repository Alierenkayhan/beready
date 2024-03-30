using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class AlertController2 : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    [SerializeField] TextMeshProUGUI alertTitleTmp;
    [SerializeField] TextMeshProUGUI alertBodyTmp;
    [SerializeField] string title;
    [SerializeField] string bodys;

    [SerializeField] GameObject fare;
    
    private float deactivateTime = 4f;

    private void Start()
    {
        Invoke("DeactivateAlert", deactivateTime);
    }

    private void OnEnable()
    {
        alert.SetActive(true);
        fare.SetActive(false);
        alertTitleTmp.text = title;
        alertBodyTmp.text = bodys;
        Invoke("DeactivateAlert", deactivateTime);
    }

    private void DeactivateAlert()
    {
        alert.SetActive(false);
        fare.SetActive(true);
    }
}
