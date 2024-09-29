using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class arrangeItemForLastLevel : MonoBehaviour
{
    public GameObject[] firstkiditems;
    void Start()
    {
        CheckDroppedObjectName();
    }
    void CheckDroppedObjectName()
    {
        string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");
 
        if (!string.IsNullOrEmpty(droppedObjectName))
        {
            Debug.Log("Dropped Object Name: " + droppedObjectName);
            string[] droppedObjectNamesArray = droppedObjectName.Split(',');

            foreach (var item in firstkiditems)
            {
                if (!droppedObjectNamesArray.Contains(item.name))
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }      
    }
}
