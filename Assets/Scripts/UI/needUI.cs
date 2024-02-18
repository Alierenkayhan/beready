using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class needUI : MonoBehaviour
{

    public TMP_Text textEkrani;
    public GameObject textEkraniGameObject;
    public GameObject end;
    public List<string> liste;

    private int currentIndex = 0;

    void Start()
    {
        textEkraniGameObject.SetActive(false);
        end.SetActive(false);
        Invoke("Yazdir", 7f);
    }

    void Yazdir()
    {
        if (currentIndex < liste.Count)
        {
            textEkraniGameObject.SetActive(true);
            string item = liste[currentIndex];
            textEkrani.text = "Şu an " + item + " a ihtiyacın var.";


            if (currentIndex >= liste.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            textEkraniGameObject.SetActive(false);
            end.SetActive(true);
        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("firstaidkit"))
                {
                    if (liste[currentIndex] == hit.collider.gameObject.name)
                    {
                        hit.collider.gameObject.SetActive(false);
                        currentIndex++;
                        Yazdir();
                    }
                }            
            }   
        }
    }
}
