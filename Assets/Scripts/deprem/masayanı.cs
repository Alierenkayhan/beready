using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class masayanı : MonoBehaviour
{
    public GameObject depremManager;
    public bool live = false;
    public bool depremStart = false;
    public GameObject feedbackFalse;
    public GameObject feedbackTrue;
    private Coroutine checkAnswers;
    private Earthquake earthquake;
    public statecontrol control;

    private void Awake()
    {
        earthquake = depremManager.GetComponent<Earthquake>();
    }

    private void Update()
    {
        if (!depremStart && earthquake.isShakeStart && control != null)
        {
            print("Masayanı script Check answers started");
            checkAnswers = StartCoroutine(check());
            depremStart = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // if (depremStart == true)
        // {
        //     if (this.gameObject.CompareTag("live"))
        //     {
        //         if (Input.GetKey(KeyCode.LeftControl))
        //         {
        //             live = true;
        //             feedbackTrue.SetActive(true);
        //         }
        //         else
        //         {
        //             feedbackFalse.SetActive(true);
        //         }
        //     }
        //     else
        //     {
        //         feedbackFalse.SetActive(true);
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out statecontrol c))
        {
            control = c;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(checkAnswers != null) StopCoroutine(checkAnswers);
        checkAnswers = null;
        control = null;
    }

    IEnumerator check()
    {
        yield return new WaitForSeconds(0.5f);
        if (control != null)
        {
            if (control.EvaluateAnswer())
            {
                feedbackTrue.SetActive(true);
            }
            else
            {
                feedbackFalse.SetActive(true);
            }
        }
    }
}
