using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class end : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    [SerializeField] TextMeshProUGUI alertTitleTmp;
    [SerializeField] TextMeshProUGUI alertBodyTmp;
    [SerializeField] string title;
    [SerializeField] string bodys;

    [SerializeField] GameObject fare;
    void Start()
    {
        alert.SetActive(true);
        fare.SetActive(false);
        alertTitleTmp.text = title;
        alertBodyTmp.text = bodys;
    }

}
