using System;
using UnityEngine;

namespace UI
{
    public class resetPickedItems : MonoBehaviour
    {
        public GameObject items;
    
        public void Reset()
        {
            PlayerPrefs.DeleteKey("DroppedObjectNames");
            PlayerPrefs.Save();
            for (var i = 0; i < items.transform.childCount; i++)
            {
                var x = items.transform.GetChild(i).gameObject;
                if(x.TryGetComponent(out PickUp p))
                {
                    try
                    {
                        p.OnResetItem();
                    }
                    catch (NullReferenceException e) //Items haven't initialized
                    {
                    }
                }
            }
        }
    }
}
