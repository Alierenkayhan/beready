using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masayanı : MonoBehaviour
{
    public GameObject depremManager;
    public bool live = false;
    public bool depremStart = false;
    public GameObject feedbackFalse;
    public GameObject feedbackTrue;

    private void Update()
    {
        depremStart = depremManager.GetComponent<Earthquake>().isShakeStart;
    }
    private void OnTriggerStay(Collider other)
    {
        if (depremStart == true)
        {
            if (this.gameObject.tag == "live")
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    live = true;
                    feedbackTrue.SetActive(true);
                }
                else
                {
                    feedbackFalse.SetActive(true);
                }
            }
            else
            {
                feedbackFalse.SetActive(true);
            }
        }
    }     
}
