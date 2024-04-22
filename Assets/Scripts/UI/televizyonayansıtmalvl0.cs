using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class televizyonayansıtmalvl0 : MonoBehaviour
{
    public TMP_Text televizyonTxt;
    private string previous = "";
    private bool revision = false;
    
    
    private string[] required = {"Pil", "Su", "Telsiz", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak"};

    private List<string> items = new List<string>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("Revision"))
        {
            revision = true;
        }
        else
        {
            PlayerPrefs.SetString("Revision", "false");
            
            PlayerPrefs.SetString("RevisedObjects", "");
            
            PlayerPrefs.DeleteKey("DroppedObjectNames");
            PlayerPrefs.Save();

            items = required.ToList();
        }
        
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("Revision") && PlayerPrefs.GetString("Revision") == "false")
        {
            if (PlayerPrefs.HasKey("DroppedObjectNames"))
            {
                string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");

                if (!string.IsNullOrEmpty(droppedObjectName))
                {
                    if (previous != droppedObjectName)
                    {
                        Debug.Log("Dropped Object Name: " + droppedObjectName);
                        previous = droppedObjectName;
                    }
                    string[] droppedObjectNamesArray = droppedObjectName.Split(',');

                    televizyonTxt.text = "";

                    foreach (var item in droppedObjectNamesArray)
                    {
                        televizyonTxt.text += item + ", ";
                    }
                } else {
                    televizyonTxt.text = "";
                }
            }
            else
            {
                televizyonTxt.text = "";
            }
        }
        else
        {
            foreach (var x in PlayerPrefs.GetString("RevisedObjects").Split(","))
            {
                items.Remove(x);
            }
            televizyonTxt.text = "Gereken Eşyalar:\n\n" + string.Join(", ", items);

            if (items.Count <= 0)
            {
                televizyonTxt.text = "Tebrikler, eğitimi tamamladınız!";
                
                //TODO: Tleevizyonun üstünde gerekli eşyalar diye ayrı bir text var, o revision yapılırken kapatılmalı veya değiştirilmeli
            }
        }
        
    }
}
