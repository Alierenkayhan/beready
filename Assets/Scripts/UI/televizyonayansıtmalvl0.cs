using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class televizyonayansıtmalvl0 : MonoBehaviour
{
    public TMP_Text informationTxt;
    public TMP_Text televizyonTxt;
    private string previous = "";
    private bool revision = false;

    // Your required items and list of items
    private string[] required = { "Pil", "Su", "Telsiz", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak" };
    private List<string> items = new() { "Pil", "Su", "Telsiz", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak" };

    // List of GameObjects corresponding to the items
    public List<GameObject> itemGameObjects = new List<GameObject>();

    private void Start()
    {
        // PlayerPrefs initialization
        if (!PlayerPrefs.HasKey("Revision"))
        {
            PlayerPrefs.SetString("Revision", "false");
            PlayerPrefs.SetString("RevisedObjects", "");
            PlayerPrefs.DeleteKey("DroppedObjectNames");
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetString("Revision") == "true")
        {
            revision = true;
            informationTxt.gameObject.SetActive(false);
            print("Revision true");
        }
        else if (PlayerPrefs.GetString("Revision") == "false")
        {
            revision = false;
            informationTxt.gameObject.SetActive(true);
            print("Revision false");
        }

        print(PlayerPrefs.GetString("RevisedObjects"));
        print(string.Join(",", items));
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Revision") == "false")
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

                        // Activate the corresponding GameObject if its name matches with the dropped object name
                        foreach (var obj in itemGameObjects)
                        {
                            if (obj.name.Equals(item, StringComparison.OrdinalIgnoreCase))
                            {
                                obj.SetActive(true); // Activate the GameObject if it matches
                            }
                        }
                    }
                }
                else
                {
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
                if (PlayerPrefs.HasKey("ContactPerson"))
                {
                    televizyonTxt.text = "Son levele geri dönebilirsiniz. Lütfen kapıya doğru gidin.";
                }
                else
                {
                    televizyonTxt.text = "İletişim kişisi tanımlamalısınız.";
                }
            }
        }
    }
}
