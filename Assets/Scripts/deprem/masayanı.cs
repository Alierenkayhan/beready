using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class masayanı : MonoBehaviour
{
    public GameObject depremManager;
    public bool live = false;
    public bool depremStart = false;
    public GameObject feedbackFalse;
    public GameObject feedbackTrue;
    public AlertController alert;
    private Coroutine checkAnswers;
    private Earthquake earthquake;
    public statecontrol control;

    // private void Awake()
    // {
    //     earthquake = depremManager.GetComponent<Earthquake>();
    // }
    //
    // private void Update()
    // {
    //     
    // }
    //
    // private void OnTriggerStay(Collider other)
    // {
    //     // if (depremStart == true)
    //     // {
    //     //     if (this.gameObject.CompareTag("live"))
    //     //     {
    //     //         if (Input.GetKey(KeyCode.LeftControl))
    //     //         {
    //     //             live = true;
    //     //             feedbackTrue.SetActive(true);
    //     //         }
    //     //         else
    //     //         {
    //     //             feedbackFalse.SetActive(true);
    //     //         }
    //     //     }
    //     //     else
    //     //     {
    //     //         feedbackFalse.SetActive(true);
    //     //     }
    //     // }
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent(out statecontrol c))
    //     {
    //         control = c;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if(checkAnswers != null) StopCoroutine(checkAnswers);
    //     checkAnswers = null;
    //     control = null;
    // }

    // IEnumerator check()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     if (control != null)
    //     {
    //         switch (control.EvaluateAnswer())
    //         {
    //             case "masa":
    //                 alert.alert("Seçim", "Yaptığın seçim fena değildi. Uzmanlar sabitlenmiş eşyaların yanına çökülmesini ve tutunmayı öneriyor.", "Tamam");
    //                 break;
    //             case "dolap":
    //                 alert.alert("Seçim", "Yaptığın seçim uzmanlar tarafından önerilmiyor. Sabitlenmemiş eşyalar üzerine düşebilir ve sana zarar verebilir.", "Tamam");
    //                 break;
    //             case "pencere":
    //                 alert.alert("Seçim", "Yaptığın seçim uzmanlar tarafından önerilmiyor. Deprem esnasında pencerelerin kırılma ve cam parçaları ile seni yaralama riski var. Aynı zamanda sarsıntıdan aşağı düşebilirsin.", "Tamam");
    //                 break;
    //             case "exit":
    //                 alert.alert("Seçim", "Yaptığın seçim uzmanlar tarafından önerilmiyor. Deprem esnasında merdiven inmek veya asansöre binmek tehlikeli olabilir. Deprem durunca bina terk edilmelidir.", "Tamam");
    //                 break;
    //             default:
    //                 alert.alert("Seçim", "Güvensiz yerde durmak uzmanlar tarafından önerilmiyor. En yakın güvenli yere çökmeli ve bir yere tutunmalısın.", "Tamam");
    //                 break;
    //         }
    //         // if (control.EvaluateAnswer())
    //         // {
    //         //     feedbackTrue.SetActive(true);
    //         // }
    //         // else
    //         // {
    //         //     feedbackFalse.SetActive(true);
    //         // }
    //     }
    // }
}
