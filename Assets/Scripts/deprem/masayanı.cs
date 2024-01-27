using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masayanı : MonoBehaviour
{
    public GameObject depremManager;
    public bool live = false;
    public bool depremStart = false;
    private void Update()
    {
        depremStart = depremManager.GetComponent<Earthquake>().isShakeStart;
    }
    private void OnTriggerStay(Collider other)
    {
        if (depremStart == true)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                live = true;
            }
        }
    }     
}
