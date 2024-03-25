using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class televizyonayansıtmalvl0 : MonoBehaviour
{
    public TMP_Text televizyonTxt;

    void Update()
    {
        if (PlayerPrefs.HasKey("DroppedObjectNames"))
        {
            string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");

            if (!string.IsNullOrEmpty(droppedObjectName))
            {
                Debug.Log("Dropped Object Name: " + droppedObjectName);
                string[] droppedObjectNamesArray = droppedObjectName.Split(',');

                televizyonTxt.text = "";

                foreach (var item in droppedObjectNamesArray)
                {
                    televizyonTxt.text += item + ", ";
                }
            }
        }
    }
}
